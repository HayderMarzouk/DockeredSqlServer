using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DockeredSqlServer
{

    /// <summary>
    /// Abstract Sql server instance
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class SqlServerInstanceBase : IDisposable
    {
        protected SqlServerInstanceBase(SqlServerConfiguration config, ILogger logger)
        {
            Config = config;
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        protected SqlServerConfiguration Config { get; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public abstract void Stop();


        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public virtual void CreateDatabase(string databaseName)
        {
            using (var con = new SqlConnection(GetConnectionstring("master")))
            {
                con.Open();
                ExecuteCommand(con, $"CREATE DATABASE [{databaseName}]");
            }
        }

        /// <summary>
        /// Executes the scripts.
        /// </summary>
        /// <param name="scriptsPath">The scripts path.</param>
        /// <param name="database">The database.</param>
        public void ExecuteScripts(string scriptsPath, string database)
        {
            var files = Directory.GetFiles(scriptsPath);

            using (var con = new SqlConnection(GetConnectionstring(database)))
            {
                con.Open();
                foreach (var file in files.OrderBy(f => f))
                {
                    ExecuteScript(file, con);
                }
            }
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="scriptPath">The script path.</param>
        /// <param name="con">The con.</param>
        public void ExecuteScript(string scriptPath, SqlConnection con)
        {
            string script = File.ReadAllText(scriptPath);
            var commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase).Where(c => !string.IsNullOrWhiteSpace(c));
            foreach (var command in commandStrings)
            {
                ExecuteCommand(con, command);
            }
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="scriptPath">The script path.</param>
        /// <param name="database">The database.</param>
        public void ExecuteScript(string scriptPath, string database)
        {
            using (var con = new SqlConnection(GetConnectionstring(database)))
            {
                con.Open();
                ExecuteScript(scriptPath, con);
            }

        }

        private void ExecuteCommand(SqlConnection con, string commandText)
        {
            var command = con.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Gets the connectionstring.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <returns></returns>
        public string GetConnectionstring(string database)
        {
            return $"Server={Config.InstanceName},{Config.Port};Database={database};User ID={Config.AdminUserName};Password={Config.AdminPassword};";
        }
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
