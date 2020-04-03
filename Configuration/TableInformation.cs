using System;
using System.Collections.Generic;

namespace Ora2Uml.Configuration
{
    public class TableInformation
    {
        public static Int32 DefaultColor { get; set; } = 0xFFAAAA;
        public static String DefaultOwner { get; set; } = String.Empty;

        // Owner = Schema
        public String Owner { get; set; }

        // Name = Tablename
        public String Name { get; set; }

        public IList<String> Tags { get; set; } 

        // Backgroundcolor of the table
        public Int32 Color { get; set; }

        public TableInformation()
            : this(DefaultOwner, String.Empty)
        {
        }

        // For databases without owner/schema information
        public TableInformation(String name)
            : this(DefaultOwner, name)
        {
        }

        // For databases with owner/schema information
        public TableInformation(String owner, String name)
        {
            Owner = owner;
            Name = name;
            Color = DefaultColor;
            Tags = new List<String>();
        }
    }
}