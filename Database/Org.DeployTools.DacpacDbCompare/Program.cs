using System;
using System.Data.Common;
using CommandLine.Text;
using Org.DeployTools.Shared;

namespace Org.DeployTools.DacpacDbCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = new Options();
                if (!CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    Console.Error.WriteLine("Invalid arguments");
                    Console.Error.WriteLine(HelpText.AutoBuild(options, current => HelpText.DefaultParsingErrorsHandler(options, current)));
                    Environment.Exit(2);
                }

                CreateScript(options);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
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
