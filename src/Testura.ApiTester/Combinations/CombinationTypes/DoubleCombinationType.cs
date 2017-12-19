﻿using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class DoubleCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            return new[] { new Combination(name, double.MaxValue), new Combination(name, double.MinValue) };
        }
    }
}
