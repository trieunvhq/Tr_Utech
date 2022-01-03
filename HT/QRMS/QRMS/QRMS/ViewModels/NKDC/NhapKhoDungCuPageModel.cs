
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
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();
        public ObservableCollection<NhapKhoDungCuModel> DonHangs { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ComboModel SelectedDonHang { get; set; }

        public bool IsThongBao { get; set; } = false;
        public string ThongBao { get; set; } = "";
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

        protected async void LoadDbLocal()
        {
            DonHangs.Clear();
            Historys.Clear();

            List<NhapKhoDungCuModel> donhang_ = await App.Dblocal.GetPurchaseOrderAsyncWithKey(_No);
            foreach (NhapKhoDungCuModel item in donhang_)
            {
                if (!DonHangs.Contains(item))
                {
                    DonHangs.Add(item);
                }
            }

            List<TransactionHistoryModel> historys = await App.Dblocal.GetHistoryAsyncWithKey(_No);
            foreach(TransactionHistoryModel item in historys)
            {
                if (!Historys.Contains(item))
                {
                    Historys.Add(item);
                }
            }
        }

        public void LoadModels(string id)
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
                            if (result.Result.data == 0)
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
                });
            }
        }
         
        public async void ScanComplate(string str)
        {
            if(Historys!=null)
            {
                bool IsTonTai_ = false;
                int index_ = 0;
                var qr = MySettings.QRRead(str);

                for (int i=0;i<Historys.Count;++i)
                {
                    if(Historys[i].EXT_QRCode == str)
                    {
                        IsTonTai_ = true;
                        index_ = i;
                        break;
                    }    
                }

                if (IsTonTai_)
                {
                    Color = Color.Red;
                    ThongBao = "Mã QR đã được quét";
                    IsThongBao = true;
                    StartDemThoiGianGGS();
                }
                else
                {
                    for(int i=0;i< DonHangs.Count;++i)
                    { 
                        if(DonHangs[i].ItemCode== qr.Code)
                        {
                            decimal soluong_ = Convert.ToDecimal(qr.Quantity);
                            NhapKhoDungCuModel model_ = DonHangs[i];
                            if(model_.Quantity < model_.SoLuongDaNhap + soluong_)
                            { 
                                var answer = await UserDialogs.Instance.ConfirmAsync("Bạn đã nhập kho vượt quá số lượng đơn mua", "Vượt quá số lượng", "Đồng ý", "Huỷ bỏ");
                                if (answer)
                                {
                                    model_.SoLuongDaNhap = model_.SoLuongDaNhap + soluong_;
                                    model_.SoLuongBox = model_.SoLuongBox + 1;
                                    DonHangs.RemoveAt(i);
                                    DonHangs.Insert(0, model_);
                                }
                            }
                            else
                            {
                                model_.SoLuongDaNhap = model_.SoLuongDaNhap + soluong_;
                                model_.SoLuongBox = model_.SoLuongBox + 1;
                                DonHangs.RemoveAt(i);
                                DonHangs.Insert(0, model_);
                            }

                            await App.Dblocal.UpdatePurchaseOrderAsync(model_);

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
                                RecordStatus = "N",
                                CreateDate = DateTime.Now,
                                UserCreate = MySettings.UserName,
                                page = 0,
                                token = MySettings.Token
                            };

                            Historys.Add(history);
                            await App.Dblocal.SaveHistoryAsync(history);
                            
                            //
                            Color = Color.Green;
                            ThongBao = "Thành công";
                            IsThongBao = true;
                            StartDemThoiGianGGS();
                            break;
                        } 
                    }    
                }    
            }    
        }

        private bool tt = false;
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private void StartDemThoiGianGGS()
        {
            StopDemThoiGianGGS();
            CancellationTokenSource cts = this.cancellation;

            Device.StartTimer(TimeSpan.FromSeconds(3),
                  () =>
                  {
                      if (cts.IsCancellationRequested) return false;
                      if (IsThongBao)
                      {
                          if (tt)
                          {
                              tt = false;
                              IsThongBao = false;
                              ThongBao = "";
                              StopDemThoiGianGGS();
                          }
                          tt = true;
                      }     
                      return true; // or true for periodic behavior
                  });
        }
        public void StopDemThoiGianGGS()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
    }
}
