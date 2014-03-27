using System;

namespace WSCT.Core.ConsoleTests
{
    internal class ConsoleObserver61 : ConsoleObserver
    {
        public ConsoleObserver61()
            : base("[{0,7}] 61xx ")
        {
        }

        internal override void __start()
        {
            Console.WriteLine(Header + "ConsoleObserver61 started", LogLevel.Info);
        }
    }
}