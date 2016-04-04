using System;
using System.Diagnostics;

namespace Org.DeployTools.Shared
{
    public class ExternalProcessExecutor
    {
        public static void Exec(string path, string arguments)
        {
            int exitCode = ExecAndGetExitCode(path, arguments);
            ValidateExitWithZero(exitCode);
        }

        public static int ExecAndGetExitCode(string path, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(path, arguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };
            process.Start();
            ReadProcessOutput(process);
            process.WaitForExit();
            return process.ExitCode;
        }

        public static void ValidateExitWithZero(int exitCode)
        {
            if (exitCode > 0)
                throw new Exception(string.Format("External process exited with code {0}", exitCode));
        }

        private static void ReadProcessOutput(Process process)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                process.StandardOutput.BaseStream.Flush();
                Console.Write(process.StandardOutput.ReadToEnd());
            }
        }
    }
}
