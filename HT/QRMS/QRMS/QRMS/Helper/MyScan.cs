

using System;
using Honeywell.AIDC.CrossPlatform;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Forms;

namespace QRMS.Helper
{
    public class MyScan
	{ 
		private BarcodeReader mSelectedReader = null;
		public NhapKhoDungCuPageModel _NhapKhoDungCuPageModel;
		public XK_XKDCPageModel _XK_XKDCPageModel;
		public MyScan()
		{ 
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
					MySettings.InsertLogs(0, DateTime.Now, "OpenBarcodeReader", "OpenAsync failed, Code:" + result.Code +
						" Message:" + result.Message, "MyScan", MySettings.UserName);
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
			if(_NhapKhoDungCuPageModel!=null)
			{
				_NhapKhoDungCuPageModel.ScanComplate(barcode);
			}
			else if (_XK_XKDCPageModel != null)
			{
				_XK_XKDCPageModel.ScanComplate(barcode);
			} 
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