using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.SimpleTypes;

namespace Testura.FunkyValueTester.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class DoubleCombinationTypeTests
    {
        [Test]
        public void GetCombination_WhenGetCobinationForDouble_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new DoubleCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(double.MaxValue, combinations[0].Value);
            Assert.AreEqual(double.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(double.MinValue, combinations[1].Value);
            Assert.AreEqual(double.MinValue, combinations[1].LogValue);
        }

        [Test]
        public void GetCombination_WhenGetCobinationForNullabeDouble_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new NullableDoubleCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(double.MaxValue, combinations[0].Value);
            Assert.AreEqual(double.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(double.MinValue, combinations[1].Value);
            Assert.AreEqual(double.MinValue, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(null, combinations[2].Value);
            Assert.AreEqual(null, combinations[2].LogValue);
        }
    }
}
