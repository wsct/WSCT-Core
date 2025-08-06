using System;
using System.IO;
using NUnit.Framework;
using WSCT.Helpers;

namespace WSCT.ISO7816.StatusWord
{
    [TestFixture]
    public class StatusWordDictionaryUnitTest
    {
        private readonly StatusWordDictionary statusWordDictionary;

        public StatusWordDictionaryUnitTest()
        {
            var pathToFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ISO7816/Dictionary.StatusWord.xml");
            statusWordDictionary = SerializedObject<StatusWordDictionary>.LoadFromXml(pathToFile);
        }

        [Test(Description = "Existing exact value")]
        public void Test9000()
        {
            Assert.That(statusWordDictionary.GetDescription(0x90, 0x00), Is.EqualTo("Normal processing"));
        }

        [Test(Description = "Existing SW1, non existing SW2 value")]
        public void Test6302()
        {
            Assert.That(statusWordDictionary.GetDescription(0x63, 0x02), Is.EqualTo(String.Empty));
        }

        [Test(Description = "Existing SW1 SW2")]
        public void Test6381()
        {
            Assert.That(statusWordDictionary.GetDescription(0x63, 0x81), Is.EqualTo("File filled up by the last write"));
        }

        [Test(Description = "Existing SW1, existing SW2 in range")]
        public void Test63D0()
        {
            Assert.That(statusWordDictionary.GetDescription(0x63, 0xD0), Is.EqualTo(String.Empty));
        }

        [Test]
        public void Test6700()
        {
            Assert.That(statusWordDictionary.GetDescription(0x67, 0x00), Is.EqualTo("Wrong lengh; no further indication"));
        }

        [Test]
        public void TestSw1NotInRange()
        {
            Assert.That(statusWordDictionary.GetDescription(0x89, 0x00), Is.EqualTo(String.Empty));
        }
    }
}
