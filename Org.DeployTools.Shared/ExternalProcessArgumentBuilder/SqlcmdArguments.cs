using System.Data.SqlClient;

namespace Org.DeployTools.Shared.ExternalProcessArgumentBuilder
{
    public class SqlcmdArguments
    {
        public static string Build(SqlConnectionStringBuilder connection)
        {
            const string argumentsMaskIntegrated = "-S \"{0}\" -d \"{1}\" ";
            const string argumentsMaskSqlUser = "-S \"{0}\" -d \"{1}\" -U \"{2}\" -P \"{3}\" ";
            return
                connection.IntegratedSecurity
                    ? string.Format(argumentsMaskIntegrated, connection.DataSource, connection.InitialCatalog)
                    : string.Format(argumentsMaskSqlUser, connection.DataSource, connection.InitialCatalog,
                        connection.UserID, connection.Password);
        }
    }
}
