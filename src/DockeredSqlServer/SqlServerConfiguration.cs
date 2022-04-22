namespace DockeredSqlServer
{
    /// <summary>
    /// Sql server configuration base
    /// </summary>
    public class SqlServerConfiguration
    {
        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public SqlServerModes Mode { get; set; }
        /// <summary>
        /// Gets or sets the name of the docker image.
        /// </summary>
        /// <value>
        /// The name of the docker image.
        /// </value>
        public string? DockerImageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the instance.
        /// </summary>
        /// <value>
        /// The name of the instance.
        /// </value>
        public string InstanceName { get; set; }
        /// <summary>
        /// The integrated security
        /// </summary>
        public bool IntegratedSecurity { get; set; } = false;
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
        /// <summary>
        /// Time to wait in seconds 
        /// </summary>
        public int StartServerTimeOut { get; set; } = 120;
        /// <summary>
        /// Gets or sets the name of the container (optional).
        /// </summary>
        /// <value>
        /// The name of the container.
        /// </value>
        public string? ContainerName { get; set; }
    }
}
