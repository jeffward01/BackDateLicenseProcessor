using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ILicenseProductConfigurationRepository
    {
        LicenseProductConfiguration GetLicenseProductConfigurationByProductIdAndLicenseProductConfigurationId(int licenseproductId, int product_configuration_id);
    }
}