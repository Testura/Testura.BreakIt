using System;
using System.Collections.Generic;
using System.Linq;
using Testura.BreakIt.TestValues.ComplexTypes;
using Testura.BreakIt.TestValues.SimpleTypes;

namespace Testura.BreakIt.TestValues
{
    /// <summary>
    /// a
    /// </summary>
    public class TestValueFactory : ITestValueFactory
    {
        private readonly IList<IComplexTestType> _complexCombinations;
        private readonly IDictionary<Type, ISimpleTestType> _combinations;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestValueFactory"/> class.
        /// </summary>
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
                [typeof(decimal?)] = new NullableDecimalTestType(),
                [typeof(short)] = new ShortTestType(),
                [typeof(short?)] = new NullableShortTestType(),
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestValueFactory"/> class.
        /// </summary>
        /// <param name="addTestTypes">Append the default list with new test types. Will override default list types at conflict.</param>
        public TestValueFactory(IDictionary<Type, ISimpleTestType> addTestTypes)
            : this()
        {
            foreach (var addCombination in addTestTypes)
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

        /// <inheritdoc />
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
