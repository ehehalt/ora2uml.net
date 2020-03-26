using System;

namespace Ora2Uml.Objects
{
    public class Table {
        public String Owner { get; set; } = String.Empty;
        public String TableName { get; set; } = String.Empty;

        public Table()
        {    
        }

        public Table(String owner, String tableName)
        {
            this.Owner = owner;
            this.TableName = tableName;
        }

    }
}