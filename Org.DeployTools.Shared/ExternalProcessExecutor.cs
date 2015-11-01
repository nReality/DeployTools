using System;
using System.Diagnostics;

namespace Org.DeployTools.Shared
{
    public class ExternalProcessExecutor
    {
        public static void Exec(string path, string arguments)
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
            if (process.ExitCode > 0)
                throw new Exception(string.Format("External process exited with code {0}", process.ExitCode));
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
