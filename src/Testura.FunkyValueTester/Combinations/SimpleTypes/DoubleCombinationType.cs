using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class DoubleCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, double.MaxValue), new Combination(memberPath, double.MinValue) };
        }
    }
}
