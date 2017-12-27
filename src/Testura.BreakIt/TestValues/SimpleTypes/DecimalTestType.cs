namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class DecimalTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, decimal.MaxValue), new TestValue(memberPath, decimal.MinValue) };
        }
    }
}
