using System;
using System.Collections.Generic;

namespace Testura.BreakIt.Combinations.ComplexTypes
{
    internal class EnumCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if (type.IsEnum)
            {
                return GetEnumCombinations(memberPath, type, false);
            }

            var underlying = Nullable.GetUnderlyingType(type);
            if (underlying != null && underlying.IsEnum)
            {
                return GetEnumCombinations(memberPath, underlying, true);
            }

            return null;
        }

        private Combination[] GetEnumCombinations(string name, Type type, bool isNullable)
        {
            var values = Enum.GetValues(type);
            var combinations = new List<Combination>();

            if (isNullable)
            {
                combinations.Add(new Combination(name, null));
            }

            foreach (var value in values)
            {
                combinations.Add(new Combination(name, value));
            }

            return combinations.ToArray();
        }
    }
}
