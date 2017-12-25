namespace Testura.BreakIt.Combinations
{
    public class Combination
    {
        public Combination(string memberPath, object value)
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
