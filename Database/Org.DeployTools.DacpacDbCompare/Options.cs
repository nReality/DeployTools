using System.Data.SqlClient;
using CommandLine;

namespace Org.DeployTools.DacpacDbCompare
{
    class Options
    {
        [Option("server", HelpText = "Server to compare against", Required = true)]
        public string Server { get; set; }
        [Option("database", HelpText = "Database to compare against", Required = true)]
        public string Database { get; set; }

        [Option("dacpac", HelpText = "Dacpac file to use as input", Required = true)]
        public string DacpacFile { get; set; }
        [Option("profile", HelpText = "Profile file to use with comparing", Required = true)]
        public string ProfileFile { get; set; }

        [Option("output", HelpText = "Output script file", Required = true)]
        public string OutputFile { get; set; }

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
