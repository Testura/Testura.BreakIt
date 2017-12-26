using NUnit.Framework;
using Testura.BreakIt.TestValues.ComplexTypes;

namespace Testura.BreakIt.Tests.TestValues.ComplexTypes
{
    [TestFixture]
    public class EnumTestTypeTests
    {
        private enum TestEnum { FirstTest, SecondTest };

        [Test]
        public void EnumTestType_WhenGetCombinations_ShouldReturnCorrectCombinations()
        {
            var enumTestType = new EnumTestType();
            var combinations = enumTestType.GetTestValues("test", typeof(TestEnum), TestEnum.FirstTest, null, null);
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(TestEnum.FirstTest, combinations[0].Value);
            Assert.AreEqual(TestEnum.FirstTest, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(TestEnum.SecondTest, combinations[1].Value);
            Assert.AreEqual(TestEnum.SecondTest, combinations[1].LogValue);
        }

        [Test]
        public void EnumTestTypeTests_WhenGetCombinationsForNullableEnum_ShouldReturnCorrectCombinations()
        {
            var enumTestType = new EnumTestType();
            var combinations = enumTestType.GetTestValues("test", typeof(TestEnum?), TestEnum.FirstTest, null, null);
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(null, combinations[0].Value);
            Assert.AreEqual(null, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(TestEnum.FirstTest, combinations[1].Value);
            Assert.AreEqual(TestEnum.FirstTest, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(TestEnum.SecondTest, combinations[2].Value);
            Assert.AreEqual(TestEnum.SecondTest, combinations[2].LogValue);
        }
    }
}
