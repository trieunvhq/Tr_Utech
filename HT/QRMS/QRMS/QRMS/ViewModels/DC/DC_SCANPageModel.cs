
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models; 
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;  
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class DC_SCANPageModel : BaseViewModel
    {
        public DC_SCANPage _DC_SCANPage;
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<CKDCModel> TongQuats { get; set; } = new ObservableCollection<CKDCModel>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        private string _TuKho = "";
        private string _DenKho = "";
        private string _LenhDC = "";


        public DC_SCANPageModel(string TuKho_, string DenKho_)
        {
            _TuKho = TuKho_;
            _DenKho = DenKho_; 
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
                TongQuats.Clear();
                Historys.Clear();
                 
                List<string> OrderNo_ = new List<string>();

                List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(_LenhDC, _TuKho, _DenKho);

                if (MySettings.LenhDiChuyen != "")
                { 
                    if (Historys.Count == 0)
                    {
                        MySettings.LenhDiChuyen = _TuKho + "CKDC"
                            + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd");
                    }
                }
                else
                {
                    MySettings.LenhDiChuyen = _TuKho + "CKDC"
                        + DateTime.Now.Date.ToString("yy") + DateTime.Now.Date.ToString("MM") + DateTime.Now.Date.ToString("dd");
                }
                _LenhDC = MySettings.LenhDiChuyen;

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
                                App.Dblocal.DeleteHistory_CKDC(_LenhDC, _TuKho, _DenKho);

                                await Controls.LoadingUtility.HideAsync();
                                await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thành công", "Thành công", "Đồng ý", "");
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
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "NhapKhoDungCuPageModel", MySettings.UserName);
                });
            }
        }
        private int _trangthai_quet;
        private void ShowThongBao(bool isshow)
        {
            // 
            if (isshow)
            {
                if (_trangthai_quet == 1)
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
                    IsQuet = false;
                    ShowThongBao(true);
                    //StartDemThoiGian_HienThiCam();
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

                                _trangthai_quet = 2;
                                //
                                break;
                            }
                        }
                        if (_trangthai_quet != 2)
                            _trangthai_quet = 3;
                    }
                    IsQuet = false;
                    ShowThongBao(true);
                    //StartDemThoiGian_HienThiCam();
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

        //private int tt = 10;
        //private CancellationTokenSource cancellation = new CancellationTokenSource();
        //private void StartDemThoiGianGGS()
        //{
        //    StopDemThoiGianGGS();
        //    CancellationTokenSource cts = this.cancellation;

        //    Device.StartTimer(TimeSpan.FromSeconds(1),
        //          () =>
        //          {
        //              if (cts.IsCancellationRequested) return false;
        //              if (IsThongBao)
        //              {
        //                  if (tt <= 0)
        //                  {
        //                      IsThongBao = false;
        //                      ThongBao = "";
        //                      ThoiGian = "";
        //                      StopDemThoiGianGGS();
        //                  }
        //                  else
        //                  {
        //                      ThoiGian = "  (" + tt + ")";
        //                  }    
        //                  --tt;
        //              }     
        //              return true; // or true for periodic behavior
        //          });
        //}
        //public void StopDemThoiGianGGS()
        //{
        //    tt = 10;
        //    Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        //}
        //// 
        //private CancellationTokenSource cancellation_HienThiCam = new CancellationTokenSource();
        //private int tt_HienThiCam = 0;
        //private void StartDemThoiGian_HienThiCam()
        //{
        //    StopDemThoiGian_HienThiCam();
        //    CancellationTokenSource cts = this.cancellation_HienThiCam;

        //    IsQuet = false;
        //    ShowThongBao(true);

        //    Device.StartTimer(TimeSpan.FromSeconds(2),
        //          () =>
        //          {
        //              if (IsTat)
        //                  StopDemThoiGian_HienThiCam();
        //              if (cts.IsCancellationRequested) return false;
        //              Device.BeginInvokeOnMainThread(async () =>
        //              {
        //                  isDangQuet = false;
        //                  IsQuet = true;
        //                  _DC_SCANPage.ResetCamera();
        //                  ShowThongBao(false);
        //                  StopDemThoiGian_HienThiCam();
        //                  ++tt_HienThiCam;
        //              });
        //              return true; // or true for periodic behavior
        //          });
        //}
        //public void StopDemThoiGian_HienThiCam()
        //{
        //    tt_HienThiCam = 0;
        //    Interlocked.Exchange(ref this.cancellation_HienThiCam, new CancellationTokenSource()).Cancel();
        //}
    }
}
