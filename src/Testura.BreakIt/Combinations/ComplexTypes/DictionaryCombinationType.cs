using System;
using System.Collections;
using System.Collections.Generic;
using Force.DeepCloner;
using Testura.BreakIt.Extensions;

namespace Testura.BreakIt.Combinations.ComplexTypes
{
    internal class DictionaryCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if (!type.IsDictionary() || defaultValue == null)
            {
                return null;
            }

            var combinations = new List<Combination>();
            var dictionaryClone = defaultValue.DeepClone() as IDictionary;
            foreach (var key in dictionaryClone.Keys)
            {
                var combination = combinationFactory.GetCombinations($"{memberPath}[{key}]", dictionaryClone[key].GetType(), dictionaryClone[key], excludeList);
                foreach (var combination1 in combination)
                {
                    var newDic = dictionaryClone.DeepClone();
                    newDic[key] = combination1.Value;
                    combination1.Value = newDic;
                    combinations.Add(combination1);
                }
            }

            return combinations.ToArray();
        }
    }
}
