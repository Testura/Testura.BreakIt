using System.Collections.Generic;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    /// <summary>
    /// Provides the functionallity to save test values logs in memory.
    /// </summary>
    public class MemoryTestValueLogger : TestValueLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTestValueLogger"/> class.
        /// </summary>
        public MemoryTestValueLogger()
        {
            LogLines = new List<string>();
        }

        /// <summary>
        /// Gets all log lines.
        /// </summary>
        public List<string> LogLines { get; }

        /// <inheritdoc />
        protected override void Log(string message)
        {
            LogLines.Add(message);
        }
    }
}
