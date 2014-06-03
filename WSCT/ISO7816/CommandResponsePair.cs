using System.Xml.Serialization;
using WSCT.Core;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("CommandResponsePair")]
    public class CommandResponsePair
    {
        #region >> Fields

        private CommandAPDU _cAPDU;

        private ErrorCode _errorCode;
        private ResponseAPDU _rAPDU;

        #endregion

        #region >> Properties

        /// <summary>
        /// Command APDU of the Command Response Pair.
        /// </summary>
        [XmlElement("commandAPDU")]
        public CommandAPDU CApdu
        {
            get { return _cAPDU ?? (_cAPDU = new CommandAPDU()); }
            set { _cAPDU = value; }
        }

        /// <summary>
        /// Response APDU of the Command Response Pair.
        /// </summary>
        [XmlElement("responseAPDU")]
        public ResponseAPDU RApdu
        {
            get { return _rAPDU ?? (_rAPDU = new ResponseAPDU()); }
            set { _rAPDU = value; }
        }

        /// <summary>
        /// Error code of last transmitted C-APDU.
        /// </summary>
        [XmlIgnore]
        public ErrorCode ErrorCode
        {
            get { return _errorCode; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CommandResponsePair()
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="cAPDU">Command APDU of the new CRP.</param>
        public CommandResponsePair(CommandAPDU cAPDU)
        {
            _cAPDU = cAPDU;
        }

        #endregion

        #region >> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardChannel"></param>
        /// <returns></returns>
        public ErrorCode Transmit(ICardChannel cardChannel)
        {
            _errorCode = cardChannel.Transmit(CApdu, RApdu);
            return ErrorCode;
        }

        #endregion
    }
}