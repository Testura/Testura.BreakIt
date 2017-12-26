using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.Tests.Help;
using Testura.BreakIt.TestValues.TestValueLoggers;

namespace Testura.BreakIt.Tests
{
    [TestFixture]
    public class BreakItTests : BreakItBase
    {
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
            var results = FunkyApiTester.Execute(myApi, nameof(myApi.CallApiEnums), new List<object> { MyApi.SomeEnum.Hello, MyApi.SomeEnum.bu });

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
            var memoryLogger = new MemoryTestValueLogger();
            var options = new TesterOptions();
            options.Validation = ((testValue, o1, exception) => (int)o1 == 1);

            var apiTester = new BreakIt(memoryLogger);
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" }, options);

            Assert.IsFalse(result[0].IsSuccess);
            Assert.IsTrue(result[1].IsSuccess);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatReturnValue_ShouldBeAbleToValidateAndHaveCorrectValueInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryTestValueLogger();
            var options = new TesterOptions();
            options.Validation = (testValue, o1, exception) => (int)o1 == 1;

            var apiTester = new BreakIt(memoryLogger);
            apiTester.Execute(myApi, nameof(myApi.CallApiWithValidation), new List<object> { 1, "someName" }, options);

            StringAssert.Contains("NOT OK", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInLog()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryTestValueLogger();
            var apiTester = new BreakIt(memoryLogger);
            apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            StringAssert.Contains("Something is wrong", memoryLogger.LogLines[0]);
        }

        [Test]
        public void Execute_WhenExecuteApiTesterWithMethodThatShowException_ShouldSeeExceptionInResult()
        {
            var myApi = new MyApi();
            var memoryLogger = new MemoryTestValueLogger();
            var apiTester = new BreakIt(memoryLogger);
            var result = apiTester.Execute(myApi, nameof(myApi.CallApiWithException), new List<object> { 1, "someName" });

            Assert.IsNotNull(result[0].Exception);
            Assert.AreEqual("Something is wrong", result[0].Exception.Message);
        }
    }
}
