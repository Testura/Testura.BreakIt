using System;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    /// <summary>
    /// Provides a way to log test values to the console.
    /// </summary>
    public class ConsoleTestValueLogger : TestValueLogger
    {
        /// <inheritdoc />
        protected override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
