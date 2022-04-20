using Ductus.FluentDocker;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace DockeredSqlServer 
{ 
    /// <summary>
    /// Created a SQL server instance using Docker for tests purposes
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    #pragma warning disable S3881 // "IDisposable" should be implemented correctly ==> https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2215
    public class SqlServerDockeredInstance : SqlServerInstanceBase
    #pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        private IContainerService? _container;
        private bool _dockerStarted;
        private DockeredSqlServerConfiguration _config;
        private readonly ILogger<SqlServerDockeredInstance> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDockeredInstance"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SqlServerDockeredInstance(DockeredSqlServerConfiguration config) : base(config)
        {
            _config = config;
        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <exception cref="System.Exception">Cannot start mysql</exception>
        public override void Start()
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dockerFilePath = Path.Combine(currentDir, "Environment\\Resources\\Dockerfile");
            var dockerFileContent = File.ReadAllText(dockerFilePath);

            _container = Fd
            .DefineImage(_config.DockerImageName)
                .ReuseIfAlreadyExists()
                .FromString(dockerFileContent)
                .Builder()

            .UseContainer()
                .UseImage(_config.DockerImageName)
                .ExposePort(_config.Port, 1433)
                .WithEnvironment("ACCEPT_EULA=Y", $"SA_PASSWORD={_config.AdminPassword}", "MSSQL_MEMORY_LIMIT_MB=4000")
                .Wait("sqlserver", (service, cnt) => {
                    if (cnt > 120)
                    {
                        throw new FluentDockerException("Failed to wait for sqlserver service");
                    }
                    return IsServerUp()? 0 : 2000;
                })
                .Build().Start();

            _dockerStarted = true;
        }
       

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            if (_container != null && _dockerStarted)
            {
                _container.Stop();
            }
            _dockerStarted = false;
        }

        private bool IsServerUp()
        {
            string sqlConnectionString = GetConnectionstring("master");
            _logger.LogInformation("IsServerUp: Checking sql connection");
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                }
                _logger.LogInformation($"IsServerUp: Connection established");
                return true;
            }
            catch
            {
                _logger.LogWarning("IsServerUp: Connection failed");
                return false;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                base.Dispose(false);
                if (_container != null)
                {
                    _container.Dispose();
                    _container = null;
                }
            }
        }
    }
}