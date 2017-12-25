using NUnit.Framework;
using Testura.BreakIt.Combinations.ComplexTypes;

namespace Testura.BreakIt.Tests.Combinations.ComplexTypes
{
    [TestFixture]
    public class EnumCombinationTypeTests
    {
        private enum TestEnum { FirstTest, SecondTest };

        [Test]
        public void EnumCombinationType_WhenGetCombinations_ShouldReturnCorrectCombinations()
        {
            var combinationTypes = new EnumCombinationType();
            var combinations = combinationTypes.GetCombinations("test", typeof(TestEnum), TestEnum.FirstTest, null, null);
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(TestEnum.FirstTest, combinations[0].Value);
            Assert.AreEqual(TestEnum.FirstTest, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(TestEnum.SecondTest, combinations[1].Value);
            Assert.AreEqual(TestEnum.SecondTest, combinations[1].LogValue);
        }

        [Test]
        public void EnumCombinationType_WhenGetCombinationsForNullableEnum_ShouldReturnCorrectCombinations()
        {
            var combinationTypes = new EnumCombinationType();
            var combinations = combinationTypes.GetCombinations("test", typeof(TestEnum?), TestEnum.FirstTest, null, null);
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
