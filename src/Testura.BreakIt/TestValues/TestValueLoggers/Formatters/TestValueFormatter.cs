namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    public class TestValueFormatter : ILogFormatter
    {
        public string GetFormat(TestValueResult result)
        {
            var combination = result.TestingValue;
            var tag = $"Testing parameter {combination.MemberPath} =>";

            if (combination.LogValue == null)
            {
                return $"{tag} null";
            }

            if (combination.LogValue is string)
            {
                var value = combination.LogValue;
                if (string.IsNullOrEmpty((string)value))
                {
                    return $"{tag} empty";
                }

                return $"{tag} \"{value}\"";
            }

            return $"{tag} {combination.LogValue}";
        }
    }
}
