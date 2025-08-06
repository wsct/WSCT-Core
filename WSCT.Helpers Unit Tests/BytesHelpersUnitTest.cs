using System;
using NUnit.Framework;

namespace WSCT.Helpers
{
    [TestFixture]
    public class BytesHelpersUnitTest
    {
        [Test]
        public void ShouldSetMaskedBitsTo1()
        {
            const byte source = 0x59;
            const byte mask = 0x93;

            var result = source.SetBits(mask, true);

            Assert.That(result, Is.EqualTo(0xDB));
        }

        [Test]
        public void ShouldSetMaskedBitsTo0()
        {
            const byte source = 0x59;
            const byte mask = 0x93;

            var result = source.SetBits(mask, false);

            Assert.That(result, Is.EqualTo(0x48));
        }

        [Test]
        public void ShouldConvertAByteToAByteArray()
        {
            const byte source = 0x83;

            var result = source.ToByteArray();

            Assert.That(result, Is.EqualTo(new byte[] { 0x83 }));
        }

        [Test]
        public void ShouldConvertAUintToAByteArrayOfLength1()
        {
            const uint source = 0x12345678;

            var result = source.ToByteArray(1);

            Assert.That(result, Is.EqualTo(new byte[] { 0x78 }));
        }

        [Test]
        public void ShouldConvertAUintToAByteArrayOfLength2()
        {
            const uint source = 0x12345678;

            var result = source.ToByteArray(2);

            Assert.That(result, Is.EqualTo(new byte[] { 0x56, 0x78 }));
        }

        [Test]
        public void ShouldConvertAUintToAByteArrayOfLength3()
        {
            const uint source = 0x12345678;

            var result = source.ToByteArray(3);

            Assert.That(result, Is.EqualTo(new byte[] { 0x34, 0x56, 0x78 }));
        }

        [Test]
        public void ShouldConvertAUintToAByteArrayOfLength4()
        {
            const uint source = 0x12345678;

            var result = source.ToByteArray(4);

            Assert.That(result, Is.EqualTo(new byte[] { 0x12, 0x34, 0x56, 0x78 }));
        }

        [Test]
        public void ShouldConvertAnEvenNumberOfHexadecimalCharactersToAByteArray()
        {
            const string source = "01 23 45 67";

            var result = source.FromHexa();

            Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x23, 0x45, 0x67 }));
        }

        [Test]
        public void ShouldConvertAnOddNumberOfHexadecimalCharactersToAByteArray()
        {
            const string source = "8 01 23 45 67";

            var result = source.FromHexa();

            Assert.That(result, Is.EqualTo(new byte[] { 0x08, 0x01, 0x23, 0x45, 0x67 }));
        }

        [Test]
        public void ShouldConvertAStringToAByteArrayOfAsciiCodes()
        {
            const string source = "1PAY.SYS.DDF01";

            var result = source.FromString();

            Assert.That(result, Is.EqualTo(new byte[] { 0x31, 0x50, 0x41, 0x59, 0x2E, 0x53, 0x59, 0x53, 0x2E, 0x44, 0x44, 0x46, 0x30, 0x31 }));
        }

        [Test]
        public void ShouldConvertAByteArrayOfAsciiCodesToAString()
        {
            var source = new byte[] { 0x31, 0x50, 0x41, 0x59, 0x2E, 0x53, 0x59, 0x53, 0x2E, 0x44, 0x44, 0x46, 0x30, 0x31 };

            var result = source.ToAsciiString();
            var spanResult = source.AsSpan().ToAsciiString();

            Assert.That(result, Is.EqualTo("1PAY.SYS.DDF01"));
            Assert.That(spanResult, Is.EqualTo("1PAY.SYS.DDF01"));
        }

        [Test]
        public void ShouldConvertBcdCodedByteArrayToAByteArray()
        {
            var source = new byte[] { 0x12, 0x34 };

            var result = source.FromBcd();
            var spanResult = source.AsSpan().FromBcd();

            Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03, 0x04 }));
            Assert.That(spanResult, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03, 0x04 }));
        }

        [Test]
        public void ShouldConvertABcdCodedByteArrayToAByteArrayOfLength3()
        {
            var source = new byte[] { 0x12, 0x34 };

            var result = source.FromBcd(3);
            var spanResult = source.AsSpan().FromBcd(3);

            Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03 }));
            Assert.That(spanResult, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03 }));
        }

        [Test]
        public void ShouldConvertAnOddByteArrayToABcdCodedByteArray()
        {
            var source = new byte[] { 0x01, 0x02, 0x03, 0x04 };

            var result = source.ToBcd();
            var spanResult = source.AsSpan().ToBcd();

            Assert.That(result, Is.EqualTo(new byte[] { 0x12, 0x34 }));
            Assert.That(spanResult, Is.EqualTo(new byte[] { 0x12, 0x34 }));
        }

        [Test]
        public void ShouldConvertAnEvenByteArrayToABcdCodedByteArray()
        {
            var source = new byte[] { 0x01, 0x02, 0x03 };

            var result = source.ToBcd(0xF);
            var spanResult = source.AsSpan().ToBcd(0xF);

            Assert.That(result, Is.EqualTo(new byte[] { 0x12, 0x3F }));
            Assert.That(spanResult, Is.EqualTo(new byte[] { 0x12, 0x3F }));
        }
    }
}