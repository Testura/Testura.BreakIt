namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class BoolTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, false), new TestValue(memberPath, true) };
        }
    }
}
