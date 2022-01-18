﻿
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
    public class NhapKhoDungCuPageModel : BaseViewModel, INotifyPropertyChanged
    {
        public NhapKhoDungCuPage _NhapKhoDungCuPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> DonHangs { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        private string _ID = "";
        private string _No = "";
        private DateTime _Date;

        private IBarcodeReader _barcodeReader;
        private string _barcodeDataText;
        private string _barcodeSymbology;
        private DateTime _scanTime;
        private string _statusMessage;
        public int ScanCount { get; set; } = 0;


        public bool IsMatDoc_Camera;


        public NhapKhoDungCuPageModel(string id, string no, DateTime d)
        {
            BarcodeDataText = "Initializing...";
            _ID = id;
            _No = no;
            _Date = d;
            LoadModels("");
        }

        public override void OnAppearing()
        {
            Initialize();
            _daQuetQR = new List<string>();
            base.OnAppearing();
        }


        protected void LoadDbLocal()
        {
            try
            {
                DonHangs.Clear();
                Historys.Clear();

                List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(_No);
                foreach (NhapKhoDungCuModel item in donhang_)
                {
                    if (!DonHangs.Contains(item))
                    {
                        if (item.SoLuongDaNhap >= item.Quantity)
                            item.ColorSLDaNhap = "#ff0000";
                        else
                            item.ColorSLDaNhap = "#000000";
                        //
                        item.Color = "#000000";
                        //
                        item.sQuantity = item.Quantity.ToString("N0");
                        item.sSoLuongDaNhap = item.SoLuongDaNhap.ToString("N0");
                        DonHangs.Add(item);
                    }
                }

                List<TransactionHistoryModel> historys = App.Dblocal.GetHistoryAsyncWithKey(_No);
                foreach (TransactionHistoryModel item in historys)
                {
                    if (!Historys.Contains(item))
                    {
                        Historys.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
            }
           
        }

        public void LoadModels(string id)
        {
            try
            {
                LoadDbLocal();

                if (DonHangs.Count == 0)
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<NhapKhoDungCuModel>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.getitem,
                                                 new
                                                 {
                                                     ID = _ID
                                                 });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DonHangs = new ObservableCollection<NhapKhoDungCuModel>();

                            for (int i = 0; i < result.Result.data.Count; ++i)
                            {
                                if (result.Result.data[i].SoLuongDaNhap >= result.Result.data[i].Quantity)
                                    result.Result.data[i].ColorSLDaNhap = "#ff0000";
                                else
                                    result.Result.data[i].ColorSLDaNhap = "#000000";
                                //
                                result.Result.data[i].Color = "#000000";
                                //

                                result.Result.data[i].sQuantity = result.Result.data[i].Quantity.ToString("N0");
                                result.Result.data[i].sSoLuongDaNhap = result.Result.data[i].SoLuongDaNhap.ToString("N0");
                                if (result.Result.data[i].ItemCode == id)
                                {
                                    DonHangs.Insert(0, result.Result.data[i]);
                                }
                                else
                                {
                                    DonHangs.Add(result.Result.data[i]);
                                }

                                App.Dblocal.SavePurchaseOrderAsync(result.Result.data[i]);
                            }
                        });
                    }
                }
                else
                {
                    MySettings.InsertLogs(0, DateTime.Now, "LoadModels", "DonHangs.Count == 0", "NhapKhoDungCuPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
            }
            
        }


        public async void LuuLais()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                Historys);
                    if (result != null && result.Result != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (result.Result.data == 1)
                            {
                                App.Dblocal.DeleteHistoryAsyncWithKey(_No);
                                Historys.Clear();

                                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.updateitem,
                                                DonHangs);
                                if (result2 != null && result2.Result != null)
                                {
                                    if (result2.Result.data == 1)
                                    {
                                        App.Dblocal.DeletePurchaseOrderAsyncWithKey(_No);
                                        DonHangs.Clear();

                                        await Controls.LoadingUtility.HideAsync();
                                        await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thành công", "Thành công", "Đồng ý", "");
                                        LoadModels("");
                                    }
                                    else
                                    {
                                        await Controls.LoadingUtility.HideAsync();
                                        await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thất bại", "Thất bại", "Đồng ý", "");
                                    }
                                }
                                else
                                {
                                    await Controls.LoadingUtility.HideAsync();
                                    await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thất bại", "Thất bại", "Đồng ý", "");
                                }     
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thất bại", "Thất bại", "Đồng ý", "");
                            }
                        });
                    } 
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();

                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
                    MySettings.InsertLogs(0,DateTime.Now, "LuuLais", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
                });
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
                if (!_daQuetQR.Contains(str))
                    _daQuetQR.Add(str);
                else
                {
                    IsQuet = false;
                    
                    //StartDemThoiGian_HienThiCam();
                    if (IsMatDoc_Camera)
                    { }
                    else
                    {
                        _NhapKhoDungCuPage.CloseCam();
                    }    
                }
                 
                if (Historys != null)
                {
                    isDangQuet = true;
                    bool IsTonTai_ = false;
                    int index_ = 0;
                    var qr = MySettings.QRRead(str);

                    for (int i = 0; i < Historys.Count; ++i)
                    {
                        if (Historys[i].EXT_QRCode == str)
                        {
                            IsTonTai_ = true;
                            index_ = i;
                            break;
                        }
                    }

                    if (IsTonTai_)
                    {
                        ++_so_luong_da_quet;
                        Color = Color.Blue;
                        IsThongBao = true;
                        ThongBao = "Mã QR đã được quét: " + _so_luong_da_quet;
                    }
                    else
                    {
                        for (int i = 0; i < DonHangs.Count; ++i)
                        {
                            if (DonHangs[i].ItemCode == qr.Code)
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                NhapKhoDungCuModel model_ = DonHangs[i];
                                bool iscoluu = true;
                                if (model_.Quantity < model_.SoLuongDaNhap + soluong_)
                                {
                                    var answer = await UserDialogs.Instance.ConfirmAsync("Bạn đã nhập kho vượt quá số lượng đơn mua", "Vượt quá số lượng", "Đồng ý", "Huỷ bỏ");
                                    if (answer)
                                    {
                                        model_.SoLuongDaNhap = model_.SoLuongDaNhap + soluong_;
                                        model_.SoLuongBox = model_.SoLuongBox + 1;
                                        DonHangs.RemoveAt(i);
                                        if(model_.SoLuongDaNhap>=model_.Quantity)
                                            model_.ColorSLDaNhap = "#ff0000";
                                        else
                                            model_.ColorSLDaNhap = "#0008ff";

                                        model_.Color = "#0008ff";
                                        model_.sQuantity = model_.Quantity.ToString("N0");
                                        model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ToString("N0");
                                        DonHangs.Insert(0, model_);
                                    }
                                    else
                                    { iscoluu = false; }    
                                }
                                else
                                {
                                    model_.SoLuongDaNhap = model_.SoLuongDaNhap + soluong_;
                                    model_.SoLuongBox = model_.SoLuongBox + 1;
                                    DonHangs.RemoveAt(i);
                                    if (model_.SoLuongDaNhap >= model_.Quantity)
                                        model_.ColorSLDaNhap = "#ff0000";
                                    else
                                        model_.ColorSLDaNhap = "#0008ff";

                                    model_.Color = "#0008ff";
                                    model_.sQuantity = model_.Quantity.ToString("N0");
                                    model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ToString("N0");
                                    DonHangs.Insert(0, model_);
                                }

                                App.Dblocal.UpdatePurchaseOrderAsync(model_);
                                if(iscoluu)
                                {
                                    TransactionHistoryModel history = new TransactionHistoryModel
                                    {
                                        ID = 0,
                                        TransactionType = "I",
                                        OrderNo = _No,
                                        OrderDate = _Date,
                                        ItemCode = qr.Code,
                                        ItemName = qr.Name,
                                        ItemType = qr.DC,
                                        Quantity = soluong_,
                                        Unit = qr.Unit,
                                        EXT_OtherCode = qr.OtherCode,
                                        EXT_Serial = qr.Serial,
                                        EXT_PartNo = qr.PartNo,
                                        EXT_LotNo = qr.LotNo,
                                        EXT_MfDate = qr.MfDate,
                                        EXT_RecDate = qr.RecDate,
                                        EXT_ExpDate = qr.ExpDate,
                                        EXT_QRCode = str,
                                        CustomerCode = qr.CustomerCode,
                                        ExportStatus = "N",
                                        RecordStatus = "N",
                                        WarehouseCode_From = MySettings.CodeKho,
                                        WarehouseName_From = MySettings.MaKho,
                                        CreateDate = DateTime.Now,
                                        UserCreate = MySettings.UserName,
                                        page = 0,
                                        token = MySettings.Token
                                    };

                                    Historys.Add(history);
                                    App.Dblocal.SaveHistoryAsync(history);
                                }
                                ++_so_luong_quet_thanh_cong;
                                Color = Color.Blue;
                                IsThongBao = true;
                                ThongBao = "Thành công: " + _so_luong_quet_thanh_cong;
                                //
                                break;
                            }
                        } 
                    }
                    if (IsMatDoc_Camera)
                    { }
                    else
                    {
                        _NhapKhoDungCuPage.CloseCam();
                    } 
                    IsQuet = false;  
                }
                else
                {
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "NhapKhoDungCuPageModel", MySettings.UserName);
                }
                //
                if (IsMatDoc_Camera)
                {
                    Stop();
                    Initialize();
                }
                else
                { 
                }
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
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
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
                StatusMessage = $"Barcode #{++ScanCount} received: "+ e.Data;

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
