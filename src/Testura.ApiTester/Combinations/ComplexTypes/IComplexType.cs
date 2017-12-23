using System;
using System.Collections.Generic;

namespace Testura.ApiTester.Combinations.ComplexTypes
{
    internal interface IComplexType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ICombinationFactory combinationFactory);
    }
}
