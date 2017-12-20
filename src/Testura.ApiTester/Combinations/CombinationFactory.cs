using System;
using System.Collections.Generic;
using System.Linq;
using Force.DeepCloner;
using Testura.ApiTester.Combinations.CombinationTypes;
using Testura.ApiTester.Extensions;

namespace Testura.ApiTester.Combinations
{
    public class CombinationFactory : ICombinationFactory
    {
        private readonly List<Func<string, Type, bool>> _excludeList;
        private readonly IDictionary<Type, ICombinationType> _combinations;

        public CombinationFactory(List<Func<string, Type, bool>> excludeList)
        {
            _excludeList = excludeList ?? new List<Func<string, Type, bool>>();
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
                [typeof(bool)] = new BoolCombinationType(),
            };
        }

        public CombinationFactory(List<Func<string, Type, bool>> excludeList, IDictionary<Type, ICombinationType> addCombinations)
            : this(excludeList)
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

        public Combination[] GetCombinations(string name, Type type, object defaultValue)
        {
            if (_excludeList.Any(e => e(name, type)))
            {
                return new Combination[0];
            }

            if (_combinations.ContainsKey(type))
            {
                return _combinations[type].GetCombinations(name, type, defaultValue);
            }

            if (type.IsCollection() || type.IsICollection())
            {
                return new[] { new Combination(name, null) };
            }

            if (IsBuiltInTye(type))
            {
                throw new Exception($"Failed to create combination for name = {name}, type = {type}");
            }

            if (defaultValue == null)
            {
                return new Combination[0];
            }

            var allCombinations = new List<Combination>();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var clone = defaultValue.DeepClone();
                var combinations = GetCombinations($"{name}.{propertyInfo.Name}", propertyInfo.PropertyType, propertyInfo.GetValue(clone));
                if (combinations != null)
                {
                    foreach (var combination in combinations)
                    {
                        var secondClone = clone.DeepClone();
                        propertyInfo.SetValue(secondClone, combination.Value);
                        combination.Value = secondClone;
                        allCombinations.Add(combination);
                    }
                }
            }

            return allCombinations.ToArray();
        }

        private static bool IsBuiltInTye(Type type)
        {
            return type.Module.ScopeName == "CommonLanguageRuntimeLibrary";
        }
    }
}
