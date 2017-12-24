using System;
using System.Collections;
using System.Collections.Generic;
using Force.DeepCloner;

namespace Testura.FunkyValueTester.Combinations.ComplexTypes
{
    internal class CollectionCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if (!(defaultValue is IList))
            {
                return null;
            }

            var allCombinations = new List<Combination>();
            var listClone = defaultValue.DeepClone() as IList;
            for (int n = 0; n < listClone.Count; n++)
            {
                var item = listClone[n];
                var combinations = combinationFactory.GetCombinations($"{memberPath}[{n}]", item.GetType(), item, excludeList);
                foreach (var comb in combinations)
                {
                    var newList = listClone.DeepClone();
                    newList[n] = comb.Value;
                    comb.Value = newList;
                    allCombinations.Add(comb);
                }
            }

            return allCombinations.ToArray();
        }
    }
}
