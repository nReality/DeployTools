using System;
using System.Data.SqlClient;
using CommandLine;

namespace Org.DeployTools.Shared.CommandLineOptions
{
    public class ConnectionStringOptions : ICommandLineOptions
    {
        [Option("server", HelpText = "Server to compare against", Required = true)]
        public string Server { get; set; }

        [Option("database", HelpText = "Database to compare against", Required = true)]
        public string Database { get; set; }

        [Option("integrated", HelpText = "Use integrated security for authentication")]
        public bool UseIntegratedSecutory { get; set; }

        [Option("username", HelpText = "Username to use with authentication")]
        public string Username { get; set; }

        [Option("password", HelpText = "Password to use with authentication")]
        public string Password { get; set; }

        [Option("trust-server-certificate", HelpText = "Trust the server certificate. ('SSL Provider: The certificate chain was issued by an authority that is not trusted.')")]
        public bool TrustServerCertificate { get; set; }

        public SqlConnectionStringBuilder ConnectionStringBuilder()
        {
            return UseIntegratedSecutory
                ? IntegratedSecutirySqlConnectionStringBuilder()
                : SqlSecutiryConnectionStringBuilder();
        }

        private SqlConnectionStringBuilder SqlSecutiryConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = Server,
                InitialCatalog = Database,
                UserID = Username,
                Password = Password,
                TrustServerCertificate = TrustServerCertificate
            };
        }

        private SqlConnectionStringBuilder IntegratedSecutirySqlConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = Server,
                InitialCatalog = Database,
                IntegratedSecurity = true,
                TrustServerCertificate = TrustServerCertificate
            };
        }

        public virtual void GuardArgumentsValid()
        {
            if (!UseIntegratedSecutory && (Username == null || Password == null))
                throw new Exception("Expected sql username and password when using sql authentication");
        }

        public virtual void Setup()
        {
        }
    }
}
