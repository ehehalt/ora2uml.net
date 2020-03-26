using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Ora2Uml.Objects
{
    public class Column
    {
        public String Owner { get; set; } = String.Empty;
        public String TableName { get; set; } = String.Empty;
        public String ColumnName { get; set; } = String.Empty;
        public String DataType { get; set; } = String.Empty;
        public Boolean Nullable { get; set; } = true;

        public String FullName 
        {
            get
            {
                var parts = new List<String>();

                if(Owner != null) parts.Add(Owner);
                if(TableName != null) parts.Add(TableName);
                if(ColumnName != null) parts.Add(ColumnName);

                return String.Join(".", parts);
            }
        }

        public Column()
        {    
        }

        public Column(String owner, String tableName, String columnName, String dataType, Boolean nullable)
        {
            this.Owner = owner;
            this.TableName = tableName;
            this.ColumnName = columnName;
            this.DataType = dataType;
            this.Nullable = nullable;
        }

        public override String ToString()
        {
            return $"{FullName} {DataType} {(Nullable ? "NULL" : "NOT NULL")}";
        }
    }
}