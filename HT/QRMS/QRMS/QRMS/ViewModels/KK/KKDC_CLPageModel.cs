
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
        private const int _Part = 10000;
        public KKDC_CLPage _KKDC_CLPage;

        public ObservableCollection<TransactionHistoryModel> Historys { get; set; } = new ObservableCollection<TransactionHistoryModel>();

        public KKDC_CLPageModel(KKDC_CLPage fd)
        {
            _KKDC_CLPage = fd;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadDbLocal();

            if (Historys.Count == 0)
                _KKDC_CLPage.LoadColor(0);
            else
                _KKDC_CLPage.LoadColor(1);
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
                        _KKDC_CLPage.LoadColor(0);
                        await Controls.LoadingUtility.HideAsync();
                        await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã xoá dữ liệu thành công", "Đồng ý", "");
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Controls.LoadingUtility.HideAsync();
                    await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                    MySettings.InsertLogs(0, DateTime.Now, "DeleteDBLocal", ex.Message, "KK_SCANPageModel", MySettings.UserName);
                });
            }
        }

        public async void SaveDBServer()
        {
            try
            {
                //LoadDbLocal();

                //for (int i = 0; i < 10; i++)
                //{
                //    TransactionHistoryModel his_ = Historys[0];
                //    Historys.Add(his_);
                //}    

                //if (Historys.Count == 0)
                //{
                //    _KKDC_CLPage.LoadColor(0);
                //    return;
                //}

                //await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                //{
                //    var result = InsertToServer();
                //    Device.BeginInvokeOnMainThread(async () =>
                //    {
                //        if (result)
                //        {
                //            _KKDC_CLPage.LoadColor(0);
                //            App.Dblocal.UpdateAllHistorySavedNoKey_KKDC();
                //            await Controls.LoadingUtility.HideAsync();
                //            await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
                //        }
                //        else
                //        {
                //            await Controls.LoadingUtility.HideAsync();
                //            await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thất bại", "Đồng ý", "");
                //        }
                //    });
                //});

                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    LoadDbLocal();

                    if (Historys.Count == 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _KKDC_CLPage.LoadColor(0);
                            Controls.LoadingUtility.HideAsync();
                            return;
                        });
                    }

                    //for (int i = 0; i < 10000; i++)
                    //{
                    //    TransactionHistoryModel his_ = Historys[0];
                    //    Historys.Add(his_);
                    //}

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
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Controls.LoadingUtility.HideAsync();
                            return;
                        });
                    }

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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (result.Result.data == 1)
                            {
                                _KKDC_CLPage.LoadColor(0);
                                App.Dblocal.UpdateAllHistorySavedNoKey_KKDC();
                                await Controls.LoadingUtility.HideAsync();
                                await _KKDC_CLPage.Load_popup_DangXuat("Bạn đã lưu thành công", "Đồng ý", "");
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
            bool Result_ = true;
            try
            { 
                bool isSending = true;
                int NumSended = 0;
                int index_his = 0;

                while (isSending)
                {
                    string xml_ = "";
                    int count = 0;
                    string status_ = "";

                    if (NumSended == 0)
                        status_ = "start";
                    else
                        status_ = "sending";

                    for (int i = 0; i < _Part; i++)
                    {
                        string temp_ = MySettings.MyToString(Historys[index_his]) + "❖";
                        if (Historys[index_his].ID == 0 && temp_ != null)
                        {
                            xml_ += temp_;
                            count++;
                        }

                        if (index_his == Historys.Count - 1)
                        {
                            status_ = "end";
                            isSending = false;
                            break;
                        }

                        index_his++;
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
                        Key = MySettings.Token,
                        Status = status_,
                        DATA = xml_
                    };

                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                    (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                    Histori_);
                    if (result != null && result.Result != null)
                    {
                        if (result.Result.data != 1)
                        {
                            Result_ = false;
                        }
                    }
                    else
                        Result_ = false;

                    NumSended++;
                }    
            }
            catch (Exception ex)
            {
                Result_ = false;
            }

            return Result_;
        }

    }
}
