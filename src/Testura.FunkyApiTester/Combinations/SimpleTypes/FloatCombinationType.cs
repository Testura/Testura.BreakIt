using System;

namespace Testura.FunkyApiTester.Combinations.SimpleTypes
{
    public class FloatCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, float.MaxValue), new Combination(name, float.MinValue) };
        }
    }
}
