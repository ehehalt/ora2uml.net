using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllConsColumns : Base
    {
        internal static string ColColumnName => "column_name";
        internal static string ColConstraintName => "constraint_name";

        internal static string FulColumnName => $"{TblName}.{ColColumnName}";
        internal static string FulConstraintName => $"{TblName}.{ColConstraintName}";

        internal static string TblName => "all_cons_columns";
    }
}