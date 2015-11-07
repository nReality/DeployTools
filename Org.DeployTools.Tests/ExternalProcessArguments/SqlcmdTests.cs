using System.Data.SqlClient;
using NUnit.Framework;
using Org.DeployTools.Shared.ExternalProcessArgumentBuilder;

namespace Org.DeployTools.Tests.ExternalProcessArguments
{
    internal class SqlcmdTests
    {
        private SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        [SetUp]
        public void Setup()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "s",
                InitialCatalog = "d",
            };
        }

        [Test]
        public void IntegratedSecurity()
        {
            _sqlConnectionStringBuilder.IntegratedSecurity = true;

            var builder = SqlcmdArgumentsBuilder.Build(_sqlConnectionStringBuilder);
            var arguments = builder.ToString();

            Assert.IsTrue(arguments.Contains("-S \"s\" -d \"d\""), "server and database details missing in " + arguments);
            Assert.IsFalse(arguments.Contains("-U \"user\" -P \"pass\""), "user and password details present in " + arguments);
        }

        [Test]
        public void AddScript()
        {
            var builder = SqlcmdArgumentsBuilder.Build(_sqlConnectionStringBuilder);
            var arguments = builder.AddScript("file").ToString();

            Assert.IsTrue(arguments.Contains("-i \"file\""), "script path missing in " + arguments);
        }

        [Test]
        [TestCase(true, "trust flag missing")]
        [TestCase(false, "trust flag present")]
        public void TrustServerCertificate(bool trust, string details)
        {
            _sqlConnectionStringBuilder.TrustServerCertificate = trust;

            var builder = SqlcmdArgumentsBuilder.Build(_sqlConnectionStringBuilder);
            var arguments = builder.AddScript("file").ToString();

            Assert.AreEqual(trust, arguments.Contains("-C"), details + " missing in " + arguments);
        }

        [Test]
        public void SqlAuthentication()
        {
            _sqlConnectionStringBuilder.IntegratedSecurity = false;
            _sqlConnectionStringBuilder.UserID = "user";
            _sqlConnectionStringBuilder.Password = "pass";

            var builder = SqlcmdArgumentsBuilder.Build(_sqlConnectionStringBuilder);
            var arguments = builder.ToString();

            Assert.IsTrue(arguments.Contains("-S \"s\" -d \"d\""), "server and database details missing in " + arguments);
            Assert.IsTrue(arguments.Contains("-U \"user\" -P \"pass\""), "user and password details missing in " + arguments);
        }
    }
}
