using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.ApiTester.Combinations.Loggers;

namespace Testura.ApiTester.Tests
{
    [TestFixture]
    public class ApiTesterSimpleTests : ApiTesterBase
    {
        [Test]
        public void Execute_WhenExecuteApiTesterWithSimpleMethod_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var results = ApiTester.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "someName" });

            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithSimpleMethod_ShouldGetCorrectNumberOfLogLine()
        {
            var myApi = new MyApi();
            ApiTester.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "someName" });

            Assert.AreEqual(4, LogLines.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatReturnValue_ShouldBeAbleToValidateAndHaveCorrectValueInResult()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new ApiTester(memoryLogger, (o) => o.ReturnValidation((o1, exception) => (int)o1 == 1));
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" });

            Assert.IsFalse(result[0].IsValidationOk);
            Assert.IsTrue(result[1].IsValidationOk);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatReturnValue_ShouldBeAbleToValidateAndHaveCorrectValueInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new ApiTester(memoryLogger, (o) => o.ReturnValidation((o1, exception) => (int)o1 == 1));
            apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" });

            StringAssert.Contains("NOT OK", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new ApiTester(memoryLogger);
            apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            StringAssert.Contains("Something is wrong", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInResult()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new ApiTester(memoryLogger);
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            Assert.IsNotNull(result[0].Exception);
            Assert.AreEqual("Something is wrong", result[0].Exception.Message);
        }

        private class MyApi
        {
            public void CallApi(int id, string name) { }

            public int CallApiWithValidation(int id, string name)
            {
                if (id == int.MaxValue)
                {
                    return -1;
                }

                return 1;
            }

            public int CallApiWithException(int id, string name)
            {
                if (id == int.MaxValue)
                {
                    throw new Exception("Something is wrong");
                }

                return 1;
            }
        }
    }
}
