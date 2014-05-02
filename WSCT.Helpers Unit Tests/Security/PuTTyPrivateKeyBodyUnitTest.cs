using System;
using System.Linq;
using NUnit.Framework;

namespace WSCT.Helpers.Security
{
    [TestFixture]
    public class PuTTyPrivateKeyBodyUnitTest
    {
        private const string Base64Key =
            "AAABAArPQSPazk967rCtnqNPJAl2cD2oakHKkximIqcmft6OuCKOjMmn8kueqkBmTtOpsaXLLaNKhD7/jtzTjLZh0N1F/YJY6qgbdgvs6zIllIPYX7uW7rq4CIBOP9S6lhFbt/5l6t/+EQqQrXrTOh0hiGMuTkbzAq1L6fsd8QtWTnfQRYddWpsUefl3AVh2oT2U6bpHovaB+rJjbp0D5yaccX7e8c2o9oU3mGqt6ll9POad5hCAXfOxaeOSTNnD9nblwp25uQRsm1i2neA41XeGu3JTgjImo3itx4lqAV1dJ8+1mr4GKT9g68IslkDkUj4SuSS36C5i23pASr6JQFRZnmEAAACBAO6ZYlAmIe4c2a4CPf4smUOt89PlNc6QbfcfcXxCbIz7esfTTAgAUFgaesgj6GKTgnOfVpLA5WbxyWj2bKYjXE5ps8D7p495VD9ANviuhog3spPkN8j1NLn7NcvVQxEUFlHolFtzt+vq+NkYloR/cFJwD0cuZi2OB+nbui9zQSFjAAAAgQCPCoGhRH39hj9lM8gBLfEWBy+UUSZkdbDY+J6SswoA9yC+DrSKROZY8/iWFm5NB3eaVwBe3o4g/6RKIuZnCPidh+tDqg8zSd0Bk7sEUrXXbAYODMEVK/kS/XdIQ4CHhblAiKfuMHQGRBhfyYnmwhnZud0pUTVYGq9/U0W2tkRWNwAAAIEA2C3XNK0PR+MtE+a3Fl/ydDDTyNgyUvt3SteQPFHacaI/ZVtbo1RNJRuXHTeshk5S+Sl809KQYCHChstB77+5bwDK/YqIWcqBQl79uTZAgi+MpuoUXaJ+G9f7e9EFGoraNaV+CN8+pIDYrehpzDUjEPUmqETKtxd9CZeivhtet2g=";

        private static void KeyCheck(PuTTyPrivateKeyBody key)
        {
            Assert.AreEqual(0x100, key.D.Length);
            Assert.AreEqual(new[] { 0x0A, 0xCF }, key.D.Take(2));
            Assert.AreEqual(0x61, key.D.Last());

            Assert.AreEqual(0x81, key.P.Length);
            Assert.AreEqual(new[] { 0x00, 0xD8 }, key.P.Take(2));
            Assert.AreEqual(0x68, key.P.Last());

            Assert.AreEqual(0x81, key.Q.Length);
            Assert.AreEqual(new[] { 0x00, 0x8F }, key.Q.Take(2));
            Assert.AreEqual(0x37, key.Q.Last());

            Assert.AreEqual(0x81, key.Iqmp.Length);
            Assert.AreEqual(new[] { 0x00, 0xEE }, key.Iqmp.Take(2));
            Assert.AreEqual(0x63, key.Iqmp.Last());
        }

        [Test]
        public void CreateFromBase64()
        {
            var key = PuTTyPrivateKeyBody.Create(Base64Key);

            KeyCheck(key);
        }

        [Test]
        public void CreateFromBytes()
        {
            var key = PuTTyPrivateKeyBody.Create(Convert.FromBase64String(Base64Key));

            KeyCheck(key);
        }
    }
}