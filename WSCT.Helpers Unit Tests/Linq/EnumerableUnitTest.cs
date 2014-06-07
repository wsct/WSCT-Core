using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WSCT.Helpers.Linq
{
    [TestFixture]
    class EnumerableUnitTest
    {
        readonly List<int> source = new List<int> { 1, 2, 3, 4, 5 };

        [Test]
        public void Following()
        {
            Assert.AreEqual(2, source.Following(i => i == 1));
            Assert.AreEqual(5, source.Following(i => i == 4));
            Assert.Throws<InvalidOperationException>(() => source.Following(i => i == 5));
            Assert.Throws<InvalidOperationException>(() => source.Following(i => i == 6));
        }

        [Test]
        public void Preceding()
        {
            Assert.AreEqual(1, source.Preceding(i => i == 2));
            Assert.AreEqual(4, source.Preceding(i => i == 5));
            Assert.Throws<InvalidOperationException>(() => source.Preceding(i => i == 1));
            Assert.Throws<InvalidOperationException>(() => source.Preceding(i => i == 6));
        }
    }
}
