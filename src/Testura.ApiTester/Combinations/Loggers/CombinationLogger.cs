using System;

namespace Testura.ApiTester.Combinations
{
    public abstract class CombinationLogger
    {
        public void Log(Combination combination, Exception invokeException)
        {
            string testingValue;
            string resultValue;

            if (combination.LogValue == null)
            {
                testingValue = $"Testing parameter {combination.Name} => null";
            }
            else if (combination.LogValue.GetType() == typeof(string))
            {
                var value = combination.LogValue;
                if (string.IsNullOrEmpty((string) value))
                {
                    testingValue = $"Testing parameter {combination.Name} => empty";
                }
                else
                {
                    testingValue = $"Testing parameter {combination.Name} => \"{value}\"";
                }
                
            }
            else
            {
                testingValue = $"Testing parameter {combination.Name} => {combination.LogValue}";
            }

            if (invokeException == null)
            {
                resultValue = "Result: No exception";
            }
            else
            {
                resultValue = $"Result: Exception - {invokeException.Message}";
            }

            Log($"{testingValue} - {resultValue}");
            
        }

        public abstract void Log(string message);
    }
}