using System;

namespace UMPG.USL.BackDateProcessor.Business.Logging
{
    public interface IDataHarmonizationLogManager
    {
        void LogErrors(Exception ex);

        void LogMessage(string message);
    }
}