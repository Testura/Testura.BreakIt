using System;
using System.Collections.Generic;
using System.Linq;
using Testura.ApiTester.Combinations.ComplexTypes;
using Testura.ApiTester.Combinations.SimpleTypes;

namespace Testura.ApiTester.Combinations
{
    public class CombinationFactory : ICombinationFactory
    {
        private readonly IList<IComplexType> _complexCombinations;
        private readonly IDictionary<Type, ICombinationType> _combinations;

        public CombinationFactory()
        {
            _complexCombinations = new List<IComplexType>
            {
                new CollectionCombinationType(),
                new DictionaryCombinationType(),
                new EnumCombinationType(),
                new CustomClassCombinationType()
            };

            _combinations = new Dictionary<Type, ICombinationType>
            {
                [typeof(string)] = new StringCombinationType(),
                [typeof(int)] = new IntCombinationType(),
                [typeof(double)] = new DoubleCombinationType(),
                [typeof(float)] = new FloatCombinationType(),
                [typeof(int?)] = new NullableIntCombinationType(),
                [typeof(double?)] = new NullableDoubleCombinationType(),
                [typeof(float?)] = new NullableFloatCombinationType(),
                [typeof(bool?)] = new NullableBoolCombinationType(),
                [typeof(bool)] = new BoolCombinationType()
            };
        }

        public CombinationFactory(IDictionary<Type, ICombinationType> addCombinations)
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

        public Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList = null)
        {
            if (excludeList != null && excludeList.Any(e => e(name, type)))
            {
                return new Combination[0];
            }

            if (_combinations.ContainsKey(type))
            {
                return _combinations[type].GetCombinations(name, type, defaultValue);
            }

            foreach (var complexCombination in _complexCombinations)
            {
                var combinations = complexCombination.GetCombinations(name, type, defaultValue, excludeList, this);
                if (combinations != null)
                {
                    return combinations;
                }
            }

            return new Combination[0];
        }
    }
}
