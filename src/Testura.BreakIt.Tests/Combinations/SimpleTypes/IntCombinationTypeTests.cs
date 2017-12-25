using NUnit.Framework;
using Testura.BreakIt.Combinations.SimpleTypes;

namespace Testura.BreakIt.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class IntCombinationTypeTests
    {
        [Test]
        public void IntCombinationType_WhenGetCombinationForInt_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new IntCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(int.MaxValue, combinations[0].Value);
            Assert.AreEqual(int.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(int.MinValue, combinations[1].Value);
            Assert.AreEqual(int.MinValue, combinations[1].LogValue);
        }

        [Test]
        public void IntCombinationType_WhenGetCombinationForNullabeInt_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new NullableIntCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(int.MaxValue, combinations[0].Value);
            Assert.AreEqual(int.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(int.MinValue, combinations[1].Value);
            Assert.AreEqual(int.MinValue, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(null, combinations[2].Value);
            Assert.AreEqual(null, combinations[2].LogValue);
        }
    }
}
