using System;
using System.Collections.Generic;

namespace Testura.BreakIt.TestValues.ComplexTypes
{
    internal interface IComplexTestType
    {
        TestValue[] GetTestValues(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList, ITestValueFactory combinationFactory);
    }
}
