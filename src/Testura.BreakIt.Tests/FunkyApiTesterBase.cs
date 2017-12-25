using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.Combinations.CombinationLoggers;

namespace Testura.BreakIt.Tests
{
    [TestFixture]
    public abstract class FunkyApiTesterBase
    {
        protected MemoryCombinationLogger MemoryCombinationLogger { get; private set; }

        protected BreakIt FunkyApiTester { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryCombinationLogger();
            FunkyApiTester = new BreakIt(combinationLogger: MemoryCombinationLogger);
        }
    }
}
