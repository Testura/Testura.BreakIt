namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class StringTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, string.Empty), new TestValue(memberPath, null) };
        }
    }
}
