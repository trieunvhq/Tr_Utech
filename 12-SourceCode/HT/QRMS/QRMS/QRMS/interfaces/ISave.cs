
using System.IO;

namespace QRMS.interfaces
{
    public interface ISave
    {
        string Save(MemoryStream fileStream, string filename);
    }
}
