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
        private static String ColColumnName => "column_name";
        private static String ColDataType => "data_type";
        private static String ColNullable => "nullable";

        private static String TableName => "all_tab_columns";

        private static String SqlSelect => $" SELECT {ColOwner}, {ColTableName}, {ColColumnName}, {ColDataType}, {ColNullable} FROM {TableName} ";

        public static IList<Column> ReadColumns(String connString, Table table)
        {
            return ReadColumns(connString, $" where table_name = '{table.TableName}'");
        }

        public static IList<Column> ReadColumns(String connString, String whereClause)
        {
            IList<Column> columns = new List<Column>();

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
                        var tableName = rdr[ColTableName].ToString();
                        var columnName = rdr[ColColumnName].ToString();
                        var dataType = rdr[ColDataType].ToString();
                        var nullable = rdr[ColNullable].ToString() == "N" ? false : true;

                        columns.Add(new Column(owner, tableName, columnName, dataType, nullable));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                columns.Clear();
            }

            return columns;
        }
    }
}