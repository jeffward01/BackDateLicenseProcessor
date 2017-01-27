using UMPG.USL.BackDateProcessor.Data.Infrastructure;
using System;
using System.Linq;
using NLog;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public class SnapshotWorkTrackRepository : ISnapshotWorkTrackRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Snapshot_WorksTrack GetTrackForCloneTrackId(int cloneTrackId)
        {
            using (var context = new DataContext())
            {
                return context.Snapshot_Tracks.FirstOrDefault(_ => _.CloneWorksTrackId == cloneTrackId);
            }
        }

        public Snapshot_WorksTrack SaveWorksTrack(Snapshot_WorksTrack worksTrack)
        {
            Logger.Info("Saving Snapshot track Id: " + worksTrack.CloneWorksTrackId + ", " + worksTrack.Title);
            using (var context = new DataContext())
            {
                context.Snapshot_Tracks.Add(worksTrack);
                context.SaveChanges();
                return worksTrack;
            }
        }

        public bool DeleteTrackBySnapshotTrackId(int snapshotTrackId)
        {
            using (var context = new DataContext())
            {
                var address = context.Snapshot_Tracks.Find(snapshotTrackId);
                context.Snapshot_Tracks.Attach(address);
                context.Snapshot_Tracks.Remove(address);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}