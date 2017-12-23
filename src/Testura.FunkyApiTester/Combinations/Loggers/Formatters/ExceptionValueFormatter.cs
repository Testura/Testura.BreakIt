namespace Testura.FunkyApiTester.Combinations.Loggers.Formatters
{
    public class ExceptionValueFormatter : ILogFormatter
    {
        private const string Tag = "Exception =>";

        public string GetFormat(CombinationResult result)
        {
            if (result.Exception == null)
            {
                return $"{Tag} No exception";
            }

            return $"{Tag} {result.Exception.Message}";
        }
    }
}