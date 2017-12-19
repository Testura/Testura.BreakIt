using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class StringCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] {new Combination(name, string.Empty), new Combination(name, null)};
        }
    }
}
