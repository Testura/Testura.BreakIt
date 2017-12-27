namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class NullableBoolTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, false), new TestValue(memberPath, true), new TestValue(memberPath, null) };
        }
    }
}
