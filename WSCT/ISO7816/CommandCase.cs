﻿namespace WSCT.ISO7816
{
    /// <summary>
    /// Enumeration of the 4 Command Cases.
    /// </summary>
    public enum CommandCase : byte
    {
        /// <summary>
        /// Unknow command case (should be an anormal or temporary state).
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Command case 1: no (Lc UDC) and Le in C-APDU, no UDR in R-APDU.
        /// </summary>
        CC1 = 1,

        /// <summary>
        /// Command case 2: no (Lc UDC) in C-APDU, Le found in C-APDU, UDR found in R-APDU.
        /// </summary>
        CC2 = 2,

        /// <summary>
        /// Command case 3: no Le in C-APDU, (Lc UDC) found in  C-APDU, no UDR in R-APDU.
        /// </summary>
        CC3 = 3,

        /// <summary>
        /// Command case 4: (Lc UDC) and Le found in C-APDU, UDR found in R-APDU.
        /// </summary>
        CC4 = 4,
        
        /// <summary>
        /// Command Case 2 Extended: no (Lc UDC) in C-APDU, Le found in C-APDU, UDR found in R-APDU.
        /// </summary>
        CC2E = 5,
        
        /// <summary>
        /// Command case 3 extended: no Le in C-APDU, (Lc UDC) found in  C-APDU, no UDR in R-APDU.
        /// </summary>
        CC3E = 6,
        
        /// <summary>
        /// Command case 4 extended: (Lc UDC) and Le found in C-APDU, UDR found in R-APDU.
        /// </summary>        
        CC4E = 7
    }
}