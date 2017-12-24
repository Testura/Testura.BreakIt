using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.SimpleTypes;

namespace Testura.FunkyValueTester.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class BoolCombinationTypeTests
    {
        [Test]
        public void BoolCombinationType_WhenGetCombinationForBool_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new BoolCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(false, combinations[0].Value);
            Assert.AreEqual(false, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(true, combinations[1].Value);
            Assert.AreEqual(true, combinations[1].LogValue);
        }

        [Test]
        public void BoolCombinationType_WhenGetCombinationForNullabeBool_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new NullableBoolCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(false, combinations[0].Value);
            Assert.AreEqual(false, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(true, combinations[1].Value);
            Assert.AreEqual(true, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(null, combinations[2].Value);
            Assert.AreEqual(null, combinations[2].LogValue);
        }
    }
}
