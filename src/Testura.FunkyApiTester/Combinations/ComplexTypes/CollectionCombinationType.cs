using System;
using System.Collections;
using System.Collections.Generic;
using Force.DeepCloner;
using Testura.FunkyApiTester.Extensions;

namespace Testura.FunkyApiTester.Combinations.ComplexTypes
{
    internal class CollectionCombinationType : IComplexType
    {
        public Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory)
        {
            if ((!type.IsCollection() && !type.IsICollection()) || defaultValue == null)
            {
                return null;
            }

            var combinations = new List<Combination>();
            var listClone = defaultValue.DeepClone();
            var enumerable = listClone as IEnumerable;
            int index = 0;
            foreach (var item in enumerable)
            {
               combinations.AddRange(combinationFactory.GetCombinations($"{name}.{type.ConvertToReadableType()}[{index}]", item.GetType(), item, excludeList));
                index++;
            }

            return combinations.ToArray();
        }
    }
}
