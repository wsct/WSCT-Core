using System;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadRecordCommand : CommandAPDU
    {
        #region >> Enumerations

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use ReadRecordCommand class instead", false)]
        public enum CCx
        {
            /// <summary>
            /// 
            /// </summary>
            CC2B2 = 0xB2,

            /// <summary>
            /// 
            /// </summary>
            CC4B3 = 0xB3
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SearchMode
        {
            /// <summary>
            /// 
            /// </summary>
            ReadFirstOccurence = 0x00,

            /// <summary>
            /// 
            /// </summary>
            ReadLastOccurence = 0x01,

            /// <summary>
            /// 
            /// </summary>
            ReadNextOccurence = 0x02,

            /// <summary>
            /// 
            /// </summary>
            ReadPreviousOccurence = 0x03,

            /// <summary>
            /// 
            /// </summary>
            ReadRecordP1 = 0x04,

            /// <summary>
            /// 
            /// </summary>
            ReadRecordFromP1UpToLast = 0x05,

            /// <summary>
            /// 
            /// </summary>
            ReadRecordFromLastUpToP1 = 0x06,

            /// <summary>
            /// 
            /// </summary>
            Ruf = 0x07
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public byte Sfi
        {
            set { P2 = (byte)((value << 3) | (P2 & 0x07)); }
            get { return (byte)((P2 & 0xF8) >> 3); }
        }

        /// <summary>
        /// 
        /// </summary>
        public SearchMode Search
        {
            set { P2 = (byte)((P2 & 0xFC) | (int)value); }
            get { return (SearchMode)(P2 & 0x03); }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Record
        {
            set { P1 = value; }
            get { return P1; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadRecordCommand()
        {
            Ins = 0xB2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="sfi"></param>
        /// <param name="search"></param>
        /// <param name="le"></param>
        public ReadRecordCommand(byte record, byte sfi, SearchMode search, UInt32 le)
            : this()
        {
            Record = record;
            Sfi = sfi;
            Search = search;
            Le = le;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="sfi"></param>
        /// <param name="search"></param>
        /// <param name="udc"></param>
        /// <param name="le"></param>
        public ReadRecordCommand(byte record, byte sfi, SearchMode search, byte[] udc, UInt32 le)
            : this(record, sfi, search, le)
        {
            Ins = 0xB3;
            Udc = udc;
        }

        #endregion
    }
}