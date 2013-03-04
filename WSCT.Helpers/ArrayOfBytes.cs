using System;
using System.Collections.Generic;
using System.Text;

namespace WSCT.Helpers
{
    /// <summary>
    /// Allows simple manipulation and conversion between Byte[] and String types data.
    /// <para><c>ArrayOfBytes</c> now provides extension methods on <c>Byte[]</c> and <c>String</c></para>
    /// </summary>
    public static class ArrayOfBytes
    {
        /// <summary>
        /// Default separator to be used for <c>toHexa(...)</c> methods
        /// </summary>
        static public Char defaultSeparator = ' ';

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>Byte[]</c>
        /// </summary>
        /// <param name="value">Source data to convert</param>
        /// <returns>A new <c>Byte[]</c></returns>
        /// <example>
        ///     <code>
        ///     Byte value = 0x12;
        ///     Byte[] data = value.toByteArray();
        ///     // now data = {0x12}
        ///     </code>
        /// </example>
        static public Byte[] toByteArray(this Byte value)
        {
            Byte[] byteArray = new Byte[1];
            byteArray[0] = value;
            return byteArray;
        }

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>Byte[]</c>, which length is given
        /// </summary>
        /// <param name="value">Source data to convert</param>
        /// <param name="length">Length of the  to create</param>
        /// <returns>A new <c>Byte[]</c></returns>
        /// <example>
        ///     <code>
        ///     UInt32 value = 0x12345678;
        ///     Byte[] data = value.toByteArray(4);
        ///     // now data = {0x12,0x34,0x56,0x78}
        ///     </code>
        ///     <code>
        ///     UInt32 value = 0x12;
        ///     Byte[] data = value.toByteArray(2);
        ///     // now data = {0x00,0x12}
        ///     </code>
        /// </example>
        static public Byte[] toByteArray(this UInt32 value, int length)
        {
            Byte[] byteArray = new Byte[length];
            for (int i = length - 1; i >= 0; i--)
            {
                byteArray[i] = (Byte)(value % 0x100);
                value /= 0x100;
            }
            return byteArray;
        }

