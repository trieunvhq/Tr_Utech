

using Honeywell.AIDC.CrossPlatform;
using QRMS.ViewModels;
using Xamarin.Forms;

namespace QRMS.Helper
{
    public class MyScan
	{
		private int _TT = 0;
		private BarcodeReader mSelectedReader = null;
		public NhapKhoDungCuPageModel _NhapKhoDungCuPageModel;
		public MyScan(int tt, NhapKhoDungCuPageModel NhapKhoDungCuPageModel_)
		{
			_TT = tt;
			_NhapKhoDungCuPageModel = NhapKhoDungCuPageModel_;
		} 
		public async void OpenBarcodeReader()
		{
			mSelectedReader = GetBarcodeReader();
			if (!mSelectedReader.IsReaderOpened)
			{
				BarcodeReader.Result result = await mSelectedReader.OpenAsync();

				if (result.Code == BarcodeReader.Result.Codes.SUCCESS ||
					result.Code == BarcodeReader.Result.Codes.READER_ALREADY_OPENED)
				{
					//SetScannerAndSymbologySettings();
				}
				else
				{
					await Application.Current.MainPage.DisplayAlert("Error", "OpenAsync failed, Code:" + result.Code +
						" Message:" + result.Message, "OK");
				}
			}
		}

		public BarcodeReader GetBarcodeReader()
		{
			BarcodeReader reader = new BarcodeReader();

			reader.BarcodeDataReady += MBarcodeReader_BarcodeDataReady;

			return reader;
		}

		private void MBarcodeReader_BarcodeDataReady(object sender, BarcodeDataArgs e)
		{
			var barcode = e.Data;
			switch(_TT)
            {
				case 1:// Nhập kho dụng cụ
					_NhapKhoDungCuPageModel.ScanComplate(barcode);
					break;
            }				
			//Code logic scan ở đây;
		}

		public async void CloseBarcodeReader()
		{
			if (mSelectedReader != null && mSelectedReader.IsReaderOpened)
			{
				BarcodeReader.Result result = await mSelectedReader.CloseAsync();
				if (result.Code != BarcodeReader.Result.Codes.SUCCESS)
				{
					await Application.Current.MainPage.DisplayAlert("Error", "CloseAsync failed, Code:" + result.Code +
						" Message:" + result.Message, "OK");
				}
			}
		}
	}
}