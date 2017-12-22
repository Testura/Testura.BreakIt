using System;
using System.Collections.Generic;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public class EnumCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            var values = Enum.GetValues(type);
            var combinations = new List<Combination>();
            foreach (var value in values)
            {
                combinations.Add(new Combination(name, value));
            }

            return combinations.ToArray();
        }
    }
}
