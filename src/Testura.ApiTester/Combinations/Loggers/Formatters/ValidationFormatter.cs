namespace Testura.ApiTester.Combinations.Loggers.Formatters
{
    public class ValidationFormatter : ILogFormatter
    {
        private const string Tag = "Validation =>";

        public string GetFormat(CombinationResult result)
        {
            if (result.IsValidationOk == null)
            {
                return $"{Tag} No validation done";
            }

            return result.IsValidationOk.Value ? $"{Tag} Validation OK" : $"{Tag} Validation NOT OK";
        }
    }
}
