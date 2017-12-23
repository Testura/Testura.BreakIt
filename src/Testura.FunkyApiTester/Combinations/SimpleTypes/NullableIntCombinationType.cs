using System;

namespace Testura.FunkyApiTester.Combinations.SimpleTypes
{
    public class NullableIntCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, int.MaxValue), new Combination(name, int.MinValue), new Combination(name, null) };
        }
    }
}
