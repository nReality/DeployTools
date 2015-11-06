using System;
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
            var passed = ParseArguments(args, options);

            Assert.IsTrue(passed, "failed to parse args");
            var builder = options.ConnectionStringBuilder();
            Assert.AreEqual("server", builder.DataSource);
            Assert.AreEqual("database", builder.InitialCatalog);
            Assert.AreEqual(true, builder.IntegratedSecurity);
        }

        [Test]
        public void CanHaveSqlAuthentication()
        {
            var args = new[] {"--server", "server", "--database", "database", "--username", "user", "--password", "pass"};

            var options = new ConnectionStringOptions();
            var passed = ParseArguments(args, options);

            Assert.IsTrue(passed, "failed to parse args");
            var builder = options.ConnectionStringBuilder();
            Assert.AreEqual(false, builder.IntegratedSecurity);
            Assert.AreEqual("user", builder.UserID);
            Assert.AreEqual("pass", builder.Password);
        }

        [Test]
        [TestCase("--username", "user")]
        [TestCase("--password", "pass")]
        public void MustHaveUsernameAndPassword(string argumentName, string value)
        {
            var args = new[] { "--server", "server", "--database", "database", argumentName, value };

            var options = new ConnectionStringOptions();

            var failed = false;
            try
            {
                ParseArguments(args, options);
            }
            catch (Exception)
            {
                failed = true;
            }
            Assert.IsTrue(failed, "expected parsing to fail");
        }

        [Test]
        public void UseTrustServerCert()
        {
            var args = new[]
            {
                "--server", "server", "--database", "database", "--username", "user", "--password", "pass",
                "--trust-server-certificate"
            };

            var options = new ConnectionStringOptions();
            var passed = ParseArguments(args, options);

            Assert.IsTrue(passed, "failed to parse args");
            var builder = options.ConnectionStringBuilder();
            Assert.AreEqual(true, builder.TrustServerCertificate);
        }

        private static bool ParseArguments(string[] args, ConnectionStringOptions options)
        {
            var passed = CommandLine.Parser.Default.ParseArguments(args, options);
            options.GuardArgumentsValid();
            return passed;
        }
    }
}
