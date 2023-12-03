namespace WSCT.Core.Events
{
    public class AfterControlEventArgs : AfterEventArgs
    {
        public uint ControlCode;

        public byte[] Command;

        public byte[] Response;
    }
}
