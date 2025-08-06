using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WSCT.Helpers.Linq
{
    [TestFixture]
    internal class EnumerableUnitTest
    {
        private readonly List<int> source = new List<int> { 1, 2, 3, 4, 5 };

        [Test]
        public void Following()
        {
            Assert.That(source.Following(i => i == 1), Is.EqualTo(2));
            Assert.That(source.Following(i => i == 4), Is.EqualTo(5));
            Assert.Throws<InvalidOperationException>(() => source.Following(i => i == 5));
            Assert.Throws<InvalidOperationException>(() => source.Following(i => i == 6));
        }

        [Test]
        public void Preceding()
        {
            Assert.That(source.Preceding(i => i == 2), Is.EqualTo(1));
            Assert.That(source.Preceding(i => i == 5), Is.EqualTo(4));
            Assert.Throws<InvalidOperationException>(() => source.Preceding(i => i == 1));
            Assert.Throws<InvalidOperationException>(() => source.Preceding(i => i == 6));
        }
    }
}