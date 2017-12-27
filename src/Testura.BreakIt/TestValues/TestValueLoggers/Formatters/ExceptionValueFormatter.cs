namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    /// <summary>
    /// Provides a way to formatt the exception part of the test value result.
    /// </summary>
    public class ExceptionValueFormatter : ILogFormatter
    {
        private const string Tag = "Exception =>";

        /// <inheritdoc />
        public string GetFormat(TestValueResult result)
        {
            if (result.Exception == null)
            {
                return $"{Tag} No exception";
            }

            return $"{Tag} {result.Exception.Message}";
        }
    }
}