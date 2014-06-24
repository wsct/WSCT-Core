using System;

namespace WSCT.ISO7816.AnswerToReset
{
    public class MalformedAtrException : Exception
    {
        public InterfaceId? ByteId = null;

        #region >> Constructors

        /// <inheritdoc />
        public MalformedAtrException()
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="byteId"></param>
        public MalformedAtrException(InterfaceId byteId)
            : base(String.Format("Parsing error looking for {0}", byteId))
        {
            ByteId = byteId;
        }

        /// <inheritdoc />
        public MalformedAtrException(string message)
            : base(message)
        {
        }

        #endregion
    }
}