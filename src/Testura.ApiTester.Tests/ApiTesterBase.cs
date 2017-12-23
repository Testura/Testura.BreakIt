using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Testura.ApiTester.Combinations.Loggers;

namespace Testura.ApiTester.Tests
{
    [TestFixture]
    public abstract class ApiTesterBase
    {
        protected MemoryCombinationLogger MemoryCombinationLogger { get; private set; }

        protected ApiTester ApiTester { get; private set; }

        protected List<string> LogLines => MemoryCombinationLogger.LogLines;

        [SetUp]
        public void SetUp()
        {
            MemoryCombinationLogger = new MemoryCombinationLogger();
            ApiTester = new ApiTester(combinationLogger: MemoryCombinationLogger);
        }
    }
}
