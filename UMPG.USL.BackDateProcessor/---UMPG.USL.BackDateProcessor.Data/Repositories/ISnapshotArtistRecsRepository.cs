using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface ISnapshotArtistRecsRepository
    {
        Snapshot_ArtistRecs SaveSnapshotArtistRecs(Snapshot_ArtistRecs artistRecsSnapshot);

        Snapshot_ArtistRecs GetSnapshotArtistRecsByArtistId(int artistId);

        bool DeleteRecsArtistByProductHeaderSnapshotId(int snapshotLicenseProductId);

        bool DeleteRecsArtisByArtistSnapshotId(int artstSnapshotId);
    }
}