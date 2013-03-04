using System;

using WSCT.Core.APDU;
using WSCT.Helpers;
using WSCT.ISO7816;
using WSCT.ISO7816.Commands;
using WSCT.Stack;
using WSCT.Stack.Core;
using WSCT.Wrapper;

namespace WSCT.Core.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WindowWidth = 132;
                Console.WindowHeight = 50;
            }
            catch (Exception)
            {
            }
            Console.ForegroundColor = ConsoleColor.Gray;

            // Console.WriteLine((new Core.CardChannel()).GetType().GetMember("reconnect").GetValue(0));

            Console.WriteLine();
            Console.WriteLine("=========== S t a t u s W o r d D i c t i o n a r y");

            #region >> StatusWordDictionary

            //            StatusWordDictionary swd = SerializedObject<StatusWordDictionary>.loadFromXml(@"Dictionary.StatusWord.xml");
            //            Console.WriteLine("SW: {0:X4} Description: {1}", 0x9000, swd.getDescription(0x90, 0x00));
            //            Console.WriteLine("SW: {0:X4} Description: {1}", 0x6800, swd.getDescription(0x68, 0x00));
            //            Console.WriteLine("SW: {0:X4} Description: {1}", 0x6800, swd.getDescription(0x68, 0x84));
            //            Console.WriteLine("SW: {0:X4} Description: {1}", 0x6A00, swd.getDescription(0x6A, 0x83));

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C o n s o l e O b s e r v e r");

            #region >> ConsoleObserver

            ConsoleObserver logger = new ConsoleObserver();
            ConsoleObserver61xx logger61xx = new ConsoleObserver61xx();

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d C o n t e x t");

            #region >> CardContext

            ICardContext context = new Core.CardContext();
            logger.observeContext((Core.CardContextObservable)context);

            context.establish();
            context.listReaderGroups();
            context.listReaders(context.groups[0]);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d   i n s e r t i o n   d e t e c t i o n");

            #region >> StatusChangeMonitor

            StatusChangeMonitor monitor = new StatusChangeMonitor(context);

            logger.observeMonitor(monitor);

            AbstractReaderState readerState = monitor.waitForCardPresence(0);
            if (readerState == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(">> Insert a card in one of the {0} readers (time out in 15s)", context.readersCount);
                readerState = monitor.waitForCardPresence(15000);
            }

            if (readerState == null)
            {
                Console.WriteLine(">> Time Out! No card found");
                return;
            }

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d C h a n n e l");

            #region >> CardChannel

            ICardChannel cardChannel = new Core.CardChannel(context, readerState.readerName);
            logger.observeChannel((Core.CardChannelObservable)cardChannel);

            cardChannel.connect(ShareMode.SCARD_SHARE_SHARED, Protocol.SCARD_PROTOCOL_ANY);

            //Console.WriteLine(cardChannel.getStatus());

            Byte[] recvBuffer = null;
            cardChannel.getAttrib(Attrib.SCARD_ATTR_ATR_STRING, ref recvBuffer);

            recvBuffer = null;
            cardChannel.getAttrib(Attrib.SCARD_ATTR_DEVICE_FRIENDLY_NAME, ref recvBuffer);

            recvBuffer = null;
            cardChannel.getAttrib(Attrib.SCARD_ATTR_ATR_STRING, ref recvBuffer);

            cardChannel.reconnect(ShareMode.SCARD_SHARE_SHARED, Protocol.SCARD_PROTOCOL_ANY, Disposition.SCARD_RESET_CARD);

            Console.WriteLine();

            CommandAPDU cAPDU = new CommandAPDU("00A4040005A000000069");
            ICardResponse rAPDU = new ResponseAPDU();

            cardChannel.transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.transmit(cAPDU, rAPDU);
            }

            Console.WriteLine();

            cAPDU = new CommandAPDU("00A404000E315041592E5359532E4444463031");
            rAPDU = new ResponseAPDU();
            cardChannel.transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.transmit(cAPDU, rAPDU);
            }

            cAPDU = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x02, new Byte[2] { 0x3F, 0x00 });
            rAPDU = new ResponseAPDU();
            cardChannel.transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.transmit(cAPDU, rAPDU);
            }

            cardChannel.disconnect(Disposition.SCARD_UNPOWER_CARD);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d C h a n n e l S t a c k");

            #region >> CardChannelStack

            ICardChannelStack cardStack = new CardChannelStack();

            ICardChannelLayer cardLayer = new CardChannelLayer();
            ICardChannelLayer cardLayer61 = new CardChannelLayer61xx();

            logger61xx.observeChannel((ICardChannelObservable)cardLayer61);
            cardStack.addLayer(cardLayer61);

            logger.observeChannel((ICardChannelObservable)cardLayer);
            cardStack.addLayer(cardLayer);

            cardStack.attach(context, readerState.readerName);

            cardStack.connect(ShareMode.SCARD_SHARE_SHARED, Protocol.SCARD_PROTOCOL_ANY);

            cardStack.reconnect(ShareMode.SCARD_SHARE_SHARED, Protocol.SCARD_PROTOCOL_ANY, Disposition.SCARD_RESET_CARD);

            // Use of a CommandResponsePair object to manage the dialog
            cAPDU = new SelectCommand(SelectCommand.SelectionMode.SELECT_DF_NAME, SelectCommand.FileOccurrence.FIRST_OR_ONLY, SelectCommand.FileControlInformation.RETURN_FCI, "A000000069".fromHexa(), 0xFF);
            CommandResponsePair crp = new CommandResponsePair(cAPDU);
            crp.transmit(cardStack);

            cardStack.disconnect(Disposition.SCARD_UNPOWER_CARD);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d   r e m o v a l   d e t e c t i o n");

            #region >> StatusChangeMonitor

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">> Waiting for a change since last call (time out in 10s)");
            // "unpower" change should be fired for the previously used reader
            monitor.waitForChange(10000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">> Remove the card in one of the readers {0} (time out in 10s)", readerState.readerName);
            // Wait for another change
            monitor.waitForChange(10000);

            #endregion

            Console.WriteLine();

            context.release();
        }
    }
}