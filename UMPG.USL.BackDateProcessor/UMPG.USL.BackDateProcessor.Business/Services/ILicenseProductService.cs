using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Services
{
    public interface ILicenseProductService
    {
        List<LicenseProduct> GetProductsNew(int licenseId);
    }
}