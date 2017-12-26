namespace Testura.BreakIt.TestValues
{
    public class TestValue
    {
        public TestValue(string memberPath, object value)
        {
            MemberPath = memberPath;
            Value = value;
            LogValue = value;
        }

        public string MemberPath { get; set; }

        public object Value { get; set; }

        public object LogValue { get; set; }
    }
}
