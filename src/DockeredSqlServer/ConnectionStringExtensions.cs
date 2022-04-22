using System.Text;

namespace DockeredSqlServer
{
    /// <summary>
    /// Connection string extensions
    /// </summary>
    public static class ConnectionStringExtensions
    {
        /// <summary>
        /// Builds the connection string.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="database">The database.</param>
        /// <returns></returns>
        public static string BuildConnectionString(this SqlServerConfiguration config, string database)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Server={0},{1};", config.InstanceName, config.Port);
            result.AppendFormat("Database={0};", database);

            if (config.IntegratedSecurity)
            {
                result.Append("Integrated Security=True;");
            }
            else
            {
                result.AppendFormat("User ID={0};Password={1};", config.AdminUserName, config.AdminPassword);
            }

            result.Append("MultipleActiveResultSets=True;");

            return result.ToString();
        }
    }
}
