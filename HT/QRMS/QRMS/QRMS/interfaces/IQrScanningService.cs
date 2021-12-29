using System;
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
