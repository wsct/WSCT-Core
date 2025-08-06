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
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC1));
            Assert.That(commandApdu.IsCc1, Is.True);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.False);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case1ApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x00));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x00));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case1Xml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test(Description = "Case 2 C-APDU")]
        public void TestCase2ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case2Apdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC2));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.True);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.False);


            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case2ApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x01));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x00));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case2Xml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test(Description = "Case 2 Extended C-APDU")]
        public void TestCase2EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case2EApdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC2E));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.True);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.True);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case2EApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x03));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x00));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case2EXml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test(Description = "Case 3 C-APDU")]
        public void TestCase3ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3Apdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC3));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.True);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.False);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case3ApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x00));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x01));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case3Xml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test(Description = "Case 3 Extended C-APDU")]
        public void TestCase3EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3EApdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC3E));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.True);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.True);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case3EApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x00));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x03));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case3EXml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test(Description = "Case 4 C-APDU")]
        public void TestCase4ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4Apdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC4));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.True);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.False);
            Assert.That(commandApdu.IsExtended, Is.False);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case4ApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x01));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x01));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case4Xml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }

            // Test automatic conversion to extended
            commandApdu.Lc = 0xF00D;
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC4E));
            commandApdu = new CommandAPDU(Case4Apdu);
            commandApdu.Le = 0xF00D;
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC4E));
        }

        [Test(Description = "Case 4 Extended C-APDU")]
        public void TestCase4EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4EApdu);
            Assert.That(commandApdu.CommandCase, Is.EqualTo(CommandCase.CC4E));
            Assert.That(commandApdu.IsCc1, Is.False);
            Assert.That(commandApdu.IsCc2, Is.False);
            Assert.That(commandApdu.IsCc3, Is.False);
            Assert.That(commandApdu.IsCc4, Is.False);
            Assert.That(commandApdu.IsCc2E, Is.False);
            Assert.That(commandApdu.IsCc3E, Is.False);
            Assert.That(commandApdu.IsCc4E, Is.True);
            Assert.That(commandApdu.IsExtended, Is.True);

            // Test encoding
            Assert.That(commandApdu.BinaryCommand, Is.EqualTo(Case4EApduByte));
            Assert.That(commandApdu.LeFieldSize, Is.EqualTo(0x02));
            Assert.That(commandApdu.LcFieldSize, Is.EqualTo(0x03));

            // Test XML serialization
            var memoryStream = new MemoryStream();
            TextWriter writer = new StreamWriter(memoryStream);
            XmlSerializer.Serialize(writer, commandApdu);
            writer.Flush();
            var outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.That(outputXml, Is.EqualTo(Case4EXml));

            // Test XML deserialization
            using (TextReader textReader = new StringReader(outputXml))
            {
                var unserializedApdu = (CommandAPDU)XmlSerializer.Deserialize(textReader);
                Assert.That(unserializedApdu, Is.EqualTo(commandApdu));
            }
        }

        [Test]
        public void LcUpdatesCC1State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00); // CC1

            // none > short
            command.Udc = ShortUdc; // CC3

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3));

            // short > none
            command.HasLc = false; // CC1
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC1));

            // none > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E));

            // extended > none
            command.HasLc = false; // 1

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC1));
        }

        [Test]
        public void LeUpdatesCC1State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00); // CC1

            // none > short
            command.Le = ShortLe; // CC2

            Assert.That(command.LeFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2));

            // short > none
            command.HasLe = false; // CC1

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC1));

            // none > extended
            command.Le = ExtendedLe; // CC2E

            Assert.That(command.LeFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E));

            // extended > none
            command.HasLe = false; // CC1

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC1));
        }

        [Test]
        public void LcUpdatesCC2State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10); // CC2

            // none > short
            command.Udc = ShortUdc; // CC4

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4));

            // short > none
            command.HasLc = false; // CC2

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2));

            // none > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > none
            command.HasLc = false; // CC2

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E)); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC2State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x08); // CC2

            // short > short
            command.Le = ShortLe; // CC2

            Assert.That(command.LeFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2));

            // short > extended
            command.Le = ExtendedLe; // CC2E

            Assert.That(command.LeFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E));

            // extended > extended
            command.Le = ExtendedLe; // CC2E

            Assert.That(command.LeFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E));

            // extended > short
            command.Le = ShortLe; // CC2

            Assert.That(command.LeFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E)); // Extended status unchanged
        }

        [Test]
        public void LcUpdatesCC3State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00) { Udc = ShortUdc }; // CC3

            // short > short
            command.Udc = ShortUdc; // CC3

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3));

            // short > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E));

            // extended > extended
            command.Udc = ExtendedUdc; // CC3E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E));

            // extended > short
            command.Udc = ShortUdc; // CC3

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E)); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC3State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00) { Udc = ShortUdc }; // CC3

            // none > short
            command.Le = ShortLe; // CC4

            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4));

            // short > none
            command.HasLe = false; // CC3

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3));

            // extended > extended
            command.Le = ExtendedLe; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ExtendedLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > none
            command.HasLe = false; // CC3

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E)); // Extended status unchanged
        }

        [Test]
        public void LcUpdatesCC4State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10) { Udc = ShortUdc }; // CC4

            // short > short
            command.Udc = ShortUdc; // CC4

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(1));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4));

            // short > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > extended
            command.Udc = ExtendedUdc; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ExtendedLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > short
            command.Udc = ShortUdc; // CC4

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E)); // Extended status unchanged
        }

        [Test]
        public void LeUpdatesCC4State()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x10) { Udc = ShortUdc }; // CC4

            // short > short
            command.Le = ShortLe; // CC4

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(1));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(1));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4));

            // short > extended
            command.Le = ExtendedLe; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ExtendedLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > extended
            command.Le = ExtendedLe; // CC4E

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ExtendedLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            // extended > short
            command.Le = ShortLe; // CC4

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));
            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E)); // Extended status unchanged
        }

        [Test]
        public void Le00UpdatesTo000100WhenCC4BecomesCC4E()
        {
            var command = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x00)
            {
                Udc = ShortUdc // CC4
            };

            command.Udc = ExtendedUdc; // CC4E

            Assert.That(command.Le, Is.EqualTo(0x0100));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
        }

        [Test]
        public void EnforceCC2ToCC2E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00, ShortLe); // CC2

            command.EnforceExtended();

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC2E));

            Assert.That(command.HasLc, Is.False);

            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(3));
        }

        [Test]
        public void EnforceCC3ToCC3E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00)
            {
                Udc = ShortUdc // CC3
            };

            command.EnforceExtended();

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC3E));

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));

            Assert.That(command.HasLe, Is.False);
        }

        [Test]
        public void EnforceCC4ToCC4E()
        {
            var command = new CommandAPDU(0x00, 0xC0, 0x00, 0x00, ShortLe)
            {
                Udc = ShortUdc // CC4
            };

            command.EnforceExtended();

            Assert.That(command.CommandCase, Is.EqualTo(CommandCase.CC4E));

            Assert.That(command.Lc, Is.EqualTo(ShortLc));
            Assert.That(command.LcFieldSize, Is.EqualTo(3));

            Assert.That(command.Le, Is.EqualTo(ShortLe));
            Assert.That(command.LeFieldSize, Is.EqualTo(2));
        }
    }
}