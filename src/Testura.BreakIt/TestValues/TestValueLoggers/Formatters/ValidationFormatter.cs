namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    /// <summary>
    /// Provices the functionallity to format the validation part of a test result.
    /// </summary>
    public class ValidationFormatter : ILogFormatter
    {
        private const string Tag = "Validation =>";

        /// <inheritdoc />
        public string GetFormat(TestValueResult result)
        {
            if (result.IsSuccess == null)
            {
                return $"{Tag} No validation done";
            }

            return result.IsSuccess.Value ? $"{Tag} Validation OK" : $"{Tag} Validation NOT OK";
        }
    }
}
