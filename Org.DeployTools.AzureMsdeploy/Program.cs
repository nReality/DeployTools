using System;
using Org.DeployTools.Shared;

namespace Org.DeployTools.AzureMsdeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            const string deployUrlMask = "https://{0}.scm.azurewebsites.net:443/msdeploy.axd?site={0}";
            const string argumentMask = "-verb:sync -source:contentPath='{0}' -dest:contentPath='{1}',ComputerName='{4}',UserName='{2}',Password='{3}',AuthType='Basic'";
            var deployUrl = string.Format(deployUrlMask, options.Sitename);
            var arguments = string.Format(argumentMask, options.PackageDir, options.Sitename, options.Username, options.Password, deployUrl);
            Console.WriteLine("deploying to url {0} ({1})", deployUrl, options.Sitename);
            Console.WriteLine("from {0}", options.PackageDir);
            ExternalProcessExecutor.Exec(DefaultSettings.Msdeploy, arguments);
        }
    }
}
