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

            var arguments = SqlcmdArguments.Build(_sqlConnectionStringBuilder);

            Assert.IsTrue(arguments.Contains("-S \"s\" -d \"d\""), "server and database details missing in " + arguments);
        }
    }
}
