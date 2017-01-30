using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class ViewManager : IViewManager
    {
        private readonly ILicenseManager _licenseManager;
        private readonly IValidationManger _validationManger;

        public ViewManager(ILicenseManager licenseManager, IValidationManger validationManger)
        {
            _validationManger = validationManger;
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
            //  DisplayLicensesThatRequireSnapshots();
            DisplayNavOptions();
        }

        private void DisplayLicensesThatRequireSnapshots()
        {
            Console.WriteLine("Eldest 100 Licenses that require snapshots: ");
            var licenses = _licenseManager.GetAllEIALicensesThatRequireASnapshot();
            DisplayGridOfLicenses(licenses);
            GetInputFromListOfUnSnapshottedLicenses();
        }

        private void DisplayGridOfLicenses(List<License> licenses)
        {
            Console.WriteLine("Index    | LicenseId |  LicenseNumber    |   LicenseName  |  Created Date");
            foreach (var license in licenses)
            {
                Console.WriteLine(licenses.IndexOf(license) + 1 + "). " + license.LicenseId + "- " + license.LicenseNumber + " - " + license.LicenseName + "  - " + license.CreatedDate.ToString().Split(' ').First());
            }
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

                var successOrErrorMessage = _validationManger.Validate(licenseNumber);

                Console.WriteLine(successOrErrorMessage);
            }
            Console.WriteLine("Process Complete.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            SnapshotProcessCompletePostMessage();
        }

        private void SnapshotProcessCompletePostMessage()
        {
            Console.WriteLine("\n All your licenses have either been snapshotted or logged with errors.");
            DisplayNavOptions();

        }

        private void DisplayNavOptions()
        {
            Console.WriteLine("\n \nWhat would you like to do? \nPress 1. to view list of licenses that require a snapshot");
            Console.WriteLine("Press 2. to view licenses with errors");
            Console.WriteLine("Press 3. to exit");
            var feedback = Console.ReadLine();
            var answer = feedback[0];

            if (answer == '1')
            {
                Console.Clear();
                DisplayLicensesThatRequireSnapshots();
            }
            if (answer == '2')
            {
                Console.Clear();
                DisplayLicensesWithErrors();
                //View errors
            }
            if (answer == '3')
            {
                Console.Clear();
                Console.WriteLine("Good bye!!");
                Console.WriteLine("Please press any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();

                DisplayNavOptions();
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

        private void DisplayLicensesWithErrors()
        {
            var licensesWithErrors = _licenseManager.GetLicensesWithErrors();

            DisplayGridOfLicenseErrors(licensesWithErrors);
        }

        private void DisplayGridOfLicenseErrors(List<LicenseError> licenseErrors)
        {
            Console.WriteLine("Index    | LicenseId |  LicenseNumber    |   LicenseName  |  Created Date");
            foreach (var licenseError in licenseErrors)
            {
                Console.WriteLine(licenseErrors.IndexOf(licenseError) + 1 + "). " + licenseError.License.LicenseId + "- " + licenseError.License.LicenseNumber + " - " + licenseError.License.LicenseName + "  - " + licenseError.License.CreatedDate.ToString().Split(' ').First());
                foreach (var error in licenseError.LicenseErrorInfo.LicenseErrors)
                {
                    Console.WriteLine("           Error: " + error.LicenseError);
                }
                Console.WriteLine("\n");
            }
            DisplayNavOptions();
        }
    }
}