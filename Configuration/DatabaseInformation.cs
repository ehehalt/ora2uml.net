using System;
using System.Collections.Generic;

namespace Ora2Uml.Configuration
{
    public class DatabaseInformation
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }

        public DatabaseInformation()
            : this(String.Empty)
        {
        }

        public DatabaseInformation(String serviceName)
            : this(String.Empty, serviceName)
        {
        }

        public DatabaseInformation(String host, String serviceName, int port = 1521)
        {
            this.Host = host;
            this.ServiceName = serviceName;
            this.Port = port;
        }
    }
}