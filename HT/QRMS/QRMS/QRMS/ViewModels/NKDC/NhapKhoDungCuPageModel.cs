
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using LIB;
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
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class NhapKhoDungCuPageModel : BaseViewModel
    {
        public NhapKhoDungCuPage _NhapKhoDungCuPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> DonHangs { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;
         

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = ""; 
        public Color Color { get; set; } = Color.Red;
   
        public string PurchaseOrderNo { get; set; }
         

        public NhapKhoDungCuPageModel(NhapKhoDungCuPage fd)
        {
            PurchaseOrderNo = fd._PurchaseOrderNo;
            _NhapKhoDungCuPage = fd;  
            LoadModels("");
        }

        public override void OnAppearing()
        {
            if(mSelectedReader==null)
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

                List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);
                foreach (NhapKhoDungCuModel item in donhang_)
                {
                    if (!DonHangs.Contains(item))
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
                        DonHangs.Add(item);
                    }
                }

                List<TransactionHistoryModel> historys = App.Dblocal.GetHistoryAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);
                foreach (TransactionHistoryModel item in historys)
                {
                    if (!Historys.Contains(item))
                    {
                        item.token = MySettings.Token;
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
                //LoadDbLocal();

                if (DonHangs.Count == 0)
                {
                    var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<List<NhapKhoDungCuModel>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.getpurchaseorderitem,
                                                 new
                                                 {
                                                     PurchaseOrderNo = _NhapKhoDungCuPage._PurchaseOrderNo,
                                                     WarehouseCode = _NhapKhoDungCuPage._WarehouseCode
                                                 });
                    if (result2 != null && result2.Result != null && result2.Result.data != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {  
                            MySettings.InsertLogs(0, DateTime.Now, "getpurchaseorderitem", APICaller.myjson, "getpurchaseorderitem", MySettings.UserName);
                            //

                            DonHangs = new ObservableCollection<NhapKhoDungCuModel>();

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
                                    DonHangs.Insert(0, result2.Result.data[i]);
                                }
                                else
                                {
                                    DonHangs.Add(result2.Result.data[i]);
                                }

                                App.Dblocal.SavePurchaseOrderAsync(result2.Result.data[i]);
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
                                                 OrderNo = _NhapKhoDungCuPage._PurchaseOrderNo,
                                                 TransactionType = "I",
                                                 WarehouseCode_From = _NhapKhoDungCuPage._WarehouseCode
                                             });
                    if (result != null && result.Result != null && result.Result.data != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MySettings.InsertLogs(0, DateTime.Now, "gethistory", APICaller.myjson, "gethistory", MySettings.UserName);
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
                        });
                    }
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
                                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);
                                App.Dblocal.DeleteHistoryAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);


                                //App.Dblocal.DeleteHistoryAll();
                                //App.Dblocal.DeletePurchaseOrderAll();

                                Historys.Clear();
                                DonHangs.Clear();

                                await Controls.LoadingUtility.HideAsync();

                                string strdsdf_ = "";
                                for(int i=0;i<Historys.Count;++i)
                                {
                                    strdsdf_ = Historys[i].Unit;
                                }
                                MySettings.InsertLogs(0, DateTime.Now, "QR", strdsdf_, "QR", MySettings.UserName);
                                //
                                _NhapKhoDungCuPage.Load_popup_DangXuat("Bạn đã lưu thành công. JSON: ", "Đồng ý", "");
                                LoadModels("");

                                MySettings.InsertLogs(0, DateTime.Now, "Bạn đã lưu thành công", APICaller.myjson, "Bạn đã lưu thành công", MySettings.UserName);
                                //var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                //                (Constaint.ServiceAddress, Constaint.APIurl.updateitem,
                                //                DonHangs);
                                //if (result2 != null && result2.Result != null)
                                //{
                                //    if (result2.Result.data == 1)
                                //    {
                                //        App.Dblocal.DeletePurchaseOrderAsyncWithKey(_NhapKhoDungCuPage._PurchaseOrderNo);
                                //        DonHangs.Clear();

                                //        await Controls.LoadingUtility.HideAsync();
                                //        await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thành công", "Thành công", "Đồng ý", "");
                                //        LoadModels("");
                                //    }
                                //    else
                                //    {
                                //        await Controls.LoadingUtility.HideAsync();
                                //        await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thất bại", "Thất bại", "Đồng ý", "");
                                //    }
                                //}
                                //else
                                //{
                                //    await Controls.LoadingUtility.HideAsync();
                                //    await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thất bại", "Thất bại", "Đồng ý", "");
                                //}     
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                string strrr_ = "ErrorCode: " + result.Result.ErrorCode
                                    + ". RespondCode: " + result.Result.RespondCode
                                    + ". data: " + result.Result.data.ToString()
                                    + ". JSON" + APICaller.myjson;

                                MySettings.InsertLogs(0, DateTime.Now, "Bạn đã lưu thất bại", strrr_, "Bạn đã lưu thành công", MySettings.UserName);

                                _NhapKhoDungCuPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
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
                     
                    MySettings.InsertLogs(0,DateTime.Now, "LuuLais", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
                });
            }
        }
         
        public bool isDangQuet = false;
        public async void ScanComplate(string str)
        {
            try
            { 
                IsThongBao = false;
                ThongBao = "";
                
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
                    isDangQuet = true;
                    bool IsTonTai_ = false;
                    int index_ = 0;
                  
                    var qr = MySettings.QRRead(str);
                    if(qr==null)
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
                    }
                    else
                    {
                        for (int i = 0; i < DonHangs.Count; ++i)
                        {
                            if (DonHangs[i].ItemCode == qr.Code
                                && (DonHangs[i].Serial == null
                                    || DonHangs[i].Serial == ""
                                    || DonHangs[i].Serial == "None"
                                    || (DonHangs[i].Serial == qr.Serial)))
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                NhapKhoDungCuModel model_ = DonHangs[i];
                   
                                if (model_.Quantity < model_.SoLuongDaNhap + soluong_)
                                { 
                                    _NhapKhoDungCuPage.model_ = model_;
                                    _NhapKhoDungCuPage.soluong_ = soluong_;
                                    _NhapKhoDungCuPage.i = i;
                                    _NhapKhoDungCuPage.qr = qr;
                                    _NhapKhoDungCuPage.str = str;
                                    //var answer = await UserDialogs.Instance.ConfirmAsync(, "Vượt quá số lượng", );
                                    await _NhapKhoDungCuPage.Load_popup_DangXuat("Đã đủ số lượng cần nhập", "Đồng ý", "Huỷ bỏ");
                                      
                                }
                                else
                                {
                                    XuLyTiepLuu(true, model_, soluong_, i, qr, str);
                                }

                                //
                                break;
                            }
                            else if(i == DonHangs.Count - 1)
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
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "NhapKhoDungCuPageModel", MySettings.UserName);
                }

                //CloseBarcodeReader();
                //OpenBarcodeReader();
            }
            catch (Exception ex)
            {
                //CloseBarcodeReader();
                //OpenBarcodeReader();
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
            } 
        }

        public void XuLyTiepLuu(bool iscoluu, NhapKhoDungCuModel model_, decimal soluong_,int i,QRModel qr, string str)
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
                model_.sSoLuongBox = model_.SoLuongBox.ToString("N0");
      
                DonHangs.Insert(0, model_);

                App.Dblocal.UpdatePurchaseOrderAsync(model_);


                TransactionHistoryModel history = new TransactionHistoryModel
                {
                    ID = 0,
                    TransactionType = "I",
                    OrderNo = _NhapKhoDungCuPage._PurchaseOrderNo,
                    OrderDate = _NhapKhoDungCuPage._PurchaseOrderDate,
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
                    WarehouseCode_From = _NhapKhoDungCuPage._WarehouseCode,
                    WarehouseName_From = _NhapKhoDungCuPage._WarehouseName,
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
            isDangQuet = false;
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
