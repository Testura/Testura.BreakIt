using System;
using System.Collections;
using System.Collections.Generic;
using Force.DeepCloner;
using Testura.ApiTester.Extensions;

namespace Testura.ApiTester.Combinations.ComplexTypes
{
    internal class DictionaryCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if (!type.IsDictionary() || defaultValue == null)
            {
                return null;
            }

            var combinations = new List<Combination>();
            var dictionaryClone = defaultValue.DeepClone() as IDictionary;
            foreach (var key in dictionaryClone.Keys)
            {
                combinations.AddRange(combinationFactory.GetCombinations($"{name}.{type.ConvertToReadableType()}[{key}]", dictionaryClone[key].GetType(), dictionaryClone[key], excludeList));
            }

            return combinations.ToArray();
        }
    }
}
