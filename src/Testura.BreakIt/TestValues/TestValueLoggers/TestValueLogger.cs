using System.Collections.Generic;
using System.Linq;
using Testura.BreakIt.TestValues.TestValueLoggers.Formatters;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    /// <summary>
    /// Provides methods to log test values and result.
    /// </summary>
    public abstract class TestValueLogger
    {
        private readonly List<ILogFormatter> _logFormatters;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestValueLogger"/> class.
        /// </summary>
        protected TestValueLogger()
        {
            _logFormatters = new List<ILogFormatter>
            {
                new TestValueFormatter(),
                new ValidationFormatter(),
                new ReturnValueFormatter(),
                new ExceptionValueFormatter()
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestValueLogger"/> class.
        /// </summary>
        /// <param name="logFormatters">A list of formatters for the test value</param>
        protected TestValueLogger(List<ILogFormatter> logFormatters)
        {
            _logFormatters = new List<ILogFormatter>(logFormatters);
        }

        /// <summary>
        /// Log the test value result
        /// </summary>
        /// <param name="testValueResult">The test value result</param>
        public void Log(TestValueResult testValueResult)
        {
            Log(string.Join(", ", _logFormatters.Select(l => l.GetFormat(testValueResult)).ToArray()));
        }

        /// <summary>
        /// Log the formatted test value result.
        /// </summary>
        /// <param name="message">THe test value result formatted in a string.</param>
        protected abstract void Log(string message);
    }
}