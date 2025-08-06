using System;
using System.Globalization;
using System.Text;

namespace WSCT.Helpers
{
    /// <summary>
    /// Helper class allowing simple manipulation and conversion between <see cref="byte"/>[] and <see cref="string"/> types data.
    /// </summary>
    public static class BytesHelpers
    {
        private const char DefaultHexaSeparator = ' ';
        private const byte DefaultBcdFiller = 0x00;
        private const char DefaultBcdCharFiller = 'F';

        /// <summary>
        /// Set targeted bits to 0 or 1 in <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mask">Targeted bits are to be set to 1 in the mask.</param>
        /// <param name="setBitTo1">Set masked bits to 1 if the value is <c>true</c> or to 0 if <c>false</c>.</param>
        /// <returns></returns>
        public static byte SetBits(this byte value, byte mask, bool setBitTo1)
        {
            if (setBitTo1)
            {
                return (byte)(value | mask);
            }

            return (byte)(value & (0xFF ^ mask));
        }

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>byte[]</c>.
        /// </summary>
        /// <param name="value">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c>.</returns>
        /// <example>
        ///     <code>
        ///     byte value = 0x12;
        ///     byte[] data = value.toByteArray();
        ///     // now data = { 0x12 }
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this byte value)
        {
            return new[] { value };
        }

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>byte[]</c>, which length is given.
        /// </summary>
        /// <param name="value">Source data to convert.</param>
        /// <param name="length">Length of the array to create.</param>
        /// <returns>A new <c>byte[]</c></returns>
        /// <example>
        ///     <code>
        ///     UInt32 value = 0x12345678;
        ///     byte[] data = value.toByteArray(4);
        ///     // now data = { 0x12, 0x34, 0x56, 0x78}
        ///     </code>
        ///     <code>
        ///     UInt32 value = 0x12;
        ///     byte[] data = value.toByteArray(2);
        ///     // now data = { 0x00, 0x12 }
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this UInt32 value, int length)
        {
            var byteArray = new byte[length];

            for (var i = length - 1; i >= 0; i--)
            {
                byteArray[i] = (byte)(value % 0x100);
                value /= 0x100;
            }

            return byteArray;
        }

        /// <summary>
        /// Converts a string representing hexadecimal values into a <c>byte[]</c>.
        /// </summary>
        /// <param name="hexa">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c>.</returns>
        /// <example>
        ///     <code>
        ///     string str = "12 34 56";
        ///     byte[] data = str.fromHexa();
        ///     // Now data = { 0x12, 0x34, 0x56 }
        ///     </code>
        ///     Example with odd number of digits
        ///     <code>
        ///     string str = "23456";
        ///     byte[] data = str.fromHexa();
        ///     // Now data = { 0x02, 0x34, 0x56 }
        ///     </code>
        /// </example>
        public static byte[] FromHexa(this string hexa)
        {
            byte[] bytes;

            hexa = hexa.Replace(" ", "");
            hexa = hexa.Replace("-", "");

            if (hexa.Length % 2 == 0)
            {
                bytes = new byte[hexa.Length / 2];
                for (var index = 0; index < hexa.Length / 2; index++)
                {
                    bytes[index] = Byte.Parse(hexa.Substring(2 * index, 2), NumberStyles.HexNumber);
                }
            }
            else
            {
                bytes = new byte[hexa.Length / 2 + 1];
                bytes[0] = Byte.Parse(hexa.Substring(0, 1), NumberStyles.HexNumber);
                for (var index = 1; index < hexa.Length / 2 + 1; index++)
                {
                    bytes[index] = Byte.Parse(hexa.Substring(2 * index - 1, 2), NumberStyles.HexNumber);
                }
            }

            return bytes;
        }

        /// <summary>
        /// Converts a string into a <c>byte[]</c>.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c> where each character of <paramref name="buffer"/> is encoded as its ascii value.</returns>
        /// <example>
        ///     <code>
        ///     string str = "123";
        ///     byte[] data = str.ToAsciiString();
        ///     // Now data = { 0x31, 0x32, 0x33 }
        ///     </code>
        /// </example>
        public static byte[] FromString(this string buffer)
        {
            var byteBuffer = new byte[buffer.Length];

            for (var i = 0; i < buffer.Length; i++)
            {
                byteBuffer[i] = Convert.ToByte(buffer[i]);
            }

            return byteBuffer;
        }

        #region ** ToHexa

        /// <summary>
        /// Converts an array of bytes to the equivalent string in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits.</param>
        /// <returns>A string representation of the array where each byte is represented as its hexadecimal value. <c>0</c> means no separator.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa('-');
        ///     // Now data = "31-32-33"
        ///     </code>
        /// </example>
        public static string ToHexa(this ReadOnlySpan<byte> buffer, char separator = DefaultHexaSeparator)
        {
            var length = buffer.Length;

            if (length == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder(separator == 0 ? 2 * length : 2 + 3 * (length - 1));

            if (length > 0)
            {
                stringBuilder.AppendFormat("{0:X2}", buffer[0]);
            }

            if (separator == '\0')
            {
                for (var i = 1; i < length; i++)
                {
                    stringBuilder.AppendFormat("{0:X2}", buffer[i]);
                }
            }
            else
            {
                for (var i = 1; i < length; i++)
                {
                    stringBuilder.AppendFormat("{1}{0:X2}", buffer[i], separator);
                }
            }

            return stringBuilder.ToString();
        }

        /// <inheritdoc cref="ToHexa(ReadOnlySpan{byte},char)" />
        public static string ToHexa(this Span<byte> buffer, char separator = DefaultHexaSeparator)
        {
            return ((ReadOnlySpan<byte>)buffer).ToHexa(separator);
        }

        /// <inheritdoc cref="ToHexa(ReadOnlySpan{byte},char)" />
        public static string ToHexa(this byte[] buffer, char separator = DefaultHexaSeparator)
        {
            return new ReadOnlySpan<byte>(buffer).ToHexa(separator);
        }

        /// <summary>
        /// Converts first bytes of a byte Array to the equivalent string in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <param name="length">Maximum number of bytes in the array to convert.</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits.</param>
        /// <returns>A string representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa(2, '-');
        ///     // Now data = "31-32"
        ///     </code>
        /// </example>
        public static string ToHexa(this ReadOnlySpan<byte> buffer, int length, char separator = DefaultHexaSeparator)
        {
            return buffer.Slice(0, length).ToHexa(separator);
        }

        /// <inheritdoc cref="ToHexa(ReadOnlySpan{byte},int,char)" />
        public static string ToHexa(this Span<byte> buffer, int length, char separator = DefaultHexaSeparator)
        {
            return ((ReadOnlySpan<byte>)buffer).ToHexa(length, separator);
        }

        /// <inheritdoc cref="ToHexa(ReadOnlySpan{byte},int,char)" />
        public static string ToHexa(this byte[] buffer, int length, char separator = DefaultHexaSeparator)
        {
            return new ReadOnlySpan<byte>(buffer, 0, length).ToHexa(separator);
        }

        #endregion

        #region ** ToAsciiString

        /// <summary>
        /// Converts a <c>byte[]</c> to the equivalent string (byte > char).
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <returns>A string representation of the array where each byte is represented as a character (global default encoding).</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.ToAsciiString();
        ///     // Now data = "123"
        ///     </code>
        /// </example>
        public static string ToAsciiString(this ReadOnlySpan<byte> buffer)
        {
            if (buffer.IsEmpty)
            {
                return String.Empty;
            }

            var s = new StringBuilder();

            foreach (var b in buffer)
            {
                s.Append(Convert.ToChar(b));
            }

            return s.ToString();
        }

        /// <inheritdoc cref="ToAsciiString(ReadOnlySpan{byte})" />
        public static string ToAsciiString(this Span<byte> buffer)
        {
            return ((ReadOnlySpan<byte>)buffer).ToAsciiString();
        }

        /// <inheritdoc cref="ToAsciiString(ReadOnlySpan{byte})" />
        public static string ToAsciiString(this byte[] buffer)
        {
            return new ReadOnlySpan<byte>(buffer).ToAsciiString();
        }

        #endregion

        #region ** ToBcd

        /// <summary>
        /// Converts an array of bytes to an array of bytes "BCD coded".
        /// </summary>
        /// <param name="bytes">Source array of bytes.</param>
        /// <param name="filler">Value to be used a a filler is length is odd.</param>
        /// <returns>A new array of bytes "BCD coded".</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ///     byte[] data = value.toBcd(0xF);
        ///     // Now data = { 0x12, 0x34, 0x56, 0x78, 0x9F }
        ///     </code>
        /// </example>
        public static byte[] ToBcd(this ReadOnlySpan<byte> bytes, byte filler = DefaultBcdFiller)
        {
            var bcd = new byte[bytes.Length / 2 + bytes.Length % 2];

            int index;
            for (index = 0; index + 1 < bytes.Length; index += 2)
            {
                bcd[index / 2] = (byte)((bytes[index] << 4) + bytes[index + 1]);
            }

            if (bytes.Length % 2 == 1)
            {
                bcd[index / 2] = (byte)((bytes[index] << 4) + filler);
            }

            return bcd;
        }

        /// <inheritdoc cref="ToBcd(ReadOnlySpan{byte},byte)" />
        public static byte[] ToBcd(this Span<byte> bytes, byte filler = DefaultBcdFiller)
        {
            return ((ReadOnlySpan<byte>)bytes).ToBcd(filler);
        }

        /// <inheritdoc cref="ToBcd(ReadOnlySpan{byte},byte)" />
        public static byte[] ToBcd(this byte[] bytes, byte filler = DefaultBcdFiller)
        {
            return new ReadOnlySpan<byte>(bytes).ToBcd(filler);
        }

        #endregion

        #region ** ToBcdString

        /// <summary>
        /// Converts an array of bytes to a string representation "BCD coded"
        /// </summary>
        /// <param name="bytes">Source array of bytes</param>
        /// <param name="filler">Value to be used a a filler is length is odd</param>
        /// <returns>A new array of bytes "BCD coded"</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ///     string data = value.toBcd('F');
        ///     // Now data = "123456789F"
        ///     </code>
        /// </example>
        public static string ToBcdString(this ReadOnlySpan<byte> bytes, char filler = DefaultBcdCharFiller)
        {
            var bcd = new StringBuilder(bytes.Length);

            foreach (var b in bytes)
            {
                bcd.Append(b);
            }

            if (bytes.Length % 2 == 1)
            {
                bcd.Append(filler);
            }

            return bcd.ToString();
        }

        /// <inheritdoc cref="ToBcdString(ReadOnlySpan{byte},char)" />
        public static string ToBcdString(this Span<byte> bytes, char filler = DefaultBcdCharFiller)
        {
            return ((ReadOnlySpan<byte>)bytes).ToBcdString(filler);
        }

        /// <inheritdoc cref="ToBcdString(ReadOnlySpan{byte},char)"/>
        public static string ToBcdString(this byte[] bytes, char filler = DefaultBcdCharFiller)
        {
            return new ReadOnlySpan<byte>(bytes).ToBcdString(filler);
        }

        #endregion

        #region ** FromBcd

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     byte[] source = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90 };
        ///     byte[] data = source.fromBcd(9);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this ReadOnlySpan<byte> bcd)
        {
            return bcd.FromBcd((UInt32)bcd.Length * 2);
        }

        /// <inheritdoc cref="FromBcd(ReadOnlySpan{byte})" />
        public static byte[] FromBcd(this Span<byte> bcd)
        {
            return ((ReadOnlySpan<byte>)bcd).FromBcd();
        }

        /// <inheritdoc cref="FromBcd(ReadOnlySpan{byte})" />
        public static byte[] FromBcd(this byte[] bcd)
        {
            return new ReadOnlySpan<byte>(bcd).FromBcd();
        }

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90 };
        ///     byte[] data = value.fromBcd(7);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this ReadOnlySpan<byte> bcd, UInt32 length)
        {
            var bytes = new byte[length];

            int i;
            for (i = 0; i + 1 < length; i += 2)
            {
                bytes[i] = (byte)((bcd[i / 2] & 0xF0) >> 4);
                bytes[i + 1] = (byte)(bcd[i / 2] & 0x0F);
            }

            if (length % 2 == 1)
            {
                bytes[i] = (byte)((bcd[i / 2] & 0xF0) >> 4);
            }

            return bytes;
        }

        /// <inheritdoc cref="FromBcd(ReadOnlySpan{byte},UInt32)" />
        public static byte[] FromBcd(this Span<byte> bcd, UInt32 length)
        {
            return ((ReadOnlySpan<byte>)bcd).FromBcd(length);
        }

        /// <inheritdoc cref="FromBcd(ReadOnlySpan{byte},UInt32)" />
        public static byte[] FromBcd(this byte[] bcd, UInt32 length)
        {
            return new ReadOnlySpan<byte>(bcd).FromBcd(length);
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     string value = "1234567890";
        ///     byte[] data = value.fromBcd();
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this string bcd)
        {
            return bcd.FromBcd((UInt32)bcd.Length);
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     string value = "1234567890";
        ///     byte[] data = value.fromBcd(7);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this string bcd, UInt32 length)
        {
            var bytes = new byte[length];

            for (var i = 0; i < length; i++)
            {
                bytes[i] = Byte.Parse(bcd[i].ToString());
            }

            return bytes;
        }

        #endregion

        #region ** Get4BytesPrefixedArray

        /// <summary>
        /// Extract an array of bytes prefixed by a 4 bytes coded length from a bigger array.
        /// ... 'LL LL LL LL' 'VV VV ... VV' ...
        /// </summary>
        /// <param name="bytes">Source array of bytes.</param>
        /// <param name="offset">Input: Offset of the first byte to look for in <paramref name="bytes"/>. Output: Offset of next byte after the retrieved array.</param>
        /// <returns></returns>
        public static byte[] Get4BytesPrefixedArray(this ReadOnlySpan<byte> bytes, ref int offset)
        {
            if (bytes.IsEmpty)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length - offset < 4)
            {
                throw new Exception($"Error: Can't get {4} bytes at offset {offset}.");
            }

            // First 4 bytes are the length of data
            var length = bytes[offset + 0] * 0x01000000 + bytes[offset + 1] * 0x00010000 + bytes[offset + 2] * 0x00000100 + bytes[offset + 3];
            offset += 4;

            if (bytes.Length - offset < length)
            {
                throw new Exception($"Error: Can't get {length} bytes at offset {offset}.");
            }

            // Extract value
            var value = bytes.Slice(offset, length).ToArray();
            offset += length;

            return value;
        }

        /// <inheritdoc cref="Get4BytesPrefixedArray(System.ReadOnlySpan{byte},ref int)" />
        public static byte[] Get4BytesPrefixedArray(this Span<byte> bytes, ref int offset)
        {
            return ((ReadOnlySpan<byte>)bytes).Get4BytesPrefixedArray(ref offset);
        }

        /// <inheritdoc cref="Get4BytesPrefixedArray(System.ReadOnlySpan{byte},ref int)" />
        public static byte[] Get4BytesPrefixedArray(this byte[] bytes, ref int offset)
        {
            return new ReadOnlySpan<byte>(bytes).Get4BytesPrefixedArray(ref offset);
        }

        #endregion
    }
}