        /// <summary>
        /// Converts a String representing hexadecimal values into a <c>Byte[]</c>
        /// </summary>
        /// <param name="hexa">Source data to convert</param>
        /// <returns>A new Byte[]</returns>
        /// <example>
        ///     <code>
        ///     String str = "12 34 56";
        ///     Byte[] data = ArrayOfBytes.fromHexa(str);
        ///     // Now data = {0x12,0x34,0x56}
        ///     </code>
        ///     Example with odd number of digits
        ///     <code>
        ///     String str = "23456";
        ///     Byte[] data = ArrayOfBytes.fromHexa(str);
        ///     // Now data = {0x02,0x34,0x56}
        ///     </code>
        /// </example>
        static public Byte[] fromHexa(this String hexa)
        {
            Byte[] bytes;

            hexa = hexa.Replace(" ", "");
            hexa = hexa.Replace("-", "");

            if (hexa.Length % 2 == 0)
            {
                bytes = new Byte[hexa.Length / 2];
                for (int index = 0; index < hexa.Length / 2; index++)
                    bytes[index] = Byte.Parse(hexa.Substring(2 * index, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                bytes = new Byte[hexa.Length / 2 + 1];
                bytes[0] = Byte.Parse(hexa.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
                for (int index = 1; index < hexa.Length / 2 + 1; index++)
                    bytes[index] = Byte.Parse(hexa.Substring(2 * index - 1, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }

        /// <summary>
        /// Converts a String into a <c>Byte[]</c>
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A new array of bytes where each character of <paramref name="buffer"/> is encoded as its ascii value</returns>
        /// <example>
        ///     <code>
        ///     String str = "123";
        ///     Byte[] data = ArrayOfBytes.toString(str);
        ///     // Now data = {0x31,0x32,0x33}
        ///     </code>
        /// </example>
        static public Byte[] fromString(this String buffer)
        {
            Byte[] byteBuffer = new Byte[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
                byteBuffer[i] = Convert.ToByte(buffer[i]);
            return byteBuffer;
        }

        /// <summary>
        /// Converts an array of bytes to the equivalent String in Hexa
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value</returns>
        /// <example>
        ///     <code>
        ///     Byte[] data = new Byte[] {0x31,0x32,0x33};
        ///     String str = ArrayOfBytes.toHexa(data);
        ///     // Now str = "31 32 33"
        ///     </code>
        /// </example>
        static public String toHexa(this Byte[] buffer)
        {
            return (buffer == null ? "" : toHexa(buffer, buffer.Length, defaultSeparator));
        }

        /// <summary>
        /// Converts an array of bytes to the equivalent String in Hexa
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value. <c>0</c> means no separator.</returns>
        /// <example>
        ///     <code>
        ///     Byte[] data = new Byte[] {0x31,0x32,0x33};
        ///     String str = ArrayOfBytes.toHexa(data);
        ///     // Now str = "31 32 33"
        ///     </code>
        /// </example>
        static public String toHexa(this Byte[] buffer, Char separator)
        {
            return (buffer == null ? "" : toHexa(buffer, buffer.Length, separator));
        }

        /// <summary>
        /// Converts first bytes of a Byte Array to the equivalent String in Hexa
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <param name="size">Maximum number of bytes in the array to convert</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     Byte[] data = new Byte[] {0x31,0x32,0x33};
        ///     String str = ArrayOfBytes.toHexa(data, 2);
        ///     // Now str = "31 32"
        ///     </code>
        /// </example>
        static public String toHexa(this Byte[] buffer, int size)
        {
            return (buffer == null ? "" : toHexa(buffer, size, defaultSeparator));
        }

        /// <summary>
        /// Converts first bytes of a Byte Array to the equivalent String in Hexa
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <param name="size">Maximum number of bytes in the array to convert</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     Byte[] data = new Byte[] {0x31,0x32,0x33};
        ///     String str = ArrayOfBytes.toHexa(data, 2, '-');
        ///     // Now str = "31-32"
        ///     </code>
        /// </example>
        static public String toHexa(this Byte[] buffer, int size, Char separator)
        {
            StringBuilder s = new StringBuilder();
            if (size > buffer.Length) size = buffer.Length;
            if (size > 0)
                s.AppendFormat("{0:X2}", buffer[0]);
            if (separator == 0)
            {
                for (int i = 1; i < size; i++)
                    s.AppendFormat("{0:X2}", buffer[i]);
            }
            else
            {
                for (int i = 1; i < size; i++)
                    s.AppendFormat("{1}{0:X2}", buffer[i], separator);
            }
            return s.ToString();
        }

        /// <summary>
        /// Converts an array of bytes to the equivalent String (Byte > Char)
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A String representation of the array where each byte is represented as a character (global default encoding)</returns>
        /// <example>
        ///     <code>
        ///     Byte[] data = new Byte[] {0x31,0x32,0x33};
        ///     String str = ArrayOfBytes.toString(data);
        ///     // Now str = "123"
        ///     </code>
        /// </example>
        static public String toString(this Byte[] buffer)
        {
            StringBuilder s = new StringBuilder();
            if (buffer != null)
            {
                for (int i = 0; i < buffer.Length; i++)
                    s.Append(Convert.ToChar(buffer[i]));
            }
            return s.ToString();
        }

        /// <summary>
        /// Converts an array of bytes to an array of bytes "BCD coded"
        /// </summary>
        /// <param name="bytes">Source array of bytes</param>
        /// <returns>A new array of bytes "BCD coded"</returns>
        /// <example>
        /// <code>Byte[] bytes = new Byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }</code>
        /// <c>ArrayOfBytes.toBCD(bytes)</c> ==> <c>{ 0x12, 0x34, 0x56, 0x78, 0x90 }</c>
        /// </example>
        static public Byte[] toBCD(this Byte[] bytes)
        {
            return toBCD(bytes, 0x0);
        }

        /// <summary>
        /// Converts an array of bytes to an array of bytes "BCD coded"
        /// </summary>
        /// <param name="bytes">Source array of bytes</param>
        /// <param name="filler">Value to be used a a filler is length is odd</param>
        /// <returns>A new array of bytes "BCD coded"</returns>
        /// <example>
        /// <code>Byte[] bytes = new Byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }</code>
        /// <c>ArrayOfBytes.toBCD(bytes, 0xF)</c> ==> <c>{ 0x12, 0x34, 0x56, 0x78, 0x9F }</c>
        /// </example>
        static public Byte[] toBCD(this Byte[] bytes, Byte filler)
        {
            Byte[] bcd = new Byte[bytes.Length / 2 + bytes.Length % 2];

            int index;
            for (index = 0; index + 1 < bytes.Length; index += 2)
            {
                bcd[index / 2] = (Byte)((bytes[index] << 4) + bytes[index + 1]);
            }
            if (bytes.Length % 2 == 1)
            {
                bcd[index / 2] = (Byte)((bytes[index] << 4) + filler);
                index += 2;
            }

            return bcd;
        }

        /// <summary>
        /// Converts an array of bytes to a string representation "BCD coded"
        /// </summary>
        /// <param name="bytes">Source array of bytes</param>
        /// <param name="filler">Value to be used a a filler is length is odd</param>
        /// <returns>A new array of bytes "BCD coded"</returns>
        /// <example>
        /// <code>Byte[] bytes = new Byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }</code>
        /// <c>ArrayOfBytes.toBCD(bytes, 'F')</c> ==> <c>"123456789F"</c>
        /// </example>
        static public String toBCD(this Byte[] bytes, Char filler)
        {
            StringBuilder bcd = new StringBuilder(bytes.Length);

            foreach (Byte b in bytes)
            {
                bcd.Append(b);
            }
            if (bytes.Length % 2 == 1)
            {
                bcd.Append(filler);
            }

            return bcd.ToString();
        }

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>Byte[] bcd = new Byte[5] { 0x12, 0x34, 0x56, 0x78, 0x90 }</code>
        ///     <c>ArrayOfBytes.fromBCD(bcd, 9)</c> ==> <c>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 }</c>
        /// </example>
        static public Byte[] fromBCD(this Byte[] bcd)
        {
            return fromBCD(bcd, (UInt32)bcd.Length);
        }

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>Byte[] bcd = new Byte[5] { 0x12, 0x34, 0x56, 0x78, 0x90 }</code>
        ///     <c>ArrayOfBytes.fromBCD(bcd, 7)</c> ==> <c>{ 1, 2, 3, 4, 5, 6, 7 }</c>
        /// </example>
        static public Byte[] fromBCD(this Byte[] bcd, UInt32 length)
        {
            Byte[] bytes = new Byte[length];

            int i;
            for (i = 0; i + 1 < length; i += 2)
            {
                bytes[i] = (Byte)((bcd[i / 2] & 0xF0) >> 4);
                bytes[i + 1] = (Byte)(bcd[i / 2] & 0x0F);
            }
            if (length % 2 == 1)
            {
                bytes[i] = (Byte)((bcd[i / 2] & 0xF0) >> 4);
            }

            return bytes;
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>String bcd = "1234567890"</code>
        ///     <c>ArrayOfBytes.fromBCD(bcd)</c> ==> <c>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }</c>
        /// </example>
        static public Byte[] fromBCD(this String bcd)
        {
            return fromBCD(bcd, (UInt32)bcd.Length);
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>String bcd = "1234567890"</code>
        ///     <c>ArrayOfBytes.fromBCD(bcd, 7)</c> ==> <c>{ 1, 2, 3, 4, 5, 6, 7 }</c>
        /// </example>
        static public Byte[] fromBCD(this String bcd, UInt32 length)
        {
            Byte[] bytes = new Byte[length];

            for (int i = 0; i < length; i++)
            {
                bytes[i] = Byte.Parse(bcd[i].ToString());
            }

            return bytes;
        }

    }
}