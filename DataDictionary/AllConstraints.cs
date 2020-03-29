using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllConstraints : Base
    {
        internal static string TypePrimaryKey => "P";

        internal static string ColConstraintName => "constraint_name";
        internal static string ColTableName => "table_name";
        internal static string ColOwner => "owner";
        internal static string ColConstraintType => "constraint_type";

        internal static string FulConstraintName => $"{TblName}.{ColConstraintName}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";
        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulConstraintType => $"{TblName}.{ColConstraintType}";

        internal static string TblName => "all_constraints";

/*
    select =   "select acc.column_name from all_constraints ac, all_cons_columns acc "
    select += "where ac.table_name = '#{table_name}' "
    select += "and ac.owner = '#{owner}' " unless owner.nil?
    select += "and ac.constraint_name = acc.constraint_name "
    select += "and ac.constraint_type = 'P'"
*/

    }
}