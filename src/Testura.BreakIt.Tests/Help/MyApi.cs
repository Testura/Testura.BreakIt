using System;
using System.Collections.Generic;

namespace Testura.BreakIt.Tests.Help
{
    public class MyApi
    {
        public enum SomeEnum { FirstValue, SecondValue };

        public void CallApi(int id, string name) { }

        public void CallApiEnums(SomeEnum some, SomeEnum? someNullable) { }

        public void CallApiList(List<string> hej) { }

        public void CallApiListComplex(List<SomeComplexType> list) { }

        public void CallApiDictionaryComplex(Dictionary<string, SomeComplexType> list) { }


        public int CallApiWithValidation(int id, string name)
        {
            if (id == int.MaxValue)
            {
                return -1;
            }

            return 1;
        }

        public int CallApiWithException(int id, string name)
        {
            if (id == int.MaxValue)
            {
                throw new Exception("Something is wrong");
            }

            return 1;
        }
    }
}
