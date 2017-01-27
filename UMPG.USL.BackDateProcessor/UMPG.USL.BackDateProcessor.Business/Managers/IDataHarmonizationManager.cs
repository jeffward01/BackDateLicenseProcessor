using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public interface IDataHarmonizationManager
    {
        bool CreateLicenseSnapshot(int licenseId, DataHarmonizationQueue queueItem);

        bool DeleteLicenseSnapshot(int licenseId, DataHarmonizationQueue queueItem);
    }
}