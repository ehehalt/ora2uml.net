using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Ora2Uml.Objects;
using Ora2Uml.PlantUML;
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

            // CheckDatabase(database);

            var tables = Reader.ReadTables(connectionString, new string[]{"SYS"}, new string[]{"COUNTRIES", "REGIONS", "LOCATIONS"});

            // OutputTableInformation(tables);
            Console.WriteLine($"Tables read: {tables.Count}");

            var umlData = Template.GeneratePlantUML(tables);
            var umlPath = Path.Join("Sample", "sample.puml");
            File.WriteAllText(umlPath, umlData);
        }

        static void CheckDatabase(Database database)
        {
            if (database.CheckConnection())
            {
                Console.WriteLine($"Connection checked successfully!");
            }
            else
            {
                Console.WriteLine($"Connection check fails ...");
            }
        }

        static void OutputTableInformation(IList<Table> tables)
        {
            foreach(Table table in tables)
            {
                Console.WriteLine($"Table: {table}");
                if (!String.IsNullOrEmpty(table.Comments))
                {
                    Console.WriteLine($"       {table.Comments}");
                }

                foreach (Column column in table.Columns)
                {
                    Console.WriteLine($"  Column: {column}");
                    if (!String.IsNullOrEmpty(column.Comments))
                    {
                        Console.WriteLine($"          {column.Comments}");
                    }
                }
            }
        }
    }
}
