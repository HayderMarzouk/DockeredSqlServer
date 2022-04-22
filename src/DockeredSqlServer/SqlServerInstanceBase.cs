using System;

namespace DockeredSqlServer
{

    /// <summary>
    /// Abstract Sql server instance
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class SqlServerInstanceBase : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerInstanceBase"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        protected SqlServerInstanceBase(SqlServerConfiguration config)
        {
            Config = config;
        }
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public SqlServerConfiguration Config { get; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlServerInstanceBase"/> class.
        /// </summary>
        ~SqlServerInstanceBase()
        {
            Dispose(false);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
            }
        }
    }
}
