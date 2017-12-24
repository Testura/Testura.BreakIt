using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class StringCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, string.Empty), new Combination(memberPath, null) };
        }
    }
}
