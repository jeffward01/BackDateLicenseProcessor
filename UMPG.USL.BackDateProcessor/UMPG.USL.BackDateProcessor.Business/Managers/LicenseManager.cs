using System.Collections.Generic;
using System.Linq;
using UMPG.USL.BackDateProcessor.Data.Repositories;
using UMPG.USL.BackDateProcessor.Data.Repositories.BackDateLicenseSnapshot;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class LicenseManager : ILicenseManager
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IBackDateLicenseChangedRepository _backDateLicenseChangedRepository;
        public LicenseManager(ILicenseRepository licenseRepository, IBackDateLicenseChangedRepository backDateLicenseChangedRepository)
        {
            _backDateLicenseChangedRepository = backDateLicenseChangedRepository;
            _licenseRepository = licenseRepository;
        }

        public int GetTotalLicenseEAICount()
        {
            return _licenseRepository.GetTotalEIALicenseCount();
        }

        public int GetCountForEIARequireSnapshots()
        {
            var snapshottedLicenseIds = _licenseRepository.GetAllEIALicenseIdsThatHAVESnapshot();
            var licenseIds = _licenseRepository.GetAllEIALicenseIds();
            var licenseThatNeedASnapshot = licenseIds.Except(snapshottedLicenseIds).ToList();
            return licenseThatNeedASnapshot.Count;
        }

        public List<License> GetAllEIALicensesThatRequireASnapshot()
        {
            var snapshottedLicenseIds = _licenseRepository.GetAllEIALicenseIdsThatHAVESnapshot();
            var licenseIds = _licenseRepository.GetAllEIALicenseIds();
            var errorCount = _licenseRepository.GetLicenseErrorCount();
            var licenseThatNeedASnapshot = licenseIds.Except(snapshottedLicenseIds).ToList();
            if (errorCount > 0)
            {
                var licensesWithErrors = _licenseRepository.GetAllLicenseIdsWithErrors();

                licenseThatNeedASnapshot = licenseThatNeedASnapshot.Except(licensesWithErrors).ToList();
            }
            else
            {
                licenseThatNeedASnapshot = licenseIds.Except(snapshottedLicenseIds).ToList();
            }

            var licensesThatNeedSnapshot =
                _licenseRepository.GetAllLicensesForListOfLicenseIds(licenseThatNeedASnapshot)
                    //.OrderByDescending(_ => _.CreatedDate)
                    .Take(100).ToList();

            foreach (var license in licensesThatNeedSnapshot)
            {
                if (snapshottedLicenseIds.Contains(license.LicenseId))
                {
                    licensesThatNeedSnapshot.Remove(license);
                }
            }
            return licensesThatNeedSnapshot;
        }



        public List<LicenseError> GetLicensesWithErrors()
        {
                var listOfLicensesWithErrors = new List<LicenseError>();
            var licenseIdsWithErrors = _licenseRepository.GetAllLicenseIdsWithErrors();
            var listOfLicenseErrors = new List<License>();
            
            foreach (var errorLicenseId in licenseIdsWithErrors)
            {
                listOfLicenseErrors.Add(_licenseRepository.GetLite(errorLicenseId));

            }


            foreach (var license in listOfLicenseErrors)
            {
                var newLicenseErrorEntry = new LicenseError();
                newLicenseErrorEntry.License = license;
                newLicenseErrorEntry.LicenseErrorInfo =
                    _backDateLicenseChangedRepository.GetLicenseChangedForLicenseIdFull(license.LicenseId);
                listOfLicensesWithErrors.Add(newLicenseErrorEntry);
            }
            return listOfLicensesWithErrors;
        }

        public int GetCountForEIAHaveSnapshots()
        {
            return _licenseRepository.GetTotalSnapshotLicenseCount();
        }
    }
}