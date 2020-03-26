using System;
using System.Collections.Generic;

namespace Ora2Uml.DataDictionary
{
    public class Table {
        public String Schema { get; set; }  
        public String Name { get; set; }

        public IList<Column> Columns { get; protected set; } = new List<Column>();
    }
}