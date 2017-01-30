using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Providers
{
    public interface IMechsDataProvider
    {
        List<RecsProductChanges> ValidateLicense(int licenseId);
    }
}