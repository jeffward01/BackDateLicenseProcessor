using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotAffiliationBaseRepository
    {
        List<Snapshot_AffiliationBase> GetAllAffiliationBasesForAffilationId(int affiliationId);

        Snapshot_AffiliationBase SaveSnapshotAffiliationBase(Snapshot_AffiliationBase snapshotAffiliation);

        bool DeleteAffilationByAffiliationBaseSnapshotId(int affiliationSnapshotId);
    }
}