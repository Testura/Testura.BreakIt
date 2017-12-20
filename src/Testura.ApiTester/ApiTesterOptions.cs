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

        internal List<Func<string, Type, bool>> ExcludeList { get; }

        internal Func<object, Exception, bool> Validation { get; private set; }

        public ApiTesterOptions Exclude(Func<string, Type, bool> exclude)
        {
            ExcludeList.Add(exclude);
            return this;
        }

        public ApiTesterOptions ReturnValidation(Func<object, Exception, bool> validation)
        {
            Validation = validation;
            return this;
        }
    }
}
