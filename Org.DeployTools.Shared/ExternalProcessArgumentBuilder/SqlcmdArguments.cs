using System.Data.SqlClient;

namespace Org.DeployTools.Shared.ExternalProcessArgumentBuilder
{
    public class SqlcmdArguments
    {
        public static string Build(SqlConnectionStringBuilder connection)
        {
            const string argumentsMaskIntegrated = "-S \"{0}\" -d \"{1}\" ";
            return string.Format(argumentsMaskIntegrated, connection.DataSource, connection.InitialCatalog);
        }
    }
}
