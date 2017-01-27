using System.Collections.Generic;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.BackDateProcessor.Data.Providers
{
    public interface IRecsProvider
    {
        List<WorksRecording> RetrieveTracksWithWriters(int productId);

        ProductHeader RetrieveProductHeader(int productId);
    }
}