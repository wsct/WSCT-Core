namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class WriteRecordCommand : CommandAPDU
    {
        #region >> Enumerations

        /// <summary>
        /// 
        /// </summary>
        public enum TargetType
        {
            /// <summary>
            /// 
            /// </summary>
            FirstRecord = 0x00,

            /// <summary>
            /// 
            /// </summary>
            LastRecord = 0x01,

            /// <summary>
            /// 
            /// </summary>
            NextRecord = 0x02,

            /// <summary>
            /// 
            /// </summary>
            PreviousRecord = 0x03,

            /// <summary>
            /// 
            /// </summary>
            RecordNumberInP1 = 0x04
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to "record number" information.
        /// Write access: <see cref="Target"/> is updated.
        /// </summary>
        public byte Record
        {
            set
            {
                P1 = value;
                Target = TargetType.RecordNumberInP1;
            }
            get { return P1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TargetType Target
        {
            set { P2 = (byte)((P2 & 0xFC) | (int)value); }
            get { return (TargetType)(P2 & 0x03); }
        }

        /// <summary>
        /// Short File Identifier value.
        /// </summary>
        public byte Sfi
        {
            set { P2 = (byte)((value << 3) | (P2 & 0x03)); }
            get { return (byte)(P2 & 0xFC); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public WriteRecordCommand()
        {
            Ins = 0xD2;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="recordNumber"></param>
        /// <param name="sfi"></param>
        /// <param name="target"></param>
        /// <param name="udc"></param>
        public WriteRecordCommand(byte recordNumber, byte sfi, TargetType target, byte[] udc)
            : this()
        {
            if (target == TargetType.RecordNumberInP1)
            {
                Record = recordNumber;
            }
            else
            {
                P1 = 0x00;
            }
            Target = target;
            Sfi = sfi;
            Udc = udc;
        }

        #endregion
    }
}