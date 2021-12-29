using System.Threading.Tasks;  
using ZXing.Mobile;  
using Xamarin.Forms;
using QRMS.interfaces;
using QRMS.Droid.Renderer;

[assembly: Dependency(typeof(QrScanningService))]  
  
namespace QRMS.Droid.Renderer
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}