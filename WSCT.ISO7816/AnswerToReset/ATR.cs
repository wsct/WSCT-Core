using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using WSCT.Helpers;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816 ATR
    /// </summary>
    public class ATR
    {
        #region >> Properties

        /// <summary>
        /// TS character
        /// </summary>
        public TSCharacter ts
        { get; set; }

        /// <summary>
        /// Raw ATR
        /// </summary>
        public Byte[] atr
        { get; set; }

        /// <summary>
        /// Raw interface bytes of ATR
        /// </summary>
        public List<InterfaceByte> interfaceBytes
        { get; set; }

        /// <summary>
        /// Raw historic bytes of ATR
        /// </summary>
        public Byte[] historicBytes
        { get; set; }

        /// <summary>
        /// Raw TCK checksum (1 or 2 bytes)
        /// </summary>
        public uint tck
        { get; set; }

        /// <summary>
        /// <c>true</c> only if TCK byte is mandatory based on ATR definition.
        /// </summary>
        public Boolean hasTCK
        {
            get
            {
                InterfaceByte ta2 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TA2); });
                if (ta2 != null && (ta2.value & 0x0F) != 0)
                    return true;
                InterfaceByte td1 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD1); });
                if (td1 != null && (td1.value & 0x0F) != 0)
                    return true;
                InterfaceByte td2 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD2); });
                if (td2 != null && (td2.value & 0x0F) != 0)
                    return true;
                InterfaceByte td3 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD3); });
                if (td3 != null && (td3.value & 0x0F) != 0)
                    return true;
                InterfaceByte td4 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD4); });
                if (td4 != null && (td4.value & 0x0F) != 0)
                    return true;
                return false;
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ATR()
        {
            interfaceBytes = new List<InterfaceByte>();
            historicBytes = new Byte[0];
        }

        /// <summary>
        /// Constructor initializing an ATR given a byte array
        /// </summary>
        /// <param name="atr"></param>
        public ATR(Byte[] atr)
            : this()
        {
            parse(atr);
        }

        /// <summary>
        /// Construtor initializing an ATR given a string representation
        /// </summary>
        /// <param name="sAtr"></param>
        public ATR(String sAtr)
            : this()
        {
            parse(sAtr);
        }

        #endregion

        #region >> Private Members

        private uint parseInterface(Byte mask, uint offset, InterfaceByte.Id nextId)
        {
            if ((mask & 0x10) != 0)
                interfaceBytes.Add(new InterfaceByte(nextId, atr[offset++]));
            if ((mask & 0x20) != 0)
                interfaceBytes.Add(new InterfaceByte(nextId + 1, atr[offset++]));
            if ((mask & 0x40) != 0)
                interfaceBytes.Add(new InterfaceByte(nextId + 2, atr[offset++]));
            if ((mask & 0x80) != 0)
                interfaceBytes.Add(new InterfaceByte(nextId + 3, atr[offset++]));
            return offset;
        }

        #endregion

        #region >> Members

        /// <summary>
        /// Returns the interface byte given its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InterfaceByte getInterfaceByte(InterfaceByte.Id id)
        {
            return interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD1); });
        }

        /// <summary>
        /// Returns true if an interface byte is defined in the ATR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean hasInterfaceByte(InterfaceByte.Id id)
        {
            return interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD1); }) != null;
        }

        /// <summary>
        /// Parses an ATR given by a byte array
        /// </summary>
        /// <param name="atr"></param>
        /// <returns>Number of bytes in the ATR</returns>
        public uint parse(Byte[] atr)
        {
            this.atr = atr;
            uint offset = 0;

            // TS
            ts = new TSCharacter(atr[offset++]);

            // T0
            interfaceBytes.Add(new InterfaceByte(InterfaceByte.Id.T0, atr[offset++]));

            // Discovery of TA1 ... TC5
            InterfaceByte t0 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.T0); });
            offset = parseInterface(t0.value, offset, InterfaceByte.Id.TA1);
            InterfaceByte td1 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD1); });
            if (td1 != null)
            {
                offset = parseInterface(td1.value, offset, InterfaceByte.Id.TA2);
                InterfaceByte td2 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD2); });
                if (td2 != null)
                {
                    offset = parseInterface(td2.value, offset, InterfaceByte.Id.TA3);
                    InterfaceByte td3 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD3); });
                    if (td3 != null)
                    {
                        offset = parseInterface(td3.value, offset, InterfaceByte.Id.TA4);
                        InterfaceByte td4 = interfaceBytes.Find(delegate(InterfaceByte tx) { return (tx.id == InterfaceByte.Id.TD4); });
                        if (td4 != null)
                        {
                            offset = parseInterface(td4.value, offset, InterfaceByte.Id.TA5);
                        }
                    }
                }
            }

            // Historic bytes
            historicBytes = new Byte[t0.value & 0x0F];
            Array.Copy(atr, offset, historicBytes, 0, historicBytes.Length);
            offset += (uint)historicBytes.Length;

            // TCK
            if (hasTCK)
                tck = atr[offset++];

            return offset;
        }

        /// <summary>
        /// Parses an ATR given by a string
        /// </summary>
        /// <param name="sATR"></param>
        /// <returns>Number of bytes in the ATR</returns>
        public uint parse(String sATR)
        {
            return parse(sATR.fromHexa());
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            String s = "";
            foreach (InterfaceByte tx in interfaceBytes)
            {
                s += String.Format("{0}{1}", (s == "" ? "" : " "), tx);
            }
            s += " historic:" + historicBytes.toHexa();
            if (hasTCK)
                s += String.Format(" TCK:{0:X}", tck);
            return s;
        }
    }
}
