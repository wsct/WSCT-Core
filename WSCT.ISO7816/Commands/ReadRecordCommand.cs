using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            CC2_B2 = 0xB2,
            /// <summary>
            /// 
            /// </summary>
            CC4_B3 = 0xB3
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SearchMode
        {
            /// <summary>
            /// 
            /// </summary>
            READ_FIRST_OCCURENCE = 0x00,
            /// <summary>
            /// 
            /// </summary>
            READ_LAST_OCCURENCE = 0x01,
            /// <summary>
            /// 
            /// </summary>
            READ_NEXT_OCCURENCE = 0x02,
            /// <summary>
            /// 
            /// </summary>
            READ_PREVIOUS_OCCURENCE = 0x03,

            /// <summary>
            /// 
            /// </summary>
            READ_RECORD_P1 = 0x04,
            /// <summary>
            /// 
            /// </summary>
            READ_RECORD_FROM_P1_UP_TO_LAST = 0x05,
            /// <summary>
            /// 
            /// </summary>
            READ_RECORD_FROM_LAST_UP_TO_P1 = 0x06,
            /// <summary>
            /// 
            /// </summary>
            RUF = 0x07
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public Byte sfi
        {
            set { p2 = (Byte)((value << 3) | (p2 & 0x07)); }
            get { return (Byte)((p2 & 0xF8) >> 3); }
        }

        /// <summary>
        /// 
        /// </summary>
        public SearchMode searchMode
        {
            set { p2 = (Byte)((p2 & 0xFC) | (int)value); }
            get { return (SearchMode)(p2 & 0x03); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Byte record
        {
            set { p1 = value; }
            get { return p1; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadRecordCommand()
            : base()
        {
            ins = 0xB2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="sfi"></param>
        /// <param name="searchMode"></param>
        /// <param name="le"></param>
        public ReadRecordCommand(Byte record, Byte sfi, SearchMode searchMode, UInt32 le)
            : this()
        {
            this.record = record;
            this.sfi = sfi;
            this.searchMode = searchMode;
            this.le = le;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="sfi"></param>
        /// <param name="searchMode"></param>
        /// <param name="udc"></param>
        /// <param name="le"></param>
        public ReadRecordCommand(Byte record, Byte sfi, SearchMode searchMode, Byte[] udc, UInt32 le)
            : this(record, sfi, searchMode, le)
        {
            this.ins = 0xB3;
            this.udc = udc;
        }

        #endregion
    }
}
