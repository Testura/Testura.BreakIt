using NUnit.Framework;
using Testura.ApiTester.Combinations;

namespace Testura.ApiTester.Tests
{
    [TestFixture]
    public class CombinationFactoryTests
    {
        private ICombinationFactory _combinationFactory;

        [SetUp]
        public void SetUp()
        {
            _combinationFactory = new CombinationFactory(new CombinationFactoryOptions());
        }

        [Test]
        public void GetCombination_WhenGetCombinationForString_ShouldReturnCorrectCombinations()
        {
            var combinations = _combinationFactory.GetCombinations("test", typeof(string), "s");
            Assert.AreEqual(2, combinations.Length);
            Assert.AreEqual(string.Empty, combinations[0].Value);
            Assert.AreEqual(null, combinations[1].Value);
        }

        [Test]
        public void GetCombination_WhenGetCombinationForTypeThatsInExcludeList_ShouldReturnEmptyArray()
        {
            var combinationFactory = new CombinationFactory(new CombinationFactoryOptions().Exclude(((s, type) => type == typeof(string))));
            var combinations = combinationFactory.GetCombinations("test", typeof(string), "s");
            Assert.AreEqual(0, combinations.Length);
        }
    }
}