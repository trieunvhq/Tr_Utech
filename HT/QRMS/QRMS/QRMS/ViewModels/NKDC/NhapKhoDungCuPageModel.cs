
using Acr.UserDialogs;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
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
    public class NhapKhoDungCuPageModel : BaseViewModel
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


        public NhapKhoDungCuPageModel(string id, string no, DateTime d)
        {
            _ID = id;
            _No = no;
            _Date = d;
            LoadModels("");
        }

        public override void OnAppearing()
        {
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

                                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.updateitem,
                                                DonHangs);
                                if (result2 != null && result2.Result != null)
                                {
                                    if (result2.Result.data == 1)
                                    {
                                        App.Dblocal.DeletePurchaseOrderAsyncWithKey(_No);
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

        private int _trangthai_quet;
        private void ShowThongBao(bool isshow)
        { 
            // 
            if (isshow)
            {
                if (_trangthai_quet==1)
                {
                    Color = Color.Blue;
                    ThongBao = "Mã QR đã được quét"; 
                }
                else if (_trangthai_quet == 2)
                {
                    Color = Color.Green;
                    ThongBao = "Thành công";
                }
                else if (_trangthai_quet == 3)
                {
                    Color = Color.Red;
                    ThongBao = "Mã không tồn tại";
                }
                IsThongBao = true;
            }
            else
            {
                IsThongBao = false;
            }
        }
        public bool isDangQuet = false;
        public async void ScanComplate(string str)
        {
            try
            {
                if (isDangQuet)
                    return;
                if (!_daQuetQR.Contains(str))
                    _daQuetQR.Add(str);
                else
                {
                    StartDemThoiGian_HienThiCam();
                }    

                _trangthai_quet = 0;
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
                        _trangthai_quet = 1; 
                    }
                    else
                    {
                        for (int i = 0; i < DonHangs.Count; ++i)
                        {
                            if (DonHangs[i].ItemCode == qr.Code)
                            {
                                decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                                NhapKhoDungCuModel model_ = DonHangs[i];
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
                                        DonHangs.Insert(0, model_);
                                    }
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
                                    DonHangs.Insert(0, model_);
                                }

                                App.Dblocal.UpdatePurchaseOrderAsync(model_);

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

                                _trangthai_quet = 2;
                                //
                                break;
                            }
                        }
                        if (_trangthai_quet != 2)
                            _trangthai_quet = 3;
                    }
                    StartDemThoiGian_HienThiCam();
                }
                else
                 {
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "NhapKhoDungCuPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
            } 
        }

        private int tt = 10;
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private void StartDemThoiGianGGS()
        {
            StopDemThoiGianGGS();
            CancellationTokenSource cts = this.cancellation;

            Device.StartTimer(TimeSpan.FromSeconds(1),
                  () =>
                  {
                      if (cts.IsCancellationRequested) return false;
                      if (IsThongBao)
                      {
                          if (tt <= 0)
                          {
                              IsThongBao = false;
                              ThongBao = "";
                              ThoiGian = "";
                              StopDemThoiGianGGS();
                          }
                          else
                          {
                              ThoiGian = "  (" + tt + ")";
                          }    
                          --tt;
                      }     
                      return true; // or true for periodic behavior
                  });
        }
        public void StopDemThoiGianGGS()
        {
            tt = 10;
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
        // 
        private CancellationTokenSource cancellation_HienThiCam = new CancellationTokenSource();
        private int tt_HienThiCam = 0;
        private void StartDemThoiGian_HienThiCam()
        {
            StopDemThoiGian_HienThiCam();
            CancellationTokenSource cts = this.cancellation_HienThiCam;

            IsQuet = false;
            ShowThongBao(true);

            Device.StartTimer(TimeSpan.FromSeconds(2),
                  () =>
                  {
                      if (IsTat)
                          StopDemThoiGian_HienThiCam();
                      if (cts.IsCancellationRequested) return false;
                      Device.BeginInvokeOnMainThread(async () =>
                      {
                          isDangQuet = false;
                          IsQuet = true;
                          _NhapKhoDungCuPage.ResetCamera();
                          ShowThongBao(false);
                          StopDemThoiGian_HienThiCam();
                          ++tt_HienThiCam;
                      });
                      return true; // or true for periodic behavior
                  });
        }
        public void StopDemThoiGian_HienThiCam()
        {
            tt_HienThiCam = 0;
            Interlocked.Exchange(ref this.cancellation_HienThiCam, new CancellationTokenSource()).Cancel();
        }
    }
}
