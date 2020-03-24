using System;

namespace Ora2Uml.DataDictionary
{
    public class Connection : IConnection {
        public String Name { get; set; } = String.Empty;
        public String User { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;

        public override String ToString() => $"{User}@{Name}";
    }
}