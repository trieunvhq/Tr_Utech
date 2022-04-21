 
using Honeywell.AIDC.CrossPlatform;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.Shares;
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
    public class NK_SCANPageModel : BaseViewModel
    {
        public NK_SCANPage _NK_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> NhapKhos { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();

        public ObservableCollection<NhapKhoDungCuModel> ViewNhapKhos { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();

        public Command ScanCommand { get; }

        private ZXingScannerPage scanPage;
        private MobileBarcodeScanningOptions options;

        NhapKhoDungCuModel _NhapKhoDungCuModel;

        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();
        private List<string> NKItemCode = new List<string>();

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string No { get; set; }
        public int _indexHistory = 0;


        public NK_SCANPageModel(NK_SCANPage fd)
        {
            No = fd.No;
            _NK_SCANPage = fd;
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
                List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_NKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
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
                List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);

                for (int i = 0; i < NhapKhos.Count; i++)
                {
                    foreach (NhapKhoDungCuModel dh in donhang_)
                    {
                        if (NhapKhos[i].ItemCode == dh.ItemCode && NhapKhos[i].Serial == dh.Serial)
                        {
                            NhapKhos[i].Color = dh.Color;
                            App.Dblocal.UpdatePurchaseOrderAsync(NhapKhos[i]);
                        }
                    }
                }

                //Lấy dữ liệu số lượng đã scan được từ dblocal:
                for (int i = 0; i < NhapKhos.Count; i++)
                {
                    foreach (TransactionHistoryModel hs in Historys)
                    {
                        if(NhapKhos[i].ItemCode == hs.ItemCode &&
                                            (NhapKhos[i].Serial == null
                                            || NhapKhos[i].Serial == ""
                                            || NhapKhos[i].Serial == "None"
                                            || (NhapKhos[i].Serial == hs.EXT_Serial)))
                        {
                            NhapKhos[i].sQuantity = NhapKhos[i].Quantity.ConvertToString();
                            NhapKhos[i].SoLuongDaNhap += hs.Quantity;
                            NhapKhos[i].sSoLuongDaNhap = NhapKhos[i].SoLuongDaNhap.ConvertToString();
                            NhapKhos[i].SoLuongBox += 1;

                            if (NhapKhos[i].SoLuongDaNhap >= NhapKhos[i].Quantity)
                                NhapKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                NhapKhos[i].ColorSLDaNhap = "#000000";

                            App.Dblocal.UpdatePurchaseOrderAsync(NhapKhos[i]);
                        }
                    }    
                }

                //
                IsThongBao = true;
                Color = Color.Red; 
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "NK_SCANPageModel", MySettings.UserName);
            }
        }


        //Lấy dữ liệu từ server:
        public void LoadModels(string id)
        {
            try
            {
                NhapKhos.Clear();
                ViewNhapKhos.Clear();
                Historys.Clear();
                _daQuetQR.Clear();
                NKItemCode.Clear();

                //Lấy dữ liệu đơn nhập kho từ server:
                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<NhapKhoDungCuModel>>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.getpurchaseorderitem,
                                                new
                                                {
                                                    PurchaseOrderNo = _NK_SCANPage.No,
                                                    WarehouseCode = _NK_SCANPage.WarehouseCode
                                                });
                if (result2 != null && result2.Result != null && result2.Result.data != null)
                { 
                    NhapKhos = new ObservableCollection<NhapKhoDungCuModel>();

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
                            NhapKhos.Insert(0, result2.Result.data[i]);
                        }
                        else
                        {
                            NhapKhos.Add(result2.Result.data[i]);
                        }

                        App.Dblocal.SavePurchaseOrderAsync(result2.Result.data[i]);
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
                                                    OrderNo = _NK_SCANPage.No,
                                                    TransactionType = "I",
                                                    WarehouseCode_From = _NK_SCANPage.WarehouseCode
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
                        for (int i = 0; i < NhapKhos.Count; i++)
                        {
                            foreach (TransactionHistoryModel hs in Historys)
                            {
                                if (NhapKhos[i].ItemCode == hs.ItemCode && (NhapKhos[i].Serial == null
                                            || NhapKhos[i].Serial == ""
                                            || NhapKhos[i].Serial == "None"
                                            || (NhapKhos[i].Serial == hs.EXT_Serial)))
                                {
                                    NhapKhos[i].sQuantity = NhapKhos[i].Quantity.ConvertToString();
                                    NhapKhos[i].SoLuongDaNhap += hs.Quantity;
                                    NhapKhos[i].sSoLuongDaNhap = NhapKhos[i].SoLuongDaNhap.ConvertToString();
                                    NhapKhos[i].SoLuongBox += 1;

                                    if (NhapKhos[i].SoLuongDaNhap >= NhapKhos[i].Quantity)
                                        NhapKhos[i].ColorSLDaNhap = "#ff0000";
                                    else
                                        NhapKhos[i].ColorSLDaNhap = "#000000";

                                    NhapKhos[i].Color = "#000000";

                                    App.Dblocal.UpdatePurchaseOrderAsync(NhapKhos[i]);
                                }
                            }
                        }
                    }
                }

                ViewTableNhapKhos();

                IsThongBao = true;
                Color = Color.Red;
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "NK_SCANPageModel", MySettings.UserName);
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

                        TransactionHistoryShortModel Histori_ = new TransactionHistoryShortModel {
                            TransactionType = "I",
                            OrderNo = _NK_SCANPage.No,
                            ExportStatus = "N",
                            RecordStatus = "N",
                            WarehouseCode_From = _NK_SCANPage.WarehouseCode,
                            WarehouseName_From = _NK_SCANPage.WarehouseName,
                            WarehouseCode_To = _NK_SCANPage.WarehouseCode_To,
                            WarehouseName_To = _NK_SCANPage.WarehouseName_To,
                            DATA = xml_
                        };

                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Histori_);
                        if (result != null && result.Result != null)
                        {
                            //MySettings.InsertLogs(0, DateTime.Now, "1inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                NhapKhos.Clear();
                                _daQuetQR.Clear();
                                App.Dblocal.DeleteAllHistory_NKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                                //MySettings.InsertLogs(0, DateTime.Now, "2inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);

                                MySettings.To_Page = "homepage";
                                await Controls.LoadingUtility.HideAsync();
                                await _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");

                                //LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
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
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "NK_SCANPageModel", MySettings.UserName);
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

                        ThongBao = "Nhãn đã được nhập";

                        return;
                    }
                    else
                    {
                        for (int i = 0; i < NhapKhos.Count; ++i)
                        {
                            if (NhapKhos[i].ItemCode == qr.Code
                                && (NhapKhos[i].Serial == null
                                    || NhapKhos[i].Serial == ""
                                    || NhapKhos[i].Serial == "None"
                                    || (NhapKhos[i].Serial == qr.Serial)))
                            {
                                temp_ = "7";
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity); temp_ = "8";
                                _NhapKhoDungCuModel = NhapKhos[i]; temp_ = "9";

                                if (_NhapKhoDungCuModel.Quantity < _NhapKhoDungCuModel.SoLuongDaNhap + soluong_)
                                {
                                    temp_ = "10";
                                    _NK_SCANPage.soluong_ = soluong_;
                                    _NK_SCANPage.i = i;
                                    _NK_SCANPage.qr = qr;
                                    _NK_SCANPage.str = str;
                                    temp_ = "11";

                                    //var ans = await UserDialogs.Instance.ConfirmAsync("Thông báo", "Vượt sl", "OK");

                                    //if (ans)
                                    //    await Application.Current.MainPage.DisplayAlert("Thông báo", "Vượt số lượng", "OK");

                                    await _NK_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", ""); temp_ = "12";
                                }
                                else
                                {
                                    temp_ = "13";
                                    UpdateTableNhapKhos(qr);
                                    XuLyTiepLuu(true, soluong_, i, qr, str);
                                }

                                //
                                break;
                            }
                            else if (i == NhapKhos.Count - 1)
                            {
                                Color = Color.Red;
                                IsThongBao = true;
                                ThongBao = "Dụng cụ không có trong phiếu nhập!";
                            }
                        }
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
                ThongBao = "ex: "+ex.Message;
                MySettings.InsertLogs(0, DateTime.Now, temp_+". ScanComplate", ex.Message, "NK_SCANPageModel", MySettings.UserName);
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
                    MySettings.InsertLogs(0, DateTime.Now, "Scan_Clicked", ex.Message, "NK_SCANPageModel", MySettings.UserName);
                });
            }
        }

        //Xử lý lưu khi scan:
        public void XuLyTiepLuu(bool iscoluu, decimal soluong_, int i, QRModel qr, string str)
        {
            if (iscoluu)
            {
                _NhapKhoDungCuModel.SoLuongDaNhap = _NhapKhoDungCuModel.SoLuongDaNhap + soluong_;
                _NhapKhoDungCuModel.SoLuongBox = _NhapKhoDungCuModel.SoLuongBox + 1;
                NhapKhos.RemoveAt(i);
                if (_NhapKhoDungCuModel.SoLuongDaNhap >= _NhapKhoDungCuModel.Quantity)
                    _NhapKhoDungCuModel.ColorSLDaNhap = "#ff0000";
                else
                    _NhapKhoDungCuModel.ColorSLDaNhap = "#0008ff";

                _NhapKhoDungCuModel.Color = "#0008ff";
                _NhapKhoDungCuModel.sQuantity = _NhapKhoDungCuModel.Quantity.ConvertToString();
                _NhapKhoDungCuModel.sSoLuongDaNhap = _NhapKhoDungCuModel.SoLuongDaNhap.ConvertToString();
                _NhapKhoDungCuModel.sSoLuongBox = _NhapKhoDungCuModel.SoLuongBox.ConvertToString();

                NhapKhos.Insert(0, _NhapKhoDungCuModel);
                     
                App.Dblocal.UpdatePurchaseOrderAsync(_NhapKhoDungCuModel);


                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = "I",
                    OrderNo = _NK_SCANPage.No,
                    OrderDate = _NK_SCANPage.Date,
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
                    WarehouseCode_From = _NK_SCANPage.WarehouseCode,
                    WarehouseName_From = _NK_SCANPage.WarehouseName,
                    WarehouseCode_To = _NK_SCANPage.WarehouseCode_To,
                    WarehouseName_To = _NK_SCANPage.WarehouseName_To,
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

                    ThongBao = "Bạn hãy scan nhãn nhập kho";
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn nhập kho";

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

                    //MySettings.InsertLogs(0, DateTime.Now, "MBarcodeReader_BarcodeDataReady", str_ + "|==>|" + e.Data, "NK_SCANPageModel", MySettings.UserName);

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


        //Xử lý hiển thị bảng nhập kho theo ItemCode:
        private void ViewTableNhapKhos()
        {
            foreach (NhapKhoDungCuModel item in NhapKhos)
            {
                if (!NKItemCode.Contains(item.ItemCode))
                {
                    NKItemCode.Add(item.ItemCode);

                    if (item.SoLuongDaNhap >= item.Quantity)
                        item.ColorSLDaNhap = "#ff0000";
                    else
                        item.ColorSLDaNhap = "#000000";

                    item.Color = "#000000";

                    ViewNhapKhos.Add(new NhapKhoDungCuModel {
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
                    for (int i = 0; i< ViewNhapKhos.Count; i++)
                    {
                        if (ViewNhapKhos[i].ItemCode == item.ItemCode)
                        {
                            decimal soluong_ = Convert.ToDecimal(item.SoLuongDaNhap);
                            decimal quantity_ = Convert.ToDecimal(item.Quantity);
                            int soluongbox_ = Convert.ToInt32(item.SoLuongBox);

                            ViewNhapKhos[i].SoLuongDaNhap += soluong_;
                            ViewNhapKhos[i].Quantity += quantity_;
                            ViewNhapKhos[i].sQuantity = ViewNhapKhos[i].Quantity.ConvertToString();
                            ViewNhapKhos[i].sSoLuongDaNhap = ViewNhapKhos[i].SoLuongDaNhap.ConvertToString();
                            ViewNhapKhos[i].SoLuongBox += soluongbox_;
                            ViewNhapKhos[i].sSoLuongBox = ViewNhapKhos[i].SoLuongBox.ConvertToString();

                            if (ViewNhapKhos[i].SoLuongDaNhap >= ViewNhapKhos[i].Quantity)
                                ViewNhapKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewNhapKhos[i].ColorSLDaNhap = "#000000";
                        }    
                    }    
                }    
            }    
        }

        //Update hiển thị bảng nhập kho theo ItemCode:
        private void UpdateTableNhapKhos(QRModel item)
        {
            for (int i = 0; i < ViewNhapKhos.Count; i++)
            {
                if (ViewNhapKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity);

                    NhapKhoDungCuModel model_ = ViewNhapKhos[i];

                    model_.SoLuongDaNhap += soluong_;
                    model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ConvertToString();
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ConvertToString();

                    if (model_.SoLuongDaNhap >= model_.Quantity)
                        model_.ColorSLDaNhap = "#ff0000";
                    else
                        model_.ColorSLDaNhap = "#0008ff";

                    model_.Color = "#0008ff";

                    ViewNhapKhos.RemoveAt(i);
                    ViewNhapKhos.Insert(0, model_);
                }
            }
        }
    }
}
