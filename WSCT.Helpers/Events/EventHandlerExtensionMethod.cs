using System;
using System.Runtime.CompilerServices;

namespace WSCT.Helpers.Events
{
    public static class EventHandlerExtensionMethod
    {
        /// <summary>
        /// Raises the specified event handler.
        /// </summary>
        /// <param name="handler">Event handler to raise.</param>
        /// <param name="sender"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Raise(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, null);
            }
        }

        /// <summary>
        /// Raises the specified event handler.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">Event handler to raise.</param>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}