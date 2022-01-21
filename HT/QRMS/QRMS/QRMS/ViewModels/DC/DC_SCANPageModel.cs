
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models; 
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class DC_SCANPageModel : BaseViewModel
    {
        public DC_SCANPage _DC_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<ChuyenKhoDungCuModelBPL> TongQuats { get; set; } = new ObservableCollection<ChuyenKhoDungCuModelBPL>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;
         

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = ""; 
        public Color Color { get; set; } = Color.Red;

        public string TransferOrderNo { get; set; }

        public DC_SCANPageModel(DC_SCANPage fd)
        {
            _DC_SCANPage = fd;
            TransferOrderNo = fd.TransferOrderNo;
        }

        public override void OnAppearing()
        {
            OpenBarcodeReader();
               _daQuetQR = new List<string>();
            base.OnAppearing();
            LoadModels("");
        }


        public void LoadModels(string id)
        {
            try
            {
                LoadDbLocal();

                if (TongQuats.Count == 0)
                {
                    var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<ChuyenKhoDungCuModelBPL>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstructionitem,
                                                 new
                                                 {
                                                     TransferOrderNo = _DC_SCANPage.TransferOrderNo,
                                                     WarehouseCode_From = _DC_SCANPage.WarehouseCode_From,
                                                     WarehouseCode_To = _DC_SCANPage.WarehouseCode_To
                                                 });
                    if (result2 != null && result2.Result != null && result2.Result.data != null)
                    {
                        //

                        TongQuats = new ObservableCollection<ChuyenKhoDungCuModelBPL>();

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
                                DonHangs.Insert(0, result2.Result.data[i]);
                            }
                            else
                            {
                                DonHangs.Add(result2.Result.data[i]);
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
                                                 OrderNo = _NhapKhoDungCuPage._PurchaseOrderNo,
                                                 TransactionType = "I",
                                                 WarehouseCode_From = _NhapKhoDungCuPage._WarehouseCode
                                             });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        //
                        Historys = new ObservableCollection<TransactionHistoryModel>();

                        for (int i = 0; i < result.Result.data.Count; ++i)
                        {
                            List<TransactionHistoryModel> historys = App.Dblocal.GetHistoryAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);
                            if (!Historys.Contains(result.Result.data[i]))
                            {
                                result.Result.data[i].token = MySettings.Token;
                                Historys.Add(result.Result.data[i]);
                                App.Dblocal.SaveHistoryAsync(result.Result.data[i]);
                            }
                        }
                    }
                }
                IsThongBao = true;
                Color = Color.Red;
                ThongBao = "DonHangsDB: " + DonHangs.Count + " .HistorysDB: " + Historys.Count;
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
            }

        }

        protected void LoadDbLocal()
        {
            try
            {
                TongQuats.Clear();
                Historys.Clear();
                 
                List<string> OrderNo_ = new List<string>();

                List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(_DC_SCANPage.TransferOrderNo, _DC_SCANPage.WarehouseCode_From, _DC_SCANPage.WarehouseCode_To);

                if (MySettings.LenhDiChuyen != "")
                { 
                    if (Historys.Count == 0)
                    {
                        MySettings.LenhDiChuyen = _DC_SCANPage.WarehouseCode_From + "CKDC"
                            + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd");
                    }
                }
                else
                {
                    MySettings.LenhDiChuyen = _DC_SCANPage.WarehouseCode_From + "CKDC"
                        + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd");
                }
                _DC_SCANPage.TransferOrderNo = MySettings.LenhDiChuyen;

                foreach (TransactionHistoryModel item in historys)
                {
                    if (!Historys.Contains(item))
                    {
                        Historys.Add(item);
                    }

                    if(OrderNo_.Contains(item.OrderNo))
                    {
                        for(int i =0;i< TongQuats.Count;++i)
                        {
                            if(TongQuats[i].OrderNo == item.OrderNo)
                            {
                                TongQuats[i].SoLuongQuet = TongQuats[i].SoLuongQuet + item.Quantity;
                                TongQuats[i].SoNhan = 1 + TongQuats[i].SoNhan;
                            }    
                        }    
                    }    
                    else
                    {
                        OrderNo_.Add(item.OrderNo);
                        TongQuats.Add(new CKDCModel
                        {
                            TransactionType = item.TransactionType,
                            OrderNo = item.OrderNo,
                            WarehouseCode_From = item.WarehouseCode_From,
                            WarehouseName_From = item.WarehouseName_From,
                            WarehouseType_From = item.WarehouseType_From,
                            WarehouseCode_To = item.WarehouseCode_To,
                            WarehouseName_To = item.WarehouseName_To,
                            WarehouseType_To = item.WarehouseType_To,
                            ItemCode = item.ItemCode,
                            ItemName = item.ItemName,
                            ItemType = item.ItemType,
                            SoLuongQuet = item.Quantity,
                            sSoLuongQuet = item.Quantity.ToString("N0"),
                            SoNhan = 1,
                            Unit = item.Unit,
                            Color = "#000000",
                            ColorSLDaNhap = "#000000",
                        }) ;
                    }    
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "DC_SCANPageModel", MySettings.UserName);
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
                                App.Dblocal.DeleteHistory_CKDC(_DC_SCANPage.TransferOrderNo, _DC_SCANPage.WarehouseCode_From, _DC_SCANPage.WarehouseCode_To);

                                await Controls.LoadingUtility.HideAsync();
                                _DC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thành công" , "Đồng ý", ""); 
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                _DC_SCANPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", ""); 
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
                     
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
                });
            }
        } 
        public void ScanComplate(string str)
        {
            try
            { 
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
                        IsThongBao = true;
                        Color = Color.Red;
                        ThongBao = "Nhãn đã được quét";
                    }
                    else
                    {
                        for (int i = 0; i < TongQuats.Count; ++i)
                        {
                            if (TongQuats[i].ItemCode == qr.Code)
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                CKDCModel model_ = TongQuats[i];
                                
                                model_.SoLuongQuet = model_.SoLuongQuet + soluong_;
                                model_.SoNhan = model_.SoNhan + 1;
                                TongQuats.RemoveAt(i);

                                model_.ColorSLDaNhap = "#0008ff";

                                model_.Color = "#0008ff";
                                model_.sSoLuongQuet = model_.SoLuongQuet.ToString("N0");
                                TongQuats.Insert(0, model_);
                                  
                                TransactionHistoryModel history = new TransactionHistoryModel
                                {
                                    ID = 0,
                                    TransactionType = "C",
                                    OrderNo = TongQuats[i].OrderNo,
                                    OrderDate = DateTime.Now,
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
                                 
                                //
                                break;
                            }
                            else if (i == TongQuats.Count - 1)
                            {
                                Color = Color.Red;
                                IsThongBao = true;
                                ThongBao = "Dụng cụ không có trong lệnh nhập!";
                            }
                        }
                        IsThongBao = true;
                        Color = Color.Blue;
                        ThongBao = "Thành công";
                    }
                }
                else
                {
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "DC_SCANPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "DC_SCANPageModel", MySettings.UserName);
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
