using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class StringCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, string.Empty), new Combination(name, null) };
        }
    }
}
