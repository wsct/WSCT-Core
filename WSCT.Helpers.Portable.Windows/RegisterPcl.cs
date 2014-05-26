namespace WSCT.Helpers.Portable.Windows
{
    public class RegisterPcl
    {
        public static void Register()
        {
            PortableInjector.Register<IPortableFile>(new PortableFile());
        }
    }
}
