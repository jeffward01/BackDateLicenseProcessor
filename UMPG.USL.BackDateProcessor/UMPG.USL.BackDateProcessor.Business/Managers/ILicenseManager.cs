using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public interface ILicenseManager
    {
        int GetTotalLicenseEAICount();
        int GetCountForEIARequireSnapshots();
        List<License> GetAllEIALicensesThatRequireASnapshot();
        int GetCountForEIAHaveSnapshots();
    }
}