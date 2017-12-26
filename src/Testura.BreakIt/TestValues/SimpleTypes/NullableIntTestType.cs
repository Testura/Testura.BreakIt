namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class NullableIntTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, int.MaxValue), new TestValue(memberPath, int.MinValue), new TestValue(memberPath, null) };
        }
    }
}
