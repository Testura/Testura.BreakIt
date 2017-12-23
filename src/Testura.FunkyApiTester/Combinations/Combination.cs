namespace Testura.FunkyApiTester.Combinations
{
    public class Combination
    {
        public Combination(string name, object value)
        {
            Name = name;
            Value = value;
            LogValue = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }

        public object LogValue { get; set; }
    }
}
