using System;
using WSCT.Core.APDU;
using WSCT.Helpers;
using WSCT.ISO7816;
using WSCT.ISO7816.Commands;
using WSCT.Stack;
using WSCT.Wrapper;
using WSCT.Wrapper.Desktop.Core;
using WSCT.Wrapper.Desktop.Stack;

namespace WSCT.Core.ConsoleTests
{
    internal class Program
    {
        private static void Main( /*string[] args*/)
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

            var logger = new ConsoleObserver();
            var logger61 = new ConsoleObserver61();

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d C o n t e x t");

            #region >> CardContext

            ICardContext context = new CardContext();
            logger.ObserveContext((CardContextObservable)context);

            context.Establish();
            context.ListReaderGroups();
            context.ListReaders(context.Groups[0]);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d   i n s e r t i o n   d e t e c t i o n");

            #region >> StatusChangeMonitor

            var monitor = new StatusChangeMonitor(context);

            logger.ObserveMonitor(monitor);

            var readerState = monitor.WaitForCardPresence(0);
            if (readerState == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(">> Insert a card in one of the {0} readers (time out in 15s)", context.ReadersCount);
                readerState = monitor.WaitForCardPresence(15000);
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

            ICardChannel cardChannel = new CardChannel(context, readerState.ReaderName);
            logger.ObserveChannel((CardChannelObservable)cardChannel);

            cardChannel.Connect(ShareMode.Shared, Protocol.Any);

            //Console.WriteLine(cardChannel.getStatus());

            byte[] recvBuffer = null;
            cardChannel.GetAttrib(Attrib.AtrString, ref recvBuffer);

            recvBuffer = null;
            cardChannel.GetAttrib(Attrib.DeviceFriendlyName, ref recvBuffer);

            recvBuffer = null;
            cardChannel.GetAttrib(Attrib.AtrString, ref recvBuffer);

            cardChannel.Reconnect(ShareMode.Shared, Protocol.Any, Disposition.ResetCard);

            Console.WriteLine();

            var cAPDU = new CommandAPDU("00A4040005A000000069");
            ICardResponse rAPDU = new ResponseAPDU();

            cardChannel.Transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).Sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).Sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.Transmit(cAPDU, rAPDU);
            }

            Console.WriteLine();

            cAPDU = new CommandAPDU("00A404000E315041592E5359532E4444463031");
            rAPDU = new ResponseAPDU();
            cardChannel.Transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).Sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).Sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.Transmit(cAPDU, rAPDU);
            }

            cAPDU = new CommandAPDU(0x00, 0xA4, 0x00, 0x00, 0x02, new byte[] { 0x3F, 0x00 });
            rAPDU = new ResponseAPDU();
            cardChannel.Transmit(cAPDU, rAPDU);
            if (((ResponseAPDU)rAPDU).Sw1 == 0x61)
            {
                cAPDU = new CommandAPDU(String.Format("00C00000{0:X2}", ((ResponseAPDU)rAPDU).Sw2));
                rAPDU = new ResponseAPDU();
                cardChannel.Transmit(cAPDU, rAPDU);
            }

            cardChannel.Disconnect(Disposition.UnpowerCard);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d C h a n n e l S t a c k");

            #region >> CardChannelStack

            ICardChannelStack cardStack = new CardChannelStack();

            ICardChannelLayer cardLayer = new CardChannelLayer();
            ICardChannelLayer cardLayer61 = new CardChannelLayer61();

            logger61.ObserveChannel((ICardChannelObservable)cardLayer61);
            cardStack.AddLayer(cardLayer61);

            logger.ObserveChannel((ICardChannelObservable)cardLayer);
            cardStack.AddLayer(cardLayer);

            cardStack.Attach(context, readerState.ReaderName);

            cardStack.Connect(ShareMode.Shared, Protocol.Any);

            cardStack.Reconnect(ShareMode.Shared, Protocol.Any, Disposition.ResetCard);

            // Use of a CommandResponsePair object to manage the dialog
            cAPDU = new SelectCommand(SelectCommand.SelectionMode.SelectDFName, SelectCommand.FileOccurrence.FirstOrOnly, SelectCommand.FileControlInformation.ReturnFci, "A000000069".FromHexa(),
                0xFF);
            var crp = new CommandResponsePair(cAPDU);
            crp.Transmit(cardStack);

            cardStack.Disconnect(Disposition.UnpowerCard);

            #endregion

            Console.WriteLine();
            Console.WriteLine("=========== C a r d   r e m o v a l   d e t e c t i o n");

            #region >> StatusChangeMonitor

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">> Waiting for a change since last call (time out in 10s)");
            // "unpower" change should be fired for the previously used reader
            monitor.WaitForChange(10000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">> Remove the card in one of the readers {0} (time out in 10s)", readerState.ReaderName);
            // Wait for another change
            monitor.WaitForChange(10000);

            #endregion

            Console.WriteLine();

            context.Release();
        }
    }
}