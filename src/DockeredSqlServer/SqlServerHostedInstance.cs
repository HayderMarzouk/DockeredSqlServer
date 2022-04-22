namespace DockeredSqlServer
{
    /// <summary>
    /// A hosted sql server implementation
    /// </summary>
    /// <seealso cref="Snal.Medical.Testing.SqlServerInstanceBase" />
    public class SqlServerHostedInstance : SqlServerInstanceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerHostedInstance"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SqlServerHostedInstance(SqlServerConfiguration config): base (config)
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
    }
}
