using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.BackDateProcessor.Business.Providers;
using UMPG.USL.BackDateProcessor.Data.Repositories;
using UMPG.USL.BackDateProcessor.Data.Repositories.BackDateLicenseSnapshot;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class ValidationManger : IValidationManger
    {


        private readonly ILicenseRepository _licenseRepository; 
        private readonly IMechsDataProvider _mechsDataProvider;
        private readonly IBackDateLicenseChangedRepository _backDateLicenseChangedRepository;
        private readonly IBackDateLicenseErrorRepository _backDateLicenseErrorRepository;
        private readonly IDataHarmonizationQueueRepository _dataHarmonizationQueueRepository;

        public ValidationManger(IMechsDataProvider mechsDataProvider, IBackDateLicenseChangedRepository backDateLicenseChangedRepository, IBackDateLicenseErrorRepository backDateLicenseErrorRepository, IDataHarmonizationQueueRepository dataHarmonizationQueueRepository, ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
            _dataHarmonizationQueueRepository = dataHarmonizationQueueRepository;
            _backDateLicenseErrorRepository = backDateLicenseErrorRepository;
            _backDateLicenseChangedRepository = backDateLicenseChangedRepository;
            _mechsDataProvider = mechsDataProvider;
        }


        public string Validate(string licenseNumber)
        {

            var license = _licenseRepository.GetLicenseForLicenseNumber(licenseNumber);
            var results = _mechsDataProvider.ValidateLicense(license.LicenseId);
            if (results.Count == 0)
            {
                Console.WriteLine("Success! No changes have been detected, " + licenseNumber + " is about to be Snapshotted.");
                //Snapshot License
                _dataHarmonizationQueueRepository.CreateDataHarmonizationRequest(
                    CreateDataHarmonizationQueueItem(license.LicenseId, (int) ActionTypEnum.Create));




                Console.WriteLine("Snapshot Entry created, Snapshot will be handled by DHSnapshotProcessor \n");
                

            }
            
            //Create error
            var result =_backDateLicenseChangedRepository.CreateBackDateLicenseChanged(new BackDateLicenseChanged
            {
                LicenseId = license.LicenseId
            });
                //log license error
            foreach (var error in results)
            {
                _backDateLicenseErrorRepository.CreateBackDateLicenseError(new BackDateLicenseError
                {
                    BackDateLicenseChangedId = result.BackDateLicenseChangedId,
                    LicenseError = error.ChangeMessage
                });
            }
                

            return "Errors present!  Please look at the error log to view errors";



        }


        private DataHarmonizationQueue CreateDataHarmonizationQueueItem(int licenseId, int actionTypeId)
        {
            return new DataHarmonizationQueue
            {
                DataProcessorStatusId = 1,
                ActionTypeId = actionTypeId,
                LicenseId = licenseId,
                CreatedDate = DateTime.Now
            };
        }






        //If there is an error, show output to user, and create an entry in error table (with error message)

    }
}
