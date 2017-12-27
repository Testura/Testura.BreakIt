namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    /// <summary>
    /// Provides an interface to convert test value results to a formatted string.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Get test value or part of the test value in a formatted string.
        /// </summary>
        /// <param name="result">The test value result</param>
        /// <returns>The test value (or parts) in a formatted string. </returns>
        string GetFormat(TestValueResult result);
    }
}
