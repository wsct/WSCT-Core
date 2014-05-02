using System;
using System.Linq;
using NUnit.Framework;

namespace WSCT.Helpers.Security
{
    [TestFixture]
    public class Ssh1PublicKeyBodyUnitTest
    {
        private const string Base64Key =
            "AAAAB3NzaC1yc2EAAAABJQAAAQEAhVF4ujVF1EF/2F1PM3q8dLS9ox0egMJrhVZWDYVyDjWJqjNzDGysT0+JGkMhh9eO/MndiJcJs6U3TjEcyWEP/l82nPNOGVKv6Gn/anonr2yctEWAVDMTg8UTP6flgMCH7D5QypLSLPhblYJ3Z0g8xzsawF/LrqhFw8ac4ShycF+B1A35jfHL/SojzmfD/LbxrpbswUnxnj55qJWHJwAFugPWynnIBY8I3NRTNLSet0AjbIYjB6pMks9m7G6XkWahiubuhvI+s/2GoVmbGLoSJb6SW4ATnDe/Qh3PmECDm49cQ4hGXIH4ieHLV8sMPxvCRB31Zl7DNyWtskdU5IFuRQ==";

        private static void KeyCheck(Ssh1PublicKeyBody key)
        {
            Assert.AreEqual("ssh-rsa", key.Type);

            Assert.AreEqual(new[] { 0x25 }, key.E);

            Assert.AreEqual(0x101, key.N.Length);
            Assert.AreEqual(new[] { 0x00, 0x85 }, key.N.Take(2));
            Assert.AreEqual(0x45, key.N.Last());
        }

        [Test]
        public void CreateFromBase64()
        {
            var key = Ssh1PublicKeyBody.Create(Base64Key);

            KeyCheck(key);
        }

        [Test]
        public void CreateFromBytes()
        {
            var key = Ssh1PublicKeyBody.Create(Convert.FromBase64String(Base64Key));

            KeyCheck(key);
        }
    }
}