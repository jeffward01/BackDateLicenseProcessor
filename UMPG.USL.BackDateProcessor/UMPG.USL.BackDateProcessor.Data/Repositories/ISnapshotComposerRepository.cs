﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotComposerRepository
    {
        Snapshot_Composer SaveComposerSnapshot(Snapshot_Composer composerSnapshot);

        List<Snapshot_Composer> GetAllComposersByRecsCopyrightid(int recsCopyrightId);

        bool DeleteComposerSnapshotByComposer(Snapshot_Composer composerToDelete);
    }
}