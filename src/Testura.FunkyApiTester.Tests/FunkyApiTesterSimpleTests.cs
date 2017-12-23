using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.FunkyApiTester.Combinations.Loggers;

namespace Testura.FunkyApiTester.Tests
{
    [TestFixture]
    public class FunkyApiTesterSimpleTests : FunkyApiTesterBase
    {
        private enum SomeEnum { Hello, bu };

        [Test]
        public void Execute_WhenExecuteApiTesterWithSimpleMethod_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var results = FunkyApiTester.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "someName" });

            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithSimpleMethodWithNullable_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var results = FunkyApiTester.Execute(myApi, nameof(myApi.CallApiEnums), new List<object> { SomeEnum.Hello, SomeEnum.bu });

            Assert.AreEqual(5, results.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithSimpleMethod_ShouldGetCorrectNumberOfLogLine()
        {
            var myApi = new MyApi();
            FunkyApiTester.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "someName" });

            Assert.AreEqual(4, LogLines.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodWithList_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var result = FunkyApiTester.Execute(myApi, nameof(myApi.CallApiList), new List<object> { new List<string> { "Buu" } });

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodWithListThatContainsComplexClass_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var result = FunkyApiTester.Execute(myApi, nameof(myApi.CallApiListComplex), new List<object> { new List<SomeComplexType> { new SomeComplexType { Hej = "test", Number = 1 } } });

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodWithDictionaryThatContainsComplexClass_ShouldGetCorrectNumberOfResults()
        {
            var myApi = new MyApi();
            var result = FunkyApiTester.Execute(myApi, nameof(myApi.CallApiDictionaryComplex), new List<object> { new Dictionary<string, SomeComplexType> { ["Bu"] = new SomeComplexType { Hej = "test", Number = 1 }, ["Tjo"] = new SomeComplexType { Hej = "test", Number = 1 } } });

            Assert.AreEqual(8, result.Count);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatReturnValue_ShouldBeAbleToValidateAndHaveCorrectValueInResult()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var options = new TesterOptions();
            options.SetReturnValidation((o1, exception) => (int)o1 == 1);

            var apiTester = new Testura.FunkyApiTester.FunkyApiTester(memoryLogger);
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" }, options);

            Assert.IsFalse(result[0].ResultOk);
            Assert.IsTrue(result[1].ResultOk);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatReturnValue_ShouldBeAbleToValidateAndHaveCorrectValueInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var options = new TesterOptions();
            options.SetReturnValidation((o1, exception) => (int)o1 == 1);

            var apiTester = new Testura.FunkyApiTester.FunkyApiTester(memoryLogger);
            apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" }, options);

            StringAssert.Contains("NOT OK", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new Testura.FunkyApiTester.FunkyApiTester(memoryLogger);
            apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            StringAssert.Contains("Something is wrong", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInResult()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryCombinationLogger();
            var apiTester = new Testura.FunkyApiTester.FunkyApiTester(memoryLogger);
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            Assert.IsNotNull(result[0].Exception);
            Assert.AreEqual("Something is wrong", result[0].Exception.Message);
        }

        private class MyApi
        {
            public void CallApi(int id, string name) { }

            public void CallApiEnums(SomeEnum some, SomeEnum? someNullable) { }

            public void CallApiList(List<string> hej) { }

            public void CallApiListComplex(List<SomeComplexType> list) { }

            public void CallApiDictionaryComplex(Dictionary<string, SomeComplexType> list) { }

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

        private class SomeComplexType
        {
            public string Hej { get; set; }
            public int Number { get; set; }
        }
    }
}
