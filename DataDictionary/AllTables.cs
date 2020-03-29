using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    internal class AllTables : Base
    {
        internal static string ColOwner => "owner";
        internal static string ColTableName => "table_name";

        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";

        internal static string TblName => "all_tables";

        private static string SqlSelect => @"SELECT 
            " + ColOwner + @",
            " + ColTableName + @",
            " + AllTabComments.ColComments + @"
        FROM (
            SELECT
                " + FulOwner + @",
                " + FulTableName + @",
                " + AllTabComments.FulComments + @"
            FROM
                " + TblName + @"
                LEFT OUTER JOIN " + AllTabComments.TblName + @" ON 
                    " + FulOwner + " = " + AllTabComments.FulOwner + @" AND
                    " + FulTableName + " = " + AllTabComments.FulTableName + @"
        ) ";

        internal static IList<Table> ReadTables(string connString, string[] ownerWhiteList, string[] tableWhiteList)
        {
            var whereClause = $"";
            var whereParts = new List<String>();

            if (ownerWhiteList != null && ownerWhiteList.Length > 0)
            {
                whereParts.Add($" {ColOwner} IN ({String.Join(",", ownerWhiteList.Select(x => $"'{x.ToUpper()}'"))}) ");
            }

            if (tableWhiteList != null && tableWhiteList.Length > 0)
            {
                whereParts.Add($" {ColTableName} IN ({String.Join(",", tableWhiteList.Select(x => $"'{x.ToUpper()}'"))}) ");
            }

            if (whereParts.Count > 0)
            {
                whereClause = $" WHERE {String.Join(" AND ", whereParts)} ";
            }

            return ReadTables(connString, whereClause);
        }

        internal static IList<Table> ReadTables(string connString, string whereClause)
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
                        var comments = GetString(rdr[AllTabComments.ColComments]);

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