
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using QRMS.Models.KKDC;
using QRMS.Models.Shares;
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

        public string _StartColor { get; set; } = "#00a79d";
        public string _EndColor { get; set; } = "#05aff2";

        public KKDC_CLPageModel(KKDC_CLPage fd)
        {
            _KKDC_CLPage = fd;
            LoadDbLocal();

            if (Historys.Count == 0)
            {
                _StartColor = "#A0A0A0";
                _EndColor = "#E0E0E0";
            }
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
                
                List<TransactionHistoryModel> history_ = App.Dblocal.GetAllHistorySaveServerNoKey_KKDC();
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

                        _StartColor = "#A0A0A0";
                        _EndColor = "#E0E0E0";
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
                {
                    _StartColor = "#A0A0A0";
                    _EndColor = "#E0E0E0";
                    return;
                }

                string xml_ = "";
                int count = 0;

                foreach (TransactionHistoryModel item in Historys)
                {
                    string temp_ = MySettings.MyToString(item) + "❖";
                    if (item.ID == 0 && temp_ != null)
                    {
                        xml_ += temp_;
                        count++;
                    }
                }

                if (count == 0)
                    return;

                xml_ = xml_.Trim('❖');

                TransactionHistoryShortModel Histori_ = new TransactionHistoryShortModel
                {
                    TransactionType = "K",
                    OrderNo = Historys[0].OrderNo,
                    ExportStatus = "N",
                    RecordStatus = "N",
                    WarehouseCode_From = Historys[0].WarehouseCode_From,
                    WarehouseName_From = Historys[0].WarehouseName_From,
                    DATA = xml_
                };

                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                Histori_);
                    if (result != null && result.Result != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (result.Result.data == 1)
                            {
                                App.Dblocal.UpdateAllHistorySavedNoKey_KKDC();
                                await Controls.LoadingUtility.HideAsync();
                                await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                                _StartColor = "#A0A0A0";
                                _EndColor = "#E0E0E0";
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


        private bool InsertToServer()
        {
            try
            {
                LoadDbLocal();

                if (Historys.Count == 0)
                {
                    _StartColor = "#A0A0A0";
                    _EndColor = "#E0E0E0";
                    return false;
                }

                string xml_ = "";
                int count = 0;

                foreach (TransactionHistoryModel item in Historys)
                {
                    string temp_ = MySettings.MyToString(item) + "❖";
                    if (item.ID == 0 && temp_ != null)
                    {
                        xml_ += temp_;
                        count++;
                    }
                }

                if (count == 0)
                    return false;

                xml_ = xml_.Trim('❖');

                TransactionHistoryShortModel Histori_ = new TransactionHistoryShortModel
                {
                    TransactionType = "K",
                    OrderNo = Historys[0].OrderNo,
                    ExportStatus = "N",
                    RecordStatus = "N",
                    WarehouseCode_From = Historys[0].WarehouseCode_From,
                    WarehouseName_From = Historys[0].WarehouseName_From,
                    DATA = xml_
                };

                var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                Histori_);
                if (result != null && result.Result != null)
                {
                    if (result.Result.data == 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
