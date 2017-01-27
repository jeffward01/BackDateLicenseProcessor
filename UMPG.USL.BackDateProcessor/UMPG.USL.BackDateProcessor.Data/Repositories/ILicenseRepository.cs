using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ILicenseRepository
    {
        License GetLicenseById(int id);
        License GetLite(int id);
    }
}