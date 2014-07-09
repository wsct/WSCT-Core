using System.Text;
using WSCT.Helpers.Portable;

namespace WSCT.Helpers.Desktop
{
    internal class PortableEncoding : IPortableEncoding
    {
        /// <inheritdoc />
        public Encoding Default
        {
            get { return Encoding.Default; }
        }
    }
}
