using System;

namespace Ora2Uml.DataDictionary
{
    public abstract class BaseObject : IObject {
        public String Name { get; set; } = String.Empty;

        public override String ToString() => $"{Name}";
    }
}