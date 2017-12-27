using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.TestValues.TestValueLoggers;

namespace Testura.BreakIt.Tests
{
    [TestFixture]
    public abstract class BreakItBase
    {
        protected MemoryTestValueLogger MemoryCombinationLogger { get; private set; }

        protected BreakItTester breakIt { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryTestValueLogger();
            breakIt = new BreakItTester(testValueLogger: MemoryCombinationLogger);
        }
    }
}
