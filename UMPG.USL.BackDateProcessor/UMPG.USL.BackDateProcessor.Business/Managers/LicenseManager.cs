using System.Collections.Generic;
using System.Linq;
using UMPG.USL.BackDateProcessor.Data.Repositories;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class LicenseManager : ILicenseManager
    {
        public readonly ILicenseRepository _licenseRepository;

        public LicenseManager(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public int GetTotalLicenseEAICount()
        {
            return _licenseRepository.GetTotalEIALicenseCount();
        }

        public int GetCountForEIARequireSnapshots()
        {
            var snapshottedLicenseIds = _licenseRepository.GetAllEIALicenseIdsThatHAVESnapshot();
            var licenseIds = _licenseRepository.GetAllEIALicenseIds();
            var licenseThatNeedASnapshot = licenseIds.Except(snapshottedLicenseIds).ToList();
            return licenseThatNeedASnapshot.Count;
        }

        public List<License> GetAllEIALicensesThatRequireASnapshot()
        {
            var snapshottedLicenseIds = _licenseRepository.GetAllEIALicenseIdsThatHAVESnapshot();
            var licenseIds = _licenseRepository.GetAllEIALicenseIds();
            var licenseThatNeedASnapshot = licenseIds.Except(snapshottedLicenseIds).ToList();
            return
                _licenseRepository.GetAllLicensesForListOfLicenseIds(licenseThatNeedASnapshot)
                    //.OrderByDescending(_ => _.CreatedDate)
                    .Take(100).ToList();
        }

        public int GetCountForEIAHaveSnapshots()
        {
            return _licenseRepository.GetTotalSnapshotLicenseCount();
        }
    }
}