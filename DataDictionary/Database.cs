using System;
using Oracle.ManagedDataAccess.Client;

namespace Ora2Uml.DataDictionary
{
    public class Database : BaseObject {

        public Connection Connection { get; private set; }

        public Database(Connection connection)
        {
            this.Connection = connection;
        }

        public bool CheckConnection()
        {
            var connectionString = Connection.ConnectionString;
            var connectionOk = true;
            try
            {
                var conn = new OracleConnection(connectionString);
                conn.Open();
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Ora2Uml.DataDictionary.Database.CheckConnection: {ex.Message}");
                connectionOk = false;
            }
            return connectionOk;
        }
    }
}