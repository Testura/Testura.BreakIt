using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.SimpleTypes;

namespace Testura.FunkyValueTester.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class StringCombinationTypeTests
    {
        [Test]
        public void GetCombination_WhenGetCobinationForString_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new StringCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(string.Empty, combinations[0].Value);
            Assert.AreEqual(string.Empty, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(null, combinations[1].Value);
            Assert.AreEqual(null, combinations[1].LogValue);
        }
    }
}
