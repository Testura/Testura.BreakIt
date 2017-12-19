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
        public void Execute_WhenExecuteApiTesterWithSimpleMethod_ShouldGetCorrectNumberOfLogLine()
        {
            var myApi = new MyApi();
            ApiTester.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "someName" });

            Assert.AreEqual(4, LogLines.Count);
        }

        private class MyApi
        {
            public void CallApi(int id, string name) { }
        }
    }
}
