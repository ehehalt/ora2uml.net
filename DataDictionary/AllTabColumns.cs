using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public static class AllTabColumns
    {
        private static String ColOwner => "owner";
        private static String ColTableName => "table_name";
        private static String ColName => "name";
        private static String TableName => "all_tab_columns";

        private static String SqlSelect => $" SELECT {ColOwner}, {ColTableName}, {ColName} FROM {TableName} ";

        public static (IList<Column> columns, String error) ReadColumns(String connString, Table table)
        {
            return ReadColumns(connString, $" where table_name = '{table.Name}'");
        }

        public static (IList<Column> columns, String error) ReadColumns(String connString, String whereClause)
        {
            IList<Column> columns = new List<Column>();
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
                        var tableName = rdr[TableName].ToString();
                        var name = rdr[ColTableName].ToString();

                        columns.Add(new Column(owner, tableName, name));
                    }
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                columns.Clear();
            }

            return (columns, error);
        }
    }
}