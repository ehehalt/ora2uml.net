using System;
using System.Collections.Generic;
using System.Linq;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{

    public static class Reader
    { 
        public static IList<String> ColumnNameBlackList { get; } = new List<String>();
        
        public static IList<Table> ReadTables(String connectionString, String tableWhereClause = "")
        {
            var tables = AllTables.ReadTables(connectionString, tableWhereClause);
            var foreachTables = tables.Select(t => t);

            foreach (Table table in foreachTables)
            {
                var columns = AllTabColumns.ReadColumns(connectionString, table);
                columns = AllConstraints.MarkPrimaryKeys(connectionString, table, columns);
                columns = columns.Where(c => !ColumnNameBlackList.Contains(c.ColumnName.ToUpper())).ToList();
                
                table.Columns = columns;
                tables = AllConstraints.MarkRelations(connectionString, table, tables);
            }

            return tables;
        }

        public static IList<Table> ReadTables(string connectionString, string[] ownerWhiteList, string[] tableWhiteList)
        {
            var tables = AllTables.ReadTables(connectionString, ownerWhiteList, tableWhiteList);
            var foreachTables = tables.Select(t => t);

            foreach (Table table in foreachTables)
            {
                var columns = AllTabColumns.ReadColumns(connectionString, table);
                columns = AllConstraints.MarkPrimaryKeys(connectionString, table, columns);
                columns = columns.Where(c => !ColumnNameBlackList.Contains(c.ColumnName.ToUpper())).ToList();

                table.Columns = columns;
                tables = AllConstraints.MarkRelations(connectionString, table, tables);
            }

            return tables;
        }
    }
}
