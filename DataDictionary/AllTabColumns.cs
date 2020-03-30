using System;
using System.Collections.Generic;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllTabColumns : Base
    {
        internal static string ColOwner => "owner";
        internal static string ColTableName => "table_name";
        internal static string ColColumnName => "column_name";
        internal static string ColDataType => "data_type";
        internal static string ColDataLength => "data_length";
        internal static string ColNullable => "nullable";
        internal static string ColDataPrecision => "data_precision";
        internal static string ColDataScale => "data_scale";

        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";
        internal static string FulColumnName => $"{TblName}.{ColColumnName}";
        internal static string FulDataType => $"{TblName}.{ColDataType}";
        internal static string FulDataLength => $"{TblName}.{ColDataLength}";
        internal static string FulNullable => $"{TblName}.{ColNullable}";
        internal static string FulDataPrecision => $"{TblName}.{ColDataPrecision}";
        internal static string FulDataScale => $"{TblName}.{ColDataScale}";

        internal static string TblName => "all_tab_columns";

        private static string SqlSelect => @"SELECT
            " + ColOwner + @",
            " + ColTableName + @",
            " + ColColumnName + @",
            " + ColDataType + @",
            " + ColDataLength + @",
            " + ColNullable + @",
            " + ColDataPrecision + @",
            " + ColDataScale + @",
            " + AllColComments.ColComments + @"
        FROM ( 
            SELECT
                " + FulOwner + @",
                " + FulTableName + @",
                " + FulColumnName + @",
                " + FulDataType + @",
                " + FulDataLength + @",
                " + FulNullable + @",
                " + FulDataPrecision + @",
                " + FulDataScale + @",
                " + AllColComments.FulComments + @"
            FROM
                " + TblName + @"
                LEFT OUTER JOIN " + AllColComments.TblName + @" ON
                    " + FulOwner + " = " + AllColComments.FulOwner + @" AND 
                    " + FulTableName + " = " + AllColComments.FulTableName + @" AND 
                    " + FulColumnName + " = " + AllColComments.FulColumnName + @"
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
                        String comments = GetString(rdr[AllColComments.ColComments]);

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
                var methodName = MethodBase.GetCurrentMethod().Name;
                Console.Error.WriteLine($"{methodName}: {ex.Message}");
                columns.Clear();
            }

            return columns;
        }        
    }
}