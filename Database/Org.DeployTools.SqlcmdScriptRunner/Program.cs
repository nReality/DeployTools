using System;
using System.Data.SqlClient;
using Org.DeployTools.Shared;

namespace Org.DeployTools.SqlcmdScriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            foreach (var file in options.Files)
            {
                DeployScript(DefaultSettings.SqlcmdPath, options.ConnectionStringBuilder(), file);
            }
        }

        private static void DeployScript(string sqlcmd, SqlConnectionStringBuilder connection, string scriptFile)
        {
            const string argumentsMaskIntegrated = "-S \"{0}\" -d {1} -i \"{2}\"";
            var arguments = string.Format(argumentsMaskIntegrated, connection.DataSource, connection.InitialCatalog, scriptFile);
            Console.WriteLine("Executing script {0}", scriptFile);
            ExternalProcessExecutor.Exec(sqlcmd, arguments);
        }
    }
}
