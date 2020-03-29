using System;
using System.Collections.Generic;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public static class Reader
    { 
        public static IList<Table> ReadTables(String connectionString, String tableWhereClause = "")
        {
            var tables = AllTables.ReadTables(connectionString, tableWhereClause);
            foreach (Table table in tables)
            {
                table.Columns = AllTabColumns.ReadColumns(connectionString, table);
            }

            return tables;
        }

        public static IList<Table> ReadTables(string connectionString, string[] ownerWhiteList, string[] tableWhiteList)
        {
            var tables = AllTables.ReadTables(connectionString, ownerWhiteList, tableWhiteList);
            foreach (Table table in tables)
            {
                table.Columns = AllTabColumns.ReadColumns(connectionString, table);
            }

            return tables;
        }
    }
}
