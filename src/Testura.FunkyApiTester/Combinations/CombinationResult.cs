using System;

namespace Testura.FunkyApiTester.Combinations
{
    public class CombinationResult
    {
        public string Name { get; set; }

        public Combination TestingValue { get; set; }

        public bool? ResultOk { get; set; }

        public object ReturnValue { get; set; }

        public Exception Exception { get; set; }
    }
}
