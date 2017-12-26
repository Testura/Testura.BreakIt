namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class NullableBoolTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, false), new TestValue(memberPath, true), new TestValue(memberPath, null) };
        }
    }
}
