namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class ShortTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, short.MaxValue), new TestValue(memberPath, short.MinValue) };
        }
    }
}
