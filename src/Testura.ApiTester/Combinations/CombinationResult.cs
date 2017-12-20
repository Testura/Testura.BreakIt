using System;

namespace Testura.ApiTester.Combinations
{
    public class CombinationResult
    {
        public string Name { get; set; }

        public Combination TestingValue { get; set; }

        public bool? IsValidationOk { get; set; }

        public object ReturnValue { get; set; }

        public Exception Exception { get; set; }
    }
}
