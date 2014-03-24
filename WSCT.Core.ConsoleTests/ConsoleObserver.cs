using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Wrapper;

using WSCT.Helpers;

namespace WSCT.Core.ConsoleTests
{
    class ConsoleObserver
    {
        internal String header;
        internal ConsoleColor highlightColor = ConsoleColor.White;
        internal ConsoleColor standardColor = ConsoleColor.Gray;

        #region >> Constructors

        public ConsoleObserver()
            : this("[{0,7}] Core ")
        {
        }

        public ConsoleObserver(String _header)
        {
            header = _header;
            __start();
        }

        #endregion

        internal virtual void __start()
        {
            Console.WriteLine(String.Format(header + "ConsoleObserver started", LogLevel.Info));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void observeContext(Core.ICardContextObservable context)
        {
            context.afterEstablishEvent += notifyEstablish;
            context.afterGetStatusChangeEvent += notifyGetStatusChange;
            context.afterListReaderGroupsEvent += notifyListReaderGroups;
            context.afterListReadersEvent += notifyListReaders;
            context.afterReleaseEvent += notifyRelease;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        public void observeChannel(Core.ICardChannelObservable channel)
        {
            channel.beforeConnectEvent += beforeConnect;
            channel.afterConnectEvent += notifyConnect;

            channel.beforeDisconnectEvent += beforeDisconnect;
            channel.afterDisconnectEvent += notifyDisconnect;

            channel.beforeGetAttribEvent += beforeGetAttrib;
            channel.afterGetAttribEvent += notifyGetAttrib;

            channel.beforeReconnectEvent += beforeReconnect;
            channel.afterReconnectEvent += notifyReconnect;

            channel.beforeTransmitEvent += beforeTransmit;
            channel.afterTransmitEvent += notifyTransmit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monitor"></param>
        public void observeMonitor(Core.StatusChangeMonitor monitor)
        {
            monitor.OnCardInsertionEvent += onCardInsertionEvent;
            monitor.OnCardRemovalEvent += onCardRemovalEvent;
        }

        #region >> CardChannelObservervable delegates

        public void notifyConnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Info, errorCode));
            else
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Warning, errorCode));
        }

        public void notifyDisconnect(ICardChannel cardChannel, Disposition disposition, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Info, errorCode));
            else
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Warning, errorCode));
        }

        public void notifyGetAttrib(ICardChannel cardChannel, Attrib attrib, Byte[] buffer, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Info, errorCode));
                Console.WriteLine(String.Format(header + ">> Byte[]: [{1}]", LogLevel.Info, buffer.toHexa()));
                if (attrib == Attrib.AtrString)
                {
                    ISO7816.AnswerToReset.ATR atr = new ISO7816.AnswerToReset.ATR(buffer);
                    Console.WriteLine(header + ">> ATR: [{1}]", LogLevel.Info, atr);
                }
                else if (attrib != Attrib.AtrString)
                {
                    Console.WriteLine(String.Format(header + ">> String: \"{1}\"", LogLevel.Info, buffer.toString()));
                }
            }
            else
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Warning, errorCode));
        }

        public void notifyReconnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Info, errorCode));
            else
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Warning, errorCode));
        }

        public void notifyTransmit(ICardChannel cardChannel, ICardCommand cardCommand, ICardResponse cardResponse, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Info, errorCode));
                Console.WriteLine(String.Format(header + ">> RAPDU: [{1}]", LogLevel.Info, cardResponse));
            }
            else
                Console.WriteLine(String.Format(header + ">> Error: {1}", LogLevel.Warning, errorCode));
        }

        public void beforeConnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "connect(\"{1}\",{2},{3})", LogLevel.Info, cardChannel.readerName, shareMode, preferedProtocol));
            Console.ForegroundColor = standardColor;
        }

        public void beforeDisconnect(ICardChannel cardChannel, Disposition disposition)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "disconnect({1})", LogLevel.Info, disposition));
            Console.ForegroundColor = standardColor;
        }

        public void beforeGetAttrib(ICardChannel cardChannel, Attrib attrib, Byte[] buffer)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "getAttrib({1})", LogLevel.Info, attrib));
            Console.ForegroundColor = standardColor;
        }

        public void beforeReconnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "reconnect({1},{2},{3})", LogLevel.Info, shareMode, preferedProtocol, initialization));
            Console.ForegroundColor = standardColor;
        }

        public void beforeTransmit(ICardChannel cardChannel, ICardCommand cardCommand, Byte[] recvBuffer, UInt32 recvSize)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "transmit({1})", LogLevel.Info, cardCommand));
            Console.ForegroundColor = standardColor;
        }

        public void beforeTransmit(ICardChannel cardChannel, ICardCommand cardCommand, ICardResponse reponse)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "transmit({1})", LogLevel.Info, cardCommand));
            Console.ForegroundColor = standardColor;
        }

        #endregion

        #region >> CardContextObservable delegates

        private void notifyEstablish(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "establish(): {1}", LogLevel.Info, errorCode));
            Console.ForegroundColor = standardColor;
        }

        private void notifyGetStatusChange(ICardContext cardContext, UInt32 timeout, AbstractReaderState[] readerStates, ErrorCode errorCode)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "getStatusChange(): {1}", LogLevel.Info, errorCode));
            Console.ForegroundColor = standardColor;
            if (errorCode == ErrorCode.Success)
                foreach (AbstractReaderState readerState in readerStates)
                {
                    Console.WriteLine(String.Format(header + ">> {2}", LogLevel.Info, readerState.EventState, readerState));
                }
        }

        private void notifyListReaders(ICardContext cardContext, string group, ErrorCode errorCode)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "listReaders({2}): {1}", LogLevel.Info, errorCode, group));
            Console.ForegroundColor = standardColor;
            if (errorCode == ErrorCode.Success)
                foreach (String reader in cardContext.readers)
                    Console.WriteLine(String.Format(header + ">> Reader description Found: {1}", LogLevel.Info, reader));
        }

        private void notifyListReaderGroups(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "listReaderGroups(): {1}", LogLevel.Info, errorCode));
            Console.ForegroundColor = standardColor;
            if (errorCode == ErrorCode.Success)
                foreach (String group in cardContext.groups)
                    Console.WriteLine(String.Format(header + ">> Reader groups descriptionFound: {1}", LogLevel.Info, group));
        }

        private void notifyRelease(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = highlightColor;
            Console.WriteLine(String.Format(header + "release(): {1}", LogLevel.Info, errorCode));
            Console.ForegroundColor = standardColor;
        }

        #endregion

        #region >> StatusChangeMonitor delegates

        private void onCardInsertionEvent(AbstractReaderState readerState)
        {
            Console.ForegroundColor = standardColor;
            Console.WriteLine(String.Format(header + ">> Card insertion detected on reader {1}", LogLevel.Info, readerState.ReaderName));
        }

        private void onCardRemovalEvent(AbstractReaderState readerState)
        {
            Console.ForegroundColor = standardColor;
            Console.WriteLine(String.Format(header + ">> Card removal detected on reader {1}", LogLevel.Info, readerState.ReaderName));
        }

        #endregion
    }
}