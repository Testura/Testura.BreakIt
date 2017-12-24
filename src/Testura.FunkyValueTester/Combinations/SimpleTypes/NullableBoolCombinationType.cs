using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class NullableBoolCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue)
        {
            return new[] { new Combination(memberPath, false), new Combination(memberPath, true), new Combination(memberPath, null) };
        }
    }
}
