using System;
using System.Data.Common;
using System.Data.SqlClient;
using Org.DeployTools.Shared;

namespace Org.DeployTools.DacpacDbCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = args[0],
                    InitialCatalog = args[1],
                    IntegratedSecurity = true
                };
                CreateScript(DefaultSettings.SqlPackagePath, connectionStringBuilder, args[2], args[3], args[4]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static void CreateScript(string sqlpackage, DbConnectionStringBuilder connection, string dacpacFile, string profileFile, string outputFile)
        {
            const string argumentsMask = "/Action:Script /OverwriteFiles:True /Quiet:False /TargetConnectionString:\"{0}\" /SourceFile:{1} /Profile:{2} /OutputPath:{3}";
            var arguments = string.Format(argumentsMask, connection.ConnectionString, dacpacFile, profileFile, outputFile);
            ExternalProcessExecutor.Exec(sqlpackage, arguments);
        }
    }
}
