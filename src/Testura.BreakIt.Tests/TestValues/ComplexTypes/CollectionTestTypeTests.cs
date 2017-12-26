using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.TestValues;
using Testura.BreakIt.TestValues.ComplexTypes;

namespace Testura.BreakIt.Tests.TestValues.ComplexTypes
{
    [TestFixture]
    public class CollectionTestTypeTests
    {
        public static IEnumerable CollectionDataSets
        {
            get
            {
                yield return new TestCaseData(new List<string> {"testData"}, typeof(List<string>))
                    .SetName("CollectionTestType_WhenGetTestValuesForAList_ShouldGetCorrectCombination");

                yield return new TestCaseData(new string[] { "testData" }, typeof(string[]))
                    .SetName("CollectionTestType_WhenGetTestValuesForAnArray_ShouldGetCorrectCombination");
            }
        }

        [TestCaseSource(nameof(CollectionDataSets))]
        public void Combinations(IList list, Type expectedType)
        {
            var collectionTestType = new CollectionTestType();
            var result = collectionTestType.GetTestValues("test", list.GetType(), list, null, new TestValueFactory());

            Assert.AreEqual(2, result.Length);

            Assert.AreEqual("test[0]", result[0].MemberPath);
            Assert.AreEqual(expectedType, result[0].Value.GetType());
            Assert.AreEqual(1, ((IList) result[0].Value).Count);
            Assert.AreEqual(string.Empty, result[0].LogValue);

            Assert.AreEqual("test[0]", result[1].MemberPath);
            Assert.AreEqual(expectedType, result[1].Value.GetType());
            Assert.AreEqual(1, ((IList)result[1].Value).Count);
            Assert.AreEqual(null, result[1].LogValue);
        }

        [Test]
        public void CollectionTestType_WhenHavingMultipleDefaultItemsInList_ShouldHaveCorrectIndexInMememberPath()
        {
            var collectionTestType = new CollectionTestType();
            var list = new List<string>
            {
                "Test1",
                "Test2",
                "Test3"
            };

            var result = collectionTestType.GetTestValues("test", list.GetType(), list, null, new TestValueFactory());
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual("test[0]", result[0].MemberPath);
            Assert.AreEqual("test[1]", result[2].MemberPath);
            Assert.AreEqual("test[2]", result[4].MemberPath);
        }
    }
}
