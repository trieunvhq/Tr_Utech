 
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using LIB;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.interfaces;
using QRMS.Models;
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
    public class NK_SCANPageModel : BaseViewModel
    {
        public NK_SCANPage _NK_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> NhapKhos { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ObservableCollection<SaleOrderItemScanBPL> XuatKhos { get; set; } = new ObservableCollection<SaleOrderItemScanBPL>();
        public ObservableCollection<ChuyenKhoDungCuModelBPL> ChuyenKhos { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();


        NhapKhoDungCuModel _NhapKhoDungCuModel;
        SaleOrderItemScanBPL _SaleOrderItemScanBPL;
        ChuyenKhoDungCuModelBPL _ChuyenKhoDungCuModelBPL;

        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR = new List<string>();


        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string No { get; set; }


        public NK_SCANPageModel(NK_SCANPage fd)
        {
            No = fd.No;
            _NK_SCANPage = fd;
            LoadModels("");
        }

        public override void OnAppearing()
        {
            if (mSelectedReader == null)
                OpenBarcodeReader();
            _daQuetQR = new List<string>();
            base.OnAppearing();
        }


        protected void LoadDbLocal()
        {
            try
            {
                NhapKhos.Clear();
                ChuyenKhos.Clear();
                XuatKhos.Clear();
                Historys.Clear();

                if (MySettings.Index_Page == 1)
                {
                    List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                    foreach (NhapKhoDungCuModel item in donhang_)
                    {
                        if (!NhapKhos.Contains(item))
                        {
                            //if (item.SoLuongDaNhap >= item.Quantity)
                            //    item.ColorSLDaNhap = "#ff0000";
                            //else
                            //    item.ColorSLDaNhap = "#000000";
                            ////
                            //item.Color = "#000000";
                            ////
                            item.sQuantity = item.Quantity.ToString("N0");
                            item.sSoLuongDaNhap = item.SoLuongDaNhap.ToString("N0");
                            NhapKhos.Add(item);
                        }
                    }

                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_NKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                    foreach (TransactionHistoryModel item in historys)
                    {
                        if (!Historys.Contains(item))
                        {
                            item.token = MySettings.Token;
                            Historys.Add(item);
                        }
                    }
                }
                else if (MySettings.Index_Page == 2)
                {
                    List<SaleOrderItemScanBPL> donhang_ = App.Dblocal.GetSaleOrderItemScanAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                    foreach (SaleOrderItemScanBPL item in donhang_)
                    {
                        if (!XuatKhos.Contains(item))
                        {
                            //if (item.SoLuongDaNhap >= item.Quantity)
                            //    item.ColorSLDaNhap = "#ff0000";
                            //else
                            //    item.ColorSLDaNhap = "#000000";
                            ////
                            //item.Color = "#000000";
                            ////
                            item.sQuantity = item.Quantity.ToString("N0");
                            item.sSoLuongDaNhap = item.SoLuongDaNhap.ToString("N0");
                            XuatKhos.Add(item);
                        }
                    }

                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_XKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                    foreach (TransactionHistoryModel item in historys)
                    {
                        if (!Historys.Contains(item))
                        {
                            item.token = MySettings.Token;
                            Historys.Add(item);
                        }
                    }
                }
                else if (MySettings.Index_Page == 3)
                {
                    List<ChuyenKhoDungCuModelBPL> donhang_ = App.Dblocal.GetTransferInstructionAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode, _NK_SCANPage.WarehouseCode_To);
                    foreach (ChuyenKhoDungCuModelBPL item in donhang_)
                    {
                        if (!ChuyenKhos.Contains(item))
                        {
                            //if (item.SoLuongDaNhap >= item.Quantity)
                            //    item.ColorSLDaNhap = "#ff0000";
                            //else
                            //    item.ColorSLDaNhap = "#000000";
                            ////
                            //item.Color = "#000000";
                            ////
                            item.sQuantity = item.Quantity.ToString("N0");
                            item.sSoLuongDaChuyen = item.SoLuongDaChuyen.ToString("N0");
                            ChuyenKhos.Add(item);
                        }
                    }

                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode, _NK_SCANPage.WarehouseCode_To);
                    foreach (TransactionHistoryModel item in historys)
                    {
                        if (!Historys.Contains(item))
                        {
                            item.token = MySettings.Token;
                            Historys.Add(item);
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
                LoadDbLocal();


                if (MySettings.Index_Page == 1)
                {
                    if (NhapKhos.Count == 0)
                    {
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
                else if (MySettings.Index_Page == 2)
                {
                    if (XuatKhos.Count == 0)
                    {
                        var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<SaleOrderItemScanBPL>>>
                                                     (Constaint.ServiceAddress, Constaint.APIurl.getsaleorderitemscanbarcode,
                                                     new
                                                     {
                                                         SaleOrderNo = _NK_SCANPage.No,
                                                         WarehouseCode = _NK_SCANPage.WarehouseCode
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
                    if (Historys.Count == 0)
                    {
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransactionHistoryModel>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.gethistory,
                                                 new
                                                 {
                                                     OrderNo = _NK_SCANPage.No,
                                                     TransactionType = "O",
                                                     WarehouseCode_From = _NK_SCANPage.WarehouseCode
                                                 });
                        if (result != null && result.Result != null && result.Result.data != null)
                        {
                            //
                            Historys = new ObservableCollection<TransactionHistoryModel>();

                            for (int i = 0; i < result.Result.data.Count; ++i)
                            { 
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
                else if (MySettings.Index_Page == 3)
                {
                    if (ChuyenKhos.Count == 0)
                    {
                        var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<ChuyenKhoDungCuModelBPL>>>
                                                     (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstructionitem,
                                                     new
                                                     {
                                                         TransferOrderNo = _NK_SCANPage.No,
                                                         WarehouseCode_From = _NK_SCANPage.WarehouseCode,
                                                         WarehouseCode_To = _NK_SCANPage.WarehouseCode_To
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
                    if (Historys.Count == 0)
                    {
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransactionHistoryModel>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.gethistory,
                                                 new
                                                 {
                                                     OrderNo = _NK_SCANPage.No,
                                                     TransactionType = "C",
                                                     WarehouseCode_From = _NK_SCANPage.WarehouseCode
                                                 });
                        if (result != null && result.Result != null && result.Result.data != null)
                        {
                            //
                            Historys = new ObservableCollection<TransactionHistoryModel>();

                            for (int i = 0; i < result.Result.data.Count; ++i)
                            { 
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
                if (MySettings.Index_Page == 1)
                {
                    await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                    {
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Historys);
                        if (result != null && result.Result != null)
                        {
                            //MySettings.InsertLogs(0, DateTime.Now, "1inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                NhapKhos.Clear();
                                App.Dblocal.DeleteAllHistory_NKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                                //MySettings.InsertLogs(0, DateTime.Now, "2inserthistory", APICaller.myjson, "NK_SCANPageModel", MySettings.UserName);

                                await Controls.LoadingUtility.HideAsync();

                                //
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công. JSON: ", "Đồng ý", "");
                                LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                            }
                        }
                    });
                }
                else if (MySettings.Index_Page == 2)
                {
                    await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                    {
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Historys);
                        if (result != null && result.Result != null)
                        {
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                XuatKhos.Clear();

                                App.Dblocal.DeleteAllHistory_XKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);
                                App.Dblocal.DeleteSaleOrderItemScanBPLAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode);

                                await Controls.LoadingUtility.HideAsync();

                                //
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công. JSON: ", "Đồng ý", "");
                                LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                            }
                        }
                    });
                }
                else if (MySettings.Index_Page == 3)
                {
                    await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                    {
                        var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Historys);
                        if (result != null && result.Result != null)
                        {
                            if (result.Result.data == 1)
                            {
                                Historys.Clear();
                                ChuyenKhos.Clear();

                                App.Dblocal.DeleteHistory_CKDC(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode, _NK_SCANPage.WarehouseCode_To);
                                App.Dblocal.DeleteTransferInstructionAsyncWithKey(_NK_SCANPage.No, _NK_SCANPage.WarehouseCode, _NK_SCANPage.WarehouseCode_To);

                                await Controls.LoadingUtility.HideAsync();

                                //
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công. JSON: ", "Đồng ý", "");
                                LoadModels("");
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                _NK_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                            }
                        }
                    });
                }
                
            }
            catch (Exception ex)
            {
                Controls.LoadingUtility.HideAsync();

            }
        } 
        public async void ScanComplate(string str)
        {
            string temp_ = "";
            try
            {
                IsThongBao = false; temp_ = "1";
                ThongBao = ""; temp_ = "2";

                if (!_daQuetQR.Contains(str))
                    _daQuetQR.Add(str);
                else
                {
                    IsThongBao = true; 
                    Color = Color.Red;
                    ThongBao = "Nhãn đã được quét";
                    //CloseBarcodeReader();
                    //OpenBarcodeReader();
                    return;
                }

                if (Historys != null)
                { 
                    bool IsTonTai_ = false;
                    int index_ = 0;
                    temp_ = "3";
                    var qr = MySettings.QRRead(str); temp_ = "4";
                    if (qr == null)
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn không đúng định dạng";
                         
                        return;
                    }
                    temp_ = "14";
                    for (int i = 0; i < Historys.Count; ++i)
                    {
                        if (Historys[i].EXT_QRCode == str)
                        {
                            IsTonTai_ = true;
                            index_ = i;
                            break;
                        }
                    }
                    temp_ = "5";
                    if (IsTonTai_)
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao = "Nhãn đã được quét"; temp_ = "6";
                    }
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
                                        _NK_SCANPage.soluong_ = soluong_;
                                        _NK_SCANPage.i = i;
                                        _NK_SCANPage.qr = qr;
                                        _NK_SCANPage.str = str;
                                        temp_ = "11";
                                        //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                        await _NK_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "Huỷ bỏ"); temp_ = "12";

                                    }
                                    else
                                    {
                                        temp_ = "13";
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
                                        _NK_SCANPage.soluong_ = soluong_;
                                        _NK_SCANPage.i = i;
                                        _NK_SCANPage.qr = qr;
                                        _NK_SCANPage.str = str; 
                                        //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                        await _NK_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "Huỷ bỏ");

                                    }
                                    else
                                    {
                                        XuLyTiepLuu(true, soluong_, i, qr, str);
                                    }

                                    //
                                    break;
                                }
                                else if (i == XuatKhos.Count - 1)
                                {
                                    Color = Color.Red;
                                    IsThongBao = true;
                                    ThongBao = "Dụng cụ không có trong phiếu nhập!";
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
                                        _NK_SCANPage.soluong_ = soluong_;
                                        _NK_SCANPage.i = i;
                                        _NK_SCANPage.qr = qr;
                                        _NK_SCANPage.str = str; 
                                        //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                        await _NK_SCANPage.Load_popup_DangXuat("Đã đủ số lượng", "Đồng ý", "Huỷ bỏ");

                                    }
                                    else
                                    {
                                        XuLyTiepLuu(true, soluong_, i, qr, str);
                                    }

                                    //
                                    break;
                                }
                                else if (i == ChuyenKhos.Count - 1)
                                {
                                    Color = Color.Red;
                                    IsThongBao = true;
                                    ThongBao = "Dụng cụ không có trong phiếu nhập!";
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
                    OrderNo = _NK_SCANPage.No,
                    OrderDate = _NK_SCANPage.Date,
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
                    EXT_QRCode = MySettings.DecodeFromUtf8(str),
                    CustomerCode = qr.CustomerCode,
                    ExportStatus = ExportStatus_,
                    RecordStatus = RecordStatus_,
                    WarehouseCode_From = _NK_SCANPage.WarehouseCode,
                    WarehouseName_From = _NK_SCANPage.WarehouseName,
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
                    Color = Color.Blue;
                    IsThongBao = true;
                    ThongBao = "1";
                    //SetScannerAndSymbologySettings();
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "2";
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
    }
}
