
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.KKDC;
using QRMS.Models.Shares;
using QRMS.Resources;
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq;
using System.Threading;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class KK_SCANPageModel : BaseViewModel
    {
        public KK_SCANPage _KK_SCANPage; 
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<KKDCModel> TongQuats { get; set; } = new ObservableCollection<KKDCModel>();
        public List<string> Itemcode_Serials = new List<string>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();
         
        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = ""; 
        public Color Color { get; set; } = Color.Red;

        public string _LenhKiemKe { get; set; } = ""; 
        public string _WarehouesCode { get; set; } = "";
        public int _indexHistory = 0;
        public bool _isDaLuu = true;

        KKDCModel _KKDCModel;


        public KK_SCANPageModel(KK_SCANPage fd)
        {
            _KK_SCANPage = fd;
        }

        public override void OnAppearing()
        {
            if (mSelectedReader == null)
                OpenBarcodeReader();

            base.OnAppearing();
            LoadModels();
        }


        protected void LoadDbLocal()
        {
            try
            { 
                Historys = new ObservableCollection<TransactionHistoryModel>();
                if(MySettings.LenhKiemKe!="")
                {
                    List<TransactionHistoryModel> history_ = App.Dblocal.GetAllHistory_KKDC(MySettings.LenhKiemKe, _KK_SCANPage.WarehouseCode);
                    foreach (TransactionHistoryModel item in history_)
                    {
                        if (!_daQuetQR.Contains(item.EXT_QRCode))
                        {
                            Historys.Add(item);
                            _daQuetQR.Add(item.EXT_QRCode);
                            _indexHistory += 1;

                            //
                            decimal soluong_ = 0;
                            bool isTonTaiItemCode = false;

                            soluong_ = Convert.ToDecimal(item.Quantity);

                            for (int i = 0; i < TongQuats.Count; ++i)
                            {
                                if (TongQuats[i].ItemCode == item.ItemCode
                                    && (TongQuats[i].EXT_Serial == null
                                        || TongQuats[i].EXT_Serial == ""
                                        || TongQuats[i].EXT_Serial == "None"
                                        || (TongQuats[i].EXT_Serial == item.EXT_Serial)))
                                {
                                    isTonTaiItemCode = true;

                                    KKDCModel model_ = TongQuats[i];

                                    soluong_ = Convert.ToDecimal(item.Quantity);

                                    model_.SoLuongQuet = model_.SoLuongQuet + soluong_;
                                    model_.sSoLuongQuet = model_.SoLuongQuet.ToString("N0");
                                    model_.SoNhan = model_.SoNhan + 1;
                                    model_.sSoNhan = model_.SoNhan.ToString("N0");
                                    model_.ColorSLDaNhap = "#000000";
                                    model_.Color = "#000000";

                                    TongQuats.RemoveAt(i);
                                    TongQuats.Insert(0, model_);

                                    //
                                    break;
                                }
                                else if (i == TongQuats.Count - 1)
                                {

                                }
                            }

                            if (!isTonTaiItemCode)
                            {
                                TongQuats.Insert(0, new KKDCModel
                                {
                                    TransactionType = "K",
                                    OrderNo = _LenhKiemKe,
                                    WarehouseCode_From = _KK_SCANPage.WarehouseCode,
                                    ItemCode = item.ItemCode,
                                    ItemName = item.ItemName,
                                    ItemType = item.ItemType,
                                    SoLuongQuet = soluong_,
                                    sSoLuongQuet = soluong_.ToString("N0"),
                                    SoNhan = 1,
                                    sSoNhan = "1",
                                    Unit = item.Unit,
                                    EXT_Serial = item.EXT_Serial,
                                    EXT_PartNo = item.EXT_PartNo,
                                    EXT_LotNo = item.EXT_LotNo,
                                    Color = "#000000",
                                    ColorSLDaNhap = "#000000"
                                });
                            }
                        }
                    }

                    if (Historys.Count == 0)
                    {
                        MySettings.LenhKiemKe = _KK_SCANPage.WarehouseCode
                            + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd");
                    }
                }
                else
                {
                    MySettings.LenhKiemKe = _KK_SCANPage.WarehouseCode
                        + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd"); 
                }
                _LenhKiemKe = MySettings.LenhKiemKe;
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "KK_SCANPageModel", MySettings.UserName);
            }

        }

        public async void LoadModels()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Historys.Clear();
                        _daQuetQR.Clear();

                        //
                        LoadDbLocal();

                        //
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<TransactionHistoryShortModel>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.gethistoryxml,
                                                    new
                                                    {
                                                        OrderNo = MySettings.LenhKiemKe,
                                                        TransactionType = "K",
                                                        WarehouseCode_From = _KK_SCANPage.WarehouseCode
                                                    });
                        if (result != null && result.Result != null && result.Result.data != null)
                        {
                            //
                            var historys_ = MySettings.ToHistoryModel(result.Result.data);

                            if (historys_ == null)
                                return;

                            foreach (TransactionHistoryModel item in historys_)
                            {
                                if (!_daQuetQR.Contains(item.EXT_QRCode))
                                {
                                    item.token = MySettings.Token;
                                    item.page = 1;
                                    Historys.Add(item);
                                    _daQuetQR.Add(item.EXT_QRCode);
                                    _indexHistory += 1;

                                    //
                                    decimal soluong_ = 0;
                                    bool isTonTaiItemCode = false;

                                    soluong_ = Convert.ToDecimal(item.Quantity);

                                    for (int i = 0; i < TongQuats.Count; ++i)
                                    {
                                        if (TongQuats[i].ItemCode == item.ItemCode
                                            && (TongQuats[i].EXT_Serial == null
                                                || TongQuats[i].EXT_Serial == ""
                                                || TongQuats[i].EXT_Serial == "None"
                                                || (TongQuats[i].EXT_Serial == item.EXT_Serial)))
                                        {
                                            isTonTaiItemCode = true;

                                            KKDCModel model_ = TongQuats[i];

                                            soluong_ = Convert.ToDecimal(item.Quantity);

                                            model_.SoLuongQuet = model_.SoLuongQuet + soluong_;
                                            model_.sSoLuongQuet = model_.SoLuongQuet.ToString("N0");
                                            model_.SoNhan = model_.SoNhan + 1;
                                            model_.sSoNhan = model_.SoNhan.ToString("N0");
                                            model_.ColorSLDaNhap = "#000000";
                                            model_.Color = "#000000";

                                            TongQuats.RemoveAt(i);
                                            TongQuats.Insert(0, model_);

                                            //
                                            break;
                                        }
                                        else if (i == TongQuats.Count - 1)
                                        {

                                        }
                                    }

                                    if (!isTonTaiItemCode)
                                    {
                                        TongQuats.Insert(0, new KKDCModel
                                        {
                                            TransactionType = "K",
                                            OrderNo = _LenhKiemKe,
                                            WarehouseCode_From = _KK_SCANPage.WarehouseCode,
                                            ItemCode = item.ItemCode,
                                            ItemName = item.ItemName,
                                            ItemType = item.ItemType,
                                            SoLuongQuet = soluong_,
                                            sSoLuongQuet = soluong_.ToString("N0"),
                                            SoNhan = 1,
                                            sSoNhan = "1",
                                            Unit = item.Unit,
                                            EXT_Serial = item.EXT_Serial,
                                            EXT_PartNo = item.EXT_PartNo,
                                            EXT_LotNo = item.EXT_LotNo,
                                            Color = "#000000",
                                            ColorSLDaNhap = "#000000"
                                        });
                                    }
                                }
                            }
                        }

                        await Controls.LoadingUtility.HideAsync();
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Controls.LoadingUtility.HideAsync();
                    MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "KKDC_SCANPageModel", MySettings.UserName);
                });
            }

        }

        public async void SaveDBlocal()
        {
            try
            {
                if (_isDaLuu)
                    return;

                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        for (int i = _indexHistory; i < Historys.Count; i++)
                        {
                            App.Dblocal.SaveHistoryAsync(Historys[i]);
                            _indexHistory++;
                        }

                        _isDaLuu = true;
                        MySettings.To_Page = "KKDC_CLPage";
                        await Controls.LoadingUtility.HideAsync();
                        await _KK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                          
                    });
                });
            }
            catch (Exception ex)
            {
                await Controls.LoadingUtility.HideAsync();
                await _KK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");

                MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "KK_SCANPageModel", MySettings.UserName);
            }
        }

        public async void LuuLais()
        {
            try
            {
                if (Historys.Count == 0)
                    return;

                string xml_ = "";
                int count = 0;

                foreach (TransactionHistoryModel item in Historys)
                {
                    string temp_ = MySettings.MyToString(item) + "❖";
                    if (item.ID == 0 && temp_ != null)
                    {
                        xml_ += temp_;
                        count++;
                    }
                }

                if (count == 0)
                    return;

                xml_ = xml_.Trim('❖');

                TransactionHistoryShortModel Histori_ = new TransactionHistoryShortModel
                {
                    TransactionType = "K",
                    OrderNo = _LenhKiemKe,
                    ExportStatus = "N",
                    RecordStatus = "N",
                    WarehouseCode_From = _KK_SCANPage.WarehouseCode,
                    WarehouseName_From = _KK_SCANPage.WarehouseName,
                    DATA = xml_
                };

                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                Histori_);
                    if (result != null && result.Result != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (result.Result.data == 1)
                            {
                                App.Dblocal.DeleteHistory_KKDC(_LenhKiemKe, _KK_SCANPage.WarehouseCode);
                                Historys.Clear();

                                MySettings.To_Page = "homepage";

                                await Controls.LoadingUtility.HideAsync();
                                await _KK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", ""); 
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await _KK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", ""); 
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
                     
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "KK_SCANPageModel", MySettings.UserName);
                });
            }
        }

        public async void ScanComplate(string str)
        {
            string temp_ = "";
            try
            {
                IsThongBao = false; temp_ = "1";
                ThongBao = ""; temp_ = "2";

                if (Historys != null)
                {
                    //bool IsTonTai_ = false;
                    //int index_ = 0;
                    temp_ = "3";
                    var qr = MySettings.QRRead(str); temp_ = "4";

                    if (qr == null)
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn không đúng định dạng";

                        return;
                    }

                    if (qr.Type != "DC")
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn không đúng chủng loại";

                        return;
                    }

                    //if (_daQuetQR.Contains(str))
                    //{
                    //    IsThongBao = true;
                    //    Color = Color.Red;
                    //    ThongBao = "Nhãn đã được quét";
                    //    return;
                    //}

                    //temp_ = "14";

                    //for (int i = 0; i < Historys.Count; ++i)
                    //{
                    //    if (Historys[i].EXT_Serial == qr.Serial &&
                    //        Historys[i].ItemCode == qr.Code)
                    //    {
                    //        IsTonTai_ = true;
                    //        index_ = i;
                    //        break;
                    //    }
                    //}

                    //temp_ = "5";
                    //if (IsTonTai_)
                    //{
                    //    Color = Color.Red;
                    //    IsThongBao = true;
                    //    ThongBao = "Dụng cụ đã được kiểm kê"; temp_ = "6";
                    //}
                    string str_decode = MySettings.DecodeFromUtf8(str);

                    if (_daQuetQR.Contains(str_decode))
                    {
                        IsThongBao = true;
                        Color = Color.Red;
                        ThongBao = "Dụng cụ đã được kiểm kê";
                        return;
                    }
                    else
                    {
                        decimal soluong_ = 0;
                        bool isTonTaiItemCode = false;

                        soluong_ = Convert.ToDecimal(qr.Quantity);

                        for (int i = 0; i < TongQuats.Count; ++i)
                        {
                            if (TongQuats[i].ItemCode == qr.Code
                                    && (TongQuats[i].EXT_Serial == null
                                        || TongQuats[i].EXT_Serial == ""
                                        || TongQuats[i].EXT_Serial == "None"
                                        || (TongQuats[i].EXT_Serial == qr.Serial)))
                            {
                                isTonTaiItemCode = true;
                                temp_ = "7";

                                KKDCModel model_ = TongQuats[i];

                                model_.SoLuongQuet = model_.SoLuongQuet + soluong_;
                                model_.sSoLuongQuet = model_.SoLuongQuet.ToString("N0");
                                model_.SoNhan = model_.SoNhan + 1;
                                model_.sSoNhan = model_.SoNhan.ToString("N0");
                                model_.ColorSLDaNhap = "#0008ff";
                                model_.Color = "#0008ff";

                                TongQuats.RemoveAt(i);
                                TongQuats.Insert(0, model_);

                                //
                                break;
                            }
                            else if (i == TongQuats.Count - 1)
                            {
                                
                            }
                        }

                        if (!isTonTaiItemCode)
                        {
                            TongQuats.Insert(0, new KKDCModel
                            {
                                TransactionType = "K",
                                OrderNo = _LenhKiemKe,
                                WarehouseCode_From = _KK_SCANPage.WarehouseCode,
                                ItemCode = qr.Code,
                                ItemName = qr.Name,
                                ItemType = qr.Type,
                                SoLuongQuet = soluong_,
                                sSoLuongQuet = soluong_.ToString("N0"),
                                SoNhan = 1,
                                sSoNhan = "1",
                                Unit = qr.Unit,
                                EXT_Serial = qr.Serial,
                                EXT_PartNo = qr.PartNo,
                                EXT_LotNo = qr.LotNo,
                                Color = "#0008ff",
                                ColorSLDaNhap = "#0008ff"
                            });
                        }    


                        TransactionHistoryModel history = new TransactionHistoryModel
                        {
                            ID = 0,
                            TransactionType = "K",
                            OrderNo = _LenhKiemKe,
                            OrderDate = DateTime.Now,
                            ItemCode = qr.Code,
                            ItemName = qr.Name,
                            ItemType = qr.Type,
                            Quantity = soluong_,
                            Unit = qr.Unit,
                            EXT_OtherCode = qr.OtherCode,
                            EXT_Serial = qr.Serial,
                            EXT_PartNo = qr.PartNo,
                            EXT_LotNo = qr.LotNo,
                            EXT_MfDate = qr.MfDate,
                            EXT_RecDate = qr.RecDate,
                            EXT_ExpDate = qr.ExpDate,
                            EXT_QRCode = str_decode,
                            CustomerCode = qr.CustomerCode,
                            ExportStatus = "N",
                            RecordStatus = "N",
                            WarehouseCode_From = _KK_SCANPage.WarehouseCode,
                            WarehouseName_From = _KK_SCANPage.WarehouseName,
                            CreateDate = DateTime.Now,
                            UserCreate = MySettings.UserName,
                            page = 0,
                            token = MySettings.Token
                        };

                        Historys.Add(history);
                        _daQuetQR.Add(str_decode);
                        _isDaLuu = false;


                        //Hiển thị thông báo:
                        Color = Color.Blue;
                        IsThongBao = true;
                        ThongBao = "Thành công";
                    }
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "Mã không hợp lệ!";
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "NK_SCANPageModel", MySettings.UserName);
                } 
            }
            catch (Exception ex)
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "ex: " + ex.Message; 
                MySettings.InsertLogs(0, DateTime.Now, temp_ + ". ScanComplate", ex.Message, "NK_SCANPageModel", MySettings.UserName);
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
                    Color = Color.Blue;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn kiểm kê";

                    //SetScannerAndSymbologySettings();
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn kiểm kê";

                    await Application.Current.MainPage.DisplayAlert("Error", "OpenAsync failed, Code:" + result.Code +
                        " Message:" + result.Message, "OK");
                }
            }
            else
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "3";
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
                mUIContext.Post(_ =>
                {
                    ScanComplate(e.Data);
                }
                         , null);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Thông báo", "MBarcodeReader_BarcodeDataReady Error", "OK");
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
        #endregion


        protected bool isTrungNhanQR(string QR)
        {
            foreach (TransactionHistoryModel item in Historys)
            {
                if (item.EXT_QRCode == QR)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
