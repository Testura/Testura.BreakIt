using System;

namespace Testura.BreakIt.TestValues
{
    public class TestValueResult
    {
        public string MemberPath { get; set; }

        public TestValue TestingValue { get; set; }

        public bool? IsSuccess { get; set; }

        public object ReturnValue { get; set; }

        public Exception Exception { get; set; }
    }
}
