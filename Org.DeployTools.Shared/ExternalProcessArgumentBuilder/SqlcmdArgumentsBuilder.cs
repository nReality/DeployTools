using System.Data.SqlClient;

namespace Org.DeployTools.Shared.ExternalProcessArgumentBuilder
{
    public class SqlcmdArgumentsBuilder
    {
        private string _arguments;

        private SqlcmdArgumentsBuilder(string connectionArguments)
        {
            _arguments = connectionArguments + "-b ";
        }

        public static SqlcmdArgumentsBuilder Build(SqlConnectionStringBuilder connection)
        {
            const string argumentsMaskIntegrated = "-S \"{0}\" -d \"{1}\" ";
            const string argumentsMaskSqlUser = "-S \"{0}\" -d \"{1}\" -U \"{2}\" -P \"{3}\" ";
            var arguments =
                connection.IntegratedSecurity
                    ? string.Format(argumentsMaskIntegrated, connection.DataSource, connection.InitialCatalog)
                    : string.Format(argumentsMaskSqlUser, connection.DataSource, connection.InitialCatalog,
                        connection.UserID, connection.Password);
            if (connection.TrustServerCertificate)
                arguments += " -C";
            return new SqlcmdArgumentsBuilder(arguments);
        }

        public override string ToString()
        {
            return _arguments;
        }

        public SqlcmdArgumentsBuilder AddScript(string file)
        {
            _arguments += string.Format("-i \"{0}\" ", file);
            return this;
        }
    }
}
