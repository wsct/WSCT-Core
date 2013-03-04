using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using WSCT.Core;
using WSCT.Core.APDU;

namespace WSCT.ISO7816
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("CommandResponsePair")]
    public class CommandResponsePair
    {
        #region >> Fields

        CommandAPDU _cAPDU;
        ResponseAPDU _rAPDU;

        Wrapper.ErrorCode _errorCode;

        #endregion

        #region >> Properties

        /// <summary>
        /// Command APDU of the Command Response Pair
        /// </summary>
        [XmlElement("commandAPDU")]
        public CommandAPDU cAPDU
        {
            get
            {
                if (_cAPDU == null)
                    _cAPDU = new CommandAPDU();
                return _cAPDU;
            }
            set { _cAPDU = value; }
        }
        /// <summary>
        /// Response APDU of the Command Response Pair
        /// </summary>
        [XmlElement("responseAPDU")]
        public ResponseAPDU rAPDU
        {
            get
            {
                if (_rAPDU == null)
                    _rAPDU = new ResponseAPDU();
                return _rAPDU;
            }
            set { _rAPDU = value; }
        }

        /// <summary>
        /// Error code of last transmitted C-APDU.
        /// </summary>
        [XmlIgnore]
        public Wrapper.ErrorCode errorCode
        {
            get { return _errorCode; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommandResponsePair()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cAPDU">Command APDU of the new CRP</param>
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
        public Wrapper.ErrorCode transmit(ICardChannel cardChannel)
        {
            ICardResponse cardResponse = (ICardResponse)rAPDU;
            _errorCode = cardChannel.transmit(cAPDU, rAPDU);
            return errorCode;
        }

        #endregion
    }
}
