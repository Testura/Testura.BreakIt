using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.SimpleTypes;

namespace Testura.FunkyValueTester.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class FloatCombinationTypeTests
    {
        [Test]
        public void FloatCombinationType_WhenGetCombinationForFloat_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new FloatCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(float.MaxValue, combinations[0].Value);
            Assert.AreEqual(float.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(float.MinValue, combinations[1].Value);
            Assert.AreEqual(float.MinValue, combinations[1].LogValue);
        }

        [Test]
        public void FloatCombinationType_WhenGetCombinationForNullabeFloat_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new NullableFloatCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(float.MaxValue, combinations[0].Value);
            Assert.AreEqual(float.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(float.MinValue, combinations[1].Value);
            Assert.AreEqual(float.MinValue, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(null, combinations[2].Value);
            Assert.AreEqual(null, combinations[2].LogValue);
        }
    }
}
