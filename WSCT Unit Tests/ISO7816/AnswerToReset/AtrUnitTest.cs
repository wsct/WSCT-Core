﻿using NUnit.Framework;

namespace WSCT.ISO7816.AnswerToReset
{
    [TestFixture]
    public class AtrUnitTest
    {
        [Test]
        public void Atr1()
        {
            var atr = new Atr("3F 67 25 00 21 20 00 0F 78 90 00");

            Assert.That(atr.ToString(), Is.EqualTo("TS:3F T0:67 Tb1:25 Tc1:00 historic:21 20 00 0F 78 90 00"));

            Assert.That(atr.HasInterfaceByte(InterfaceId.T0), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Ta1), Is.False);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tb1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tc1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Td1), Is.False);
        }

        [Test]
        public void Atr2()
        {
            var atr = new Atr("3B 75 13 00 00 44 09 EA 90 00");

            Assert.That(atr.ToString(), Is.EqualTo("TS:3B T0:75 Ta1:13 Tb1:00 Tc1:00 historic:44 09 EA 90 00"));

            Assert.That(atr.HasInterfaceByte(InterfaceId.T0), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Ta1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tb1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tc1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Td1), Is.False);
        }

        [Test]
        public void Atr3()
        {
            var atr = new Atr("3B B7 18 00 81 31 FE 65 53 50 4B 32 34 90 00 5A");

            Assert.That(atr.ToString(), Is.EqualTo("TS:3B T0:B7 Ta1:18 Tb1:00 Td1:81 Td2:31 Ta3:FE Tb3:65 historic:53 50 4B 32 34 90 00 TCK:5A"));

            Assert.That(atr.HasInterfaceByte(InterfaceId.T0), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Ta1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tb1), Is.True);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Tc1), Is.False);
            Assert.That(atr.HasInterfaceByte(InterfaceId.Td1), Is.True);
        }

        [Test]
        public void AtrNoT0()
        {
            var exception = Assert.Throws<MalformedAtrException>(() => new Atr("3F"));
            Assert.That(exception.ByteId, Is.EqualTo(InterfaceId.T0));
        }

        [Test]
        public void AtrWrongInterfaceBytes()
        {
            var exception = Assert.Throws<MalformedAtrException>(() => new Atr("3F F5 80 00 01"));
            Assert.That(exception.ByteId, Is.EqualTo(InterfaceId.Td1));
        }

        [Test]
        public void AtrMissingHistoricBytes()
        {
            var exception = Assert.Throws<MalformedAtrException>(() => new Atr("3F 65 00 00 01 02 03"));
            Assert.That(exception.ByteId, Is.Null);
        }
    }
}