namespace Testura.BreakIt.TestValues
{
    /// <summary>
    /// Contains information about a test value.
    /// </summary>
    public class TestValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestValue"/> class.
        /// </summary>
        /// <param name="memberPath">The member path or name of parameter.</param>
        /// <param name="value">The test value.</param>
        public TestValue(string memberPath, object value)
        {
            MemberPath = memberPath;
            Value = value;
            LogValue = value;
        }

        /// <summary>
        /// Gets or sets the member path or parameter name.
        /// </summary>
        public string MemberPath { get; set; }

        /// <summary>
        /// Gets or sets the test value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the value used when logging.
        /// </summary>
        public object LogValue { get; set; }
    }
}
