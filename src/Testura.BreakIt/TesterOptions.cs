using System;
using System.Collections.Generic;
using Testura.BreakIt.TestValues;

namespace Testura.BreakIt
{
    public class TesterOptions
    {
        public TesterOptions()
        {
            ExcludeList = new List<Func<string, Type, bool>>();
        }

        public Func<TestValue, object, Exception, bool> Validation { get; set; }

        public Action<object[]> SetUp { get; set; }

        internal IList<Func<string, Type, bool>> ExcludeList { get; }

        public void AddExclude(Func<string, Type, bool> exclude)
        {
            ExcludeList.Add(exclude);
        }
    }
}
