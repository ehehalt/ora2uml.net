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

        public bool ReadSchema(String owner, IList<String>filter = null)
        {
            var ok = true;

            try 
            {
                var conn = new OracleConnection(ConnectionString);
                conn.Open();

                var cmd = new OracleCommand($"select owner, table_name from all_tables where owner = '{owner.ToUpper()}'", conn);
                
                var rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    var tableName = rdr[1].ToString();
                    if (filter != null && (!filter.Contains(tableName))) continue;
                    this.Tables.Add(new Table(rdr[0].ToString(), rdr[1].ToString()));
                }

                conn.Close();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Ora2Uml.DataDictionary.Database.ReadSchema: {ex.Message}");
                ok = false;
            }

            return ok;
        }
    }
}