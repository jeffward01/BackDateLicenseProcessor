﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshot_ComposerOriginalPublisherKnownAsRepository
    {
        Snapshot_ComposerOriginalPublisherKnownAs SaveComposerOriginalPublisherKnownAs(Snapshot_ComposerOriginalPublisherKnownAs composerSnapshot);

        List<Snapshot_ComposerOriginalPublisherKnownAs> GetAllComposerOriginalPublisherKnownAsByComposerOriginalPublisherSnapshotId(int composerOriginalPublisherSnapshotId);

        bool DeleteComposerOriginalPublisherKnownAs(Snapshot_ComposerOriginalPublisherKnownAs composerToDelete);
    }
}