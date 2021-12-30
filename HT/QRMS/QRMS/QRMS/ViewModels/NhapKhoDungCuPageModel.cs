﻿
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
        private string _ID = "";
        public NhapKhoDungCuPageModel(string id)
        {
            _ID = id;
            LoadModels("");
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
                    ThongBao = "Mã QR đã được quét";
                    IsThongBao = true; 
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

                            if(temp_[7].Length>=8)
                            {
                                try { mfdate_ = new DateTime(Convert.ToInt32(temp_[7].Substring(0, 2)), Convert.ToInt32(temp_[7].Substring(2, 2)), Convert.ToInt32(temp_[7].Substring(4, 4))); }
                                catch { mfdate_ = null; }
                            }
                            else
                            { mfdate_ = null; }
                            // 
                            if (temp_[8].Length >= 8)
                            {
                                try { Recdate_ = new DateTime(Convert.ToInt32(temp_[8].Substring(0, 2)), Convert.ToInt32(temp_[8].Substring(2, 2)), Convert.ToInt32(temp_[8].Substring(4, 4))); }
                                catch { Recdate_ = null; }
                            }
                            else
                            { Recdate_ = null; }
                            // 
                            if (temp_[9].Length >= 8)
                            {
                                try { Expdate_ = new DateTime(Convert.ToInt32(temp_[9].Substring(0, 2)), Convert.ToInt32(temp_[9].Substring(2, 2)), Convert.ToInt32(temp_[9].Substring(4, 4))); }
                                catch { Expdate_ = null; }
                            }
                            else
                            { Expdate_ = null; }
                            //  
                            //DateTime.TryParse(temp_[7], out mfdate_);
                            //DateTime.TryParse(temp_[8], out Recdate_);
                            //DateTime.TryParse(temp_[9], out Expdate_);
                            Historys.Add(new TransactionHistoryBPLModel
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
                                UserCreate = MySettings.UserName

                            });
                            //
                            ThongBao = "Mã QR đã được quét";
                            IsThongBao = true; 
                            break;
                        } 
                    }    
                    
                }    
            }    
        }
    }
}
