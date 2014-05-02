using System;
using System.Linq;

namespace WSCT.Helpers.Security
{
    /// <summary>
    /// Body part of RFC 4716: The Secure Shell (SSH) Public Key File Format.
    /// </summary>
    /// <remarks>
    /// RSA:
    ///    BIGNUM *n;              // public modulus
    ///    BIGNUM *e;              // public exponent
    ///    BIGNUM *d;              // private exponent
    ///    BIGNUM *p;              // secret prime factor
    ///    BIGNUM *q;              // secret prime factor
    ///    BIGNUM *dmp1;           // d mod (p-1)
    ///    BIGNUM *dmq1;           // d mod (q-1)
    ///    BIGNUM *iqmp;           // q^-1 mod p
    /// </remarks>
    public class Ssh1PublicKeyBody
    {
        #region >> Properties

        public string Type { get; set; }

        /// <summary>
        /// Public exponent <c>e</c>.
        /// </summary>
        public byte[] E { get; set; }

        /// <summary>
        /// Public modulus <c>n</c>.
        /// </summary>
        public byte[] N { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="base64BodyKey"></param>
        public Ssh1PublicKeyBody(string base64BodyKey)
        {
            ParseBytes(Convert.FromBase64String(base64BodyKey));
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="bodyKey"></param>
        public Ssh1PublicKeyBody(byte[] bodyKey)
        {
            ParseBytes(bodyKey);
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="Ssh1PublicKeyBody"/> instance using the base64 encoded public key.
        /// </summary>
        /// <param name="base64BodyKey"></param>
        /// <returns></returns>
        public static Ssh1PublicKeyBody Create(string base64BodyKey)
        {
            return new Ssh1PublicKeyBody(Convert.FromBase64String(base64BodyKey));
        }

        /// <summary>
        /// Creates a new <see cref="Ssh1PublicKeyBody"/> instance using the byte array.
        /// </summary>
        /// <param name="bodyKey"></param>
        /// <returns></returns>
        public static Ssh1PublicKeyBody Create(byte[] bodyKey)
        {
            return new Ssh1PublicKeyBody(bodyKey);
        }

        private void ParseBytes(byte[] bytes)
        {
            var offset = 0;

            // Type of key
            Type = bytes.Get4BytesPrefixedArray(ref offset).ToAsciiString();

            // Public exponent E
            E = bytes.Get4BytesPrefixedArray(ref offset);

            // Public modulus N
            N = bytes.Get4BytesPrefixedArray(ref offset);
        }

        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format("Type: {1}{0}n: {2}{0}e: {3}{0}",
                Environment.NewLine,
                Type,
                N.ToHexa(),
                E.ToHexa());
        }

        #endregion
    }
}