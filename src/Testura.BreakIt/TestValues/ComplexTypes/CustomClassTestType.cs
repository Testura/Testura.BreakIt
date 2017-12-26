using System;
using System.Collections.Generic;
using Force.DeepCloner;

namespace Testura.BreakIt.TestValues.ComplexTypes
{
    internal class CustomClassTestType : IComplexTestType
    {
        public TestValue[] GetTestValues(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ITestValueFactory combinationFactory)
        {
            if (IsBuiltInType(type))
            {
                throw new Exception($"Failed to create combination for memberPath = {memberPath}, type = {type}");
            }

            if (defaultValue == null)
            {
                return new TestValue[0];
            }

            var allCombinations = new List<TestValue>();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var clone = defaultValue.DeepClone();
                var combinations = combinationFactory.GetTestValues($"{memberPath}.{propertyInfo.Name}", propertyInfo.PropertyType, propertyInfo.GetValue(clone), excludeList);
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
