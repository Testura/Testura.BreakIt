﻿namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public class DecimalTestType : ISimpleTestType
    {
        public TestValue[] GetTestValue(string memberPath)
        {
            return new[] { new TestValue(memberPath, decimal.MaxValue), new TestValue(memberPath, decimal.MinValue) };
        }
    }
}
