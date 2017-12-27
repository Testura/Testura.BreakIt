namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class DoubleTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, double.MaxValue), new TestValue(memberPath, double.MinValue) };
        }
    }
}
