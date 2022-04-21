 
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
    public class CKDC_SCANPageModel : BaseViewModel
    {
        public CKDC_SCANPage _CKDC_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<ChuyenKhoDungCuModelBPL> ChuyenKhos { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();

        public ObservableCollection<ChuyenKhoDungCuModelBPL> ViewChuyenKhos { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();

        public Command ScanCommand { get; }

        private ZXingScannerPage scanPage;
        private MobileBarcodeScanningOptions options;

        ChuyenKhoDungCuModelBPL _ChuyenKhoDungCuModelBPL;

        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();
        private List<string> CKItemCode = new List<string>();

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string No { get; set; }
        public int _indexHistory = 0;


        public CKDC_SCANPageModel(CKDC_SCANPage fd)
        {
            No = fd.No;
            _CKDC_SCANPage = fd;
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
                List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(_CKDC_SCANPage.No, _CKDC_SCANPage.WarehouseCode, _CKDC_SCANPage.WarehouseCode_To);
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
                List<ChuyenKhoDungCuModelBPL> donhang_ = App.Dblocal.GetTransferInstructionAsyncWithKey(_CKDC_SCANPage.No, _CKDC_SCANPage.WarehouseCode, _CKDC_SCANPage.WarehouseCode_To);

                for (int i = 0; i < ChuyenKhos.Count; i++)
                {
                    foreach (ChuyenKhoDungCuModelBPL dh in donhang_)
                    {
                        if (ChuyenKhos[i].ItemCode == dh.ItemCode && ChuyenKhos[i].Serial == dh.Serial)
                        {
                            ChuyenKhos[i].Color = dh.Color;
                        }
                    }
                }

                //Lấy dữ liệu số lượng đã scan được từ dblocal:
                for (int i = 0; i < ChuyenKhos.Count; i++)
                {
                    foreach (TransactionHistoryModel hs in Historys)
                    {
                        if (ChuyenKhos[i].ItemCode == hs.ItemCode && (ChuyenKhos[i].Serial == null
                                            || ChuyenKhos[i].Serial == ""
                                            || ChuyenKhos[i].Serial == "None"
                                            || (ChuyenKhos[i].Serial == hs.EXT_Serial)))
                        {
                            ChuyenKhos[i].sQuantity = ChuyenKhos[i].Quantity.ConvertToString();
                            ChuyenKhos[i].SoLuongDaChuyen += hs.Quantity;
                            ChuyenKhos[i].sSoLuongDaChuyen = ChuyenKhos[i].SoLuongDaChuyen.ConvertToString();
                            ChuyenKhos[i].SoLuongBox += 1;

                            if (ChuyenKhos[i].SoLuongDaChuyen >= ChuyenKhos[i].Quantity)
                                ChuyenKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ChuyenKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }

                //
                IsThongBao = true;
                Color = Color.Red; 
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "CKDC_SCANPageModel", MySettings.UserName);
            }
        }


        //Lấy dữ liệu từ server:
        public void LoadModels(string id)
        {
            try
            {
                ChuyenKhos.Clear();
                ViewChuyenKhos.Clear();
                Historys.Clear();
                _daQuetQR.Clear();
                CKItemCode.Clear();

                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<ChuyenKhoDungCuModelBPL>>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstructionitem,
                                                new
                                                {
                                                    TransferOrderNo = _CKDC_SCANPage.No,
                                                    WarehouseCode_From = _CKDC_SCANPage.WarehouseCode,
                                                    WarehouseCode_To = _CKDC_SCANPage.WarehouseCode_To
                                                });
                if (result2 != null && result2.Result != null && result2.Result.data != null)
                {
                    //

                    ChuyenKhos = new ObservableCollection<ChuyenKhoDungCuModelBPL>();

                    for (int i = 0; i < result2.Result.data.Count; ++i)
                    {
                        if (result2.Result.data[i].SoLuongDaChuyen >= result2.Result.data[i].Quantity)
                            result2.Result.data[i].ColorSLDaNhap = "#ff0000";
                        else
                            result2.Result.data[i].ColorSLDaNhap = "#000000";
                        //
                        result2.Result.data[i].Color = "#000000";
                        //

                        result2.Result.data[i].sQuantity = result2.Result.data[i].Quantity.ConvertToString();
                        result2.Result.data[i].sSoLuongDaChuyen = result2.Result.data[i].SoLuongDaChuyen.ConvertToString();
                        if (result2.Result.data[i].ItemCode == id)
                        {
                            ChuyenKhos.Insert(0, result2.Result.data[i]);
                        }
                        else
                        {
                            ChuyenKhos.Add(result2.Result.data[i]);
                        }

                        App.Dblocal.SaveTransferInstructionAsync(result2.Result.data[i]);
                    }
                }

                //
                LoadDbLocal();

                //
                if (Historys.Count == 0)
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransactionHistoryModel>>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.gethistoryckdc,
                                                new
                                                {
                                                    OrderNo = _CKDC_SCANPage.No,
                                                    TransactionType = "C",
                                                    WarehouseCode_From = _CKDC_SCANPage.WarehouseCode,
                                                    WarehouseCode_To = _CKDC_SCANPage.WarehouseCode_To
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
                        for (int i = 0; i < ChuyenKhos.Count; i++)
                        {
                            foreach (TransactionHistoryModel hs in Historys)
                            {
                                if (ChuyenKhos[i].ItemCode == hs.ItemCode && (ChuyenKhos[i].Serial == null
                                            || ChuyenKhos[i].Serial == ""
                                            || ChuyenKhos[i].Serial == "None"
                                            || (ChuyenKhos[i].Serial == hs.EXT_Serial)))
                                {
                                    ChuyenKhos[i].sQuantity = ChuyenKhos[i].Quantity.ConvertToString();
                                    ChuyenKhos[i].SoLuongDaChuyen += hs.Quantity;
                                    ChuyenKhos[i].sSoLuongDaChuyen = ChuyenKhos[i].SoLuongDaChuyen.ConvertToString();
                                    ChuyenKhos[i].SoLuongBox += 1;

                                    if (ChuyenKhos[i].SoLuongDaChuyen >= ChuyenKhos[i].Quantity)
                                        ChuyenKhos[i].ColorSLDaNhap = "#ff0000";
                                    else
                                        ChuyenKhos[i].ColorSLDaNhap = "#000000";

                                    ChuyenKhos[i].Color = "#000000";
                                }
                            }
                        }
                    }
                }

                ViewTableChuyenKhos();

                IsThongBao = true;
                Color = Color.Red;
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "CKDC_SCANPageModel", MySettings.UserName);
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
                            TransactionType = "C",
                            OrderNo = _CKDC_SCANPage.No,
                            ExportStatus = "N",
                            RecordStatus = "N",
                            WarehouseCode_From = _CKDC_SCANPage.WarehouseCode,
                            WarehouseName_From = _CKDC_SCANPage.WarehouseName,
                            WarehouseCode_To = _CKDC_SCANPage.WarehouseCode_To,
                            WarehouseName_To = _CKDC_SCANPage.WarehouseName_To,
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
                                ChuyenKhos.Clear();
                                _daQuetQR.Clear();

                                App.Dblocal.DeleteHistory_CKDC(_CKDC_SCANPage.No, _CKDC_SCANPage.WarehouseCode, _CKDC_SCANPage.WarehouseCode_To);
                                App.Dblocal.DeleteTransferInstructionAsyncWithKey(_CKDC_SCANPage.No, _CKDC_SCANPage.WarehouseCode, _CKDC_SCANPage.WarehouseCode_To);

                                MySettings.To_Page = "homepage";

                                await Controls.LoadingUtility.HideAsync();

                                //
                                await _CKDC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                                //LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await _CKDC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
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
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "CKDC_SCANPageModel", MySettings.UserName);
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

                         ThongBao = "Nhãn đã được chuyển";

                        return;
                    }
                    else
                    {
                        for (int i = 0; i < ChuyenKhos.Count; ++i)
                        {
                            if (ChuyenKhos[i].ItemCode == qr.Code
                                && (ChuyenKhos[i].Serial == null
                                    || ChuyenKhos[i].Serial == ""
                                    || ChuyenKhos[i].Serial == "None"
                                    || (ChuyenKhos[i].Serial == qr.Serial)))
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                _ChuyenKhoDungCuModelBPL = ChuyenKhos[i];

                                if (_ChuyenKhoDungCuModelBPL.Quantity < _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen + soluong_)
                                { 
                                    _CKDC_SCANPage.soluong_ = soluong_;
                                    _CKDC_SCANPage.i = i;
                                    _CKDC_SCANPage.qr = qr;
                                    _CKDC_SCANPage.str = str; 
                                    //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                    await _CKDC_SCANPage.Load_popup_DangXuat("Đã đủ số lượng" + "\nSố lượng chỉ thị: "
                                        + _ChuyenKhoDungCuModelBPL.Quantity.ToString("N0")
                                        + "\n Số lượng đã nhập: "
                                        + (_ChuyenKhoDungCuModelBPL.SoLuongDaChuyen + soluong_).ToString("N0")
                                        , "Đồng ý", "");

                                }
                                else
                                {
                                    UpdateTableChuyenKhos(qr);
                                    XuLyTiepLuu(true, soluong_, i, qr, str);
                                }

                                //
                                break;
                            }
                            else if (i == ChuyenKhos.Count - 1)
                            {
                                Color = Color.Red;
                                IsThongBao = true;
                                ThongBao = "Dụng cụ không có trong phiếu chuyển!";
                            }
                        }
                    }
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "Mã không hợp lệ!";
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "CKDC_SCANPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "ex: "+ex.Message;
                MySettings.InsertLogs(0, DateTime.Now, temp_+". ScanComplate", ex.Message, "CKDC_SCANPageModel", MySettings.UserName);
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
                    MySettings.InsertLogs(0, DateTime.Now, "Scan_Clicked", ex.Message, "CKDC_SCANPageModel", MySettings.UserName);
                });
            }
        }

        private string indet = "";

        //Xử lý lưu khi scan:
        public void XuLyTiepLuu(bool iscoluu, decimal soluong_, int i, QRModel qr, string str)
        {
            if (iscoluu)
            {
               
                _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen = _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen + soluong_;
                _ChuyenKhoDungCuModelBPL.SoLuongBox = _ChuyenKhoDungCuModelBPL.SoLuongBox + 1;
                ChuyenKhos.RemoveAt(i);
                if (_ChuyenKhoDungCuModelBPL.SoLuongDaChuyen >= _ChuyenKhoDungCuModelBPL.Quantity)
                    _ChuyenKhoDungCuModelBPL.ColorSLDaNhap = "#ff0000";
                else
                    _ChuyenKhoDungCuModelBPL.ColorSLDaNhap = "#0008ff";

                _ChuyenKhoDungCuModelBPL.Color = "#0008ff";
                _ChuyenKhoDungCuModelBPL.sQuantity = _ChuyenKhoDungCuModelBPL.Quantity.ConvertToString();
                _ChuyenKhoDungCuModelBPL.sSoLuongDaChuyen = _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen.ConvertToString();

                ChuyenKhos.Insert(0, _ChuyenKhoDungCuModelBPL);
                     
                App.Dblocal.UpdateTransferInstructionAsync(_ChuyenKhoDungCuModelBPL);

                
                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = "C",
                    OrderNo = _CKDC_SCANPage.No,
                    OrderDate = _CKDC_SCANPage.Date,
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
                    WarehouseCode_From = _CKDC_SCANPage.WarehouseCode,
                    WarehouseName_From = _CKDC_SCANPage.WarehouseName,
                    WarehouseCode_To = _CKDC_SCANPage.WarehouseCode_To,
                    WarehouseName_To = _CKDC_SCANPage.WarehouseName_To,
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

                    ThongBao = "Bạn hãy scan nhãn chuyển kho";
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;

                    ThongBao = "Bạn hãy scan nhãn chuyển kho";

                    await Application.Current.MainPage.DisplayAlert("Error", "OpenAsync failed, Code:" + result.Code +
                        " Message:" + result.Message, "OK");
                }
            }
            else
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "Bạn hãy scan nhãn chuyển kho";
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

                    //MySettings.InsertLogs(0, DateTime.Now, "MBarcodeReader_BarcodeDataReady", str_ + "|==>|" + e.Data, "CKDC_SCANPageModel", MySettings.UserName);

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


        //Xử lý hiển thị bảng chuyển kho theo ItemCode:
        private void ViewTableChuyenKhos()
        {
            foreach (ChuyenKhoDungCuModelBPL item in ChuyenKhos)
            {
                if (!CKItemCode.Contains(item.ItemCode))
                {
                    CKItemCode.Add(item.ItemCode);

                    if (item.SoLuongDaChuyen >= item.Quantity)
                        item.ColorSLDaNhap = "#ff0000";
                    else
                        item.ColorSLDaNhap = "#000000";

                    item.Color = "#000000";

                    ViewChuyenKhos.Add(new ChuyenKhoDungCuModelBPL
                    {
                        ItemCode = item.ItemCode,
                        ItemName = item.ItemName,
                        ItemType = item.ItemType,
                        Quantity = item.Quantity,
                        sQuantity = item.sQuantity,
                        SoLuongDaChuyen = item.SoLuongDaChuyen,
                        sSoLuongDaChuyen = item.sSoLuongDaChuyen,
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
                    for (int i = 0; i < ViewChuyenKhos.Count; i++)
                    {
                        if (ViewChuyenKhos[i].ItemCode == item.ItemCode)
                        {
                            decimal soluong_ = Convert.ToDecimal(item.SoLuongDaChuyen);
                            decimal quantity_ = Convert.ToDecimal(item.Quantity);
                            int soluongbox_ = Convert.ToInt32(item.SoLuongBox);

                            ViewChuyenKhos[i].Quantity += quantity_;
                            ViewChuyenKhos[i].sQuantity = ViewChuyenKhos[i].Quantity.ConvertToString();
                            ViewChuyenKhos[i].SoLuongDaChuyen += soluong_;
                            ViewChuyenKhos[i].sSoLuongDaChuyen = ViewChuyenKhos[i].SoLuongDaChuyen.ConvertToString();
                            ViewChuyenKhos[i].SoLuongBox += soluongbox_;
                            ViewChuyenKhos[i].sSoLuongBox = ViewChuyenKhos[i].SoLuongBox.ConvertToString();

                            if (ViewChuyenKhos[i].SoLuongDaChuyen >= ViewChuyenKhos[i].Quantity)
                                ViewChuyenKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewChuyenKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }
            }
        }


        //Update hiển thị bảng chuyển kho theo ItemCode:
        private void UpdateTableChuyenKhos(QRModel item)
        {
            for (int i = 0; i < ViewChuyenKhos.Count; i++)
            {
                if (ViewChuyenKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity); 

                    ChuyenKhoDungCuModelBPL model_ = ViewChuyenKhos[i];

                    model_.SoLuongDaChuyen += soluong_;
                    model_.sSoLuongDaChuyen = model_.SoLuongDaChuyen.ConvertToString();
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ConvertToString();

                    if (model_.SoLuongDaChuyen >= model_.Quantity)
                        model_.ColorSLDaNhap = "#ff0000";
                    else
                        model_.ColorSLDaNhap = "#0008ff";

                    model_.Color = "#0008ff";

                    ViewChuyenKhos.RemoveAt(i);
                    ViewChuyenKhos.Insert(0, model_);
                }
            }
        }
    }
}
