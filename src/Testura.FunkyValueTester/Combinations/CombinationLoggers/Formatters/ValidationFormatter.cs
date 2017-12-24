namespace Testura.FunkyValueTester.Combinations.CombinationLoggers.Formatters
{
    public class ValidationFormatter : ILogFormatter
    {
        private const string Tag = "Validation =>";

        public string GetFormat(CombinationResult result)
        {
            if (result.IsSuccess == null)
            {
                return $"{Tag} No validation done";
            }

            return result.IsSuccess.Value ? $"{Tag} Validation OK" : $"{Tag} Validation NOT OK";
        }
    }
}
