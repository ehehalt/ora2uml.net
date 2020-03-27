using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllTabColumns : Base
    {
        private static string ColOwner => "owner";
        private static string ColTableName => "table_name";
        private static string ColColumnName => "column_name";
        private static string ColDataType => "data_type";
        private static string ColDataLength => "data_length";
        private static string ColNullable => "nullable";
        private static string ColDataPrecision => "data_precision";
        private static string ColDataScale => "data_scale";
        private static string ColComments => "comments";

        private static string TblAllTabColumns => "all_tab_columns";
        private static string TblAllColComments => "all_col_comments";

        private static string SqlSelect => @"SELECT
            " + ColOwner + @",
            " + ColTableName + @",
            " + ColColumnName + @",
            " + ColDataType + @",
            " + ColDataLength + @",
            " + ColNullable + @",
            " + ColDataPrecision + @",
            " + ColDataScale + @",
            " + ColComments + @"
        FROM ( 
            SELECT
                " + TblAllTabColumns + "." + ColOwner + @",
                " + TblAllTabColumns + "." + ColTableName + @",
                " + TblAllTabColumns + "." + ColColumnName + @",
                " + TblAllTabColumns + "." + ColDataType + @",
                " + TblAllTabColumns + "." + ColDataLength + @",
                " + TblAllTabColumns + "." + ColNullable + @",
                " + TblAllTabColumns + "." + ColDataPrecision + @",
                " + TblAllTabColumns + "." + ColDataScale + @",
                " + TblAllColComments + "." + ColComments + @"
            FROM
                " + TblAllTabColumns + @"
                LEFT OUTER JOIN " + TblAllColComments + @" ON
                    " + TblAllTabColumns + "." + ColOwner + " = " + TblAllColComments + "." + ColOwner + @" AND 
                    " + TblAllTabColumns + "." + ColTableName + " = " + TblAllColComments + "." + ColTableName + @" AND 
                    " + TblAllTabColumns + "." + ColColumnName + " = " + TblAllTabColumns + "." + ColColumnName + @"
        ) ";

        public static IList<Column> ReadColumns(string connString, Table table)
        {
            return ReadColumns(connString, $" where table_name = '{table.TableName}'");
        }

        public static IList<Column> ReadColumns(string connString, string whereClause)
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
                        String owner = GetString(rdr[ColOwner]);
                        String tableName = GetString(rdr[ColTableName]);
                        String columnName = GetString(rdr[ColColumnName]);
                        String dataType = GetString(rdr[ColDataType]);
                        Decimal? dataLength = GetValue<Decimal?>(rdr[ColDataLength]);
                        Boolean nullable = GetString(rdr[ColNullable]) == "N" ? false : true;
                        Int32? dataPrecision = GetValue<Int32?>(rdr[ColDataPrecision]);
                        Int32? dataScale = GetValue<Int32?>(rdr[ColDataPrecision]);
                        String comments = GetString(rdr[ColComments]);

                        var col = new Column()
                        {
                            Owner = owner,
                            TableName = tableName,
                            ColumnName = columnName,
                            DataType = dataType,
                            DataLength = dataLength,
                            Nullable = nullable,
                            DataPrecision = dataPrecision,
                            DataScale = dataScale,
                            Comments = comments
                        };
                        columns.Add(col);
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