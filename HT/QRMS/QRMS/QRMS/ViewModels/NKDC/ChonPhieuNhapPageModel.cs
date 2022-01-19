 
using Acr.UserDialogs; 
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.interfaces;
using QRMS.Models; 
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class ChonPhieuNhapPageModel : BaseViewModel, INotifyPropertyChanged
    {
        public ChonPhieuNhapPage _ChonPhieuNhapPage;
          
        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        public DateTime? _PurchaseOrderDate { get; set; }
        public string _WarehouseCode { get; set; }
        public string _PurchaseOrderNo { get; set; }

        private IBarcodeReader _barcodeReader;
        private string _barcodeDataText;
        private string _barcodeSymbology;
        private DateTime _scanTime;
        private string _statusMessage;
        public int ScanCount { get; set; } = 0;


        public bool IsMatDoc_Camera;


        public ChonPhieuNhapPageModel()
        {  
        }

        public override void OnAppearing()
        {
            Initialize(); 
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
                    && result.Result.ErrorCode =="0" && result.Result.data.Count>0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _PurchaseOrderNo = result.Result.data[0].PurchaseOrderNo;
                        _PurchaseOrderDate = result.Result.data[0].PurchaseOrderDate;
                        _WarehouseCode = result.Result.data[0].WarehouseCode;

                        //
                        ++_so_luong_quet_thanh_cong;
                        Color = Color.Blue;
                        IsThongBao = true;
                        ThongBao = "Thành công: " + _so_luong_quet_thanh_cong;

                        if (IsMatDoc_Camera)
                        { }
                        else
                        {
                            _ChonPhieuNhapPage.CloseCam();
                        }
                        IsQuet = false;


                        if (IsMatDoc_Camera)
                        {
                            Stop();
                            Initialize();
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
                    ThongBao = "Mã QR đã không tồn tại!";
                }    
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "ChonPhieuNhapPageModel", MySettings.UserName);
            }

        }
         
        private int _so_luong_da_quet = 0;
        private int _so_luong_quet_thanh_cong = 0;

        public bool isDangQuet = false;
        public async void ScanComplate(string str)
        {
            try
            {
                //
                if (IsMatDoc_Camera)
                {
                    Stop();
                }
                else
                {
                }
                //
                IsThongBao = false;
                ThongBao = "";
                //
                if (isDangQuet)
                    return;


                isDangQuet = true;
                bool IsTonTai_ = false;
                int index_ = 0;

                LoadModels(str);

            }
            catch (Exception ex)
            {
                if (IsMatDoc_Camera)
                {
                    Stop();
                    Initialize();
                }
                else
                {
                }
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "ChonPhieuNhapPageModel", MySettings.UserName);
            }
        }

        public IBarcodeReader BarcodeReader
        {
            get => _barcodeReader;
            set
            {
                _barcodeReader = value;
                OnPropertyChanged();
            }
        }

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

        internal void Initialize()
        {
            StatusMessage = "Initializing...";
            BarcodeDataText = "No barcode scanned yet";
            ScanTime = DateTime.Now;

            BarcodeReader = DependencyService.Get<IBarcodeReader>();
            if (null != BarcodeReader)
            {
                BarcodeReader.BarcodeDataReady += BarcodeReader_BarcodeDataReady;
                BarcodeReader.StatusMessage += BarcodeReader_StatusMessage;

                Task.Run(async () =>
                {
                    try
                    {
                        await BarcodeReader.Initialize();
                        await BarcodeReader.StartBarcodeReader();

                        StatusMessage = "Initialize" + "Scan a barcode";
                    }
                    catch (Exception e)
                    {
                        StatusMessage = "Initialize" + e.Message;
                    }
                });
            }
        }

        internal void Stop()
        {
            if (null != BarcodeReader)
                Task.Run(async () => await BarcodeReader.StopBarcodeReader());

            isDangQuet = false;
        }

        private void BarcodeReader_BarcodeDataReady(object sender, BarcodeDataArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //BarcodeDataText = e.Data;
                //BarcodeSymbology = e.SymbologyName;
                //ScanTime = e.TimeStamp;
                StatusMessage = $"Barcode #{++ScanCount} received: " + e.Data;

                ScanComplate(e.Data);
            });
        }

        private void BarcodeReader_StatusMessage(object sender, string statusMessage)
        {
            Device.BeginInvokeOnMainThread(() => StatusMessage = statusMessage);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion      
    }
}
