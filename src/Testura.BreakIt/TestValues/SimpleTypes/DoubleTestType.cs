namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class DoubleTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, double.MaxValue), new TestValue(memberPath, double.MinValue) };
        }
    }
}
