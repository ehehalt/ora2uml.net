using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Ora2Uml.Configuration;
using Ora2Uml.Objects;
using Ora2Uml.PlantUML;
using Ora2Uml.DataDictionary;

namespace Ora2Uml
{
    class Program
    {
        static void CreateSampleConfig()
        {
            /* Sample Config Creation */

            var config = new Config();
            config.Host = "nb-rod-me09";
            config.Port = 1521;
            config.ServiceName = "xe";
            config.UserId = "system";
            config.Password = "sysadm";
            config.Owners.Add("SYS");
            config.Tables.Add("COUNTRIES");
            config.Tables.Add("REGIONS");
            config.Tables.Add("LOCATIONS");
            config.ColumnsIgnored.Add("COUNTRY_ID");

            config.Save("Sample/sample.json");
        }

        static Config ReadConfig(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    var configFile = args[0];
                    var config = Config.Read(configFile);
                    return config;

                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.Error.WriteLine("\nStart with config file parameter:\n\n  ora2uml Samples/sample.json\n");

                System.Environment.Exit(0);
            }
            return null;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ora2Uml");

            CreateSampleConfig();

            var config = ReadConfig(args);

            // CheckDatabase(config.ConnectionString);

            var tables = Reader.ReadTables(
                config.ConnectionString, 
                config.Owners.ToArray(), 
                config.Tables.ToArray(), 
                config.ColumnsIgnored.ToArray());

            Console.WriteLine($"Tables read: {tables.Count}");

            // OutputTableInformation(tables);

            var umlData = Template.GeneratePlantUML(tables);
            var umlPath = $"{Path.GetFileNameWithoutExtension(config.ConfigFileName)}.puml";
            File.WriteAllText(umlPath, umlData);
        }

        static void CheckDatabase(String connectionString)
        {
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
