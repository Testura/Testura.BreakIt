namespace Testura.ApiTester.DefaultValues
{
    internal class DefaultValue
    {
        public DefaultValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
