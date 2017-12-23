namespace Testura.FunkyApiTester.Combinations.Loggers.Formatters
{
    public class ValidationFormatter : ILogFormatter
    {
        private const string Tag = "Validation =>";

        public string GetFormat(CombinationResult result)
        {
            if (result.ResultOk == null)
            {
                return $"{Tag} No validation done";
            }

            return result.ResultOk.Value ? $"{Tag} Validation OK" : $"{Tag} Validation NOT OK";
        }
    }
}
