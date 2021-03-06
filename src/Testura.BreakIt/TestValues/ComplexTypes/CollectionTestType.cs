﻿using System;
using System.Collections;
using System.Collections.Generic;
using Force.DeepCloner;

namespace Testura.BreakIt.TestValues.ComplexTypes
{
    internal class CollectionTestType : IComplexTestType
    {
        public TestValue[] GetTestValues(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ITestValueFactory combinationFactory)
        {
            if (!(defaultValue is IList))
            {
                return null;
            }

            var allCombinations = new List<TestValue>();
            var listClone = defaultValue.DeepClone() as IList;
            for (int n = 0; n < listClone.Count; n++)
            {
                var item = listClone[n];
                var combinations = combinationFactory.GetTestValues($"{memberPath}[{n}]", item.GetType(), item, excludeList);
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
