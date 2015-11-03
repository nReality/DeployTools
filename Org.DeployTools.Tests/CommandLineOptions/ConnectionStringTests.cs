using NUnit.Framework;
using Org.DeployTools.Shared.CommandLineOptions;

namespace Org.DeployTools.Tests.CommandLineOptions
{
    class ConnectionStringTests
    {
        [Test]
        public void CanHaveIntegratedSecurity()
        {
            var args = new[] {"--server", "server", "--database", "database", "--integrated"};

            var options = new ConnectionStringOptions();
            var passed = CommandLine.Parser.Default.ParseArguments(args, options);

            Assert.IsTrue(passed);
            var builder = options.ConnectionStringBuilder();
            Assert.AreEqual("server", builder.DataSource);
            Assert.AreEqual("database", builder.InitialCatalog);
            Assert.AreEqual(true, builder.IntegratedSecurity);
        }
    }
}
