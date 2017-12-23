using System.Collections.Generic;
using NUnit.Framework;
using Testura.FunkyApiTester.Combinations.Loggers;

namespace Testura.FunkyApiTester.Tests
{
    [TestFixture]
    public abstract class FunkyApiTesterBase
    {
        protected MemoryCombinationLogger MemoryCombinationLogger { get; private set; }

        protected Testura.FunkyApiTester.FunkyApiTester FunkyApiTester { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryCombinationLogger();
            FunkyApiTester = new Testura.FunkyApiTester.FunkyApiTester(combinationLogger: MemoryCombinationLogger);
        }
    }
}
