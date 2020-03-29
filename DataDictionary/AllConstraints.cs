using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllConstraints : Base
    {
        internal const string TypePrimaryKey = "P";
        internal const string TypeCheckConstraint = "C";
        internal const string TypeUnique = "U";

        internal static string ColConstraintName => "constraint_name";
        internal static string ColTableName => "table_name";
        internal static string ColOwner => "owner";
        internal static string ColConstraintType => "constraint_type";

        internal static string FulConstraintName => $"{TblName}.{ColConstraintName}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";
        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulConstraintType => $"{TblName}.{ColConstraintType}";

        internal static string TblName => "all_constraints";

        internal static string ReplaceTable => "$$TABLE$$";
        internal static string ReplaceOwner => "$$OWNER$$";

        private static string SqlSelectPrimaryKey => @"SELECT
            " + AllConsColumns.ColColumnName + @",
            " + ColConstraintType + @"
        FROM
            " + TblName + @"
            JOIN " + AllConsColumns.TblName + @" 
                ON " + FulConstraintName + " = " + AllConsColumns.FulConstraintName + @"
        WHERE
            " + FulTableName + $" = '{ReplaceTable}' " + @" AND
            " + FulOwner + $" = '{ReplaceOwner}' "; // + @" AND
            // " + FulConstraintType + $" = '{TypePrimaryKey}' ";

        internal static IList<Column> MarkPrimaryKeys(String connString, Table table, IList<Column> columns) {
            var sqlSelect = SqlSelectPrimaryKey;
            sqlSelect = sqlSelect.Replace(ReplaceTable, table.TableName);
            sqlSelect = sqlSelect.Replace(ReplaceOwner, table.Owner);

            try
            {
                using(OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();

                    var cmd = new OracleCommand(sqlSelect, conn);
                    var rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        String columnName = GetString(rdr[AllConsColumns.ColColumnName]);
                        String constraintType = GetString(rdr[ColConstraintType]);

                        switch(constraintType)
                        {
                            case TypePrimaryKey:
                                columns = columns.Select(n => { if (n.ColumnName.ToUpper() == columnName.ToUpper()) { n.PrimaryKey = true; } return n; }).ToList();
                                break;
                            case TypeUnique:
                                columns = columns.Select(n => { if (n.ColumnName.ToUpper() == columnName.ToUpper()) { n.Unique = true; } return n; }).ToList();
                                break;
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            
            return columns;
        }
    }
}