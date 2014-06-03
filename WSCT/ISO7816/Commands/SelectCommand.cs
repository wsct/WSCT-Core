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
        public enum FileControlInformation
        {
            /// <summary>
            /// 
            /// </summary>
            ReturnFci = 0x00,

            /// <summary>
            /// 
            /// </summary>
            ReturnFcp = 0x04,

            /// <summary>
            /// 
            /// </summary>
            ReturnFmd = 0x08,

            /// <summary>
            /// 
            /// </summary>
            NoResponseOrProprietary = 0x0C
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FileOccurrence
        {
            /// <summary>
            /// 
            /// </summary>
            FirstOrOnly = 0x00,

            /// <summary>
            /// 
            /// </summary>
            Last = 0x01,

            /// <summary>
            /// 
            /// </summary>
            Next = 0x02,

            /// <summary>
            /// 
            /// </summary>
            Previous = 0x03
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SelectionMode
        {
            /// <summary>
            /// 
            /// </summary>
            File = 0x00,

            /// <summary>
            /// 
            /// </summary>
            ChildDF = 0x01,

            /// <summary>
            /// 
            /// </summary>
            ChildEF = 0x02,

            /// <summary>
            /// 
            /// </summary>
            ParentDF = 0x03,

            /// <summary>
            /// 
            /// </summary>
            SelectDFName = 0x04,

            /// <summary>
            /// 
            /// </summary>
            SelectFromMF = 0x08,

            /// <summary>
            /// 
            /// </summary>
            SelectFromCurrentDF = 0x09
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public SelectionMode Selection
        {
            set { P1 = (byte)value; }
            get { return (SelectionMode)P1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileOccurrence Occurence
        {
            set { P2 = (byte)((P2 & 0xFC) | (int)value); }
            get { return (FileOccurrence)(P2 & 0x03); }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileControlInformation Information
        {
            set { P2 = (byte)((P2 & 0x03) | (int)value); }
            get { return (FileControlInformation)(P2 & 0xFC); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public SelectCommand()
        {
            Ins = 0xA4;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="occurence"></param>
        /// <param name="fci"></param>
        /// <param name="udc"></param>
        public SelectCommand(SelectionMode selection, FileOccurrence occurence, FileControlInformation fci, byte[] udc) :
            this()
        {
            Selection = selection;
            Information = fci;
            Occurence = occurence;
            Udc = udc;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="occurence"></param>
        /// <param name="fci"></param>
        /// <param name="udc"></param>
        /// <param name="le"></param>
        public SelectCommand(SelectionMode selection, FileOccurrence occurence, FileControlInformation fci, byte[] udc, uint le) :
            this(selection, occurence, fci, udc)
        {
            Le = le;
        }

        #endregion
    }
}