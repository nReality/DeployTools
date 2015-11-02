using System;
using System.IO;
using CommandLine;
using Org.DeployTools.Shared;
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
        [Option("project-dir", HelpText = "Local path to deploy from. The package directory will be a sub directory of this")]
        public string ProjectDir { get; set; }
        [Option("package-dir", HelpText = "Local path where the package is located")]
        public string PackageDir { get; set; }

        public void GuardArgumentsValid()
        {
            if (ProjectDir == null && PackageDir == null)
                throw new Exception("Either ProjectDir or PackageDir must be specified");
            if (ProjectDir != null && PackageDir != null)
                throw new Exception("Only one of ProjectDir or PackageDir must be specified");
        }

        public void Setup()
        {
            PackageDir = GetFullPathToPackageDir(PackageDir ?? Path.Combine(ProjectDir, DefaultSettings.PackageDirectoryInProjectDir));
        }

        private static string GetFullPathToPackageDir(string packageDir)
        {
            var isPathRooted = Path.IsPathRooted(packageDir);
            return isPathRooted ? packageDir : Path.Combine(Directory.GetCurrentDirectory(), packageDir);
        }
    }
}
