namespace DockeredSqlServer
{
    /// <summary>
    /// Sql server configuration base
    /// </summary>
    public class SqlServerConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the instance.
        /// </summary>
        /// <value>
        /// The name of the instance.
        /// </value>
        public string InstanceName { get; set; }
        /// <summary>
        /// Gets or sets the name of the admin user.
        /// </summary>
        /// <value>
        /// The name of the admin user. Default is sa
        /// </value>
        
        public string AdminUserName { get; set; } = "sa";
        /// <summary>
        /// Gets or sets the admin password.
        /// </summary>
        /// <value>
        /// The admin password.
        /// </value>
        public string AdminPassword { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port. Default is 1433
        /// </value>
        public int Port { get; set; } = 1433;
    }
}
