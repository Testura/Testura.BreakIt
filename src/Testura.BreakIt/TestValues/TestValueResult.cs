using System;

namespace Testura.BreakIt.TestValues
{
    /// <summary>
    /// Contains the results from a method invocation with a specific test value.
    /// </summary>
    public class TestValueResult
    {
        /// <summary>
        /// Gets or sets the member path or parameter name tested.
        /// </summary>
        public string MemberPath { get; set; }

        /// <summary>
        /// Gets or sets the testing value used.
        /// </summary>
        public TestValue TestingValue { get; set; }

        /// <summary>
        /// Gets or sets if the value was a success (null if no validation was provided).
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the return value from method invocation.
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Gets or sets the exception after method invocation.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
