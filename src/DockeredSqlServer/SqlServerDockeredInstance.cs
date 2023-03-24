using Ductus.FluentDocker.Services;
using System;
using System.Data.SqlClient;
using System.Threading;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDockeredInstance"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SqlServerDockeredInstance(SqlServerConfiguration config) : base(config)
        {

        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <exception cref="System.Exception">Cannot start mysql</exception>
        public override void Start()
        {
            var builder = new Ductus.FluentDocker.Builders.Builder()
                .UseContainer()
                .UseImage(Config.DockerImageName)
                .ExposePort(Config.Port, 1433)
                .WithEnvironment("ACCEPT_EULA=Y", $"SA_PASSWORD={Config.AdminPassword}", "MSSQL_MEMORY_LIMIT_MB=4000")
                .WaitForPort($"1433/tcp", TimeSpan.FromSeconds(60), "127.0.0.1");

            if (!string.IsNullOrWhiteSpace(Config.ContainerName))
            {
                builder.WithName(Config.ContainerName);
            }
            
            _container = builder.Build()
                        .Start();

            _dockerStarted = true;

            //Wait for server to start (max 60s)
            bool serverStarted = false;
            for (int i = 0; i < Config.StartServerTimeOut /2; i++)
            {
                if (IsServerUp())
                {
                    serverStarted = true;
                    break;
                }
                Thread.Sleep(2000);
            }

            if (!serverStarted)
            {
                Stop();
                throw new Exception("Cannot start Sql server instance in 120 seconds");
            }
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
            string sqlConnectionString = Config.BuildConnectionString("master");
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
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