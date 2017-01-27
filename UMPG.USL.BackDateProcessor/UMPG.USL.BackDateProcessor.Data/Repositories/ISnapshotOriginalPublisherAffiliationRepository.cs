using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotOriginalPublisherAffiliationRepository
    {
        List<Snapshot_OriginalPublisherAffiliation> GetAllOriginalPublisherAffiliationsByCaeCode(int caeCode);

        bool DeleteOriginalPublisherSnapshotById(int snapshotPhoneId);

        Snapshot_OriginalPublisherAffiliation SaveSnapshotOriginalPublisherAffiliation(Snapshot_OriginalPublisherAffiliation snapshotAdminAffiliation);

        List<Snapshot_OriginalPublisherAffiliation> GetAllOriginalPublisherAffiliationsByOriginalBuplisherId(
            int originalPublisherId);
    }
}