using System;
using System.Collections.Generic;
using System.Linq;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class ViewManager : IViewManager
    {
        private readonly ILicenseManager _licenseManager;

        public ViewManager(ILicenseManager licenseManager)
        {
            _licenseManager = licenseManager;
        }

        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to the License Snapshot BackDate Processor");
            Console.WriteLine("This applicaiton is responsible for validating, and taking snapshots of existing licenses");
            Console.WriteLine("---------------------------------------------------------------- \n \n \n \n");
            Console.WriteLine("{0} licenses exist (Executed/Accepted/Issued).  {1} have snapshots, {2} do NOT have snapshots", _licenseManager.GetTotalLicenseEAICount(), _licenseManager.GetCountForEIAHaveSnapshots(), _licenseManager.GetCountForEIARequireSnapshots());
            Console.WriteLine("press any key to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Eldest 100 Licenses that require snapshots: ");
            var licenses = _licenseManager.GetAllEIALicensesThatRequireASnapshot();
            Console.WriteLine("Index    |    LicenseNumber    |   LicenseName  |  Created Date");
            foreach (var license in licenses)
            {
                Console.WriteLine(licenses.IndexOf(license) + 1 + "). " + license.LicenseNumber + " - " + license.LicenseName + "  - " + license.CreatedDate.ToString().Split(' ').First());
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Please enter the license number(s) that you wish to snapshot");
            var licenseNumbersToSnapshot = GetLicenseNumbersToSnapshot();
            SnapshotOldLicenses(licenseNumbersToSnapshot);


        }


        private void SnapshotOldLicenses(List<int> licensesNumbersToSnapshot)
        {
            foreach (var licenseNumber in licensesNumbersToSnapshot)
            {
                Console.WriteLine("Starting snapshot process for license number: " + licenseNumber);    
                Console.WriteLine("Validation step...");

            }
            
        }

        private List<int> GetLicenseNumbersToSnapshot()
        {
            var listOfLicenseNumbers = new List<int>();

            var licenseNumbers = Console.ReadLine();
            if (licenseNumbers.Length > 3)
            {
                var listOfLicenseNumbersStringArray = licenseNumbers.Split(' ');
                foreach (var item in listOfLicenseNumbersStringArray)
                {
                    var licenseNumber = Int32.Parse(item);
                    listOfLicenseNumbers.Add(licenseNumber);
                }
            }
            else
            {
                GetLicenseNumbersToSnapshot();
            }
            return listOfLicenseNumbers;
        }
    }
}