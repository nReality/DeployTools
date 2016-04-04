using Org.DeployTools.Shared;

namespace Org.DeployTools.ServiceControl
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            var arguments = string.Format("{0} {1} {2}", options.Servername, options.Action.ToString().ToLower(), options.ServiceName);
            ExternalProcessExecutor.Exec(DefaultSettings.ServiceControl, arguments);
        }
    }
}
