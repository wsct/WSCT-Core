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
        /// TS character
        /// </summary>
        public TsCharacter Ts { get; set; }

        /// <summary>
        /// Raw ATR
        /// </summary>
        public byte[] AtrBytes { get; set; }

        /// <summary>
        /// Raw interface bytes of ATR
        /// </summary>
        public List<InterfaceByte> InterfaceBytes { get; set; }

        /// <summary>
        /// Raw historic bytes of ATR
        /// </summary>
        public byte[] HistoricBytes { get; set; }

        /// <summary>
        /// Raw TCK checksum (1 or 2 bytes)
        /// </summary>
        public uint Tck { get; set; }

        /// <summary>
        /// <c>true</c> only if TCK byte is mandatory based on ATR definition.
        /// </summary>
        public Boolean HasTck
        {
            get
            {
                var ta2 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Ta2));
                if (ta2 != null && (ta2.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td1 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td1));
                if (td1 != null && (td1.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td2 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td2));
                if (td2 != null && (td2.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td3 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td3));
                if (td3 != null && (td3.Value & 0x0F) != 0)
                {
                    return true;
                }
                var td4 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td4));
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
        public Atr(byte[] atr)
            : this()
        {
            Parse(atr);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="sAtr">String representation of the ATR.</param>
        public Atr(String sAtr)
            : this()
        {
            Parse(sAtr);
        }

        #endregion

        #region >> Private Members

        private uint ParseInterface(byte mask, uint offset, InterfaceByte.IdType nextId)
        {
            if ((mask & 0x10) != 0)
            {
                InterfaceBytes.Add(new InterfaceByte(nextId, AtrBytes[offset++]));
            }
            if ((mask & 0x20) != 0)
            {
                InterfaceBytes.Add(new InterfaceByte(nextId + 1, AtrBytes[offset++]));
            }
            if ((mask & 0x40) != 0)
            {
                InterfaceBytes.Add(new InterfaceByte(nextId + 2, AtrBytes[offset++]));
            }
            if ((mask & 0x80) != 0)
            {
                InterfaceBytes.Add(new InterfaceByte(nextId + 3, AtrBytes[offset++]));
            }
            return offset;
        }

        #endregion

        #region >> Members

        /// <summary>
        /// Returns the interface byte given its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InterfaceByte GetInterfaceByte(InterfaceByte.IdType id)
        {
            return InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td1));
        }

        /// <summary>
        /// Returns true if an interface byte is defined in the ATR.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean HasInterfaceByte(InterfaceByte.IdType id)
        {
            return InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td1)) != null;
        }

        /// <summary>
        /// Parses an ATR given by a byte array
        /// </summary>
        /// <param name="atr"></param>
        /// <returns>Number of bytes in the ATR</returns>
        public uint Parse(byte[] atr)
        {
            AtrBytes = atr;
            uint offset = 0;

            // TS
            Ts = new TsCharacter(atr[offset++]);

            // T0
            InterfaceBytes.Add(new InterfaceByte(InterfaceByte.IdType.T0, atr[offset++]));

            // Discovery of TA1 ... TC5
            var t0 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.T0));
            offset = ParseInterface(t0.Value, offset, InterfaceByte.IdType.Ta1);
            var td1 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td1));
            if (td1 != null)
            {
                offset = ParseInterface(td1.Value, offset, InterfaceByte.IdType.Ta2);
                var td2 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td2));
                if (td2 != null)
                {
                    offset = ParseInterface(td2.Value, offset, InterfaceByte.IdType.Ta3);
                    var td3 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td3));
                    if (td3 != null)
                    {
                        offset = ParseInterface(td3.Value, offset, InterfaceByte.IdType.Ta4);
                        var td4 = InterfaceBytes.Find(tx => (tx.Id == InterfaceByte.IdType.Td4));
                        if (td4 != null)
                        {
                            offset = ParseInterface(td4.Value, offset, InterfaceByte.IdType.Ta5);
                        }
                    }
                }
            }

            // Historic bytes
            HistoricBytes = new byte[t0.Value & 0x0F];
            Array.Copy(atr, offset, HistoricBytes, 0, HistoricBytes.Length);
            offset += (uint)HistoricBytes.Length;

            // TCK
            if (HasTck)
            {
                Tck = atr[offset++];
            }

            return offset;
        }

        /// <summary>
        /// Parses an ATR given by a string.
        /// </summary>
        /// <param name="sAtr"></param>
        /// <returns>Number of bytes in the ATR.</returns>
        public uint Parse(String sAtr)
        {
            return Parse(sAtr.FromHexa());
        }

        #endregion

        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            var s = InterfaceBytes.Aggregate(string.Empty, (current, tx) => current + String.Format("{0}{1}", (current == string.Empty ? string.Empty : " "), tx));
            s += " historic:" + HistoricBytes.ToHexa();
            if (HasTck)
            {
                s += String.Format(" TCK:{0:X}", Tck);
            }
            return s;
        }

        #endregion
    }
}