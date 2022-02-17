 
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using LIB;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.interfaces;
using QRMS.Models;
using QRMS.Models.Shares;
using QRMS.Models.XKDC;
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class XKDC_SCANPageModel : BaseViewModel
    {
        public XKDC_SCANPage _XKDC_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> NhapKhos { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ObservableCollection<SaleOrderItemScanBPL> XuatKhos { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();
        public ObservableCollection<ChuyenKhoDungCuModelBPL> ChuyenKhos { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();

        public ObservableCollection<NhapKhoDungCuModel> ViewNhapKhos { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ObservableCollection<SaleOrderItemScanBPL> ViewXuatKhos { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();
        public ObservableCollection<ChuyenKhoDungCuModelBPL> ViewChuyenKhos { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();


        NhapKhoDungCuModel _NhapKhoDungCuModel;
        SaleOrderItemScanBPL _SaleOrderItemScanBPL;
        ChuyenKhoDungCuModelBPL _ChuyenKhoDungCuModelBPL;

        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();


        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string No { get; set; }
        public int _indexHistory = 0;


        public XKDC_SCANPageModel(XKDC_SCANPage fd)
        {
            No = fd.No;
            _XKDC_SCANPage = fd;
            LoadModels("");
        }

        public override void OnAppearing()
        {
            if (mSelectedReader == null)
                OpenBarcodeReader();
            base.OnAppearing();
        }

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

        protected void LoadDbLocal()
        {
            try
            {
                _indexHistory = 0;
                Historys.Clear();

                if (MySettings.Index_Page == 1)
                {
                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_NKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);
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
                    List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);

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
                                NhapKhos[i].sQuantity = NhapKhos[i].Quantity.ToString("N0");
                                NhapKhos[i].SoLuongDaNhap += hs.Quantity;
                                NhapKhos[i].sSoLuongDaNhap = NhapKhos[i].SoLuongDaNhap.ToString("N0");
                                NhapKhos[i].SoLuongBox += 1;

                                if (NhapKhos[i].SoLuongDaNhap >= NhapKhos[i].Quantity)
                                    NhapKhos[i].ColorSLDaNhap = "#ff0000";
                                else
                                    NhapKhos[i].ColorSLDaNhap = "#000000";

                                App.Dblocal.UpdatePurchaseOrderAsync(NhapKhos[i]);
                            }
                        }    
                    }
                }
                else if (MySettings.Index_Page == 2)
                {
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
                            if (XuatKhos[i].ItemCode == dh.ItemCode && XuatKhos[i].Serial == dh.Serial)
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
                            if (XuatKhos[i].ItemCode == hs.ItemCode && (XuatKhos[i].Serial == null
                                                || XuatKhos[i].Serial == ""
                                                || XuatKhos[i].Serial == "None"
                                                || (XuatKhos[i].Serial == hs.EXT_Serial)))
                            {
                                XuatKhos[i].sQuantity = XuatKhos[i].Quantity.ToString("N0");
                                XuatKhos[i].SoLuongDaNhap += hs.Quantity;
                                XuatKhos[i].sSoLuongDaNhap = XuatKhos[i].SoLuongDaNhap.ToString("N0");
                                XuatKhos[i].SoLuongBox += 1;

                                if (XuatKhos[i].SoLuongDaNhap >= XuatKhos[i].Quantity)
                                    XuatKhos[i].ColorSLDaNhap = "#ff0000";
                                else
                                    XuatKhos[i].ColorSLDaNhap = "#000000";
                            }
                        }
                    }
                }
                else if (MySettings.Index_Page == 3)
                {
                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode, _XKDC_SCANPage.WarehouseCode_To);
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
                    List<ChuyenKhoDungCuModelBPL> donhang_ = App.Dblocal.GetTransferInstructionAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode, _XKDC_SCANPage.WarehouseCode_To);

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
                                ChuyenKhos[i].sQuantity = ChuyenKhos[i].Quantity.ToString("N0");
                                ChuyenKhos[i].SoLuongDaChuyen += hs.Quantity;
                                ChuyenKhos[i].sSoLuongDaChuyen = ChuyenKhos[i].SoLuongDaChuyen.ToString("N0");
                                ChuyenKhos[i].SoLuongBox += 1;

                                if (ChuyenKhos[i].SoLuongDaChuyen >= ChuyenKhos[i].Quantity)
                                    ChuyenKhos[i].ColorSLDaNhap = "#ff0000";
                                else
                                    ChuyenKhos[i].ColorSLDaNhap = "#000000";
                            }
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

        public void LoadModels(string id)
        {
            try
            {
                NhapKhos.Clear();
                ChuyenKhos.Clear();
                XuatKhos.Clear();
                ViewNhapKhos.Clear();
                ViewChuyenKhos.Clear();
                ViewXuatKhos.Clear();
                Historys.Clear();
                _daQuetQR.Clear();
                NKItemCode.Clear();
                XKItemCode.Clear();
                CKItemCode.Clear();

                if (MySettings.Index_Page == 1)
                {
                    if (NhapKhos.Count == 0)
                    {
                        var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<NhapKhoDungCuModel>>>
                                                     (Constaint.ServiceAddress, Constaint.APIurl.getpurchaseorderitem,
                                                     new
                                                     {
                                                         PurchaseOrderNo = _XKDC_SCANPage.No,
                                                         WarehouseCode = _XKDC_SCANPage.WarehouseCode
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

                                result2.Result.data[i].sQuantity = result2.Result.data[i].Quantity.ToString("N0");
                                result2.Result.data[i].sSoLuongDaNhap = result2.Result.data[i].SoLuongDaNhap.ToString("N0");  
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
                                                     TransactionType = "I",
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
                            for (int i = 0; i < NhapKhos.Count; i++)
                            {
                                foreach (TransactionHistoryModel hs in Historys)
                                {
                                    if (NhapKhos[i].ItemCode == hs.ItemCode && (NhapKhos[i].Serial == null
                                                || NhapKhos[i].Serial == ""
                                                || NhapKhos[i].Serial == "None"
                                                || (NhapKhos[i].Serial == hs.EXT_Serial)))
                                    {
                                        NhapKhos[i].sQuantity = NhapKhos[i].Quantity.ToString("N0");
                                        NhapKhos[i].SoLuongDaNhap += hs.Quantity;
                                        NhapKhos[i].sSoLuongDaNhap = NhapKhos[i].SoLuongDaNhap.ToString("N0");
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
                }
                else if (MySettings.Index_Page == 2)
                {
                    if (XuatKhos.Count == 0)
                    {
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

                                result2.Result.data[i].sQuantity = result2.Result.data[i].Quantity.ToString("N0");
                                result2.Result.data[i].sSoLuongDaNhap = result2.Result.data[i].SoLuongDaNhap.ToString("N0");
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
                                    if (item.ItemCode == hs.ItemCode && (item.Serial == null
                                                || item.Serial == ""
                                                || item.Serial == "None"
                                                || (item.Serial == hs.EXT_Serial)))
                                    {
                                        item.sQuantity = item.Quantity.ToString("N0");
                                        item.SoLuongDaNhap += hs.Quantity;
                                        item.sSoLuongDaNhap = item.SoLuongDaNhap.ToString("N0");
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
                }
                else if (MySettings.Index_Page == 3)
                {
                    if (ChuyenKhos.Count == 0)
                    {
                        var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<ChuyenKhoDungCuModelBPL>>>
                                                     (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstructionitem,
                                                     new
                                                     {
                                                         TransferOrderNo = _XKDC_SCANPage.No,
                                                         WarehouseCode_From = _XKDC_SCANPage.WarehouseCode,
                                                         WarehouseCode_To = _XKDC_SCANPage.WarehouseCode_To
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

                                result2.Result.data[i].sQuantity = result2.Result.data[i].Quantity.ToString("N0");
                                result2.Result.data[i].sSoLuongDaChuyen = result2.Result.data[i].SoLuongDaChuyen.ToString("N0");
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
                                                     OrderNo = _XKDC_SCANPage.No,
                                                     TransactionType = "C",
                                                     WarehouseCode_From = _XKDC_SCANPage.WarehouseCode,
                                                     WarehouseCode_To = _XKDC_SCANPage.WarehouseCode_To
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
                                        ChuyenKhos[i].sQuantity = ChuyenKhos[i].Quantity.ToString("N0");
                                        ChuyenKhos[i].SoLuongDaChuyen += hs.Quantity;
                                        ChuyenKhos[i].sSoLuongDaChuyen = ChuyenKhos[i].SoLuongDaChuyen.ToString("N0");
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
                }
                
                IsThongBao = true;
                Color = Color.Red;
                ViewTableChuyenKhos();
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
                if (MySettings.Index_Page == 1 && Historys.Count >= _indexHistory)
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
                            //MySettings.InsertLogs(0, DateTime.Now, "1inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                NhapKhos.Clear();
                                _daQuetQR.Clear();
                                App.Dblocal.DeleteAllHistory_NKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);
                                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode);
                                //MySettings.InsertLogs(0, DateTime.Now, "2inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);

                                MySettings.To_Page = "homepage";
                                await Controls.LoadingUtility.HideAsync();
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
                else if (MySettings.Index_Page == 2 && Historys.Count >= _indexHistory)
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
                else if (MySettings.Index_Page == 3 && Historys.Count >= _indexHistory)
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
                                ChuyenKhos.Clear();
                                _daQuetQR.Clear();

                                App.Dblocal.DeleteHistory_CKDC(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode, _XKDC_SCANPage.WarehouseCode_To);
                                App.Dblocal.DeleteTransferInstructionAsyncWithKey(_XKDC_SCANPage.No, _XKDC_SCANPage.WarehouseCode, _XKDC_SCANPage.WarehouseCode_To);

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
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "NK_SCANPageModel", MySettings.UserName);
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
                        ThongBao = "Không đúng nhãn dụng cụ";

                        return;
                    }

                    string str_decode = MySettings.DecodeFromUtf8(str);

                    if (_daQuetQR.Contains(str_decode)) 
                    {
                        IsThongBao = true;
                        Color = Color.Red;

                        if (MySettings.Index_Page == 1)
                            ThongBao = "Nhãn đã được nhập";
                        else if (MySettings.Index_Page == 2)
                            ThongBao = "Nhãn đã được xuất";
                        else
                            ThongBao = "Nhãn đã được chuyển";

                        return;
                    }

                    //temp_ = "14";
                    //for (int i = 0; i < Historys.Count; ++i)
                    //{
                    //    if ((Historys[i].EXT_Serial == null || Historys[i].EXT_Serial == qr.Serial) &&
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
                    //    temp_ = "6";

                    //    if (MySettings.Index_Page == 1)
                    //        ThongBao = "Nhãn đã được nhập"; 
                    //    else if (MySettings.Index_Page == 2)
                    //        ThongBao = "Nhãn đã được xuất";
                    //    else
                    //        ThongBao = "Nhãn đã được chuyển";
                    //}
                    else
                    {
                        if (MySettings.Index_Page == 1)
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
                                        _XKDC_SCANPage.soluong_ = soluong_;
                                        _XKDC_SCANPage.i = i;
                                        _XKDC_SCANPage.qr = qr;
                                        _XKDC_SCANPage.str = str_decode;
                                        temp_ = "11";

                                        //var ans = await UserDialogs.Instance.ConfirmAsync("Thông báo", "Vượt sl", "OK");

                                        //if (ans)
                                        //    await Application.Current.MainPage.DisplayAlert("Thông báo", "Vượt số lượng", "OK");

                                        await _XKDC_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", ""); temp_ = "12";
                                    }
                                    else
                                    {
                                        temp_ = "13";
                                        UpdateTableNhapKhos(qr);
                                        XuLyTiepLuu(true, soluong_, i, qr, str_decode);
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
                        else if (MySettings.Index_Page == 2)
                        {
                            for (int i = 0; i < XuatKhos.Count; ++i)
                            {
                                if (XuatKhos[i].ItemCode == qr.Code
                                    && (XuatKhos[i].Serial == null
                                        || XuatKhos[i].Serial == ""
                                        || XuatKhos[i].Serial == "None"
                                        || (XuatKhos[i].Serial == qr.Serial)))
                                {
                                    decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                    _SaleOrderItemScanBPL = XuatKhos[i];

                                    if (_SaleOrderItemScanBPL.Quantity < _SaleOrderItemScanBPL.SoLuongDaNhap + soluong_)
                                    { 
                                        _XKDC_SCANPage.soluong_ = soluong_;
                                        _XKDC_SCANPage.i = i;
                                        _XKDC_SCANPage.qr = qr;
                                        _XKDC_SCANPage.str = str_decode; 
                                        //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                        await _XKDC_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "");
                                    }
                                    else
                                    {
                                        UpdateTableXuatKhos(qr);
                                        XuLyTiepLuu(true, soluong_, i, qr, str_decode);
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
                        else if (MySettings.Index_Page == 3)
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
                                        _XKDC_SCANPage.soluong_ = soluong_;
                                        _XKDC_SCANPage.i = i;
                                        _XKDC_SCANPage.qr = qr;
                                        _XKDC_SCANPage.str = str_decode; 
                                        //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                        await _XKDC_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "");

                                    }
                                    else
                                    {
                                        UpdateTableChuyenKhos(qr);
                                        XuLyTiepLuu(true, soluong_, i, qr, str_decode);
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
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "Mã không hợp lệ!";
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "NK_SCANPageModel", MySettings.UserName);
                }

                //CloseBarcodeReader();
                //OpenBarcodeReader();
            }
            catch (Exception ex)
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "ex: "+ex.Message;
                //CloseBarcodeReader();
                //OpenBarcodeReader();
                MySettings.InsertLogs(0, DateTime.Now, temp_+". ScanComplate", ex.Message, "NK_SCANPageModel", MySettings.UserName);
            }
        }
        private string indet = "";
        public void XuLyTiepLuu(bool iscoluu, decimal soluong_, int i, QRModel qr, string str)
        {
            if (iscoluu)
            {
                string TransactionType_ = "";
                string ExportStatus_ = "";
                string RecordStatus_ = "";
                if (MySettings.Index_Page == 1)
                {
                    _NhapKhoDungCuModel.SoLuongDaNhap = _NhapKhoDungCuModel.SoLuongDaNhap + soluong_;
                    _NhapKhoDungCuModel.SoLuongBox = _NhapKhoDungCuModel.SoLuongBox + 1;
                    NhapKhos.RemoveAt(i);
                    if (_NhapKhoDungCuModel.SoLuongDaNhap >= _NhapKhoDungCuModel.Quantity)
                        _NhapKhoDungCuModel.ColorSLDaNhap = "#ff0000";
                    else
                        _NhapKhoDungCuModel.ColorSLDaNhap = "#0008ff";

                    _NhapKhoDungCuModel.Color = "#0008ff";
                    _NhapKhoDungCuModel.sQuantity = _NhapKhoDungCuModel.Quantity.ToString("N0");
                    _NhapKhoDungCuModel.sSoLuongDaNhap = _NhapKhoDungCuModel.SoLuongDaNhap.ToString("N0");
                    _NhapKhoDungCuModel.sSoLuongBox = _NhapKhoDungCuModel.SoLuongBox.ToString("N0");

                    NhapKhos.Insert(0, _NhapKhoDungCuModel);
                     
                    App.Dblocal.UpdatePurchaseOrderAsync(_NhapKhoDungCuModel);

                    TransactionType_ = "I";
                    ExportStatus_ = "N";
                    RecordStatus_ = "N";
                     
                }
                else if (MySettings.Index_Page == 2)
                {
                    _SaleOrderItemScanBPL.SoLuongDaNhap = _SaleOrderItemScanBPL.SoLuongDaNhap + soluong_;
                    _SaleOrderItemScanBPL.SoLuongBox = _SaleOrderItemScanBPL.SoLuongBox + 1;
                    XuatKhos.RemoveAt(i);
                    if (_SaleOrderItemScanBPL.SoLuongDaNhap >= _SaleOrderItemScanBPL.Quantity)
                        _SaleOrderItemScanBPL.ColorSLDaNhap = "#ff0000";
                    else
                        _SaleOrderItemScanBPL.ColorSLDaNhap = "#0008ff";

                    _SaleOrderItemScanBPL.Color = "#0008ff";
                    _SaleOrderItemScanBPL.sQuantity = _SaleOrderItemScanBPL.Quantity.ToString("N0");
                    _SaleOrderItemScanBPL.sSoLuongDaNhap = _SaleOrderItemScanBPL.SoLuongDaNhap.ToString("N0");

                    XuatKhos.Insert(0, _SaleOrderItemScanBPL);
                     
                    App.Dblocal.UpdateSaleOrderItemScanAsync(_SaleOrderItemScanBPL);



                    TransactionType_ = "O";
                    ExportStatus_ = "N";
                    RecordStatus_ = "N";

                }
                else if (MySettings.Index_Page == 3)
                {
                    _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen = _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen + soluong_;
                    _ChuyenKhoDungCuModelBPL.SoLuongBox = _ChuyenKhoDungCuModelBPL.SoLuongBox + 1;
                    ChuyenKhos.RemoveAt(i);
                    if (_ChuyenKhoDungCuModelBPL.SoLuongDaChuyen >= _ChuyenKhoDungCuModelBPL.Quantity)
                        _ChuyenKhoDungCuModelBPL.ColorSLDaNhap = "#ff0000";
                    else
                        _ChuyenKhoDungCuModelBPL.ColorSLDaNhap = "#0008ff";

                    _ChuyenKhoDungCuModelBPL.Color = "#0008ff";
                    _ChuyenKhoDungCuModelBPL.sQuantity = _ChuyenKhoDungCuModelBPL.Quantity.ToString("N0");
                    _ChuyenKhoDungCuModelBPL.sSoLuongDaChuyen = _ChuyenKhoDungCuModelBPL.SoLuongDaChuyen.ToString("N0");

                    ChuyenKhos.Insert(0, _ChuyenKhoDungCuModelBPL);
                     
                    App.Dblocal.UpdateTransferInstructionAsync(_ChuyenKhoDungCuModelBPL);

                    TransactionType_ = "C";
                    ExportStatus_ = "N";
                    RecordStatus_ = "N"; 
                }
                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = TransactionType_,
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
                    ExportStatus = ExportStatus_,
                    RecordStatus = RecordStatus_,
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

                    if (MySettings.Index_Page == 1)
                        ThongBao = "Bạn hãy scan nhãn nhập kho";
                    else if (MySettings.Index_Page == 2)
                        ThongBao = "Bạn hãy scan nhãn xuất kho";
                    else
                        ThongBao = "Bạn hãy scan nhãn chuyển kho";

                    //SetScannerAndSymbologySettings();
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;

                    if (MySettings.Index_Page == 1)
                        ThongBao = "Bạn hãy scan nhãn nhập kho";
                    else if (MySettings.Index_Page == 2)
                        ThongBao = "Bạn hãy scan nhãn xuất kho";
                    else
                        ThongBao = "Bạn hãy scan nhãn chuyển kho";

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
                    //byte[] utf8Bytes = new byte[e.Data.Length];
                    //for (int i = 0; i < e.Data.Length; ++i)
                    //{
                    //    //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                    //    utf8Bytes[i] = (byte)e.Data[i];
                    //}


                    //ScanComplate(Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length));

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

        private List<string> NKItemCode = new List<string>();
        private List<string> XKItemCode = new List<string>();
        private List<string> CKItemCode = new List<string>();


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
                            ViewNhapKhos[i].sQuantity = ViewNhapKhos[i].Quantity.ToString("N0");
                            ViewNhapKhos[i].sSoLuongDaNhap = ViewNhapKhos[i].SoLuongDaNhap.ToString("N0");
                            ViewNhapKhos[i].SoLuongBox += soluongbox_;
                            ViewNhapKhos[i].sSoLuongBox = ViewNhapKhos[i].SoLuongBox.ToString("N0");

                            if (ViewNhapKhos[i].SoLuongDaNhap >= ViewNhapKhos[i].Quantity)
                                ViewNhapKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewNhapKhos[i].ColorSLDaNhap = "#000000";
                        }    
                    }    
                }    
            }    
        }

        private void UpdateTableNhapKhos(QRModel item)
        {
            for (int i = 0; i < ViewNhapKhos.Count; i++)
            {
                if (ViewNhapKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity);

                    NhapKhoDungCuModel model_ = ViewNhapKhos[i];

                    model_.SoLuongDaNhap += soluong_;
                    model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ToString("N0");
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ToString("N0");

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
                            ViewXuatKhos[i].sQuantity = ViewXuatKhos[i].Quantity.ToString("N0");
                            ViewXuatKhos[i].SoLuongDaNhap += soluong_;
                            ViewXuatKhos[i].sSoLuongDaNhap = ViewXuatKhos[i].SoLuongDaNhap.ToString("N0");
                            ViewXuatKhos[i].SoLuongBox += soluongbox_;
                            ViewXuatKhos[i].sSoLuongBox = ViewXuatKhos[i].SoLuongBox.ToString("N0");

                            if (ViewXuatKhos[i].SoLuongDaNhap >= ViewXuatKhos[i].Quantity)
                                ViewXuatKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewXuatKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }
            }
        }

        private void UpdateTableXuatKhos(QRModel item)
        {
            for (int i = 0; i < ViewXuatKhos.Count; i++)
            {
                if (ViewXuatKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity); 

                    SaleOrderItemScanBPL model_ = ViewXuatKhos[i];

                    model_.SoLuongDaNhap += soluong_;
                    model_.sSoLuongDaNhap = model_.SoLuongDaNhap.ToString("N0");
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ToString("N0");

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
                            ViewChuyenKhos[i].sQuantity = ViewChuyenKhos[i].Quantity.ToString("N0");
                            ViewChuyenKhos[i].SoLuongDaChuyen += soluong_;
                            ViewChuyenKhos[i].sSoLuongDaChuyen = ViewChuyenKhos[i].SoLuongDaChuyen.ToString("N0");
                            ViewChuyenKhos[i].SoLuongBox += soluongbox_;
                            ViewChuyenKhos[i].sSoLuongBox = ViewChuyenKhos[i].SoLuongBox.ToString("N0");

                            if (ViewChuyenKhos[i].SoLuongDaChuyen >= ViewChuyenKhos[i].Quantity)
                                ViewChuyenKhos[i].ColorSLDaNhap = "#ff0000";
                            else
                                ViewChuyenKhos[i].ColorSLDaNhap = "#000000";
                        }
                    }
                }
            }
        }


        private void UpdateTableChuyenKhos(QRModel item)
        {
            for (int i = 0; i < ViewChuyenKhos.Count; i++)
            {
                if (ViewChuyenKhos[i].ItemCode == item.Code)
                {
                    decimal soluong_ = Convert.ToDecimal(item.Quantity); 

                    ChuyenKhoDungCuModelBPL model_ = ViewChuyenKhos[i];

                    model_.SoLuongDaChuyen += soluong_;
                    model_.sSoLuongDaChuyen = model_.SoLuongDaChuyen.ToString("N0");
                    model_.SoLuongBox += 1;
                    model_.sSoLuongBox = model_.SoLuongBox.ToString("N0");

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
