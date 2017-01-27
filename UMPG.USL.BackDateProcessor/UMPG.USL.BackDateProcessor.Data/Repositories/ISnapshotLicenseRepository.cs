using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotLicenseRepository
    {
        Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot);

        Snapshot_License GetLicenseSnapShotById(int id);

        Snapshot_License GetSnapshotLicenseByCloneLicenseId(int licenseId);

        bool DoesLicenseSnapshotExist(int licenseId);
        bool EditSnapshotLicense(Snapshot_License license);

        bool DeleteSnapshotLicense(Snapshot_License licenseId);
    }
}