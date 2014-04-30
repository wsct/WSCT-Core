using System;
using WSCT.Core.APDU;
using WSCT.Helpers;
using WSCT.ISO7816.AnswerToReset;
using WSCT.Wrapper;

namespace WSCT.Core.ConsoleTests
{
    internal class ConsoleObserver
    {
        internal string Header;
        internal ConsoleColor HighlightColor = ConsoleColor.White;
        internal ConsoleColor StandardColor = ConsoleColor.Gray;

        #region >> Constructors

        public ConsoleObserver()
            : this("[{0,7}] Core ")
        {
        }

        public ConsoleObserver(string header)
        {
            Header = header;
            __start();
        }

        #endregion

        internal virtual void __start()
        {
            Console.WriteLine(Header + "ConsoleObserver started", LogLevel.Info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ObserveContext(ICardContextObservable context)
        {
            context.AfterEstablishEvent += NotifyEstablish;
            context.AfterGetStatusChangeEvent += NotifyGetStatusChange;
            context.AfterListReaderGroupsEvent += NotifyListReaderGroups;
            context.AfterListReadersEvent += NotifyListReaders;
            context.AfterReleaseEvent += NotifyRelease;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        public void ObserveChannel(ICardChannelObservable channel)
        {
            channel.BeforeConnectEvent += BeforeConnect;
            channel.AfterConnectEvent += NotifyConnect;

            channel.BeforeDisconnectEvent += BeforeDisconnect;
            channel.AfterDisconnectEvent += NotifyDisconnect;

            channel.BeforeGetAttribEvent += BeforeGetAttrib;
            channel.AfterGetAttribEvent += NotifyGetAttrib;

            channel.BeforeReconnectEvent += BeforeReconnect;
            channel.AfterReconnectEvent += NotifyReconnect;

            channel.BeforeTransmitEvent += BeforeTransmit;
            channel.AfterTransmitEvent += NotifyTransmit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monitor"></param>
        public void ObserveMonitor(StatusChangeMonitor monitor)
        {
            monitor.OnCardInsertionEvent += OnCardInsertionEvent;
            monitor.OnCardRemovalEvent += OnCardRemovalEvent;
        }

        #region >> CardChannelObservervable delegates

        public void NotifyConnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Info, errorCode);
            }
            else
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Warning, errorCode);
            }
        }

        public void NotifyDisconnect(ICardChannel cardChannel, Disposition disposition, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Info, errorCode);
            }
            else
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Warning, errorCode);
            }
        }

        public void NotifyGetAttrib(ICardChannel cardChannel, Attrib attrib, byte[] buffer, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Info, errorCode);
                Console.WriteLine(Header + ">> byte[]: [{1}]", LogLevel.Info, buffer.ToHexa());
                if (attrib == Attrib.AtrString)
                {
                    var atr = new Atr(buffer);
                    Console.WriteLine(Header + ">> ATR: [{1}]", LogLevel.Info, atr);
                }
                else if (attrib != Attrib.AtrString)
                {
                    Console.WriteLine(Header + ">> string: \"{1}\"", LogLevel.Info, buffer.ToAsciiString());
                }
            }
            else
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Warning, errorCode);
            }
        }

        public void NotifyReconnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Info, errorCode);
            }
            else
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Warning, errorCode);
            }
        }

        public void NotifyTransmit(ICardChannel cardChannel, ICardCommand cardCommand, ICardResponse cardResponse, ErrorCode errorCode)
        {
            if (errorCode == ErrorCode.Success)
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Info, errorCode);
                Console.WriteLine(Header + ">> RAPDU: [{1}]", LogLevel.Info, cardResponse);
            }
            else
            {
                Console.WriteLine(Header + ">> Error: {1}", LogLevel.Warning, errorCode);
            }
        }

        public void BeforeConnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "connect(\"{1}\",{2},{3})", LogLevel.Info, cardChannel.ReaderName, shareMode, preferedProtocol);
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeDisconnect(ICardChannel cardChannel, Disposition disposition)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "disconnect({1})", LogLevel.Info, disposition);
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeGetAttrib(ICardChannel cardChannel, Attrib attrib, byte[] buffer)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "getAttrib({1})", LogLevel.Info, attrib);
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeReconnect(ICardChannel cardChannel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "reconnect({1},{2},{3})", LogLevel.Info, shareMode, preferedProtocol, initialization);
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeTransmit(ICardChannel cardChannel, ICardCommand cardCommand, byte[] recvBuffer, UInt32 recvSize)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "transmit({1})", LogLevel.Info, cardCommand);
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeTransmit(ICardChannel cardChannel, ICardCommand cardCommand, ICardResponse reponse)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "transmit({1})", LogLevel.Info, cardCommand);
            Console.ForegroundColor = StandardColor;
        }

        #endregion

        #region >> CardContextObservable delegates

        private void NotifyEstablish(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "establish(): {1}", LogLevel.Info, errorCode);
            Console.ForegroundColor = StandardColor;
        }

        private void NotifyGetStatusChange(ICardContext cardContext, UInt32 timeout, AbstractReaderState[] readerStates, ErrorCode errorCode)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "getStatusChange(): {1}", LogLevel.Info, errorCode);
            Console.ForegroundColor = StandardColor;
            if (errorCode == ErrorCode.Success)
            {
                foreach (var readerState in readerStates)
                {
                    Console.WriteLine(Header + ">> {2}", LogLevel.Info, readerState.EventState, readerState);
                }
            }
        }

        private void NotifyListReaders(ICardContext cardContext, string group, ErrorCode errorCode)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "listReaders({2}): {1}", LogLevel.Info, errorCode, @group);
            Console.ForegroundColor = StandardColor;
            if (errorCode == ErrorCode.Success)
            {
                foreach (var reader in cardContext.Readers)
                {
                    Console.WriteLine(Header + ">> Reader description Found: {1}", LogLevel.Info, reader);
                }
            }
        }

        private void NotifyListReaderGroups(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "listReaderGroups(): {1}", LogLevel.Info, errorCode);
            Console.ForegroundColor = StandardColor;
            if (errorCode == ErrorCode.Success)
            {
                foreach (var group in cardContext.Groups)
                {
                    Console.WriteLine(Header + ">> Reader groups found: {1}", LogLevel.Info, @group);
                }
            }
        }

        private void NotifyRelease(ICardContext cardContext, ErrorCode errorCode)
        {
            Console.ForegroundColor = HighlightColor;
            Console.WriteLine(Header + "release(): {1}", LogLevel.Info, errorCode);
            Console.ForegroundColor = StandardColor;
        }

        #endregion

        #region >> StatusChangeMonitor delegates

        private void OnCardInsertionEvent(AbstractReaderState readerState)
        {
            Console.ForegroundColor = StandardColor;
            Console.WriteLine(Header + ">> Card insertion detected on reader {1}", LogLevel.Info, readerState.ReaderName);
        }

        private void OnCardRemovalEvent(AbstractReaderState readerState)
        {
            Console.ForegroundColor = StandardColor;
            Console.WriteLine(Header + ">> Card removal detected on reader {1}", LogLevel.Info, readerState.ReaderName);
        }

        #endregion
    }
}