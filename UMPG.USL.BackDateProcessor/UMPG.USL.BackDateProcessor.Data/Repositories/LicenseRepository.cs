
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.BackDateProcessor.Data.Infrastructure;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        public License GetLicenseById(int id)
        {
            using (var context = new DataContext())
            {
                var license = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("LicenseMethod")
                    .FirstOrDefault(c => c.LicenseId == id && c.Deleted == null);

                return license;
            }
        }

        public License GetLite(int id)
        {
            using (var context = new DataContext())
            {
                var license = context.Licenses
                    .FirstOrDefault(c => c.LicenseId == id);
                return license;
            }
        }

        public int GetTotalEIALicenseCount()
        {
            using (var context = new DataContext())
            {
                var license = context.Licenses
                    .Count(c => c.LicenseStatusId ==  5 || c.LicenseStatusId == 6 || c.LicenseStatusId == 7);
                return license;
            }
        }

        public int GetTotalLicenseCount()
        {
            using (var context = new DataContext())
            {
                return context.Licenses
                    .Count();
            }
        }

        public int GetTotalSnapshotLicenseCount()
        {
            using (var context = new DataContext())
            {
                return context.Snapshot_Licenses
                    .Count();
            }
        }

        public List<int> GetAllEIALicenseIdsThatHAVESnapshot()
        {
            using (var context = new DataContext())
            {
                var license = context.Snapshot_Licenses.Where(c => c.LicenseStatusId == 5 || c.LicenseStatusId == 6 || c.LicenseStatusId == 7)
                    .Select(_ => _.CloneLicenseId).ToList();
                return license;
            }
        }

        public List<License> GetAllLicensesForListOfLicenseIds(List<int> licenseIds)
        {
            using (var context = new DataContext())
            {


               var licenses = context.Licenses.Where(
                    c => licenseIds.Contains(c.LicenseId) && c.LicenseStatusId == 5 || c.LicenseStatusId == 6 ||
                         c.LicenseStatusId == 7).ToList();
                //var license =
                //    context.Licenses.Where(
                //        c =>
                //            c.LicenseStatusId == 5 || c.LicenseStatusId == 6 ||
                //            c.LicenseStatusId == 7 && licenseIds.Contains(c.LicenseId)).ToList();
                //return license;
                return licenses;
            }
        }

        public List<int> GetAllEIALicenseIds()
        {
            using (var context = new DataContext())
            {
                var license = context.Licenses.Where(c => c.LicenseStatusId == 5 || c.LicenseStatusId == 6 || c.LicenseStatusId == 7)
                    .Select(_ => _.LicenseId).ToList();
                return license;
            }
        }

        public License GetLicenseForLicenseNumber(string licenseNumber)
        {
            using (var context = new DataContext())
            {
                var license = context.Licenses
                    .FirstOrDefault(c => c.LicenseNumber == licenseNumber);
                return license;
            }
        }


        public int GetLicenseErrorCount()
        {
            using (var context = new DataContext())
            {
                return context.BackDateLicenseChanges.Count();
            }
        }

        public List<int> GetAllLicenseIdsWithErrors()
        {
            using (var context = new DataContext())
            {
                return context.BackDateLicenseChanges.Select(_ => _.LicenseId).ToList();
                
            }
        }

    }
}