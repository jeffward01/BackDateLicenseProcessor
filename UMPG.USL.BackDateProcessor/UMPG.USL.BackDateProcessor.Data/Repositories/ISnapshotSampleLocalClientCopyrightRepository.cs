﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotSampleLocalClientCopyrightRepository
    {
        Snapshot_SampleLocalClientCopyright SaveSampleLocalClientCopyright(Snapshot_SampleLocalClientCopyright composerSnapshot);

        List<Snapshot_SampleLocalClientCopyright> GetAllSampleLocalClientCopyrightsForSampleId(int sampleSnapshotId);

        bool DeleteSampleLocalClientCopyright(Snapshot_SampleLocalClientCopyright composerToDelete);
    }
}