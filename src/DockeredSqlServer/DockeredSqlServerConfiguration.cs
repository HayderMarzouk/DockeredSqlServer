namespace DockeredSqlServer
{
    /// <summary>
    /// Dockered Sql Server instance
    /// </summary>
    /// <seealso cref="Talentech.SqlServer.Docker.SqlServerConfiguration" />
    public class DockeredSqlServerConfiguration: SqlServerConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the docker image.
        /// </summary>
        /// <value>
        /// The name of the docker image.
        /// </value>
        public string DockerImageName { get; set; }
    }
}
