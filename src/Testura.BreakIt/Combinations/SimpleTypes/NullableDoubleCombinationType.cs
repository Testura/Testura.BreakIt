﻿namespace Testura.BreakIt.Combinations.SimpleTypes
{
    public class NullableDoubleCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, double.MaxValue), new Combination(memberPath, double.MinValue), new Combination(memberPath, null) };
        }
    }
}