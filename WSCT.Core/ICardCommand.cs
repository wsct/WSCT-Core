using System;

namespace WSCT.Core.APDU
{
    /// <summary>
    /// Interface for generic C-APDU, ie command to send to a smartcard
    /// </summary>
    public interface ICardCommand
    {
        /// <summary>
        /// Raw binary representation of the C-APDU
        /// </summary>
        Byte[] binaryCommand { get; }

        /// <summary>
        /// Raw <b>String</b> representation (hexadecimal values) of the C-APDU
        /// </summary>
        String stringCommand { get; }

        /// <summary>
        /// Parses a raw command <paramref name="cAPDU"/> to send to the smartcard.
        /// </summary>
        /// <param name="cAPDU">C-APDU to send to the smartcard</param>
        /// <returns>An instance of the <see cref="ICardResponse"/> representation of the R-APDU (<b>this</b>)</returns>
        ICardCommand parse(Byte[] cAPDU);

        /// <summary>
        /// Parses part of a raw hexa string command <paramref name="cAPDU"/> to send to the smartcard.
        /// </summary>
        /// <param name="cAPDU">C-APDU to send to the smartcard, represented by a <b>String</b> of hexadecimal values</param>
        /// <returns>An instance of the <see cref="ICardCommand"/> representation of the C-APDU (<b>this</b>)</returns>
        ICardCommand parse(String cAPDU);
    }
}