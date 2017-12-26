using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.TestValues;
using Testura.BreakIt.TestValues.ComplexTypes;

namespace Testura.BreakIt.Tests.TestValues.ComplexTypes
{
    [TestFixture]
    public class DictionaryTestTypeTests
    {
        [Test]
        public void DictionaryTestType_WhenGetCombinationForDictionary_ShouldReturnCorrectCombination()
        {
            var dictionaryTestType = new DictionaryTestType();
            var dictionary = new Dictionary<string, int>
            {
                ["test1"] = 10
            };

            var result = dictionaryTestType.GetTestValues("test", dictionary.GetType(), dictionary, null, new TestValueFactory());

            Assert.AreEqual(2, result.Length);

            Assert.AreEqual("test[test1]", result[0].MemberPath);
            Assert.AreEqual(typeof(Dictionary<string, int>), result[0].Value.GetType());
            Assert.AreEqual(int.MaxValue, result[0].LogValue);

            Assert.AreEqual("test[test1]", result[1].MemberPath);
            Assert.AreEqual(typeof(Dictionary<string, int>), result[1].Value.GetType());
            Assert.AreEqual(int.MinValue, result[1].LogValue);
        }

        [Test]
        public void DictionaryTestType_WhenGetCombinationForIDictionary_ShouldReturnCorrectCombination()
        {
            var dictionaryTestType = new DictionaryTestType();
            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                ["test1"] = 10
            };

            var result = dictionaryTestType.GetTestValues("test", dictionary.GetType(), dictionary, null, new TestValueFactory());

            Assert.AreEqual(2, result.Length);
        }

        [Test]
        public void DictionaryTestType_WhenGetCombinationForDictionaryThatHaveMultipleKeys_ShouldReturnCorrectKey()
        {
            var dictionaryTestType = new DictionaryTestType();
            var dictionary = new Dictionary<string, int>
            {
                ["test1"] = 10,
                ["test2"] = 10,
                ["test3"] = 10
            };

            var result = dictionaryTestType.GetTestValues("test", dictionary.GetType(), dictionary, null, new TestValueFactory());

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual("test[test1]", result[0].MemberPath);
            Assert.AreEqual("test[test2]", result[2].MemberPath);
            Assert.AreEqual("test[test3]", result[4].MemberPath);
        }
    }
}
