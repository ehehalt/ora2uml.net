using System;
using Ora2Uml.DataDictionary;

namespace Ora2Uml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ora2Uml");

            var conn = new Connection(name: "xe", user: "sysadm", password: "sysadm");
            Console.WriteLine($"Connection = {conn}");
        }
    }
}
