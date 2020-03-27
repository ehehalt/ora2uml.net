using System;
using System.Collections.Generic;
using System.Linq;

namespace Ora2Uml.Objects
{
    public class Table 
    {
        public string Owner { get; set; } = String.Empty;
        public string TableName { get; set; } = String.Empty;
        public string Comments { get; set; } = String.Empty;

        public string FullName => $"{Owner}{(String.IsNullOrEmpty(Owner) ? "" : ".")}{TableName}";

        public IList<Column> Columns { get; set; } = new List<Column>();
        public IEnumerable<Column> PrimaryKeys => Columns.Where(c => c.PrimaryKey);

        // Implement with columns?
        // Referenced by
        // References 

        public Table()
        {    
        }

        public Table(string owner, string tableName, string comments)
        {
            this.Owner = owner;
            this.TableName = tableName;
            this.Comments = comments;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}