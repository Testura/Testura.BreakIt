using System;
using System.Collections.Generic;

namespace Testura.BreakIt.TestValues
{
    public interface ITestValueFactory
    {
        TestValue[] GetTestValues(string name, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList = null);
    }
}