
using Acr.UserDialogs;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.KKDC;
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
    public class KK_SCANPageModel : BaseViewModel
    {
        public KK_SCANPage _KK_SCANPage; 
        public ObservableCollection<KKDCModel> Historys { get; set; } = new ObservableCollection<KKDCModel>();
        public ComboModel SelectedDonHang { get; set; }

        private List<string> _daQuetQR;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;

        public string WarehouesCode { get; set; } = "";

        public KK_SCANPageModel()
        {
            LoadDbLocal();
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
                Historys.Clear();

                List<KKDCModel> donhang_ = App.Dblocal.GetTransactionHistory_KKDC(WarehouesCode, WarehouesCode);
                foreach (KKDCModel item in donhang_)
                {
                    if (!Historys.Contains(item))
                    {
                       
                        Historys.Add(item);
                    }
                } 
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "KK_SCANPageModel", MySettings.UserName);
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
                                Historys.Clear();

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
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "KK_SCANPageModel", MySettings.UserName);
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
                                        if (model_.SoLuongDaNhap >= model_.Quantity)
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
                                    TransactionType = "K",
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
                    IsQuet = false;
                    ShowThongBao(true);
                    //StartDemThoiGian_HienThiCam();
                }
                else
                {
                    MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", "Historys == null", "KK_SCANPageModel", MySettings.UserName);
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "ScanComplate", ex.Message, "KK_SCANPageModel", MySettings.UserName);
            }
        } 
    }
}
