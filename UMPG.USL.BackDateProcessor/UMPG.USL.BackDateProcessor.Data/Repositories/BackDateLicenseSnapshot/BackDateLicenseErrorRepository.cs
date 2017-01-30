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
    public class BackDateLicenseErrorRepository : IBackDateLicenseErrorRepository
    {

        public List<BackDateLicenseError> GetAllLicenseErrorsForBackDateLicenseChangedId(int backDateLicenseChangedId)
        {
            using (var context = new DataContext())
            {

                return
                    context.BackDateLicenseErrors.Where(_ => _.BackDateLicenseChangedId == backDateLicenseChangedId)
                        .ToList();

            }
        }


        public BackDateLicenseError EditBackDateLicenseError(BackDateLicenseError BackDateLicenseError)
        {
            using (var context = new DataContext())
            {
                context.Entry(BackDateLicenseError).State = EntityState.Modified;
                context.SaveChanges();
                return BackDateLicenseError;
            }
        }

        public BackDateLicenseError CreateBackDateLicenseError(BackDateLicenseError BackDateLicenseErrorItem)
        {
            using (var context = new DataContext())
            {
                context.BackDateLicenseErrors.Add(BackDateLicenseErrorItem);
                context.SaveChanges();
                return BackDateLicenseErrorItem;
            }
        }
    }
}
