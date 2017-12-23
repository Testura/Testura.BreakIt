using System;
using System.Collections.Generic;

namespace Testura.ApiTester
{
    public class ApiTesterOptions
    {
        public ApiTesterOptions()
        {
            ExcludeList = new List<Func<string, Type, bool>>();
        }

        internal IList<Func<string, Type, bool>> ExcludeList { get; }

        internal Func<object, Exception, bool> Validation { get; private set; }

        public void AddExclude(Func<string, Type, bool> exclude)
        {
            ExcludeList.Add(exclude);
        }

        public void ReturnValidation(Func<object, Exception, bool> validation)
        {
            Validation = validation;
        }
    }
}
