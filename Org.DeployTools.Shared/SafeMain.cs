using System;
using CommandLine.Text;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.Shared
{
    public class SafeMain
    {

        public static void RunInTryCatch(Action run)
        {
            try
            {
                run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        public static void ParseArgumentsAndRunInTryCatch<TArgumentType>(string[] args, Action<TArgumentType> run)
            where TArgumentType : ICommandLineOptions, new()
        {
            RunInTryCatch(() =>
            {
                var options = new TArgumentType();
                if (CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    run(options);
                }
                else
                {
                    Console.Error.WriteLine("Invalid arguments");
                    Console.Error.WriteLine(HelpText.AutoBuild(options,
                        current => HelpText.DefaultParsingErrorsHandler(options, current)));
                    Environment.Exit(2);
                }
            });
        }
    }
}
