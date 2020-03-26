using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Ora2Uml.Objects
{
    public class Column
    {
        public String Owner { get; set; } = String.Empty;
        public String TableName { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;

        public Column()
        {    
        }

        public Column(String owner, String tableName, String name)
        {
            this.Owner = owner;
            this.TableName = tableName;
            this.Name = name;
        }
    }
}