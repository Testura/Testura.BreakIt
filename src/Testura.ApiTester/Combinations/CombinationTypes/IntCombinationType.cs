using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class IntCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] {new Combination(name, int.MaxValue), new Combination(name, int.MinValue)};
        }
    }
}
