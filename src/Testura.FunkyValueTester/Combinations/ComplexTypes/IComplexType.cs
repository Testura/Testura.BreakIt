using System;
using System.Collections.Generic;

namespace Testura.FunkyValueTester.Combinations.ComplexTypes
{
    internal interface IComplexType
    {
        Combination[] GetCombinations(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory);
    }
}
