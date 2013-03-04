using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectCommand : CommandAPDU
    {
        #region >> Enumerations

        /// <summary>
        /// 
        /// </summary>
        public enum SelectionMode
        {
            /// <summary>
            /// 
            /// </summary>
            MF_DF_EF = 0x00,
            /// <summary>
            /// 
            /// </summary>
            CHILD_DF = 0x01,
            /// <summary>
            /// 
            /// </summary>
            CHILD_EF = 0x02,
            /// <summary>
            /// 
            /// </summary>
            PARENT_DF = 0x03,
            /// <summary>
            /// 
            /// </summary>
            SELECT_DF_NAME = 0x04,
            /// <summary>
            /// 
            /// </summary>
            SELECT_FROM_MF = 0x08,
            /// <summary>
            /// 
            /// </summary>
            SELECT_FROM_CURRENT_DF = 0x09
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FileOccurrence
        {
            /// <summary>
            /// 
            /// </summary>
            FIRST_OR_ONLY = 0x00,
            /// <summary>
            /// 
            /// </summary>
            LAST = 0x01,
            /// <summary>
            /// 
            /// </summary>
            NEXT = 0x02,
            /// <summary>
            /// 
            /// </summary>
            PREVIOUS = 0x03
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FileControlInformation
        {
            /// <summary>
            /// 
            /// </summary>
            RETURN_FCI = 0x00,
            /// <summary>
            /// 
            /// </summary>
            RETURN_FCP = 0x04,
            /// <summary>
            /// 
            /// </summary>
            RETURN_FMD = 0x08,
            /// <summary>
            /// 
            /// </summary>
            NORESPONSE_OR_PROPRIETARY = 0x0C
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public SelectionMode selectionMode
        {
            set { p1 = (Byte)value; }
            get { return (SelectionMode)p1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileOccurrence fileOccurence
        {
            set { p2 = (Byte)((p2 & 0xFC) | (int)value); }
            get { return (FileOccurrence)(p2 & 0x03); }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileControlInformation fileControlInformation
        {
            set { p2 = (Byte)((p2 & 0x03) | (int)value); }
            get { return (FileControlInformation)(p2 & 0xFC); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SelectCommand()
            : base()
        {
            ins = 0xA4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectionMode"></param>
        /// <param name="fileOccurence"></param>
        /// <param name="fci"></param>
        /// <param name="udc"></param>
        public SelectCommand(SelectionMode selectionMode, FileOccurrence fileOccurence, FileControlInformation fci, Byte[] udc) :
            this()
        {
            this.selectionMode = selectionMode;
            this.fileControlInformation = fci;
            this.fileOccurence = fileOccurence;
            this.udc = udc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectionMode"></param>
        /// <param name="fileOccurence"></param>
        /// <param name="fci"></param>
        /// <param name="udc"></param>
        /// <param name="le"></param>
        public SelectCommand(SelectionMode selectionMode, FileOccurrence fileOccurence, FileControlInformation fci, Byte[] udc, uint le) :
            this(selectionMode, fileOccurence, fci, udc)
        {
            this.le = le;
        }

        #endregion
    }
}
