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
            foreach (Table table in tables)
            {
                table.Columns = AllTabColumns.ReadColumns(connectionString, table);
                table.Columns = AllConstraints.MarkPrimaryKeys(connectionString, table, table.Columns);
            }

            return tables;
        }

        public static IList<Table> ReadTables(string connectionString, string[] ownerWhiteList, string[] tableWhiteList)
        {
            var tables = AllTables.ReadTables(connectionString, ownerWhiteList, tableWhiteList);
            foreach (Table table in tables)
            {
                var columns = AllTabColumns.ReadColumns(connectionString, table);
                columns = AllConstraints.MarkPrimaryKeys(connectionString, table, columns);
                columns = columns.Where(c => !ColumnNameBlackList.Contains(c.ColumnName.ToUpper())).ToList();

                table.Columns = columns;
            }

            return tables;
        }
    }
}
