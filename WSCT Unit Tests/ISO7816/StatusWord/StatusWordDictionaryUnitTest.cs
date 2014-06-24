using System;
using NUnit.Framework;
using WSCT.Helpers;
using WSCT.Helpers.Desktop;

namespace WSCT.ISO7816.StatusWord
{
    [TestFixture]
    public class StatusWordDictionaryUnitTest
    {
        private readonly StatusWordDictionary statusWordDictionary;

        public StatusWordDictionaryUnitTest()
        {
            RegisterPcl.Register();
            statusWordDictionary = SerializedObject<StatusWordDictionary>.LoadFromXml(@"ISO7816/Dictionary.StatusWord.xml");
        }

        [Test(Description = "Existing exact value")]
        public void Test9000()
        {
            Assert.AreEqual("Normal processing", statusWordDictionary.GetDescription(0x90, 0x00));
        }

        [Test(Description = "Existing SW1, non existing SW2 value")]
        public void Test6302()
        {
            Assert.AreEqual(String.Empty, statusWordDictionary.GetDescription(0x63, 0x02));
        }

        [Test(Description = "Existing SW1 SW2")]
        public void Test6381()
        {
            Assert.AreEqual("File filled up by the last write", statusWordDictionary.GetDescription(0x63, 0x81));
        }

        [Test(Description = "Existing SW1, existing SW2 in range")]
        public void Test63D0()
        {
            Assert.AreEqual(String.Empty, statusWordDictionary.GetDescription(0x63, 0xD0));
        }

        [Test]
        public void Test6700()
        {
            Assert.AreEqual("Wrong lengh; no further indication", statusWordDictionary.GetDescription(0x67, 0x00));
        }

        [Test]
        public void TestSw1NotInRange()
        {
            Assert.AreEqual(String.Empty, statusWordDictionary.GetDescription(0x89, 0x00));
        }
    }
}
