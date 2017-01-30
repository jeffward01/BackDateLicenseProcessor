using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories.BackDateLicenseSnapshot
{
    public interface IBackDateLicenseErrorRepository
    {
        List<BackDateLicenseError> GetAllLicenseErrorsForBackDateLicenseChangedId(int backDateLicenseChangedId);
        BackDateLicenseError EditBackDateLicenseError(BackDateLicenseError BackDateLicenseError);
        BackDateLicenseError CreateBackDateLicenseError(BackDateLicenseError BackDateLicenseErrorItem);
    }
}