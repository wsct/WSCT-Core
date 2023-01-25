using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;

namespace WSCT.ISO7816
{
    [TestFixture]
    public class CommandCaseUnitTest
    {
        private static string XmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;

        // Case 1
        private const string Case1Apdu = "00A4 0400";
        private static readonly byte[] Case1ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00 };
        private static readonly string Case1Xml = $"{XmlHeader}<commandAPDU cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\" />";

        // Case 2
        private const string Case2Apdu = "00A4 0400 F0";
        private static readonly byte[] Case2ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0xF0 };
        private static readonly string Case2Xml = $"{XmlHeader}<commandAPDU cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\" le=\"F0\" />";

        // Case 3
        private const string Case3Apdu = "00A4 0400 04 0A0B0C0D";
        private static readonly byte[] Case3ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D };
        private static readonly string Case3Xml = $"{XmlHeader}<commandAPDU cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\">0A 0B 0C 0D</commandAPDU>";

        // Case 4
        private const string Case4Apdu = "00A4 0400 04 0A0B0C0D F0";
        private static readonly byte[] Case4ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D, 0xF0 };
        private static readonly string Case4Xml = $"{XmlHeader}<commandAPDU cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\" le=\"F0\">0A 0B 0C 0D</commandAPDU>";

        // Case 2E
        private const string Case2EApdu = "00A4 0400 00F00D";
        private static readonly byte[] Case2EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0xF0, 0x0D };
        private static readonly string Case2EXml = $"{XmlHeader}<commandAPDU extended=\"true\" cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\" le=\"F00D\" />";

        // Case 3E
        private const string Case3EApdu = "00A4 0400 000004 0A0B0C0D";
        private static readonly byte[] Case3EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D };
        private static readonly string Case3EXml = $"{XmlHeader}<commandAPDU extended=\"true\" cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\">0A 0B 0C 0D</commandAPDU>";

        // Case 4E
        private const string Case4EApdu = "00A4 0400 000004 0A0B0C0D F00D";
        private static readonly byte[] Case4EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D, 0xF0, 0x0D };
        private static readonly string Case4EXml = $"{XmlHeader}<commandAPDU extended=\"true\" cla=\"00\" ins=\"A4\" p1=\"04\" p2=\"00\" le=\"F00D\">0A 0B 0C 0D</commandAPDU>";

        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(CommandAPDU));

        private const int ShortLc = 0x11;
        private const int ExtendedLc = 0x101;
        private const int ShortLe = 0x10;
        private const int ExtendedLe = 0x100;
        private static readonly byte[] ExtendedUdc = new byte[ExtendedLc];
        private static readonly byte[] ShortUdc = new byte[ShortLc];

        [Test(Description = "Case 1 C-APDU")]
        public void TestCase1ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case1Apdu);
            Assert.AreEqual(CommandCase.CC1, commandApdu.CommandCase);
            Assert.IsTrue(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsFalse(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case1ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.LeFieldSize);
            Assert.AreEqual(0x00, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case1Xml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test(Description = "Case 2 C-APDU")]
        public void TestCase2ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case2Apdu);
            Assert.AreEqual(CommandCase.CC2, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsTrue(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsFalse(commandApdu.IsExtended);


            // Test encoding
            Assert.AreEqual(Case2ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x01, commandApdu.LeFieldSize);
            Assert.AreEqual(0x00, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case2Xml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test(Description = "Case 2 Extended C-APDU")]
        public void TestCase2EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case2EApdu);
            Assert.AreEqual(CommandCase.CC2E, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsTrue(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsTrue(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case2EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x03, commandApdu.LeFieldSize);
            Assert.AreEqual(0x00, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case2EXml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test(Description = "Case 3 C-APDU")]
        public void TestCase3ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3Apdu);
            Assert.AreEqual(CommandCase.CC3, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsTrue(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsFalse(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case3ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.LeFieldSize);
            Assert.AreEqual(0x01, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case3Xml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test(Description = "Case 3 Extended C-APDU")]
        public void TestCase3EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3EApdu);
            Assert.AreEqual(CommandCase.CC3E, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsTrue(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsTrue(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case3EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.LeFieldSize);
            Assert.AreEqual(0x03, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case3EXml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test(Description = "Case 4 C-APDU")]
        public void TestCase4ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4Apdu);
            Assert.AreEqual(CommandCase.CC4, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsTrue(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsFalse(commandApdu.IsCc4E);
            Assert.IsFalse(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case4ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x01, commandApdu.LeFieldSize);
            Assert.AreEqual(0x01, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case4Xml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }

            // Test automatic conversion to extended
            commandApdu.Lc = 0xF00D;
            Assert.AreEqual(CommandCase.CC4E, commandApdu.CommandCase);
            commandApdu = new CommandAPDU(Case4Apdu);
            commandApdu.Le = 0xF00D;
            Assert.AreEqual(CommandCase.CC4E, commandApdu.CommandCase);
        }

        [Test(Description = "Case 4 Extended C-APDU")]
        public void TestCase4EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4EApdu);
            Assert.AreEqual(CommandCase.CC4E, commandApdu.CommandCase);
            Assert.IsFalse(commandApdu.IsCc1);
            Assert.IsFalse(commandApdu.IsCc2);
            Assert.IsFalse(commandApdu.IsCc3);
            Assert.IsFalse(commandApdu.IsCc4);
            Assert.IsFalse(commandApdu.IsCc2E);
            Assert.IsFalse(commandApdu.IsCc3E);
            Assert.IsTrue(commandApdu.IsCc4E);
            Assert.IsTrue(commandApdu.IsExtended);

            // Test encoding
            Assert.AreEqual(Case4EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x02, commandApdu.LeFieldSize);
            Assert.AreEqual(0x03, commandApdu.LcFieldSize);

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual(Case4EXml, outputXml);

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.AreEqual(commandApdu, unserializedApdu);
            }
        }

        [Test]
        public void LcUpdatesCC1State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00); // CC1

            // none > short
            command.Udc = ShortUdc; // CC3

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(1, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3, command.CommandCase);

            // short > none
            command.HasLc = false; // CC1
            Assert.AreEqual(CommandCase.CC1, command.CommandCase);

            // none > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3E, command.CommandCase);

            // extended > none
            command.HasLc = false; // 1

            Assert.AreEqual(CommandCase.CC1, command.CommandCase);
        }

        [Test]
        public void LeUpdatesCC1State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00); // CC1

            // none > short
            command.Le = ShortLe; // CC2

            Assert.AreEqual(1, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2, command.CommandCase);

            // short > none
            command.HasLe = false; // CC1

            Assert.AreEqual(CommandCase.CC1, command.CommandCase);

            // none > extended
            command.Le = ExtendedLe; // CC2E

            Assert.AreEqual(3, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2E, command.CommandCase);

            // extended > none
            command.HasLe = false; // CC1

            Assert.AreEqual(CommandCase.CC1, command.CommandCase);
        }

        [Test]
        public void LcUpdatesCC2State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10); // CC2

            // none > short
            command.Udc = ShortUdc; // CC4

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(1, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC4, command.CommandCase);

            // short > none
            command.HasLc = false; // CC2

            Assert.AreEqual(CommandCase.CC2, command.CommandCase);

            // none > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > none
            command.HasLc = false; // CC2

            Assert.AreEqual(CommandCase.CC2E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC2State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x08); // CC2

            // short > short
            command.Le = ShortLe; // CC2

            Assert.AreEqual(1, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2, command.CommandCase);

            // short > extended
            command.Le = ExtendedLe; // CC2E

            Assert.AreEqual(3, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2E, command.CommandCase);

            // extended > extended
            command.Le = ExtendedLe; // CC2E

            Assert.AreEqual(3, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2E, command.CommandCase);

            // extended > short
            command.Le = ShortLe; // CC2

            Assert.AreEqual(3, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC2E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void LcUpdatesCC3State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00) { Udc = ShortUdc }; // CC3

            // short > short
            command.Udc = ShortUdc; // CC3

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(1, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3, command.CommandCase);

            // short > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3E, command.CommandCase);

            // extended > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3E, command.CommandCase);

            // extended > short
            command.Udc = ShortUdc; // CC3

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(CommandCase.CC3E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC3State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00) { Udc = ShortUdc }; // CC3

            // none > short
            command.Le = ShortLe; // CC4

            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(1, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4, command.CommandCase);

            // short > none
            command.HasLe = false; // CC3

            Assert.AreEqual(CommandCase.CC3, command.CommandCase);

            // extended > extended
            command.Le = ExtendedLe; // CC4E

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ExtendedLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > none
            command.HasLe = false; // CC3

            Assert.AreEqual(CommandCase.CC3E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void LcUpdatesCC4State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10) { Udc = ShortUdc }; // CC4

            // short > short
            command.Udc = ShortUdc; // CC4

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(1, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(1, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4, command.CommandCase);

            // short > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.AreEqual(ExtendedLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > short
            command.Udc = ShortUdc; // CC4

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC4State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10) { Udc = ShortUdc }; // CC4

            // short > short
            command.Le = ShortLe; // CC4

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(1, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(1, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4, command.CommandCase);

            // short > extended
            command.Le = ExtendedLe; // CC4E

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ExtendedLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > extended
            command.Le = ExtendedLe; // CC4E

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ExtendedLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            // extended > short
            command.Le = ShortLe; // CC4

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);
            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
            Assert.AreEqual(CommandCase.CC4E, command.CommandCase); // Extended status unchanged
        }

        [Test]
        public void Le00UpdatesTo000100WhenCC4BecomesCC4E()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x00)
            {
                Udc = ShortUdc // CC4
            };

            command.Udc = ExtendedUdc; // CC4E

            Assert.AreEqual(0x0100, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
        }

        [Test]
        public void EnforceCC2ToCC2E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00, ShortLe); // CC2

            command.EnforceExtended();

            Assert.AreEqual(CommandCase.CC2E, command.CommandCase);

            Assert.False(command.HasLc);

            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(3, command.LeFieldSize);
        }

        [Test]
        public void EnforceCC3ToCC3E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00)
            {
                Udc = ShortUdc // CC3
            };

            command.EnforceExtended();

            Assert.AreEqual(CommandCase.CC3E, command.CommandCase);

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);

            Assert.False(command.HasLe);
        }

        [Test]
        public void EnforceCC4ToCC4E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00, ShortLe)
            {
                Udc = ShortUdc // CC4
            };

            command.EnforceExtended();

            Assert.AreEqual(CommandCase.CC4E, command.CommandCase);

            Assert.AreEqual(ShortLc, command.Lc);
            Assert.AreEqual(3, command.LcFieldSize);

            Assert.AreEqual(ShortLe, command.Le);
            Assert.AreEqual(2, command.LeFieldSize);
        }
    }
}