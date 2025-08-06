using System;
using NUnit.Framework;

namespace WSCT.Helpers.BasicEncodingRules
{
    [TestFixture]
    public class TlvDataUnitTest
    {
        [Test]
        public void ConstructorShortTlv()
        {
            const string data = "88 01 0F";
            var tlv = new TlvData(data);

            Assert.That(tlv.Tag, Is.EqualTo(0x88));
            Assert.That(tlv.InnerTlvs.Count, Is.EqualTo(0));
            Assert.That(tlv.ToByteArray().ToHexa(), Is.EqualTo(data));
        }

        [Test]
        public void ConstructorShortTlvConstructed()
        {
            const string data = "70 07 88 01 0F 81 02 53 52";

            var tlv = new TlvData(data);

            Assert.That(tlv.Tag, Is.EqualTo(0x70));
            Assert.That(tlv.InnerTlvs.Count, Is.EqualTo(2));
            Assert.That(tlv.ToByteArray().ToHexa(), Is.EqualTo(data));
        }

        [Test]
        public void ConstructorLongTlv()
        {
            var data = "88 81 80 " + new Byte[0x80].ToHexa();

            var tlv = new TlvData(data);

            Assert.That(tlv.Tag, Is.EqualTo(0x88));
            Assert.That(tlv.Length, Is.EqualTo(0x80));
            Assert.That(tlv.InnerTlvs.Count, Is.EqualTo(0));
            Assert.That(tlv.ToByteArray().ToHexa(), Is.EqualTo(data));
            Assert.That(String.Format("{0}", tlv), Is.EqualTo("T:88 L:8180 V:" + new Byte[0x80].ToHexa()));
        }

        [Test]
        public void ConstructorLongTlvConstructed()
        {
            var data = "70 81 89 88 81 82 " + new Byte[0x82].ToHexa() + " 81 02 53 52";

            var tlv = new TlvData(data);

            Assert.That(tlv.Tag, Is.EqualTo(0x70));
            Assert.That(tlv.Length, Is.EqualTo(0x89));
            Assert.That(tlv.InnerTlvs.Count, Is.EqualTo(2));
            Assert.That(tlv.InnerTlvs[0].Length, Is.EqualTo(0x82));
            Assert.That(tlv.ToByteArray().ToHexa(), Is.EqualTo(data));
        }
    }
}
