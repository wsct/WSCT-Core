using System;
using System.Collections.Generic;
using System.Text;

namespace WSCT.Core.ConsoleTests
{
    class ConsoleObserver61xx : ConsoleObserver
    {

        internal override void __start()
        {
            Console.WriteLine(String.Format(header + "ConsoleObserver61xx started", LogLevel.Info));
        }

        public ConsoleObserver61xx()
            : base("[{0,7}] 61xx ")
        {
        }

    }
}