﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotAdminKnownAsRepository
    {
        List<Snapshot_AdminKnownAs> GetAllAdminKnownAsForAdminSnapshotId(int adminSnapshotId);

        bool DeleteKnownasByAdminKnownAsSnapshotId(int adminKnownAsSnapshotId);

        Snapshot_AdminKnownAs SaveSnapshotAdminKnownAs(Snapshot_AdminKnownAs adminKnownAs);
    }
}