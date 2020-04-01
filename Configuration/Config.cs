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

        // Database informations

        public string Host { get; set; }
        public int Port { get; set; } = 1521;
        public string ServiceName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        // Data Dictionary informations

        // public IList<TableInformation> Tables { get; set; } = new List<TableInformation>();
        
        public IList<String> Tables { get; set; } = new List<String>();
        public IList<String> Owners { get; set; } = new List<String>();
        public IList<String> ColumnsIgnored { get; set; } = new List<String>();

        // Methods

        [JsonIgnore]
        public string ConnectionString
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"Data Source=");
                sb.Append($"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)");
                sb.Append($"(HOST={Host})(PORT={Port})))");
                sb.Append($"(CONNECT_DATA=(SERVER=DEDICATED)");
                sb.Append($"(SERVICE_NAME={ServiceName})));");
                sb.Append($"User Id={UserId};Password={Password};");
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