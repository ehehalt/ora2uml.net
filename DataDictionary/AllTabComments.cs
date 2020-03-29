using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Ora2Uml.Objects;

namespace Ora2Uml.DataDictionary
{
    public class AllTabComments : Base
    {
        internal static string ColComments => "comments";
        internal static string ColOwner => "owner";
        internal static string ColTableName => "table_name";
        
        internal static string FulComments => $"{TblName}.{ColComments}";
        internal static string FulOwner => $"{TblName}.{ColOwner}";
        internal static string FulTableName => $"{TblName}.{ColTableName}";

        internal static string TblName => "all_tab_comments";
    }
}