using System;
using System.Collections.Generic;
using System.Linq;
using Testura.BreakIt.TestValues.ComplexTypes;
using Testura.BreakIt.TestValues.SimpleTypes;

namespace Testura.BreakIt.TestValues
{
    public class TestValueFactory : ITestValueFactory
    {
        private readonly IList<IComplexTestType> _complexCombinations;
        private readonly IDictionary<Type, ISimpleTestType> _combinations;

        public TestValueFactory()
        {
            _complexCombinations = new List<IComplexTestType>
            {
                new CollectionTestType(),
                new DictionaryTestType(),
                new EnumTestType(),
                new CustomClassTestType()
            };

            _combinations = new Dictionary<Type, ISimpleTestType>
            {
                [typeof(string)] = new StringTestType(),
                [typeof(int)] = new IntTestType(),
                [typeof(double)] = new DoubleTestType(),
                [typeof(float)] = new FloatTestType(),
                [typeof(int?)] = new NullableIntTestType(),
                [typeof(double?)] = new NullableDoubleTestType(),
                [typeof(float?)] = new NullableFloatTestType(),
                [typeof(bool?)] = new NullableBoolTestType(),
                [typeof(bool)] = new BoolTestType(),
                [typeof(decimal)] = new DecimalTestType(),
                [typeof(decimal?)] = new NullableDecimalTestType()
            };
        }

        public TestValueFactory(IDictionary<Type, ISimpleTestType> addCombinations)
            : this()
        {
            foreach (var addCombination in addCombinations)
            {
                if (_combinations.ContainsKey(addCombination.Key))
                {
                    _combinations[addCombination.Key] = addCombination.Value;
                }
                else
                {
                    _combinations.Add(addCombination.Key, addCombination.Value);
                }
            }
        }

        public TestValue[] GetTestValues(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList = null)
        {
            if (excludeList != null && excludeList.Any(e => e(name, type)))
            {
                return new TestValue[0];
            }

            if (_combinations.ContainsKey(type))
            {
                return _combinations[type].GetTestValue(name);
            }

            foreach (var complexCombination in _complexCombinations)
            {
                var combinations = complexCombination.GetTestValues(name, type, defaultValue, excludeList, this);
                if (combinations != null)
                {
                    return combinations;
                }
            }

            return new TestValue[0];
        }
    }
}
