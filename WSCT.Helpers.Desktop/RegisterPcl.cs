using WSCT.Helpers.Portable;

namespace WSCT.Helpers.Desktop
{
    public class RegisterPcl
    {
        public static void Register()
        {
            PortableInjector.Register<IPortableFile>(new PortableFile());
            PortableInjector.Register<IPortableEncoding>(new PortableEncoding());
        }
    }
}