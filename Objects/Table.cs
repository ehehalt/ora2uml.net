using System;

namespace Ora2Uml.Objects
{
    public class Table {
        public String Owner { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;

        public String FullName => $"{Owner}{(String.IsNullOrEmpty(Owner) ? "" : ".")}{Name}";

        // Implement with columns?
        // Referenced by
        // References 

        public Table()
        {    
        }

        public Table(String owner, String name)
        {
            this.Owner = owner;
            this.Name = name;
        }
    }
}