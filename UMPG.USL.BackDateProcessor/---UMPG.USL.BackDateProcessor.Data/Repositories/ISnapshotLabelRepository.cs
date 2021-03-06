﻿using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotLabelRepository
    {
        Snapshot_Label SaveSnapshotLabel(Snapshot_Label snapshotLabel);

        Snapshot_Label GetSnapshotLabelByLabelId(int labelId);

        bool DeleteLabelSnapshotByProductHeaderSnapshotId(int snapshotLicenseProductId);

        bool DeleteLabelSnapshotBySnapshotId(int labelSnapshotId);
    }
}