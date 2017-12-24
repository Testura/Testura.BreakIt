using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class IntCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, int.MaxValue), new Combination(memberPath, int.MinValue) };
        }
    }
}
