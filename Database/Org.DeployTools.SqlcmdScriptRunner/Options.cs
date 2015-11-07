using System.Collections.Generic;
using CommandLine;
using Org.DeployTools.Shared;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.SqlcmdScriptRunner
{
    class Options : ConnectionStringOptions
    {
        [ValueList(typeof(List<string>))]
        public List<string> FileMasks { get; set; }

        [Option("sqlcmd-path", DefaultValue = DefaultSettings.SqlcmdPath, HelpText = "Path to sqlcmd.exe", Required = false)]
        public string SqlcmdPath { get; set; }
    }
}
