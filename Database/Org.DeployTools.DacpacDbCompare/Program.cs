using System.Data.Common;
using Org.DeployTools.Shared;

namespace Org.DeployTools.DacpacDbCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, CreateScript);
        }

        private static void CreateScript(Options options)
        {
            var connectionStringBuilder = options.ConnectionStringBuilder();
            CreateScript(DefaultSettings.SqlPackagePath, connectionStringBuilder, options.DacpacFile, options.ProfileFile,
                options.OutputFile);
        }

        private static void CreateScript(string sqlpackage, DbConnectionStringBuilder connection, string dacpacFile, string profileFile, string outputFile)
        {
            const string argumentsMask = "/Action:Script /OverwriteFiles:True /Quiet:False /TargetConnectionString:\"{0}\" /SourceFile:{1} /Profile:{2} /OutputPath:{3}";
            var arguments = string.Format(argumentsMask, connection.ConnectionString, dacpacFile, profileFile, outputFile);
            ExternalProcessExecutor.Exec(sqlpackage, arguments);
        }
    }
}
