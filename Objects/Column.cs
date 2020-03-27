using System;
using System.Collections.Generic;

namespace Ora2Uml.Objects
{
    public class Column
    {
        public string Owner { get; set; } = String.Empty;
        public string TableName { get; set; } = String.Empty;
        public string ColumnName { get; set; } = String.Empty;
        public string DataType { get; set; } = String.Empty;
        public bool Nullable { get; set; } = true;
        public bool PrimaryKey { get; set; } = false;
        public int Length { get; set; } = 0;
        public int DataPrecision { get; set; } = 0;
        public int DataScale { get; set; } = 0;
        public string Comment { get; set; } = String.Empty;

        public string FullName 
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