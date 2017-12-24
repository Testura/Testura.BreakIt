using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class NullableBoolCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, false), new Combination(name, true), new Combination(name, null) };
        }
    }
}
