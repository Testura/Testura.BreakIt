using Testura.BreakIt.Extensions;

namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    public class ReturnValueFormatter : ILogFormatter
    {
        private const string Tag = "Return value =>";

        public string GetFormat(TestValueResult result)
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
