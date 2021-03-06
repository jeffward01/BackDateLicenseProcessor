﻿using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Data.Repositories
{
    public interface IDataHarmonizationQueueRepository
    {
        int GetPendingCountForAllActionRequests();

        int GetPendingCountForCreateRequests();

        int GetPendingCountForDeleteRequests();

        DataHarmonizationQueue GetFirstItemInQueue();

        bool ArePendingItemsInQueue();

        DataHarmonizationQueue EditDataHarmonizationQueue(DataHarmonizationQueue dataHarmonizationQueue);

        DataHarmonizationQueue CreateDataHarmonizationRequest(DataHarmonizationQueue dataHarmonizationQueueItem);
    }
}