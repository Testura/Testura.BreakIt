﻿namespace Testura.BreakIt.TestValues.SimpleTypes
{
    internal class NullableFloatTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, float.MaxValue), new TestValue(memberPath, float.MinValue), new TestValue(memberPath, null) };
        }
    }
}
