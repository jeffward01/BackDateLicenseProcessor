using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories.BackDateLicenseSnapshot
{
    public interface IBackDateLicenseChangedRepository
    {
        BackDateLicenseChanged GetLicenseChangedForLicenseIdLite(int licenseId);
        BackDateLicenseChanged GetLicenseChangedForLicenseIdFull(int licenseId);
        BackDateLicenseChanged EditBackDateLicenseChanged(BackDateLicenseChanged BackDateLicenseChanged);
        BackDateLicenseChanged CreateBackDateLicenseChanged(BackDateLicenseChanged BackDateLicenseChangedItem);
    }
}