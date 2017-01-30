using System.Collections.Generic;
using System.Threading;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public interface IValidationManger
    {
        string Validate(string licenseNumber);
    }
}