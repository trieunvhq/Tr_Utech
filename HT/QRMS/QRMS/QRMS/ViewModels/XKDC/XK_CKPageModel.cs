  
using Honeywell.AIDC.CrossPlatform;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants; 
using QRMS.Models; 
using QRMS.Views;
using System;
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Threading; 
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class XK_CKPageModel : BaseViewModel, INotifyPropertyChanged
    {
        public XK_CKPage _XK_CKPage;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        public DateTime? _PurchaseOrderDate { get; set; }
        public string _WarehouseCode { get; set; }
        public string _WarehouseName { get; set; }
        public string _PurchaseOrderNo { get; set; }

        //private IBarcodeReader _barcodeReader;
        private string _barcodeDataText;
        private string _barcodeSymbology;
        private DateTime _scanTime;
        private string _statusMessage;
        public int ScanCount { get; set; } = 0;


        public bool IsMatDoc_Camera;


        public XK_CKPageModel()
        {
        }

        public override void OnAppearing()
        {
            Color = Color.Red;
            IsThongBao = true;
            ThongBao = "Bạn hãy scan phiếu xuất kho";
            OpenBarcodeReader();
            base.OnAppearing();
        }


        public void LoadModels(string BarcodeScan)
        {
            try
            {
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<PurchaseOrder>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.getpurchaseorderscan,
                                                 new
                                                 {
                                                     BarcodeScan = BarcodeScan
                                                 });
                if (result != null && result.Result != null && result.Result.data != null
                    && result.Result.ErrorCode == "0" && result.Result.data.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _PurchaseOrderNo = result.Result.data[0].PurchaseOrderNo;
                        _PurchaseOrderDate = result.Result.data[0].PurchaseOrderDate;
                        _WarehouseCode = result.Result.data[0].WarehouseCode;
                        _WarehouseName = result.Result.data[0].WarehouseName;

                        // 
                        Color = Color.Blue;
                        IsThongBao = true;
                        ThongBao = "Thành công";

                        if (IsMatDoc_Camera)
                        { }
                        else
                        {
                            _XK_CKPage.CloseCam();
                        }
                        IsQuet = false;


                        if (IsMatDoc_Camera)
                        {
                            //CloseBarcodeReader(); 
                        }
                        else
                        {
                        }
                    });
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "Mã phiếu không tồn tại!";
                }
            }
            catch (Exception ex)
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "Mã phiếu không tồn tại!";

                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "XK_CKPageModel", MySettings.UserName);
            }

        }

        public bool isDangQuet = false;
        public async void ScanComplate(string str)
        {
            try
            {
                //
                if (IsMatDoc_Camera)
                {
                    //CloseBarcodeReader();
                }
                else
                {
                }
                //
                //IsThongBao = false;
                ThongBao = "";
                //
                //if (isDangQuet)
                //    return;


                isDangQuet = true;
                bool IsTonTai_ = false;
                int index_ = 0;

                LoadModels(str);

            }
            catch (Exception ex)
            {
                if (IsMatDoc_Camera)
                {

                    //CloseBarcodeReader(); 
                }
                else
                {
                }
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "XK_CKPageModel", MySettings.UserName);
            }
        }

        //public IBarcodeReader BarcodeReader
        //{
        //    get => _barcodeReader;
        //    set
        //    {
        //        _barcodeReader = value;
        //        OnPropertyChanged();
        //    }
        //}

        public string BarcodeDataText
        {
            get => _barcodeDataText;
            set
            {
                _barcodeDataText = value;
                OnPropertyChanged();
            }
        }

        public string BarcodeSymbology
        {
            get => _barcodeSymbology;
            set
            {
                _barcodeSymbology = value;
                OnPropertyChanged();
            }
        }

        public DateTime ScanTime
        {
            get => _scanTime;
            set
            {
                _scanTime = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
         
        #region BarcodeReader Action
        private BarcodeReader mSelectedReader = null;
        private SynchronizationContext mUIContext = SynchronizationContext.Current;

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

        private void MBarcodeReader_BarcodeDataReady(object sender, Honeywell.AIDC.CrossPlatform.BarcodeDataArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    mUIContext.Post(_ =>
                    {
                        StatusMessage = $"Barcode #{++ScanCount} received: " + e.Data;

                        ScanComplate(e.Data);
                    }
                        , null);
                });
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Thông báo", "MBarcodeReader_BarcodeDataReady Error", "OK");
            }
        }

        public async void CloseBarcodeReader()
        {
            isDangQuet = false;
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
        #endregion
    }
}
