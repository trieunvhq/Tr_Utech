
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
        public ObservableCollection<TransactionHistoryBPLModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryBPLModel>();
        public ObservableCollection<NhapKhoDungCuModel> DonHangs { get; set; } = new ObservableCollection<NhapKhoDungCuModel>();
        public ComboModel SelectedDonHang { get; set; }

        public bool IsThongBao { get; set; } = false;
        public string ThongBao { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        private string _ID = "";


        public NhapKhoDungCuPageModel(string id)
        {
            _ID = id;
            LoadDbLocal();
            LoadModels("");
        }

        protected async void LoadDbLocal()
        {
            List<TransactionHistoryBPLModel> historys = await App.Dblocal.GetHistoryAsync();
            foreach(TransactionHistoryBPLModel item in historys)
            {
                if (!Historys.Contains(item))
                {
                    Historys.Add(item);
                }
            }
        }

        public void LoadModels(string id)
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
                        if(result.Result.data[i].ItemCode==id)
                        {
                            DonHangs.Insert(0, result.Result.data[i]);
                        }
                        else
                        {
                            DonHangs.Add(result.Result.data[i]);
                        }    
                    } 
                });
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
                                App.Dblocal.DeleteHistoryAll();

                                var result2 = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.updateitem,
                                                DonHangs);
                                if (result2 != null && result2.Result != null)
                                {
                                    if (result2.Result.data == 1)
                                    {
                                        await Controls.LoadingUtility.HideAsync();
                                        await UserDialogs.Instance.ConfirmAsync("Bạn đã lưu thành công", "Thành công", "Đồng ý", "");
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
            string[] temp_ = str.Split(';');
            if(Historys!=null)
            {
                bool IsTonTai_ = false;
                int index_ = 0;
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
                        if(DonHangs[i].ItemCode== temp_[1])
                        {
                            int soluong_ = Convert.ToInt32(temp_[10]);
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
                            //
                            DateTime? mfdate_;
                            DateTime? Recdate_;
                            DateTime? Expdate_;

                            string[] ngaythang_ = new string[3];
                            if(temp_[7].Length==8)
                            {
                                try { mfdate_ = new DateTime(Convert.ToInt32(temp_[7].Substring(4, 4)), Convert.ToInt32(temp_[7].Substring(2, 2)), Convert.ToInt32(temp_[7].Substring(0, 2))); }
                                catch { mfdate_ = null; }
                            }
                            else if (temp_[7].Length > 8)
                            {
                                temp_[7]= temp_[7].Replace("-","/").Replace("\\","/");
                                ngaythang_ = temp_[7].Split('/');
                                try { mfdate_ = new DateTime(Convert.ToInt32(ngaythang_[2]), Convert.ToInt32(ngaythang_[1]), Convert.ToInt32(ngaythang_[0])); }
                                catch { mfdate_ = null; }
                            }
                            else
                            { mfdate_ = null; }
                            //
                            if (temp_[8].Length == 8)
                            {
                                try { Recdate_ = new DateTime(Convert.ToInt32(temp_[8].Substring(4, 4)), Convert.ToInt32(temp_[8].Substring(2, 2)), Convert.ToInt32(temp_[8].Substring(0, 2))); }
                                catch { Recdate_ = null; }
                            }
                            else if (temp_[8].Length > 8)
                            {
                                temp_[8] = temp_[8].Replace("-", "/").Replace("\\", "/");
                                ngaythang_ = temp_[8].Split('/');
                                try { Recdate_ = new DateTime(Convert.ToInt32(ngaythang_[2]), Convert.ToInt32(ngaythang_[1]), Convert.ToInt32(ngaythang_[0])); }
                                catch { Recdate_ = null; }
                            }
                            else
                            { Recdate_ = null; }
                            //
                            if (temp_[9].Length == 8)
                            {
                                try { Expdate_ = new DateTime(Convert.ToInt32(temp_[9].Substring(4, 4)), Convert.ToInt32(temp_[9].Substring(2, 2)), Convert.ToInt32(temp_[9].Substring(0, 2))); }
                                catch { Expdate_ = null; }
                            }
                            else if (temp_[9].Length > 8)
                            {
                                temp_[9] = temp_[9].Replace("-", "/").Replace("\\", "/");
                                ngaythang_ = temp_[9].Split('/');
                                try { Expdate_ = new DateTime(Convert.ToInt32(ngaythang_[2]), Convert.ToInt32(ngaythang_[1]), Convert.ToInt32(ngaythang_[0])); }
                                catch { Expdate_ = null; }
                            }
                            else
                            { Expdate_ = null; }



                            //  
                            //DateTime.TryParse(temp_[7], out mfdate_);
                            //DateTime.TryParse(temp_[8], out Recdate_);
                            //DateTime.TryParse(temp_[9], out Expdate_);

                            TransactionHistoryBPLModel history = new TransactionHistoryBPLModel
                            {
                                TransactionType = "I",
                                ID = 0,
                                ItemCode = temp_[1],
                                ItemName = temp_[2],
                                ItemType = temp_[0],
                                Quantity = soluong_,
                                Unit = temp_[11],
                                EXT_OtherCode = temp_[3],
                                EXT_Serial = temp_[4],
                                EXT_PartNo = temp_[5],
                                EXT_LotNo = temp_[6],
                                EXT_MfDate = mfdate_,
                                EXT_RecDate = Recdate_,
                                EXT_ExpDate = Expdate_,
                                EXT_QRCode = str,
                                CustomerCode = temp_[3],
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
