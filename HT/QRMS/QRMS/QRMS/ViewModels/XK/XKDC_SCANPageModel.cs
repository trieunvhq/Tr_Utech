 
using Honeywell.AIDC.CrossPlatform;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.Shares;
using QRMS.Models.XKDC;
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace QRMS.ViewModels
{
    public class XKDC_SCANPageModel : BaseViewModel
    {
        public XKDC_SCANPage _XKDC_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<SaleOrderItemScanBPL> XuatKhos { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();

        public ObservableCollection<SaleOrderItemScanBPL> ViewXuatKhos { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();

        public Command ScanCommand { get; }

        private ZXingScannerPage scanPage;
        private MobileBarcodeScanningOptions options;

        SaleOrderItemScanBPL _SaleOrderItemScanBPL;

        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();
        private List<string> XKItemCode = new List<string>();

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string No { get; set; }
        public int _indexHistory = 0;


        public XKDC_SCANPageModel(XKDC_SCANPage fd)
        {
            No = fd.No;
            _XKDC_SCANPage = fd;
            ScanCommand = new Command(Scan_Clicked);
            LoadModels("");
        }

        public override void OnAppearing()
        {
            if (mSelectedReader == null)
                OpenBarcodeReader();
            base.OnAppearing();
        }

        //Kiểm tra trùng nhãn:
        protected bool isTrungNhanQR(string QR)
        {
            foreach (TransactionHistoryModel item in Historys)
            {
                if(item.EXT_QRCode == QR)
                {
                    return true;
                }    
            }

            return false;
        }

        //Lấy dữ liệu bảng History tại local:
        protected void LoadDbLocal()
        {
            try
            {
                _indexHistory = 0;
                Historys.Clear();

                //Lấy bảng lịch sử theo mã đơn và mã kho:
                List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_XKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);
                foreach (TransactionHistoryModel item in historys)
                {
                    if (!_daQuetQR.Contains(item.EXT_QRCode))
                    {
                        item.token = MySettings.Token;
                        item.page = 0;
                        Historys.Add(item);
                        _daQuetQR.Add(item.EXT_QRCode);
                        _indexHistory++;
                    }
                }

                //Lấy trạng thái màu từ lịch sử dblocal:
                List<SaleOrderItemScanBPL> donhang_ = App.Dblocal.GetSaleOrderItemScanAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);

                for (int i = 0; i < XuatKhos.Count; i++)
                {
                    foreach (SaleOrderItemScanBPL dh in donhang_)
                    {
                        //if (XuatKhos[i].ItemCode == dh.ItemCode && XuatKhos[i].Serial == dh.Serial)
                        //{
                        //    XuatKhos[i].Color = dh.Color;
                        //}
                        if (XuatKhos[i].ItemCode == dh.ItemCode)
                        {
                            XuatKhos[i].Color = dh.Color;
                        }
                    }
                }

                //Lấy dữ liệu số lượng đã scan được từ dblocal:
                for (int i = 0; i < XuatKhos.Count; i++)
                {
                    foreach (TransactionHistoryModel hs in Historys)
                    {
                        //if (XuatKhos[i].ItemCode == hs.ItemCode && (XuatKhos[i].Serial == null
                        //                    || XuatKhos[i].Serial == ""
                        //                    || XuatKhos[i].Serial == "None"
                        //                    || (XuatKhos[i].Serial == hs.EXT_Serial)))
                        if (XuatKhos[i].ItemCode == hs.ItemCode)
                        {
                            XuatKhos[i].sQuantity = XuatKhos[i].Quantity.ConvertToString();
                            XuatKhos[i].SoLuongDaNhap += hs.Quantity;
                            XuatKhos[i].sSoLuongDaNhap = XuatKhos[i].SoLuongDaNhap.ConvertToString();
                            XuatKhos[i].SoLuongBox += 1;

                            if (XuatKhos[i].SoLuongDaNhap >= XuatKhos[i].Quantity)
                                XuatKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                XuatKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }

                //
                IsThongBao = true;
                Color = Color.Red; 
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "XKDC_SCANPageModel", MySettings.UserName);
            }
        }


        //Lấy dữ liệu từ server:
        public void LoadModels(string id)
        {
            try
            {
                XuatKhos.Clear();
                ViewXuatKhos.Clear();
                Historys.Clear();
                _daQuetQR.Clear();
                XKItemCode.Clear();

                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<SaleOrderItemScanBPL>>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.getsaleorderitemscanbarcode,
                                                new
                                                {
                                                    SaleOrderNo = _XKDC_SCANPage.No,
                                                    WarehouseCode = _XKDC_SCANPage.WarehouseCode
                                                });
                if (result2 != null && result2.Result != null && result2.Result.data != null)
                {
                    //

                    XuatKhos = new ObservableCollection<SaleOrderItemScanBPL>();

                    for (int i = 0; i < result2.Result.data.Count; ++i)
                    {
                        if (result2.Result.data[i].SoLuongDaNhap >= result2.Result.data[i].Quantity)
                            result2.Result.data[i].ColorSLDaNhap = "#ff0000";
                        else
                            result2.Result.data[i].ColorSLDaNhap = "#000000";
                        //
                        result2.Result.data[i].Color = "#000000";
                        //

                        result2.Result.data[i].sQuantity = result2.Result.data[i].Quantity.ConvertToString();
                        result2.Result.data[i].sSoLuongDaNhap = result2.Result.data[i].SoLuongDaNhap.ConvertToString();
                        if (result2.Result.data[i].ItemCode == id)
                        {
                            XuatKhos.Insert(0, result2.Result.data[i]);
                        }
                        else
                        {
                            XuatKhos.Add(result2.Result.data[i]);
                        }

                        App.Dblocal.SaveSaleOrderItemScanAsync(result2.Result.data[i]);
                    }
                }

                //
                LoadDbLocal();

                //
                if (Historys.Count == 0)
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransactionHistoryModel>>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.gethistory,
                                                new
                                                {
                                                    OrderNo = _XKDC_SCANPage.No,
                                                    TransactionType = "O",
                                                    WarehouseCode_From = _XKDC_SCANPage.WarehouseCode
                                                });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        //
                        Historys = new ObservableCollection<TransactionHistoryModel>();

                        for (int i = 0; i < result.Result.data.Count; ++i)
                        { 
                            if (!_daQuetQR.Contains(result.Result.data[i].EXT_QRCode))
                            {
                                result.Result.data[i].token = MySettings.Token;
                                Historys.Add(result.Result.data[i]);
                                _daQuetQR.Add(result.Result.data[i].EXT_QRCode);
                                App.Dblocal.SaveHistoryAsync(result.Result.data[i]);
                            }
                        }

                        //Cộng số lượng đã get được từ server:
                        foreach (SaleOrderItemScanBPL item in XuatKhos)
                        {
                            foreach (TransactionHistoryModel hs in Historys)
                            {
                                //if (item.ItemCode == hs.ItemCode && (item.Serial == null
                                //            || item.Serial == ""
                                //            || item.Serial == "None"
                                //            || (item.Serial == hs.EXT_Serial)))
                                if (item.ItemCode == hs.ItemCode)
                                {
                                    item.sQuantity = item.Quantity.ConvertToString();
                                    item.SoLuongDaNhap += hs.Quantity;
                                    item.sSoLuongDaNhap = item.SoLuongDaNhap.ConvertToString();
                                    item.SoLuongBox += 1;

                                    if (item.SoLuongDaNhap >= item.Quantity)
                                        item.ColorSLDaNhap = "#ff0000";
                                    else
                                        item.ColorSLDaNhap = "#000000";

                                    item.Color = "#000000";
                                }
                            }
                        }
                    }
                }

                ViewTableXuatKhos();

                IsThongBao = true;
                Color = Color.Red;
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "XKDC_SCANPageModel", MySettings.UserName);
            }

        }


        public async void LuuLais()
        {
            try
            {
                if (Historys.Count >= _indexHistory)
                {
                    await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                    {
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
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                                return;
                            });
                        }

                        xml_ = xml_.Trim('❖');

                        TransactionHistoryShortModel Histori_ = new TransactionHistoryShortModel
                        {
                            TransactionType = "O",
                            OrderNo = _XKDC_SCANPage.No,
                            ExportStatus = "N",
                            RecordStatus = "N",
                            WarehouseCode_From = _XKDC_SCANPage.WarehouseCode,
                            WarehouseName_From = _XKDC_SCANPage.WarehouseName,
                            WarehouseCode_To = _XKDC_SCANPage.WarehouseCode_To,
                            WarehouseName_To = _XKDC_SCANPage.WarehouseName_To,
                            DATA = xml_
                        };

                    
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Histori_);
                        if (result != null && result.Result != null)
                        {
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                XuatKhos.Clear();
                                _daQuetQR.Clear();
                                App.Dblocal.DeleteAllHistory_XKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);
                                App.Dblocal.DeleteSaleOrderItemScanBPLAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);

                                MySettings.To_Page = "homepage";

                                await Controls.LoadingUtility.HideAsync();

                                //
                                await _XKDC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                                //LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await _XKDC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "XKDC_SCANPageModel", MySettings.UserName);
                });
            }
        }


        //Xử lý nhãn khi scan:
        public async void ScanComplate(string str)
        {
            string temp_ = "";
            try
            {
                IsThongBao = false; temp_ = "1";
                ThongBao = ""; temp_ = "2";

                if (Historys != null)
                { 
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
                        ThongBao = "Không đúng nhãn dụng cụ";

                        return;
                    }

                    if (_daQuetQR.Contains(str)) 
                    {
                        IsThongBao = true;
                        Color = Color.Red;

                        ThongBao = "Nhãn đã được xuất";

                        return;
                    }
                    else
                    {
                        for (int i = 0; i < XuatKhos.Count; ++i)
                        {
                            //if (XuatKhos[i].ItemCode == qr.Code
                            //    && (XuatKhos[i].Serial == null
                            //        || XuatKhos[i].Serial == ""
                            //        || XuatKhos[i].Serial == "None"
                            //        || (XuatKhos[i].Serial == qr.Serial)))
                            if (XuatKhos[i].ItemCode == qr.Code)
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                _SaleOrderItemScanBPL = XuatKhos[i];

                                if (_SaleOrderItemScanBPL.Quantity < _SaleOrderItemScanBPL.SoLuongDaNhap + soluong_)
                                { 
                                    _XKDC_SCANPage.soluong_ = soluong_;
                                    _XKDC_SCANPage.i = i;
                                    _XKDC_SCANPage.qr = qr;
                                    _XKDC_SCANPage.str = str; 
                                    //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                    await _XKDC_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "");
                                }
                                else
                                {
                                    UpdateTableXuatKhos(qr);
                                    XuLyTiepLuu(true, soluong_, i, qr, str);
                                }

                                //
                                break;
                            }
                            else if (i == XuatKhos.Count - 1)
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
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "Mã không hợp lệ!";
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "XKDC_SCANPageModel", MySettings.UserName);
                } 
            }
            catch (Exception ex)
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "ex: "+ex.Message;
                
                MySettings.InsertLogs(0, DateTime.Now, temp_+". ScanComplate", ex.Message, "XKDC_SCANPageModel", MySettings.UserName);
            }
        }
         
        //Scan bằng camera sau điện thoại:
        private void Scan_Clicked()
        {
            try
            {
                string barcode = string.Empty;

                options = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseFrontCameraIfAvailable = false,
                    TryHarder = true,
                    PossibleFormats = new List<ZXing.BarcodeFormat>()
                };

                scanPage = new ZXingScannerPage(options);
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.Navigation.PopModalAsync();

                        barcode = result.Text;

                        ScanComplate(barcode);
                    });
                };

                Application.Current.MainPage.Navigation.PushModalAsync(scanPage);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Thông báo", "Không thể quét", "OK");
                    MySettings.InsertLogs(0, DateTime.Now, "Scan_Clicked", ex.Message, "XKDC_SCANPageModel", MySettings.UserName);
                });
            }
        }

        //Xử lý lưu khi scan:
        public void XuLyTiepLuu(bool iscoluu, decimal soluong_, int i, QRModel qr, string str)
        {
            if (iscoluu)
            { 
                _SaleOrderItemScanBPL.SoLuongDaNhap = _SaleOrderItemScanBPL.SoLuongDaNhap + soluong_;
                _SaleOrderItemScanBPL.SoLuongBox = _SaleOrderItemScanBPL.SoLuongBox + 1;
                XuatKhos.RemoveAt(i);
                if (_SaleOrderItemScanBPL.SoLuongDaNhap >= _SaleOrderItemScanBPL.Quantity)
                    _SaleOrderItemScanBPL.ColorSLDaNhap = "#ff0000";
                else
                    _SaleOrderItemScanBPL.ColorSLDaNhap = "#0008ff";

                _SaleOrderItemScanBPL.Color = "#0008ff";
                _SaleOrderItemScanBPL.sQuantity = _SaleOrderItemScanBPL.Quantity.ConvertToString();
                _SaleOrderItemScanBPL.sSoLuongDaNhap = _SaleOrderItemScanBPL.SoLuongDaNhap.ConvertToString();

                XuatKhos.Insert(0, _SaleOrderItemScanBPL);
                     
                App.Dblocal.UpdateSaleOrderItemScanAsync(_SaleOrderItemScanBPL);


                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = "O",
                    OrderNo = _XKDC_SCANPage.No,
                    OrderDate = _XKDC_SCANPage.Date,
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
                    EXT_QRCode = str,
                    CustomerCode = qr.CustomerCode,
                    ExportStatus = "N",
                    RecordStatus = "N",
                    WarehouseCode_From = _XKDC_SCANPage.WarehouseCode,
                    WarehouseName_From = _XKDC_SCANPage.WarehouseName,
                    WarehouseCode_To = _XKDC_SCANPage.WarehouseCode_To,
                    WarehouseName_To = _XKDC_SCANPage.WarehouseName_To,
                    CreateDate = DateTime.Now,
                    UserCreate = MySettings.UserName,
                    page = 0,
                    token = MySettings.Token
                };
                Historys.Add(history);
                _daQuetQR.Add(str);
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
                    Color = Color.Blue;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn xuất kho";
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn xuất kho";

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
                    byte[] utf8Bytes = new byte[(e.Data).Length];
                    for (int i = 0; i < (e.Data).Length; ++i)
                    {
                        utf8Bytes[i] = (byte)(e.Data)[i];
                    }

                    string str_ = Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);

                    //MySettings.InsertLogs(0, DateTime.Now, "MBarcodeReader_BarcodeDataReady", str_ + "|==>|" + e.Data, "XKDC_SCANPageModel", MySettings.UserName);

                    if (str_.Contains("�"))
                        ScanComplate(e.Data);
                    else
                        ScanComplate(str_);
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

         

        //Xử lý hiển thị bảng xuất kho theo ItemCode:
        private void ViewTableXuatKhos()
        {
            foreach (SaleOrderItemScanBPL item in XuatKhos)
            {
                if (!XKItemCode.Contains(item.ItemCode))
                {
                    XKItemCode.Add(item.ItemCode);

                    if (item.SoLuongDaNhap >= item.Quantity)
                        item.ColorSLDaNhap = "#ff0000";
                    else
                        item.ColorSLDaNhap = "#000000";

                    item.Color = "#000000";

                    ViewXuatKhos.Add(new SaleOrderItemScanBPL
                    {
                        ItemCode = item.ItemCode,
                        ItemName = item.ItemName,
                        ItemType = item.ItemType,
                        Quantity = item.Quantity,
                        sQuantity = item.sQuantity,
                        SoLuongDaNhap = item.SoLuongDaNhap,
                        sSoLuongDaNhap = item.sSoLuongDaNhap,
                        SoLuongBox = item.SoLuongBox,
                        sSoLuongBox = item.sSoLuongBox,
                        Unit = item.Unit,
                        Serial = item.Serial,
                        Color = item.Color,
                        ColorSLDaNhap = item.ColorSLDaNhap
                    });
                }
                else
                {
                    for (int i = 0; i < ViewXuatKhos.Count; i++)
                    {
                        if (ViewXuatKhos[i].ItemCode == item.ItemCode)
                        {
                            decimal soluong_ = Convert.ToDecimal(item.SoLuongDaNhap);
                            decimal quantity_ = Convert.ToDecimal(item.Quantity);
                            int soluongbox_ = Convert.ToInt32(item.SoLuongBox);


                            ViewXuatKhos[i].Quantity += quantity_;
                            ViewXuatKhos[i].sQuantity = ViewXuatKhos[i].Quantity.ConvertToString();
                            ViewXuatKhos[i].SoLuongDaNhap += soluong_;
                            ViewXuatKhos[i].sSoLuongDaNhap = ViewXuatKhos[i].SoLuongDaNhap.ConvertToString();
                            ViewXuatKhos[i].SoLuongBox += soluongbox_;
                            ViewXuatKhos[i].sSoLuongBox = ViewXuatKhos[i].SoLuongBox.ConvertToString();

                            if (ViewXuatKhos[i].SoLuongDaNhap >= ViewXuatKhos[i].Quantity)
                                ViewXuatKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewXuatKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }
            }
        }

        //Update hiển thị bảng xuất kho theo ItemCode:
        private void UpdateTableXuatKhos(QRModel item)
        {
            for (int i = 0; i < ViewXuatKhos.Count; i++)
            {
                if (ViewXuatKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity); 

                    SaleOrderItemScanBPL model_ = ViewXuatKhos[i];

                    model_.SoLuongDaNhap += soluong_;
                    model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ConvertToString();
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ConvertToString();

                    if (model_.SoLuongDaNhap >= model_.Quantity)
                        model_.ColorSLDaNhap = "#ff0000";
                    else
                        model_.ColorSLDaNhap = "#0008ff";

                    model_.Color = "#0008ff";

                    ViewXuatKhos.RemoveAt(i);
                    ViewXuatKhos.Insert(0, model_);
                }
            }
        } 
    }
}
