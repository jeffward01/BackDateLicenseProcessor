using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ILicenseRepository
    {
        License GetLicenseById(int id);
        License GetLite(int id);
        int GetTotalEIALicenseCount();
        int GetTotalSnapshotLicenseCount();
        int GetTotalLicenseCount();
        List<int> GetAllEIALicenseIds();
        int GetLicenseErrorCount();
        List<License> GetAllLicensesForListOfLicenseIds(List<int> licenseIds);
        List<int> GetAllEIALicenseIdsThatHAVESnapshot();
        License GetLicenseForLicenseNumber(string licenseNumber);
        List<int> GetAllLicenseIdsWithErrors();
    }
}