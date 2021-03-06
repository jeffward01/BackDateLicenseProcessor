﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotSampleRepository
    {
        Snapshot_Sample SaveSampleSnapshot(Snapshot_Sample sampleSnapshot);

        List<Snapshot_Sample> GetAllSamplesForRecCopyrightId(int recsCopyrightId);

        bool DeleteSampleSnapshot(Snapshot_Sample composerToDelete);
    }
}