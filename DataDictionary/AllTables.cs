using System;
using System.Collections.Generic;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public static class AllTables
    {
        public static String Select => "select owner, table_name from all_tables";

        public static IList<Table>
    }
}