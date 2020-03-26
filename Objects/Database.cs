using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Ora2Uml.Objects
{
    public class Database {

        public String ConnectionString { get; private set; }
        public IList<Table> Tables { get; private set; } = new List<Table>();

        public Database(String connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public bool CheckConnection()
        {
            var connectionOk = true;
            try
            {
                var conn = new OracleConnection(ConnectionString);
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