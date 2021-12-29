
using System.IO;
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface ResizeImage
    {
        byte[] Resize(byte[] imageData, float width, float height);
    }
}
