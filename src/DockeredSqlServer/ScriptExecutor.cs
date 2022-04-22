using System.Collections.Generic;
using System.Data.SqlClient;

namespace DockeredSqlServer
{
    /// <summary>
    /// Executes a script on a sql database
    /// </summary>
    public class ScriptExecutor
    {
        private readonly SqlServerInstanceBase _sqlInstance;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptExecutor"/> class.
        /// </summary>
        /// <param name="sqlInstance">The SQL instance.</param>
        public ScriptExecutor(SqlServerInstanceBase sqlInstance)
        {
            _sqlInstance = sqlInstance;
        }

        /// <summary>
        /// Executes the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="database">The database.</param>
        public void Execute(SqlScript script, string database)
        {
            var parser = new ScriptParser(script);
            ExecuteCommands(parser.Parse(), database);
        }


        private void ExecuteCommands(IEnumerable<string> scriptCommands, string database)
        {
            using var con = new SqlConnection(_sqlInstance.Config.BuildConnectionString(database));
            con.Open();
            
            foreach (string scriptCommand in scriptCommands)
            {
                ExecuteCommand(con, scriptCommand);
            }

        }

        private void ExecuteCommand(SqlConnection con, string commandText)
        {
            var command = con.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }

    }
}
