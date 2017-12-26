namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class FloatTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, float.MaxValue), new TestValue(memberPath, float.MinValue) };
        }
    }
}
