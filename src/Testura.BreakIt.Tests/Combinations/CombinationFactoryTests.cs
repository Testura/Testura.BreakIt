using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.BreakIt.Combinations;

namespace Testura.BreakIt.Tests.Combinations
{
    [TestFixture]
    public class CombinationFactoryTests
    {
        private ICombinationFactory _combinationFactory;

        [SetUp]
        public void SetUp()
        {
            _combinationFactory = new CombinationFactory();
        }

        [Test]
        public void CombinationFactory_WhenGetCombinationForSimpleType_ShouldReturnCombinations()
        {
            var combinations = _combinationFactory.GetCombinations("test", typeof(string), "s");
            Assert.AreEqual(2, combinations.Length);
            Assert.AreEqual(string.Empty, combinations[0].Value);
            Assert.AreEqual(null, combinations[1].Value);
        }

        [Test]
        public void CombinationFactory_WhenGetCombinationForTypeThatsInExcludeList_ShouldReturnEmptyArray()
        {
            var combinationFactory = new CombinationFactory();
            var combinations = combinationFactory.GetCombinations("test", typeof(string), "s", new List<Func<string, Type, bool>> { (s, type) => type == typeof(string) });
            Assert.AreEqual(0, combinations.Length);
        }

        [Test]
        public void CombinationFactory_WhenGetCombinationForComplexType_ShouldReturnCombination()
        {
            var combinationFactory = new CombinationFactory();
            var combinations = combinationFactory.GetCombinations("test", typeof(List<string>), new List<string> { "hej "});
            Assert.AreEqual(2, combinations.Length);
        }
    }
}