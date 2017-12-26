namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    public class ExceptionValueFormatter : ILogFormatter
    {
        private const string Tag = "Exception =>";

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