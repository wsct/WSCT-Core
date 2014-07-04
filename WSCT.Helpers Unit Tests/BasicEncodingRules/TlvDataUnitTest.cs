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

            Assert.AreEqual(0x88, tlv.Tag);
            Assert.AreEqual(0, tlv.InnerTlvs.Count);
            Assert.AreEqual(data, tlv.ToByteArray().ToHexa());
        }

        [Test]
        public void ConstructorShortTlvConstructed()
        {
            const string data = "70 07 88 01 0F 81 02 53 52";

            var tlv = new TlvData(data);

            Assert.AreEqual(0x70, tlv.Tag);
            Assert.AreEqual(2, tlv.InnerTlvs.Count);
            Assert.AreEqual(data, tlv.ToByteArray().ToHexa());
        }

        [Test]
        public void ConstructorLongTlv()
        {
            var data = "88 81 80 " + new Byte[0x80].ToHexa();

            var tlv = new TlvData(data);

            Assert.AreEqual(0x88, tlv.Tag);
            Assert.AreEqual(0x80, tlv.Length);
            Assert.AreEqual(0, tlv.InnerTlvs.Count);
            Assert.AreEqual(data, tlv.ToByteArray().ToHexa());
            Assert.AreEqual("T:88 L:8180 V:" + new Byte[0x80].ToHexa(), String.Format("{0}", tlv));
        }

        [Test]
        public void ConstructorLongTlvConstructed()
        {
            var data = "70 81 89 88 81 82 " + new Byte[0x82].ToHexa() + " 81 02 53 52";

            var tlv = new TlvData(data);

            Assert.AreEqual(0x70, tlv.Tag);
            Assert.AreEqual(0x89, tlv.Length);
            Assert.AreEqual(2, tlv.InnerTlvs.Count);
            Assert.AreEqual(0x82, tlv.InnerTlvs[0].Length);
            Assert.AreEqual(data, tlv.ToByteArray().ToHexa());
        }
    }
}
