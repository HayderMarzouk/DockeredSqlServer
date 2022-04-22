using System.Linq;
using System.Text.RegularExpressions;

namespace DockeredSqlServer
{
    /// <summary>
    /// Ensures sql scripts parsing
    /// </summary>
    public class ScriptParser
    {
        const string delimiter = "GO";
        private readonly SqlScript _script;
        private readonly Regex _delimiterRegex = new Regex(@$"^\s*{delimiter}.*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptParser"/> class.
        /// </summary>
        /// <param name="script">The script.</param>
        public ScriptParser(SqlScript script)
        {
            _script = script;
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns></returns>
        public string[] Parse()
        {
            return _delimiterRegex.Split(_script.Content)
                                  .Where (s=> !string.IsNullOrWhiteSpace(s))
                                  .ToArray();
        }
    }
}
