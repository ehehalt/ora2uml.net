using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllTables : Base
    {
        private static string ColOwner => "owner";
        private static string ColTableName => "table_name";
        private static string ColComments => "comments";
        private static string TblAllTables => "all_tables";
        private static string TblAllTabComments => "all_tab_comments";

        private static string SqlSelect => @"SELECT 
            " + ColOwner + @",
            " + ColTableName + @",
            " + ColComments + @"
        FROM (
            SELECT
                " + TblAllTables + "." + ColOwner + @",
                " + TblAllTables + "." + ColTableName + @",
                " + TblAllTabComments + "." + ColComments + @"
            FROM
                " + TblAllTables + @"
                LEFT OUTER JOIN " + TblAllTabComments + @" ON 
                    " + TblAllTables + "." + ColOwner + " = " + TblAllTabComments + "." + ColOwner + @" AND
                    " + TblAllTables + "." + ColTableName + " = " + TblAllTabComments + "." + ColTableName + @"
        ) ";

        public static IList<Table> ReadTables(string connString, string whereClause)
        {
            IList<Table> tables = new List<Table>();

            try 
            {
                using(OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();

                    var cmd = new OracleCommand($"{SqlSelect} {whereClause}", conn);                
                    var rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        var owner = GetString(rdr[ColOwner]);
                        var tableName = GetString(rdr[ColTableName]);
                        var comments = GetString(rdr[ColComments]);

                        tables.Add(new Table(owner, tableName, comments));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                tables.Clear();
            }

            return tables;
        }
    }
}