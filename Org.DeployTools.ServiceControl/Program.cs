using Org.DeployTools.Shared;
using System;

namespace Org.DeployTools.ServiceControl
{
    class Program
    {
        const int AlreadyRunningExitCode = 1056;
        const int NotStartedExitCode = 1062;

        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            var arguments = string.Format("{0} {1} {2}", options.Servername, options.Action.ToString().ToLower(), options.ServiceName);
            int exitCode = ExternalProcessExecutor.ExecAndGetExitCode(DefaultSettings.ServiceControl, arguments);
            ValidateExitCode(options, exitCode);
        }

        private static void ValidateExitCode(Options options, int exitCode)
        {
            string reason = GetExitReason(options, exitCode);

            if (string.IsNullOrEmpty(reason))
            {
                ExternalProcessExecutor.ValidateExitWithZero(exitCode);
            }
            else
            {
                Console.Error.WriteLine("SC reported failure, but exiting with success ({0})", reason);
            }
        }

        private static string GetExitReason(Options options, int exitCode)
        {
            if (options.NoSuppressExitCode)
                return null;

            if (exitCode == AlreadyRunningExitCode && options.Action == Options.ScAction.Start)
                return "Service already running, so start had no effect";
            if (exitCode == NotStartedExitCode && options.Action == Options.ScAction.Stop)
                return "Service not started, so stop had no effect";
            return null;
        }
    }
}
