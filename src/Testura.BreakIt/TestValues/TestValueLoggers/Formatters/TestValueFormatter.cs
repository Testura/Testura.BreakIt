namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    /// <summary>
    /// Provides the functionallity to format the value part of a test value result.
    /// </summary>
    public class TestValueFormatter : ILogFormatter
    {
        /// <inheritdoc />
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
