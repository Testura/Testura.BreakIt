﻿using System;
using NUnit.Framework;
using Testura.FunkyValueTester.Combinations.SimpleTypes;

namespace Testura.FunkyValueTester.Tests.Combinations.SimpleTypes
{
    [TestFixture]
    public class DecimalCombinationTypeTests
    {
        [Test]
        public void GetCombination_WhenGetCobinationForDecimal_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new DecimalCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(2, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(decimal.MaxValue, combinations[0].Value);
            Assert.AreEqual(decimal.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(decimal.MinValue, combinations[1].Value);
            Assert.AreEqual(decimal.MinValue, combinations[1].LogValue);
        }

        [Test]
        public void GetCombination_WhenGetCobinationForNullabeDecimal_ShouldGetCorrectCombinationBack()
        {
            var combinationType = new NullableDecimalCombinationType();
            var combinations = combinationType.GetCombinations("test");
            Assert.AreEqual(3, combinations.Length);

            Assert.AreEqual("test", combinations[0].MemberPath);
            Assert.AreEqual(decimal.MaxValue, combinations[0].Value);
            Assert.AreEqual(decimal.MaxValue, combinations[0].LogValue);

            Assert.AreEqual("test", combinations[1].MemberPath);
            Assert.AreEqual(decimal.MinValue, combinations[1].Value);
            Assert.AreEqual(decimal.MinValue, combinations[1].LogValue);

            Assert.AreEqual("test", combinations[2].MemberPath);
            Assert.AreEqual(null, combinations[2].Value);
            Assert.AreEqual(null, combinations[2].LogValue);
        }
    }
}
