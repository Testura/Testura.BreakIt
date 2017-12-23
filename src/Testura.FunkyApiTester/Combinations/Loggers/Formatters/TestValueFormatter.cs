namespace Testura.FunkyApiTester.Combinations.Loggers.Formatters
{
    public class TestValueFormatter : ILogFormatter
    {
        public string GetFormat(CombinationResult result)
        {
            var combination = result.TestingValue;
            var tag = $"Testing parameter {combination.Name} =>";

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
