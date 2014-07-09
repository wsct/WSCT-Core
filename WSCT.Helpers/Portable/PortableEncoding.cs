using System.Text;

namespace WSCT.Helpers.Portable
{
    public class PortableEncoding
    {
        public static Encoding Default
        {
            get
            {
                return PortableInjector.Resolve<IPortableEncoding>().Default;
            }
        }
    }
}
