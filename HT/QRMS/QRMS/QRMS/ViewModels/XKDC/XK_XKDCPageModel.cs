using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.XKDC;
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
    public class XK_XKDCPageModel : BaseViewModel
    {
        public XK_XKDCPage _XK_XKDCPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<SaleOrderItemScanBPL> DonHangs { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;
         

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = ""; 
        public Color Color { get; set; } = Color.Red;


        public string SaleOrderNo { get; set; }

        public XK_XKDCPageModel(XK_XKDCPage fd)
        {
            SaleOrderNo = fd._SaleOrderNo;
            _XK_XKDCPage = fd;
            LoadModels("");
        }

        public override void OnAppearing()
        {
            OpenBarcodeReader();
            _daQuetQR = new List<string>();
            base.OnAppearing();
        }


        protected void LoadDbLocal()
        {
            try
            {
                DonHangs.Clear();
                Historys.Clear();

                //List<SaleOrderItemScanBPL> donhang_ = new List<SaleOrderItemScanBPL>();
                List<SaleOrderItemScanBPL> donhang_ = App.Dblocal.GetSaleOrderItemScanAsyncWithKey(SaleOrderNo);
                foreach (SaleOrderItemScanBPL item in donhang_)
                {
                    if (!DonHangs.Contains(item))
                    {
                        //if (item.SoLuongDaNhap >= item.Quantity)
                        //    item.ColorSLDaNhap = "#ff0000";
                        //else
                        //    item.ColorSLDaNhap = "#000000";
                        ////
                        //item.Color = "#000000";
                        //

                        item.sQuantity = item.Quantity.ToString("N0");
                        item.sSoLuongDaNhap = item.SoLuongDaNhap.ToString("N0");
                        DonHangs.Add(item);
                    }
                }

                //List<TransactionHistoryModel> historys = new List<TransactionHistoryModel>();
                List<TransactionHistoryModel> historys = App.Dblocal.GetHistoryAsyncWithKey(SaleOrderNo);
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
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "XK_XKDCPageModel", MySettings.UserName);
            }

        }
         
        public void LoadModels(string id)
        {
            try
            {
                LoadDbLocal();

                if (DonHangs.Count == 0)
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<SaleOrderItemScanBPL>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.getsaleorderitemscanbarcode,
                                                 new
                                                 {
                                                     SaleOrderNo = _XK_XKDCPage._SaleOrderNo,
                                                     WarehouseCode = _XK_XKDCPage._WarehouseCode
                                                 });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DonHangs = new ObservableCollection<SaleOrderItemScanBPL>();

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
                                
                                App.Dblocal.SaveSaleOrderItemScanAsync(result.Result.data[i]);
                            }
                        });
                    }
                }
                //
                if (Historys.Count == 0)
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransactionHistoryModel>>>
                                             (Constaint.ServiceAddress, Constaint.APIurl.gethistory,
                                             new
                                             {
                                                 OrderNo = _XK_XKDCPage._SaleOrderNo,
                                                 TransactionType = "O",
                                                 WarehouseCode_From = _XK_XKDCPage._WarehouseCode
                                             });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        //
                        Historys = new ObservableCollection<TransactionHistoryModel>();

                        for (int i = 0; i < result.Result.data.Count; ++i)
                        {
                            List<TransactionHistoryModel> historys = App.Dblocal.GetHistoryAsyncWithKey(_XK_XKDCPage._SaleOrderNo);
                            if (!Historys.Contains(result.Result.data[i]))
                            {
                                result.Result.data[i].token = MySettings.Token;
                                Historys.Add(result.Result.data[i]);
                                App.Dblocal.SaveHistoryAsync(result.Result.data[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "XK_XKDCPageModel", MySettings.UserName);
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
                                App.Dblocal.DeleteHistoryAsyncWithKey(SaleOrderNo);
                                Historys.Clear();

                                App.Dblocal.DeleteSaleOrderItemScanBPLAsyncWithKey(SaleOrderNo);
                                DonHangs.Clear();

                                await Controls.LoadingUtility.HideAsync();
                                _XK_XKDCPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                                LoadModels(""); 
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                _XK_XKDCPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", ""); 
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
                     
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "XK_XKDCPageModel", MySettings.UserName);
                });
            }
        }

        public async System.Threading.Tasks.Task ScanComplateAsync(string str)
        {
            try
            {
                //if (isDangQuet)
                //    return;
                if (!_daQuetQR.Contains(str))
                    _daQuetQR.Add(str);
                else
                { 
                    IsThongBao = true;
                    Color = Color.Red;
                    ThongBao = "Nhãn đã được quét";
                    return;
                }

                if (Historys != null)
                {
                    bool IsTonTai_ = false;
                    int index_ = 0;
                    var qr = MySettings.QRRead(str);
                    if (qr == null)
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn không đúng định dạng";

                        //CloseBarcodeReader();
                        //OpenBarcodeReader();
                        return;
                    }
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
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn đã được quét";
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < DonHangs.Count; ++i)
                        {
                            if (DonHangs[i].ItemCode == qr.Code)
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                SaleOrderItemScanBPL model_ = DonHangs[i];
                                if (model_.Quantity < model_.SoLuongDaNhap + soluong_)
                                {
                                    _XK_XKDCPage.model_ = model_;
                                    _XK_XKDCPage.soluong_ = soluong_;
                                    _XK_XKDCPage.i = i;
                                    _XK_XKDCPage.qr = qr;
                                    _XK_XKDCPage.str = str;
                                    await _XK_XKDCPage.Load_popup_DangXuat("Đã đủ số lượng cần xuất", "Đồng ý", "Huỷ bỏ"); 
                                }
                                else
                                {
                                    XuLyTiepLuu(true, model_, soluong_, i, qr, str);
                                }
                                 
                                break;
                            }
                            else if (i == DonHangs.Count - 1)
                            {
                                Color = Color.Red;
                                IsThongBao = true;
                                ThongBao = "Dụng cụ không có trong phiếu xuất!";
                            }
                        } 
                    }    
                }
                else
                {
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "XK_XKDCPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "XK_XKDCPageModel", MySettings.UserName);
            }
        }

        public void XuLyTiepLuu(bool iscoluu, SaleOrderItemScanBPL model_, decimal soluong_, int i, QRModel qr, string str)
        {
            if (iscoluu)
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

                App.Dblocal.UpdateSaleOrderItemScanAsync(model_);



                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = "I",
                    OrderNo = SaleOrderNo,
                    OrderDate = _XK_XKDCPage._SaleOrderDate,
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
                    WarehouseCode_From = _XK_XKDCPage._WarehouseCode,
                    WarehouseName_From = _XK_XKDCPage._WarehouseName,
                    CreateDate = DateTime.Now,
                    UserCreate = MySettings.UserName,
                    page = 0,
                    token = MySettings.Token
                };

                Historys.Add(history);
                App.Dblocal.SaveHistoryAsync(history);

            }
            // 
            Color = Color.Blue;
            IsThongBao = true;
            ThongBao = "Thành công";
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
                        ScanComplateAsync(e.Data);
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
