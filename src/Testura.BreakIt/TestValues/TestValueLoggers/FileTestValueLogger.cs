using System.IO;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    /// <summary>
    /// Provides the functionallity to log test values to a file.
    /// </summary>
    public class FileTestValueLogger : TestValueLogger
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTestValueLogger"/> class.
        /// </summary>
        /// <param name="path">Path to file.</param>
        public FileTestValueLogger(string path)
        {
            _path = path;
        }

        /// <inheritdoc />
        protected override void Log(string message)
        {
            using (var file = File.AppendText(_path))
            {
                file.WriteLine(message);
            }
        }
    }
}
