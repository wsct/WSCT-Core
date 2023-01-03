using System;
using WSCT.ISO7816;

namespace WSCT.Core.Fluent.Helpers
{
    public static class CommandResponsePairExtensions
    {
        /// <summary>
        /// Executes the <paramref name="action"/> when <paramref name="predicate"/> is true.
        /// </summary>
        public static CommandResponsePair If(this CommandResponsePair crp, Func<ResponseAPDU, bool> predicate, Action<CommandAPDU, ResponseAPDU> action)
        {
            if (predicate(crp.RApdu))
            {
                action(crp.CApdu, crp.RApdu);
            }

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="action"/> when <paramref name="predicate"/> is true.
        /// </summary>
        public static CommandResponsePair If(this CommandResponsePair crp, Func<CommandAPDU, ResponseAPDU, bool> predicate, Action<CommandAPDU, ResponseAPDU> action)
        {
            if (predicate(crp.CApdu, crp.RApdu))
            {
                action(crp.CApdu, crp.RApdu);
            }

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="onSuccess"/> action when <paramref name="predicate"/> is <c>true</c> or <paramref name="onFailure"/> when <c>false</c>.
        /// </summary>
        public static CommandResponsePair IfElse(this CommandResponsePair crp, Func<ResponseAPDU, bool> predicate, Action<CommandAPDU, ResponseAPDU> onSuccess, Action<CommandAPDU, ResponseAPDU> onFailure)
        {
            if (predicate(crp.RApdu))
            {
                onSuccess(crp.CApdu, crp.RApdu);
            }
            else
            {
                onFailure(crp.CApdu, crp.RApdu);
            }

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="onSuccess"/> action when <paramref name="predicate"/> is <c>true</c> or <paramref name="onFailure"/> when <c>false</c>.
        /// </summary>
        public static CommandResponsePair IfElse(this CommandResponsePair crp, Func<CommandAPDU, ResponseAPDU, bool> predicate, Action<CommandAPDU, ResponseAPDU> onSuccess, Action<CommandAPDU, ResponseAPDU> onFailure)
        {
            if (predicate(crp.CApdu, crp.RApdu))
            {
                onSuccess(crp.CApdu, crp.RApdu);
            }
            else
            {
                onFailure(crp.CApdu, crp.RApdu);
            }

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="action"/> when SW is <paramref name="sw"/>.
        /// </summary>
        public static CommandResponsePair OnStatusWord(this CommandResponsePair crp, int sw, Action<CommandAPDU, ResponseAPDU> action)
        {
            if (crp.RApdu.StatusWord == sw)
            {
                action(crp.CApdu, crp.RApdu);
            }

            return crp;
        }


        /// <summary>
        /// Executes the <paramref name="action"/> when  <paramref name="predicate"/> is <c>true</c>.
        /// </summary>
        public static CommandResponsePair OnStatusWord(this CommandResponsePair crp, Func<ushort, bool> predicate, Action<CommandAPDU, ResponseAPDU> action)
        {
            if (predicate(crp.RApdu.StatusWord))
            {
                action(crp.CApdu, crp.RApdu);
            }

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="action"/>.
        /// </summary>
        public static CommandResponsePair Then(this CommandResponsePair crp, Action<CommandAPDU, ResponseAPDU> action)
        {
            action(crp.CApdu, crp.RApdu);

            return crp;
        }

        /// <summary>
        /// Throws an <see cref="UnexpectedErrorCodeException"> when last ErrorCode is not Success.
        /// </summary>
        public static CommandResponsePair ThrowIfNotSuccess(this CommandResponsePair crp)
        {
            crp.ErrorCode.ThrowIfNotSuccess();

            return crp;
        }

        /// <summary>
        /// Throws an <see cref="UnexpectedStatusWordException"> when last SW is not 0x9000.
        /// </summary>
        public static CommandResponsePair ThrowIfSWNot9000(this CommandResponsePair crp)
        {
            crp.RApdu.ThrowIfSWNot9000();

            return crp;
        }

        /// <summary>
        /// Executes the <paramref name="action"/>.
        /// </summary>
        public static CommandResponsePair WithCommand(this CommandResponsePair crp, Action<CommandAPDU> action)
        {
            action(crp.CApdu);

            return crp;
        }


        /// <summary>
        /// Executes the <paramref name="action"/>.
        /// </summary>
        public static CommandResponsePair WithResponse(this CommandResponsePair crp, Action<ResponseAPDU> action)
        {
            action(crp.RApdu);

            return crp;
        }
    }
}
