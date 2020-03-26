using System;
using System.Collections.Generic;
using Ora2Uml.Objects;
using Ora2Uml.DataDictionary;

namespace Ora2Uml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ora2Uml");

            var filter = new List<String>();
            filter.Add("COUNTRIES");

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

            (var tables, var error) = AllTables.ReadTables(connectionString, "");
            if (error != null)
            {
                Console.Error.WriteLine(error);
                return;
            }
            else
            {
                foreach(Table table in tables)
                {
                    Console.WriteLine($"Table: {table.FullName}");
                }
            }
        }
    }
}
