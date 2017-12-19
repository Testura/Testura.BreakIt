using System;
using System.Collections.Generic;

namespace Testura.ApiTester.Combinations
{
    public class CombinationFactoryOptions
    {
        public CombinationFactoryOptions()
        {
            ExcludeList = new List<Func<string, Type, bool>>();
        }

        internal List<Func<string, Type, bool>> ExcludeList { get; }

        public CombinationFactoryOptions Exclude(Func<string, Type, bool> exclude)
        {
            ExcludeList.Add(exclude);
            return this;
        }
    }
}
