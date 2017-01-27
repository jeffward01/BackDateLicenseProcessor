namespace UMPG.USL.BackDateProcessor.Business
{
    public interface IServiceManager
    {
        void ProcessQueue();
        int GetQueueCount();
    }
}