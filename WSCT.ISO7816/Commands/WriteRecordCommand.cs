using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public enum Target
        {
            /// <summary>
            /// 
            /// </summary>
            FIRST_RECORD = 0x00,
            /// <summary>
            /// 
            /// </summary>
            LAST_RECORD = 0x01,
            /// <summary>
            /// 
            /// </summary>
            NEXT_RECORD = 0x02,
            /// <summary>
            /// 
            /// </summary>
            PREVIOUS_RECORD = 0x03,
            /// <summary>
            /// 
            /// </summary>
            RECORD_NUMBER_IN_P1 = 0x04
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to "record number" information
        /// Write access: <see cref="target"/> is updated
        /// </summary>
        public Byte record
        {
            set
            {
                p1 = value;
                target = Target.RECORD_NUMBER_IN_P1;
            }
            get { return p1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target target
        {
            set { p2 = (Byte)((p2 & 0xFC) | (int)value); }
            get { return (Target)(p2 & 0x03); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Byte sfi
        {
            set { p2 = (Byte)((value << 3) | (p2 & 0x03)); }
            get { return (Byte)(p2 & 0xFC); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WriteRecordCommand()
            : base()
        {
            ins = 0xD2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordNumber"></param>
        /// <param name="sfi"></param>
        /// <param name="target"></param>
        /// <param name="udc"></param>
        public WriteRecordCommand(Byte recordNumber, Byte sfi, Target target, Byte[] udc)
            : this()
        {
            if (target == Target.RECORD_NUMBER_IN_P1)
                this.record = recordNumber;
            else
                this.p1 = 0x00;
            this.target = target;
            this.sfi = sfi;
            this.udc = udc;
        }

        #endregion
    }
}
