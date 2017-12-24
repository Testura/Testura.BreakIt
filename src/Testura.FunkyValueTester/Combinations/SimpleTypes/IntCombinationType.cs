using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class IntCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, int.MaxValue), new Combination(memberPath, int.MinValue) };
        }
    }
}
