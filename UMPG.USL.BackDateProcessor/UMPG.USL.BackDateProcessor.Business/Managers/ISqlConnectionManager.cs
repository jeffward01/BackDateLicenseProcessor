namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public interface ISqlConnectionManager
    {
        bool CheckIfDatabaseConnectable();
    }
}