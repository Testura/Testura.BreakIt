namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class NullableDoubleTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, double.MaxValue), new TestValue(memberPath, double.MinValue), new TestValue(memberPath, null) };
        }
    }
}
