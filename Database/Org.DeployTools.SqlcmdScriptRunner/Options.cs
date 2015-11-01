using System.Collections.Generic;
using CommandLine;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.SqlcmdScriptRunner
{
    class Options : ConnectionStringOptions
    {
        [ValueList(typeof(List<string>))]
        public List<string> Files { get; set; }
    }
}
