using System.Collections.Generic;
using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.CombinationLoggers;

namespace Testura.FunkyValueTester.Tests
{
    [TestFixture]
    public abstract class FunkyApiTesterBase
    {
        protected MemoryCombinationLogger MemoryCombinationLogger { get; private set; }

        protected Testura.FunkyValueTester.FunkyValueTester FunkyApiTester { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryCombinationLogger();
            FunkyApiTester = new Testura.FunkyValueTester.FunkyValueTester(combinationLogger: MemoryCombinationLogger);
        }
    }
}
