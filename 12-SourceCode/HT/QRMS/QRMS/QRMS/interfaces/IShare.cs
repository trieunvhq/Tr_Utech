 
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface IShare
    {
        Task Show(string title, string message, string filePath);
    }
}
