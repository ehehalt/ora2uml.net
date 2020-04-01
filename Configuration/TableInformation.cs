using System;
using System.Collections.Generic;

namespace Ora2Uml.Configuration
{
    public class TableInformation
    {
        public String Owner { get; set; }
        public String Name { get; set; }

        public TableInformation(String owner, String name)
        {
            Owner = owner;
            Name = name;
        }
    }
}