using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class BoolCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, false), new Combination(name, true) };
        }
    }
}
