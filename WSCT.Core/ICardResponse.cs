using System;
using System.Collections.Generic;
using System.Text;

namespace WSCT.Core.APDU
{

    /// <summary>
    /// Interface for generic R-APDU, ie response of smartcard.
    /// </summary>
    public interface ICardResponse
    {
        /// <summary>
        /// Parses a raw response <paramref name="rAPDU"/> coming from the smartcard.
        /// </summary>
        /// <param name="rAPDU">R-APDU received from the smartcard</param>
        /// <returns>An instance of the <see cref="ICardResponse"/> representation of the R-APDU (<c>this</c>)</returns>
        ICardResponse parse(Byte[] rAPDU);

        /// <summary>
        /// Parses part of a raw response <paramref name="rAPDU"/> coming from the smartcard.
        /// </summary>
        /// <param name="rAPDU">R-APDU received from the smartcard</param>
        /// <param name="size">Number of bytes from <paramref name="rAPDU"/> to parse</param>
        /// <returns>An instance of the <see cref="ICardResponse"/> representation of the R-APDU (<c>this</c>)</returns>
        ICardResponse parse(byte[] rAPDU, UInt32 size);

        /// <summary>
        /// Parses part of a raw hexa string response <paramref name="rAPDU"/> coming from the smartcard.
        /// </summary>
        /// <param name="rAPDU">R-APDU received from the smartcard, represented by a <c>String</c> of hexadecimal values</param>
        /// <returns>An instance of the <see cref="ICardResponse"/> representation of the R-APDU (<c>this</c>)</returns>
        ICardResponse parse(String rAPDU);
    }
}