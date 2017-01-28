using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            DisplayLicensesThatRequireSnapshots();
      


        }

        private void DisplayLicensesThatRequireSnapshots()
        {
            Console.WriteLine("Eldest 100 Licenses that require snapshots: ");
            var licenses = _licenseManager.GetAllEIALicensesThatRequireASnapshot();
            Console.WriteLine("Index    |    LicenseNumber    |   LicenseName  |  Created Date");
            foreach (var license in licenses)
            {
                Console.WriteLine(licenses.IndexOf(license) + 1 + "). " + license.LicenseNumber + " - " + license.LicenseName + "  - " + license.CreatedDate.ToString().Split(' ').First());
            }
            GetInputFromListOfUnSnapshottedLicenses();
        }


        private void GetInputFromListOfUnSnapshottedLicenses()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Please enter the license number(s) that you wish to snapshot");
            var licenseNumbersToSnapshot = GetLicenseNumbersToSnapshot();
            SnapshotOldLicenses(licenseNumbersToSnapshot);
        }


        private void SnapshotOldLicenses(List<string> licensesNumbersToSnapshot)
        {
            foreach (var licenseNumber in licensesNumbersToSnapshot)
            {
                Console.WriteLine("Starting snapshot process for license number: " + licenseNumber);    
                Console.WriteLine("Validation step...");
                var validationManager = new ValidationManger(licenseNumber);
                var successOrErrorMessage = validationManager.Validate();
                Console.WriteLine(successOrErrorMessage);
            }

            SnapshotProcessCompletePostMessage();
        }

        private void SnapshotProcessCompletePostMessage()
        {
            Console.WriteLine("All your licenses have either been snapshotted or logged with errors.");
            Console.WriteLine("What would you like to do? \n  Press 1. to view list of licenses that require a snapshot");
            Console.WriteLine("Press 2. to view licenses with errors");
            Console.WriteLine("Press 3. to exit");
            GetSnapshotPostInput();


        }

        private void GetSnapshotPostInput()
        {
            var answer = Console.ReadLine();
            while (answer != "1" || answer != "2" || answer != "3")
            {
                GetSnapshotPostInput();
            }

            if (answer == "1")
            {
                DisplayLicensesThatRequireSnapshots();
            }
            if (answer == "2")
            {
                //View errors
            }
            if (answer == "3")
            {
                Console.WriteLine("Good bye!!");
                Console.WriteLine("Please press any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        private List<string> GetLicenseNumbersToSnapshot()
        {
            var listOfLicenseNumbers = new List<string>();

            var licenseNumbers = Console.ReadLine();
            if (licenseNumbers.Length > 3)
            {
                var listOfLicenseNumbersStringArray = licenseNumbers.Split(' ');
                foreach (var item in listOfLicenseNumbersStringArray)
                {
                  
                    listOfLicenseNumbers.Add(item);
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