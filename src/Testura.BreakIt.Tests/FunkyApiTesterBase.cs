using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.TestValues.TestValueLoggers;

namespace Testura.BreakIt.Tests
{
    [TestFixture]
    public abstract class FunkyApiTesterBase
    {
        protected MemoryTestValueLogger MemoryCombinationLogger { get; private set; }

        protected BreakIt FunkyApiTester { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryTestValueLogger();
            FunkyApiTester = new BreakIt(combinationLogger: MemoryCombinationLogger);
        }
    }
}
