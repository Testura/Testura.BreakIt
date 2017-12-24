using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class NullableDoubleCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, double.MaxValue), new Combination(memberPath, double.MinValue), new Combination(memberPath, null) };
        }
    }
}
