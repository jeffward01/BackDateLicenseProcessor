using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.BackDateProcessor.Data.Repositories;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class ValidationManger
    {

        private License _license;
        private readonly ILicenseRepository _licenseRepository = new LicenseRepository();

        public ValidationManger(string licenseNumber)
        {
            _license = _licenseRepository.GetLicenseForLicenseNumber(licenseNumber.ToString());
        }


        public string Validate()
        {
            
        }
    


        //Validate workcode (ensure its still active, and writers match)
        private string ValidateWorkCodeAndWriters()
        {
            
        }

        //Validate tracks (ensure they match mechs === recs)
        private string ValidateTracks()
        {

        }



        //If there is an error, show output to user, and create an entry in error table (with error message)

    }
}
