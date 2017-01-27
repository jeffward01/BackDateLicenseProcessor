using UMPG.USL.BackDateProcessor.Business.Logging;
using UMPG.USL.BackDateProcessor.Business.Services;
using UMPG.USL.BackDateProcessor.Data.Repositories;
using System;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class DataHarmonizationManager : IDataHarmonizationManager
    {
        private readonly IDataHarmonizationLogManager _logManager;
        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseProductService _licenseProductService;
        private readonly ISnapshotManager _snapshotManager;
        private readonly IDataHarmonizationQueueService _dataHarmonizationQueueService;

        public DataHarmonizationManager(IDataHarmonizationLogManager logManager, ILicenseRepository licenseRepository, IDataHarmonizationQueueService dataHarmonizationQueueService,
            ILicenseProductService licenseProductService, ISnapshotManager snapshotManager)
        {
            _dataHarmonizationQueueService = dataHarmonizationQueueService;
            _snapshotManager = snapshotManager;
            _licenseProductService = licenseProductService;
            _licenseRepository = licenseRepository;
            _logManager = logManager;
        }

        public bool CreateLicenseSnapshot(int licenseId, DataHarmonizationQueue queueItem)
        {
            var emailService = new EmailService(licenseId);
            _dataHarmonizationQueueService.MarkAsInProcess(queueItem);

            try
            {
                //get local license, save local license as licenseSnapshot
                var localLicense = _licenseRepository.GetLicenseById(licenseId);

                //get licenseProducts from 'GetProductsNew'
                var licenseProducts = _licenseProductService.GetProductsNew(localLicense.LicenseId);

                //save localLicense Snapshot and licenseProducts as snapshot
                _snapshotManager.TakeLicenseSnapshot(localLicense, licenseProducts);
            }
            catch (Exception e)
            {
                _dataHarmonizationQueueService.MarkAsError(queueItem);
                emailService.SendCreateFailedEmail();
                _logManager.LogErrors(e);
                //throw new Exception("Error Creating License Snapshot.  Error: " + e.ToString());
            }
            finally
            {
                _dataHarmonizationQueueService.CreateMarkAsComplete(queueItem);
            }

            return true;
        }

        public bool DeleteLicenseSnapshot(int licenseId, DataHarmonizationQueue queueItem)
        {
            var emailService = new EmailService(licenseId);
            _dataHarmonizationQueueService.MarkAsInProcess(queueItem);
            try
            {
                if (_snapshotManager.DoesLicenseSnapshotExist(licenseId))
                {
                    _snapshotManager.DeleteLicenseSnapshot(licenseId);
                }
                else
                {
                    /*
                    throw new Exception(
                        "Error Deleting License Snapshot.  License Snapshot cannot be found for license Id: " +
                        licenseId + ". ");
                        */
                }
            }
            catch (Exception e)
            {
                _dataHarmonizationQueueService.MarkAsError(queueItem);
                emailService.SendDeleteFailedEmail();
                _logManager.LogErrors(e);
                /*
                throw new Exception("Error Deleting License Snapshot.  License Snapshot cannot be found for license Id: " +
                        licenseId + ".  Error: " + e.ToString());
                        */
            }
            finally
            {
                _dataHarmonizationQueueService.DeleteMarkAsComplete(queueItem);
            }
            return true;
        }
    }
}