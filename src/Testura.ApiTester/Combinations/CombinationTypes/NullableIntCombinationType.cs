using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class NullableIntCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, int.MaxValue), new Combination(name, int.MinValue), new Combination(name, null) };
        }
    }
}
