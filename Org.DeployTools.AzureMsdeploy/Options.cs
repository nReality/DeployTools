using CommandLine;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.AzureMsdeploy
{
    class Options : ICommandLineOptions
    {
        [Option("username", HelpText = "Username for authentication", Required = true)]
        public string Username { get; set; }
        [Option("password", HelpText = "Password for authentication", Required = true)]
        public string Password { get; set; }
        [Option("sitename", HelpText = "Name of the site to deploy to", Required = true)]
        public string Sitename { get; set; }
        [Option("project-dir", HelpText = "Local path to deploy from", Required = true)]
        public string ProjectDir { get; set; }

        public void GuardArgumentsValid()
        {
        }

        public void Setup()
        {
        }
    }
}
