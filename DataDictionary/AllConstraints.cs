using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllConstraints : Base
    {
        internal const string TypePrimaryKey = "P";
        internal const string TypeCheckConstraint = "C";
        internal const string TypeRelation = "R";
        internal const string TypeUnique = "U";

        internal static string ColConstraintName => "constraint_name";
        internal static string ColTableName => "table_name";
        internal static string ColOwner => "owner";
        internal static string ColConstraintType => "constraint_type";
        internal static string ColROwner => "r_owner";
        internal static string ColRConstraintName => "r_constraint_name";

        internal static string FulConstraintName => $"{TblName}.{ColConstraintName}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";
        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulConstraintType => $"{TblName}.{ColConstraintType}";

        internal static string TblName => "all_constraints";

        internal static string ReplaceTable => "$$TABLE$$";
        internal static string ReplaceOwner => "$$OWNER$$";

        private static string SqlSelectPrimaryKey => @"
        SELECT
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
                var methodName = MethodBase.GetCurrentMethod().Name;
                Console.Error.WriteLine($"{methodName}: {ex.Message}");
            }
            
            return columns;
        }
    
        private static string SqlSelectRelations => @"
        SELECT DISTINCT
            rel." + ColOwner + @",
            rel." + ColTableName + @"
        FROM
            " + TblName + @" cur 
            LEFT OUTER JOIN " + TblName + @" rel 
                ON 
                    cur." + ColROwner + " = rel." + ColOwner + @" AND
                    cur." + ColRConstraintName + " = rel." + ColConstraintName + @"
        WHERE
            cur." + ColTableName + $" = '{ReplaceTable}' " + @" AND
            cur." + ColOwner + $" = '{ReplaceOwner}' " + @" AND
            cur." + ColConstraintType + $" = '{TypeRelation}' " + @" AND
            rel." + ColConstraintType + $" = '{TypePrimaryKey}' ";

        internal static IList<Table> MarkRelations(String connString, Table table, IList<Table> tables) {
            var sqlSelect = SqlSelectRelations;
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
                        String owner = GetString(rdr[ColOwner]).ToUpper();
                        String tableName = GetString(rdr[ColTableName]).ToUpper();

                        tables = tables.Select(t => { if (t.Owner.ToUpper() == owner && t.TableName.ToUpper() == tableName) { t.Childs.Add(table); } return t; }).ToList();
                    }   
                }
            }
            catch (Exception ex)
            {
                var methodName = MethodBase.GetCurrentMethod().Name;
                Console.Error.WriteLine($"{methodName}: {ex.Message}");
                Console.Error.WriteLine(sqlSelect);
            }
            
            return tables;
        }
    }
}