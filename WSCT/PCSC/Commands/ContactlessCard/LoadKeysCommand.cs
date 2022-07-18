using WSCT.ISO7816;

namespace WSCT.PCSC.Commands.ContactlessCard
{

    /// <summary>
    /// LOAD KEYS command.
    /// FF 82 <keyStructure> <keyNumber> <key.Length> <key>
    /// </summary>
    /// <remarks>More information at http://pcscworkgroup.com/Download/Specifications/pcsc3_v2.01.09.pdf §3.2.2.1.4</remarks>
    public class LoadKeysCommand : CommandAPDU
    {
        public class KeyStructure
        {
            #region >> Enumerations

            public enum KeyUsage : byte
            {
                /// <summary>Key to be used for card authentication.</summary>
                Card = 0x00,
                /// <summary>Key to be used for transmission protection.</summary>
                Reader = 0x80
            }

            public enum KeyTransmission : byte
            {
                Plain = 0x00,
                Secured = 0x40
            }

            public enum KeyStorage : byte
            {
                /// <summary>Keys are loaded into the IFD volatile memory.</summary>
                VolatileMemory = 0x00,
                /// <summary>Keys are loaded into the IFD non-volatile memory.</summary>
                NonVolativeMemory = 0x20
            }

            #endregion

            #region >> Properties

            /// <summary>Key usage.</summary>
            public KeyUsage Usage { get; set; }

            /// <summary>Key transmission mode.</summary>
            public KeyTransmission Transmission { get; set; }

            /// <summary>Key storage location.</summary>
            public KeyStorage Storage { get; set; }

            /// <summary>If <see cref="Transmission"/> is is set to <see cref="KeyTransmission.Secured"/>, it is the Reader Key number that is used for the encryption, else it is ignored by the IFD.</summary>
            public byte KeyNumber { get; set; }

            #endregion >> Properties

            /// <summary>Computes and returns the byte <code>P1</code> used in LOAD KEY.</summary>
            internal byte GetByte() => (byte)((byte)Usage + (byte)Transmission + (byte)Storage + KeyNumber % 16);
        }

        #region >> Constructors

        /// <summary>Creates a new LOAD KEY command.</summary>
        public LoadKeysCommand(byte keyNumber, byte keyStructure, byte[] key) : base(0xFF, 0x82, keyStructure, keyNumber)
        {
            Udc = key;
        }

        /// <summary>Creates a new LOAD KEY command.</summary>
        public LoadKeysCommand(byte keyNumber, KeyStructure keyStructure, byte[] key) : base(0xFF, 0x82, 0x00, keyNumber)
        {
            P1 = keyStructure.GetByte();
            Udc = key;
        }

        #endregion
    }
}
