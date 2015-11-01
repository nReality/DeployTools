using System.Data.SqlClient;
using CommandLine;

namespace Org.DeployTools.Shared.CommandLineOptions
{
    public class ConnectionStringOptions
    {
        [Option("server", HelpText = "Server to compare against", Required = true)]
        public string Server { get; set; }

        [Option("database", HelpText = "Database to compare against", Required = true)]
        public string Database { get; set; }

        public SqlConnectionStringBuilder ConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = Server,
                InitialCatalog = Database,
                IntegratedSecurity = true
            };
        }
    }
}
