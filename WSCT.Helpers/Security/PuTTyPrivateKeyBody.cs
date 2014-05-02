using System;
using System.Linq;

namespace WSCT.Helpers.Security
{
    /// <summary>
    /// Body part of puTTy Private Key File Format.
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
    public class PuTTyPrivateKeyBody
    {
        #region >> Properties

        /// <summary>
        /// Private exponent <c>d</c>.
        /// </summary>
        public byte[] D { get; set; }

        /// <summary>
        /// RSA secret prime factor <c>q</c>.
        /// </summary>
        public byte[] P { get; set; }

        /// <summary>
        /// RSA secret prime factor <c>q</c>.
        /// </summary>
        public byte[] Q { get; set; }

        /// <summary>
        /// <c>q^-1 mod p</c>.
        /// </summary>
        public byte[] Iqmp { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="base64BodyKey"></param>
        public PuTTyPrivateKeyBody(string base64BodyKey)
        {
            ParseBytes(Convert.FromBase64String(base64BodyKey));
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="bodyKey"></param>
        public PuTTyPrivateKeyBody(byte[] bodyKey)
        {
            ParseBytes(bodyKey);
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="PuTTyPrivateKeyBody"/> instance using the base64 encoded public key.
        /// </summary>
        /// <param name="base64BodyKey"></param>
        /// <returns></returns>
        public static PuTTyPrivateKeyBody Create(string base64BodyKey)
        {
            return new PuTTyPrivateKeyBody(Convert.FromBase64String(base64BodyKey));
        }

        /// <summary>
        /// Creates a new <see cref="PuTTyPrivateKeyBody"/> instance using the byte array.
        /// </summary>
        /// <param name="bodyKey"></param>
        /// <returns></returns>
        public static PuTTyPrivateKeyBody Create(byte[] bodyKey)
        {
            return new PuTTyPrivateKeyBody(bodyKey);
        }

        private void ParseBytes(byte[] bytes)
        {
            var offset = 0;

            // Private exponent D
            D = bytes.Get4BytesPrefixedArray(ref offset);

            // Iqmp
            Iqmp = bytes.Get4BytesPrefixedArray(ref offset);

            // prime factor Q
            Q = bytes.Get4BytesPrefixedArray(ref offset);

            // prime factor P
            P = bytes.Get4BytesPrefixedArray(ref offset);
        }

        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format("d: {1}{0}p: {2}{0}q: {3}{0}q^-1 mod p: {4}{0}",
                Environment.NewLine,
                D.ToHexa(),
                P.ToHexa(),
                Q.ToHexa(),
                Iqmp.ToHexa());
        }

        #endregion
    }
}