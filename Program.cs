using System;
using Ora2Uml.DataDictionary;

namespace Ora2Uml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ora2Uml");

            // var connection = new Connection(name: "xe", user: "system", password: "sysadm");
            var connectionString = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=nb-rod-me09)(PORT=1521)))";
            connectionString += "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=system;Password=sysadm;";

            var database = new Database(connectionString);
            if (database.CheckConnection())
            {
                Console.WriteLine($"Connection checked successfully!");
            }
            else
            {
                Console.WriteLine($"Connection check fails ...");
            }
        }
    }
}
