using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public static class AllTables
    {
        private static String ColOwner => "owner";
        private static String ColTableName => "table_name";
        private static String TableName => "all_tables";

        private static String SqlSelect => $" SELECT {ColOwner}, {ColTableName} FROM {TableName} ";

        public static (IList<Table> tables, String error) ReadTables(String connString, String whereClause)
        {
            IList<Table> tables = new List<Table>();
            String error = null;

            try 
            {
                using(OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();

                    var cmd = new OracleCommand($"{SqlSelect} {whereClause}", conn);                
                    var rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        var owner = rdr[ColOwner].ToString();
                        var name = rdr[ColTableName].ToString();

                        tables.Add(new Table(){
                            Owner = owner, 
                            Name = name }
                        );
                    }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                tables.Clear();
            }

            return (tables, error);
        }
    }
}