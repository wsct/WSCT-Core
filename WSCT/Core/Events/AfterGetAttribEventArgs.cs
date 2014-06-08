using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterGetAttribEventArgs : AfterEventArgs
    {
        public Attrib Attrib;

        public byte[] Buffer;
    }
}