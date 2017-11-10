using System;
using System.Collections.Generic;
using System.Linq;
using WSCT.Helpers;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816-3 ATR.
    /// </summary>
    public class Atr
    {
        #region >> Properties

        /// <summary>
        /// TS character.
        /// </summary>
        public TsCharacter Ts { get; set; }

        /// <summary>
        /// Raw ATR.
        /// </summary>
        public byte[] AtrBytes { get; set; }

        /// <summary>
        /// Raw interface bytes of ATR.
        /// </summary>
        public List<InterfaceByte> InterfaceBytes { get; set; }

        /// <summary>
        /// Raw historic bytes of ATR.
        /// </summary>
        public byte[] HistoricBytes { get; set; }

        /// <summary>
        /// Raw TCK checksum (1 or 2 bytes).
        /// </summary>
        public uint Tck { get; set; }

        /// <summary>
        /// <c>true</c> only if TCK byte is mandatory based on ATR definition.
        /// </summary>
        public Boolean HasTck
        {
            get
            {
                var ta2 = InterfaceBytes.Find(tx => (tx.Id == InterfaceId.Ta2));
                if (ta2 != null && (ta2.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td1 = InterfaceBytes.Find(tx => (tx.Id == InterfaceId.Td1));
                if (td1 != null && (td1.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td2 = InterfaceBytes.Find(tx => (tx.Id == InterfaceId.Td2));
                if (td2 != null && (td2.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td3 = InterfaceBytes.Find(tx => (tx.Id == InterfaceId.Td3));
                if (td3 != null && (td3.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td4 = InterfaceBytes.Find(tx => (tx.Id == InterfaceId.Td4));
                if (td4 != null && (td4.Value & 0x0F) != 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Atr()
        {
            InterfaceBytes = new List<InterfaceByte>();
            HistoricBytes = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="atr"></param>
        public Atr(IEnumerable<byte> atr)
            : this()
        {
            Parse(atr);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="sAtr">string representation of the ATR.</param>
        public Atr(string sAtr)
            : this()
        {
            Parse(sAtr);
        }

        #endregion

        /// <summary>
        /// Returns the interface byte given its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InterfaceByte GetInterfaceByte(InterfaceId id)
        {
            return InterfaceBytes.FirstOrDefault(tx => tx.Id == id);
        }

        /// <summary>
        /// Returns true if an interface byte is defined in the ATR.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean HasInterfaceByte(InterfaceId id)
        {
            return InterfaceBytes.Any(tx => tx.Id == id);
        }

        /// <summary>
        /// Parses an ATR given by a byte array.
        /// </summary>
        /// <param name="atr"></param>
        /// <returns>Number of bytes in the ATR</returns>
        public void Parse(IEnumerable<byte> atr)
        {
            if (atr == null)
            {
                return;
            }

            AtrBytes = atr.ToArray();

            using (var bytesEnumerator = AtrBytes.AsEnumerable().GetEnumerator())
            {
                if (bytesEnumerator.MoveNext() == false)
                {
                    throw new Exception(String.Format("Invalid ATR: {0}", AtrBytes));
                }

                // TS
                Ts = new TsCharacter(bytesEnumerator.Current);

                // Interface bytes
                ParseInterfaceBytes(bytesEnumerator);

                // Historic bytes
                ParseHistoricBytes(bytesEnumerator);

                // TCK
                if (HasTck)
                {
                    Tck = bytesEnumerator.Current;
                }
            }
        }

        /// <summary>
        /// Parses an ATR given by an hexa string.
        /// </summary>
        /// <param name="sAtr"></param>
        /// <returns>Number of bytes in the ATR.</returns>
        public void Parse(string sAtr)
        {
            Parse(sAtr.FromHexa());
        }

        private void ParseInterfaceBytes(IEnumerator<byte> bytesEnumerator)
        {
            InterfaceBytes = new List<InterfaceByte>();

            var byteIds = new Queue<InterfaceId>(new[]
            {
                InterfaceId.T0,
                InterfaceId.Ta1, InterfaceId.Tb1, InterfaceId.Tc1, InterfaceId.Td1,
                InterfaceId.Ta2, InterfaceId.Tb2, InterfaceId.Tc2, InterfaceId.Td2,
                InterfaceId.Ta3, InterfaceId.Tb3, InterfaceId.Tc3, InterfaceId.Td3,
                InterfaceId.Ta4, InterfaceId.Tb4, InterfaceId.Tc4, InterfaceId.Td4,
                InterfaceId.Ta5, InterfaceId.Tb5, InterfaceId.Tc5
            });

            var endOfInterfaceBytes = (bytesEnumerator.MoveNext() == false);

            if (endOfInterfaceBytes)
            {
                throw new MalformedAtrException(InterfaceId.T0);
            }

            while (!endOfInterfaceBytes)
            {
                // T0 or TDx
                var td = bytesEnumerator.Current;
                InterfaceBytes.Add(new InterfaceByte(byteIds.Dequeue(), td));

                // TAx
                var byteId = byteIds.Dequeue();
                if ((td & 0x10) != 0)
                {
                    if (bytesEnumerator.MoveNext() == false)
                    {
                        throw new MalformedAtrException(byteId);
                    }
                    InterfaceBytes.Add(new InterfaceByte(byteId, bytesEnumerator.Current));
                }

                // TBx
                byteId = byteIds.Dequeue();
                if ((td & 0x20) != 0)
                {
                    if (bytesEnumerator.MoveNext() == false)
                    {
                        throw new MalformedAtrException(byteId);
                    }
                    InterfaceBytes.Add(new InterfaceByte(byteId, bytesEnumerator.Current));
                }

                // TCx
                byteId = byteIds.Dequeue();
                if ((td & 0x40) != 0)
                {
                    if (bytesEnumerator.MoveNext() == false)
                    {
                        throw new MalformedAtrException(byteId);
                    }
                    InterfaceBytes.Add(new InterfaceByte(byteId, bytesEnumerator.Current));
                }

                // TDx
                if ((td & 0x80) != 0)
                {
                    if (bytesEnumerator.MoveNext() == false)
                    {
                        byteId = byteIds.Dequeue();
                        throw new MalformedAtrException(byteId);
                    }
                    // TDx will be added when starting next loop
                }
                else
                {
                    endOfInterfaceBytes = true;
                }
            }
        }

        private void ParseHistoricBytes(IEnumerator<byte> bytesEnumerator)
        {
            var t0 = InterfaceBytes.FirstOrDefault(tx => tx.Id == InterfaceId.T0);

            if (t0 == null)
            {
                throw new MalformedAtrException(String.Format("Missing T0 in {0}", AtrBytes.ToHexa()));
            }

            var historicLength = t0.Value & 0x0F;
            var historicBytes = new List<byte>();

            while (bytesEnumerator.MoveNext() && historicLength > 0)
            {
                historicBytes.Add(bytesEnumerator.Current);
                historicLength--;
            }

            if (historicLength != 0)
            {
                throw new MalformedAtrException(String.Format("{0} historic bytes missing in {1}", historicLength, AtrBytes.ToHexa()));
            }

            HistoricBytes = historicBytes.ToArray();
        }

        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            var s = String.Format("TS:{0}", Ts);
            s += InterfaceBytes.Aggregate(String.Empty, (current, tx) => String.Format("{1} {0}", tx, current));
            s += " historic:" + HistoricBytes.ToHexa();
            if (HasTck)
            {
                s += String.Format(" TCK:{0:X2}", Tck);
            }
            return s;
        }

        #endregion
    }
}