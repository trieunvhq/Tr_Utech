
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
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
    public class KKDC_CLPageModel : BaseViewModel
    {
        public KKDC_CLPage _KKDC_CLPage; 
        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();

        public KKDC_CLPageModel(KKDC_CLPage fd)
        {
            _KKDC_CLPage = fd;
            LoadDbLocal();          
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }


        protected void LoadDbLocal()
        {
            try
            {
                Historys.Clear();
                
                List<TransactionHistoryModel> history_ = App.Dblocal.GetAllHistoryNoKey_KKDC();
                foreach (TransactionHistoryModel item in history_)
                {
                    if (!Historys.Contains(item))
                    {
                        Historys.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MySettings.InsertLogs(0, DateTime.Now, "LoadDbLocal", ex.Message, "KKDC_CLPageModel", MySettings.UserName);
            }
        }

        public async void DeleteDBLocal()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        App.Dblocal.DeleteHistoryNoKey_KKDC();

                        await Controls.LoadingUtility.HideAsync();
                        await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã xoá dữ liệu thành công", "Đồng ý", "");
                    });
                });
            }
            catch (Exception ex)
            {
                await Controls.LoadingUtility.HideAsync();
                await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");

                MySettings.InsertLogs(0, DateTime.Now, "DeleteDBLocal", ex.Message, "KK_SCANPageModel", MySettings.UserName);
            }
        }

        public async void SaveDBServer()
        {
            try
            {
                LoadDbLocal();

                if (Historys.Count == 0)
                    return;

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
                                await Controls.LoadingUtility.HideAsync();
                                await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu dữ liệu server thành công", "Đồng ý", ""); 
                            }
                            else
                            {
                                await Controls.LoadingUtility.HideAsync();
                                await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", ""); 
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
                     
                    MySettings.InsertLogs(0, DateTime.Now, "LuuLais", ex.Message, "KK_SCANPageModel", MySettings.UserName);
                });
            }
        }
    }
}
