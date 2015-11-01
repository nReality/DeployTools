using CommandLine;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.DacpacDbCompare
{
    class Options : ConnectionStringOptions
    {
        [Option("dacpac", HelpText = "Dacpac file to use as input", Required = true)]
        public string DacpacFile { get; set; }
        [Option("profile", HelpText = "Profile file to use with comparing", Required = true)]
        public string ProfileFile { get; set; }

        [Option("output", HelpText = "Output script file", Required = true)]
        public string OutputFile { get; set; }
    }
}
