using NUnit.Framework;
using WSCT.ISO7816;

namespace WSCT_Unit_Tests.ISO7816
{
    [TestFixture]
    public class CommandCaseUnitTest
    {
        // Case 1
        private const string Case1Apdu = "00A4 0400";
        private static readonly byte[] Case1ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00 };

        // Case 2
        private const string Case2Apdu = "00A4 0400 F0";
        private static readonly byte[] Case2ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0xF0 };
        
        // Case 3
        private const string Case3Apdu = "00A4 0400 04 0A0B0C0D";
        private static readonly byte[] Case3ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D };

        // Case 4
        private const string Case4Apdu = "00A4 0400 04 0A0B0C0D F0";
        private static readonly byte[] Case4ApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D, 0xF0 };

        // Case 2E
        private const string Case2EApdu = "00A4 0400 00F00D";
        private static readonly byte[] Case2EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0xF0, 0x0D };
        
        // Case 3E
        private const string Case3EApdu = "00A4 0400 000004 0A0B0C0D";
        private static readonly byte[] Case3EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D };
        
        // Case 4E
        private const string Case4EApdu = "00A4 0400 000004 0A0B0C0D F00D";
        private static readonly byte[] Case4EApduByte = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x00, 0x00, 0x04, 0x0A, 0x0B, 0x0C, 0x0D, 0xF0, 0x0D };

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
            
            // Test encoding
            Assert.AreEqual(Case1ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.Ne);
            Assert.AreEqual(0x00, commandApdu.Nc);
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
            
            // Test encoding
            Assert.AreEqual(Case2ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x01, commandApdu.Ne);
            Assert.AreEqual(0x00, commandApdu.Nc);
        }
        
        [Test(Description = "Case 2 Extended C-APDU")]
        public void TestCase2EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case2EApdu);
            Assert.AreEqual(CommandCase.CC2E, commandApdu.CommandCase);
            
            // Test encoding
            Assert.AreEqual(Case2EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x03, commandApdu.Ne);
            Assert.AreEqual(0x00, commandApdu.Nc);
        }
        
        [Test(Description = "Case 3 C-APDU")]
        public void TestCase3ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3Apdu);
            Assert.AreEqual(CommandCase.CC3, commandApdu.CommandCase);
            
            // Test encoding
            Assert.AreEqual(Case3ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.Ne);
            Assert.AreEqual(0x01, commandApdu.Nc);
        }
        
        [Test(Description = "Case 3 Extended C-APDU")]
        public void TestCase3EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case3EApdu);
            Assert.AreEqual(CommandCase.CC3E, commandApdu.CommandCase);
            
            // Test encoding
            Assert.AreEqual(Case3EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x00, commandApdu.Ne);
            Assert.AreEqual(0x03, commandApdu.Nc);
        }
        
        [Test(Description = "Case 4 C-APDU")]
        public void TestCase4ConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4Apdu);
            Assert.AreEqual(CommandCase.CC4, commandApdu.CommandCase);
            
            // Test encoding
            Assert.AreEqual(Case4ApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x01, commandApdu.Ne);
            Assert.AreEqual(0x01, commandApdu.Nc);
        }
        
        [Test(Description = "Case 4 Extended C-APDU")]
        public void TestCase4EConstructorAndEncode()
        {
            // Test decoding
            var commandApdu = new CommandAPDU(Case4EApdu);
            Assert.AreEqual(CommandCase.CC4E, commandApdu.CommandCase);
            
            // Test encoding
            Assert.AreEqual(Case4EApduByte, commandApdu.BinaryCommand);
            Assert.AreEqual(0x02, commandApdu.Ne);
            Assert.AreEqual(0x03, commandApdu.Nc);
        }
    }
}