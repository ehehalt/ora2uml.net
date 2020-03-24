using System;

namespace Ora2Uml.DataDictionary
{
    interface IConnection : IObject {
        String User { get; set; }
        String Password { get; set; }
    }
}