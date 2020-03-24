using System;

namespace Ora2Uml.DataDictionary
{
    interface IConnection : IObject {
        String UserID { get; }
        String Password { get; }
    }
}