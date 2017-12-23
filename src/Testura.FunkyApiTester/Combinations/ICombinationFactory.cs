using System;
using System.Collections.Generic;

namespace Testura.FunkyApiTester.Combinations
{
    public interface ICombinationFactory
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList = null);
    }
}