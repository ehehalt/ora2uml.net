using System;
using Oracle.ManagedDataAccess.Client;

namespace Ora2Uml.DataDictionary
{
    public class Connection : IConnection {
        public String Name { get; protected set; } = String.Empty;
        public String UserID { get; protected set; } = String.Empty;
        public String Password { get; protected set; } = String.Empty;

        private OracleConnectionStringBuilder connectionStringBuilder;
        public OracleConnectionStringBuilder ConnectionStringBuilder {
            get 
            {
                if (connectionStringBuilder == null)
                {
                    connectionStringBuilder = new OracleConnectionStringBuilder();

                    connectionStringBuilder.DataSource = Name;
                    connectionStringBuilder.UserID = UserID;
                    connectionStringBuilder.Password = Password;
                 }
                return connectionStringBuilder;
            }
        }

        public String ConnectionString => $"Data Source={UserID}/{Password}@nb_rod_me09//{Name}";

        public override String ToString() => $"{UserID}@{Name}";

        public Connection() {}
        public Connection(String name, String user, String password) {
            this.Name = name;
            this.UserID = user;
            this.Password = password;
        }
    }
}