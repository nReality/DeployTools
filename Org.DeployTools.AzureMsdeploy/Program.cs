using System;
using System.IO;
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
            const string argumentMask = "-verb:sync -source:contentPath='{0}' -dest:contentPath='{1}',ComputerName='https://{1}.scm.azurewebsites.net:443/msdeploy.axd?site={1}',UserName='{2}',Password='{3}',AuthType='Basic'";
            var packageTmpDir = Path.Combine(Directory.GetCurrentDirectory(), options.ProjectDir, @"obj\Release\Package\PackageTmp");
            var arguments = string.Format(argumentMask, packageTmpDir, options.Sitename, options.Username, options.Password);
            ExternalProcessExecutor.Exec(DefaultSettings.Msdeploy, arguments);
        }
    }
}
