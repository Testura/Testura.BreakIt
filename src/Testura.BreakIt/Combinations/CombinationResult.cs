using System;

namespace Testura.BreakIt.Combinations
{
    public class CombinationResult
    {
        public string MemberPath { get; set; }

        public Combination TestingValue { get; set; }

        public bool? IsSuccess { get; set; }

        public object ReturnValue { get; set; }

        public Exception Exception { get; set; }
    }
}
