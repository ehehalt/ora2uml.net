using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ora2Uml.Configuration
{
    public class Config
    {
        [JsonIgnore]
        public string ConfigFileName { get; set; }

        // Database and user information

        public DatabaseInformation Database { get; set; } = new DatabaseInformation();
        public UserInformation User { get; set; } = new UserInformation();

        // Data Dictionary informations
        
        public IList<TableInformation> Tables { get; set; } = new List<TableInformation>();
        public IList<String> ColumnsIgnored { get; set; } = new List<String>();

        // Methods

        [JsonIgnore]
        public string OracleConnectionString
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"Data Source=");
                sb.Append($"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)");
                sb.Append($"(HOST={Database.Host})(PORT={Database.Port})))");
                sb.Append($"(CONNECT_DATA=(SERVER=DEDICATED)");
                sb.Append($"(SERVICE_NAME={Database.ServiceName})));");
                sb.Append($"User Id={User.UserId};Password={User.Password};");
                return sb.ToString();
            }
        }

        public void Save(String fileName)
        {
            using (FileStream fs = File.Create(fileName))
            {
                var options = new JsonSerializerOptions { WriteIndented = true, };
                JsonSerializer.SerializeAsync(fs, this, options);
            }
        }

        public static Config Read(String fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                var jsonString = File.ReadAllText(fileName);
                var config = JsonSerializer.Deserialize<Config>(jsonString);
                config.ConfigFileName = fileName;
                return config;
            }
        }
    }
}