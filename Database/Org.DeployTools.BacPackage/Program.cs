using System;
using Org.DeployTools.Shared;
using Microsoft.SqlServer.Dac;

namespace Org.DeployTools.BacPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
            if (options.Action == Options.BacpacAction.Export)
            {
                Export(options);
            }
            else if (options.Action == Options.BacpacAction.Import)
            {
                Import(options);
            }
        }

        private static void Import(Options options)
        {
            var package = Microsoft.SqlServer.Dac.BacPackage.Load(options.BacpacFile);
            var service = new DacServices(options.ConnectionStringBuilder().ConnectionString);
            service.ProgressChanged += ServiceOnProgressChanged;
            service.ImportBacpac(package, options.ConnectionStringBuilder().InitialCatalog);
        }

        private static void Export(Options options)
        {
            var service = new DacServices(options.ConnectionStringBuilder().ConnectionString);
            service.ProgressChanged += ServiceOnProgressChanged;
            service.ExportBacpac(options.BacpacFile, options.ConnectionStringBuilder().InitialCatalog);
        }

        private static void ServiceOnProgressChanged(object sender, DacProgressEventArgs dacProgressEventArgs)
        {
            Console.WriteLine("{0} {1}", dacProgressEventArgs.Message, dacProgressEventArgs.Status);
        }
    }
}
