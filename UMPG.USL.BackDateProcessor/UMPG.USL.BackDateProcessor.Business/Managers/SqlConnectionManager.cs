using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Configuration;


namespace UMPG.USL.BackDateProcessor.Business.Managers
{
    public class SqlConnectionManager : ISqlConnectionManager
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
        private readonly NLog.Logger _logger = LogManager.GetLogger("connectionLogger");

        public bool CheckIfDatabaseConnectable()
        {
            var provider = "System.Data.SqlClient"; // for example
            var factory = DbProviderFactories.GetFactory(provider);
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;

                try
                {
                    connection.Open();
                    _logger.Log(LogLevel.Info, "SQL Connection is OPEN!");
                    return true;
                }
                catch
                {
                    _logger.Log(LogLevel.Error, "SQL Connection is CLOSED!");
                    return false;
                }
            }
        }
    }
}
