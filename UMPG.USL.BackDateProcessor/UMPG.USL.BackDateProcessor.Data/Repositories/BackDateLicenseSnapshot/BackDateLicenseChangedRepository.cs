using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.BackDateProcessor.Data.Infrastructure;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories.BackDateLicenseSnapshot
{
    public class BackDateLicenseChangedRepository : IBackDateLicenseChangedRepository
    {

        public BackDateLicenseChanged GetLicenseChangedForLicenseIdLite(int licenseId)
        {
            using (var context = new DataContext())
            {

                return
                    context.BackDateLicenseChanges.FirstOrDefault(_ => _.LicenseId == licenseId);

            }
        }

        public BackDateLicenseChanged GetLicenseChangedForLicenseIdFull(int licenseId)
        {
            using (var context = new DataContext())
            {

                return
                    context.BackDateLicenseChanges.
                    Include("LicenseErrors").
                    FirstOrDefault(_ => _.LicenseId == licenseId);

            }
        }


        public BackDateLicenseChanged EditBackDateLicenseChanged(BackDateLicenseChanged BackDateLicenseChanged)
        {
            using (var context = new DataContext())
            {
                context.Entry(BackDateLicenseChanged).State = EntityState.Modified;
                context.SaveChanges();
                return BackDateLicenseChanged;
            }
        }

        public BackDateLicenseChanged CreateBackDateLicenseChanged(BackDateLicenseChanged BackDateLicenseChangedItem)
        {
            using (var context = new DataContext())
            {
                context.BackDateLicenseChanges.Add(BackDateLicenseChangedItem);
                context.SaveChanges();
                return BackDateLicenseChangedItem;
            }
        }
    }
}
