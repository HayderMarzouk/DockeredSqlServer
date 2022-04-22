using System.IO;

namespace DockeredSqlServer
{
    /// <summary>
    /// Represents a script that comes from some source, e.g. an embedded resource in an assembly. 
    /// </summary>
    public class SqlScript
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlScript"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public SqlScript(string content)
        {
            Content = content;
        }
        
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; }

        /// <summary>
        /// Froms the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static SqlScript FromFile(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return FromStream(fileStream);
            }
        }

        /// <summary>
        /// Froms the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">encoding</exception>
        public static SqlScript FromStream(Stream stream)
        {
            using (var resourceStreamReader = new StreamReader(stream, true))
            {
                var contents = resourceStreamReader.ReadToEnd();
                return new SqlScript(contents);
            }
        }

    }
}
