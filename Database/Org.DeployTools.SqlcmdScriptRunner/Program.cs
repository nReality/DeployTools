using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Org.DeployTools.Shared;
using Org.DeployTools.Shared.CommandLineOptions;

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
            foreach (var scriptFileMask in options.FileMasks)
            {
                RunScriptsForMask(options, scriptFileMask);
            }
        }

        private static void RunScriptsForMask(ConnectionStringOptions options, string scriptFileMask)
        {
            var directory = Directory.GetCurrentDirectory();
            if (Path.IsPathRooted(scriptFileMask))
            {
                directory = Path.GetDirectoryName(scriptFileMask) ?? directory;
                scriptFileMask = Path.GetFileName(scriptFileMask) ?? scriptFileMask;
            }
            var filesForArgPattern = Directory.GetFiles(directory, scriptFileMask).OrderBy(s => s).ToList();
            Console.WriteLine("{0} files match pattern {1} (in {2})", filesForArgPattern.Count, scriptFileMask, directory);
            foreach (var file in filesForArgPattern)
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
