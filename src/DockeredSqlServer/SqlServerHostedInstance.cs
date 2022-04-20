using Microsoft.Extensions.Logging;

namespace DockeredSqlServer
{
    /// <summary>
    /// A hosted sql server implementation
    /// </summary>
    /// <seealso cref="Snal.Medical.Testing.SqlServerInstanceBase" />
    internal class SqlServerHostedInstance : SqlServerInstanceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostedSqlServerInstance"/> class.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="adminUser">The admin user.</param>
        /// <param name="adminPassword">The admin password.</param>
        public SqlServerHostedInstance(SqlServerConfiguration config, ILogger<SqlServerHostedInstance> logger): base (config, logger)
        {

        }
        /// <summary>
        /// Do nothing
        /// </summary>
        public override void Start()
        {
            //Nothing to start
        }
        /// <summary>
        /// Do nothing
        /// </summary>
        public override void Stop()
        {
            //Nothing to stop
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public override void CreateDatabase(string databaseName)
        {
            //Databases should be created manually
        }
    }
}
