using Org.DeployTools.Shared;
using System;
using System.Threading;

namespace Org.DeployTools.ServiceControl
{
    class Program
    {
        const int AlreadyRunningExitCode = 1056;
        const int NotStartedExitCode = 1062;
        const int NotAcceptingCommandsExitCode = 1061;

        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            if (options.Action == Options.ScAction.WaitStop)
            {
                WaitForStop(options);
            }
            else
            {
                int exitCode = ExternalProcessExecutor.ExecAndGetExitCode(DefaultSettings.ServiceControl, options.GetArguments());
                ValidateExitCode(options, exitCode);
            }
        }

        private static void WaitForStop(Options options)
        {
            int exitCode = ExternalProcessExecutor.ExecAndGetExitCode(DefaultSettings.ServiceControl, options.GetArguments());
            if (exitCode == NotAcceptingCommandsExitCode)
            {
                Console.Error.WriteLine("Service is not accepting any commands right now, waiting before trying again");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                WaitForStop(options);
            }
            else if (exitCode == 0)
            {
                Console.Error.WriteLine("Service is stopping, waiting before checking again");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                WaitForStop(options);
            }
            else if (exitCode == NotStartedExitCode)
            {
                Console.WriteLine("Service successfully stopped");
            }
            else
            {
                ValidateExitCode(options, exitCode);
            }
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
