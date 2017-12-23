using System;
using System.Collections.Generic;
using Force.DeepCloner;

namespace Testura.ApiTester.Combinations.ComplexTypes
{
    internal class CustomClassCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if (IsBuiltInType(type))
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
                var combinations = combinationFactory.GetCombinations($"{name}.{propertyInfo.Name}", propertyInfo.PropertyType, propertyInfo.GetValue(clone), excludeList);
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

        private bool IsBuiltInType(Type type)
        {
            return type.Module.ScopeName == "CommonLanguageRuntimeLibrary";
        }
    }
}
