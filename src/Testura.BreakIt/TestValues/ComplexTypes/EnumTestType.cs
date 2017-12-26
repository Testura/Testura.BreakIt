using System;
using System.Collections.Generic;

namespace Testura.BreakIt.TestValues.ComplexTypes
{
    internal class EnumTestType : IComplexTestType
    {
        public TestValue[] GetTestValues(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ITestValueFactory combinationFactory)
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

        private TestValue[] GetEnumCombinations(string name, Type type, bool isNullable)
        {
            var values = Enum.GetValues(type);
            var combinations = new List<TestValue>();

            if (isNullable)
            {
                combinations.Add(new TestValue(name, null));
            }

            foreach (var value in values)
            {
                combinations.Add(new TestValue(name, value));
            }

            return combinations.ToArray();
        }
    }
}
