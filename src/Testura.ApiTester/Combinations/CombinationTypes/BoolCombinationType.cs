using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class BoolCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] {new Combination(name, false), new Combination(name, true) };
        }
    }
}
