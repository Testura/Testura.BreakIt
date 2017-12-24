using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class FloatCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, float.MaxValue), new Combination(memberPath, float.MinValue) };
        }
    }
}
