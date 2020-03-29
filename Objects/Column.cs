using System;
using System.Collections.Generic;

namespace Ora2Uml.Objects
{
    public class Column
    {
        public String Owner { get; set; } = String.Empty;
        public String TableName { get; set; } = String.Empty;
        public String ColumnName { get; set; } = String.Empty;
        public String DataType { get; set; } = String.Empty;
        public Boolean Nullable { get; set; } = true;
        public Boolean PrimaryKey { get; set; } = false;
        public Decimal? DataLength { get; set; } = null;
        public Int32? DataPrecision { get; set; } = 0;
        public Int32? DataScale { get; set; } = 0;
        public String Comments { get; set; } = String.Empty;
        public Boolean Unique { get; set; } = false;

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

        public Column(string owner, string tableName, string columnName, string dataType, Boolean nullable)
        {
            this.Owner = owner;
            this.TableName = tableName;
            this.ColumnName = columnName;
            this.DataType = dataType;
            this.Nullable = nullable;
        }

        public override string ToString()
        {
            return $"{FullName} {DataType} {(Nullable ? "NULL" : "NOT NULL")}";
        }
    }
}