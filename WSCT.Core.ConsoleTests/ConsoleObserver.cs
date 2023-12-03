using System;
using WSCT.Core.Events;
using WSCT.Helpers;
using WSCT.ISO7816.AnswerToReset;
using WSCT.Stack;
using WSCT.Wrapper;
using WSCT.Wrapper.Desktop.Core;

namespace WSCT.Core.ConsoleTests
{
    internal class ConsoleObserver
    {
        internal string Header;
        internal ConsoleColor HighlightColor = ConsoleColor.White;
        internal ConsoleColor StandardColor = ConsoleColor.Gray;

        #region >> Constructors

        public ConsoleObserver()
        {
            Header = "[{0,7}] [{1,7}] {2}";
            __start();
        }

        #endregion

        internal virtual void __start()
        {
            Console.WriteLine("ConsoleObserver started");
        }

        private void WriteWarning(object sender, string message)
        {
            WriteLine(LogLevel.Warning, sender, message);
        }

        private void WriteInfo(object sender, string message)
        {
            WriteLine(LogLevel.Info, sender, message);
        }

        private void WriteError(object sender, string message)
        {
            WriteLine(LogLevel.Error, sender, message);
        }

        private void WriteLine(LogLevel level, object sender, string message)
        {
            var channelLayer = sender as ICardChannelLayerObservable;
            if (channelLayer != null)
            {
                Console.WriteLine(Header, level, channelLayer.LayerId, message);
                return;
            }

            var channel = sender as ICardChannelObservable;
            if (channel != null)
            {
                Console.WriteLine(Header, level, "", message);
            }

            var contextLayer = sender as ICardContextLayerObservable;
            if (contextLayer != null)
            {
                Console.WriteLine(Header, level, contextLayer.LayerId, message);
                return;
            }

            var context = sender as ICardContextObservable;
            if (context != null)
            {
                Console.WriteLine(Header, level, "", message);
            }
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

        #region >> CardChannelObservable delegates

        public void NotifyConnect(object sender, AfterConnectEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }

        public void NotifyDisconnect(object sender, AfterDisconnectEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }

        public void NotifyGetAttrib(object sender, AfterGetAttribEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
                WriteInfo(sender, $">> byte[]: [{eventArgs.Buffer.ToHexa()}]");
                if (eventArgs.Attrib == Attrib.AtrString)
                {
                    var atr = new Atr(eventArgs.Buffer);
                    WriteInfo(sender, $">> ATR: [{atr}]");
                }
                else if (eventArgs.Attrib != Attrib.AtrString)
                {
                    WriteInfo(sender, $">> string: \"{eventArgs.Buffer.ToAsciiString()}\"");
                }
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }

        public void NotifyReconnect(object sender, AfterReconnectEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }

        public void NotifyTransmit(object sender, AfterTransmitEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
                WriteInfo(sender, $">> RAPDU: [{eventArgs.Response}]");
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }

        public void BeforeConnect(object sender, BeforeConnectEventArgs eventArgs)
        {
            var cardChannel = (ICardChannel)sender;
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Connect(\"{cardChannel.ReaderName}\",{eventArgs.ShareMode},{eventArgs.PreferedProtocol})");
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeDisconnect(object sender, BeforeDisconnectEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Disconnect({eventArgs.Disposition})");
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeGetAttrib(object sender, BeforeGetAttribEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"GetAttrib({eventArgs.Attrib})");
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeReconnect(object sender, BeforeReconnectEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Reconnect({eventArgs.ShareMode},{eventArgs.PreferedProtocol},{eventArgs.Initialization})");
            Console.ForegroundColor = StandardColor;
        }

        public void BeforeTransmit(object sender, BeforeTransmitEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Transmit({eventArgs.Command})");
            Console.ForegroundColor = StandardColor;
        }

        #endregion

        #region >> CardContextObservable delegates

        private void NotifyEstablish(object sender, AfterEstablishEventArgs eventArgs)
        {
            var context = sender as ICardContext;
            Console.ForegroundColor = HighlightColor;
            WriteInfo(context, $"Establish(): {eventArgs.ReturnValue}");
            Console.ForegroundColor = StandardColor;
        }

        private void NotifyGetStatusChange(object sender, AfterGetStatusChangeEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"GetStatusChange(): {eventArgs.ReturnValue}");
            Console.ForegroundColor = StandardColor;

            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                foreach (var readerState in eventArgs.ReaderStates)
                {
                    WriteInfo(sender, $">> {readerState.EventState}");
                }
            }
        }

        private void NotifyListReaders(object sender, AfterListReadersEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, String.Format("ListReaders({1}): {0}", eventArgs.ReturnValue, eventArgs.Group));
            Console.ForegroundColor = StandardColor;

            var cardContext = (ICardContext)sender;

            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                foreach (var reader in cardContext.Readers)
                {
                    WriteInfo(sender, $">> Reader description Found: {reader}");
                }
            }
        }

        private void NotifyListReaderGroups(object sender, AfterListReaderGroupsEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"ListReaderGroups(): {eventArgs.ReturnValue}");
            Console.ForegroundColor = StandardColor;

            var cardContext = (ICardContext)sender;

            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                foreach (var group in cardContext.Groups)
                {
                    WriteInfo(sender, $">> Reader groups found: {group}");
                }
            }
        }

        private void NotifyRelease(object sender, AfterReleaseEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Release(): {eventArgs.ReturnValue}");
            Console.ForegroundColor = StandardColor;
        }

        #endregion

        #region >> StatusChangeMonitor delegates

        private void OnCardInsertionEvent(object sender, OnCardInsertionEventArgs eventArgs)
        {
            Console.ForegroundColor = StandardColor;
            Console.WriteLine(">> Card insertion detected on reader {0}", eventArgs.ReaderState.ReaderName);
        }

        private void OnCardRemovalEvent(object sender, OnCardRemovalEventArgs eventArgs)
        {
            Console.ForegroundColor = StandardColor;
            Console.WriteLine(">> Card removal detected on reader {0}", eventArgs.ReaderState.ReaderName);
        }

        #endregion

        public void BeforeControl(object sender, BeforeControlEventArgs eventArgs)
        {
            Console.ForegroundColor = HighlightColor;
            WriteInfo(sender, $"Control({eventArgs.ControlCode},{eventArgs.Command.ToHexa()})");
            Console.ForegroundColor = StandardColor;
        }

        public void NotifyControl(object sender, AfterControlEventArgs eventArgs)
        {
            if (eventArgs.ReturnValue == ErrorCode.Success)
            {
                WriteInfo(sender, $">> Error: {eventArgs.ReturnValue}");
                WriteInfo(sender, $">> RAPDU: [{eventArgs.Response.ToHexa()}]");
            }
            else
            {
                WriteWarning(sender, $">> Error: {eventArgs.ReturnValue}");
            }
        }
    }
}