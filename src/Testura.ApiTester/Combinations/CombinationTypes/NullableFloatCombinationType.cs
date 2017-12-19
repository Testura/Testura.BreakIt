using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class NullableFloatCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, float.MaxValue), new Combination(name, float.MinValue), new Combination(name, null) };
        }
    }
}
