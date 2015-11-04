using CommandLine;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.BacPackage
{
    class Options : ConnectionStringOptions
    {
        [Option("action", HelpText = "Action to perform [Import | Export]", Required = true)]
        public BacpacAction Action { get; set; }

        [Option("bacpac", HelpText = "Path to the backfile to import or export", Required = true)]
        public string BacpacFile { get; set; }

        internal enum BacpacAction
        {
            Import,
            Export
        }
    }
}
