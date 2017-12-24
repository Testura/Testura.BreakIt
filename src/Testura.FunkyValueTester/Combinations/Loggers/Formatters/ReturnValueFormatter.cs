using Testura.FunkyValueTester.Extensions;

namespace Testura.FunkyValueTester.Combinations.Loggers.Formatters
{
    public class ReturnValueFormatter : ILogFormatter
    {
        private const string Tag = "Return value =>";

        public string GetFormat(CombinationResult result)
        {
            if (result.ReturnValue == null)
            {
                return $"{Tag} null or no return value";
            }

            if (result.ReturnValue.IsNumeric())
            {
                return $"{Tag} {result.ReturnValue}";
            }

            if (result.ReturnValue is string)
            {
                return $"{Tag} \"{result.ReturnValue}\"";
            }

            return $"{Tag} Complex type";
        }
    }
}
