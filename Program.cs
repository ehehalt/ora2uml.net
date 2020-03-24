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
            var connection = @"
                Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=urHost)(PORT=urPort)))
                (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=urOracleSID)));User Id=urUsername;
                Password=urPassword;";

            var database = new Database(connection);
            Console.WriteLine(database.Connection.ConnectionStringBuilder.ConnectionString);
            if (database.CheckConnection())
            {
                Console.WriteLine($"Connection {connection} checked successfully!");
            }
            else
            {
                Console.WriteLine($"Connection {connection} check fails ...");
            }
        }
    }
}
