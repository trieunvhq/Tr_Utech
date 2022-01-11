
using Acr.UserDialogs;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq; 
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class DC_ChoKhoPageModel : BaseViewModel
    {
        public ObservableCollection<WarehouseBPLModel> Kho1s { get; set; } = new ObservableCollection<WarehouseBPLModel>();
        public WarehouseBPLModel SelectedKho1 { get; set; }

        public string WarehouesName1 { get; set; }
        public string WarehouesCode1 { get; set; }


        public ObservableCollection<WarehouseBPLModel> Kho2s { get; set; } = new ObservableCollection<WarehouseBPLModel>();
        public WarehouseBPLModel SelectedKho2 { get; set; }

        public string WarehouesName2 { get; set; }
        public string WarehouesCode2 { get; set; }

        public DC_ChoKhoPageModel()
        {
            LoadModels();
        }


        public void LoadModels()
        {
            var result = APIHelper.GetObjectFromAPIAsync<BaseModel<List<WarehouseBPLModel>>>
                                              (Constaint.ServiceAddress, Constaint.APIurl.getlistwarehouses, null);
            if (result != null && result.Result != null && result.Result.data != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Kho1s = new ObservableCollection<WarehouseBPLModel>();
                    Kho2s = new ObservableCollection<WarehouseBPLModel>();

                    for (int i = 0; i < result.Result.data.Count; ++i)
                    {
                        Kho1s.Add(new WarehouseBPLModel
                        {
                            ID = result.Result.data[i].ID,
                            WarehouesName = result.Result.data[i].WarehouesName,
                            WarehouseCode = result.Result.data[i].WarehouseCode,
                        });
                        Kho2s.Add(new WarehouseBPLModel
                        {
                            ID = result.Result.data[i].ID,
                            WarehouesName = result.Result.data[i].WarehouesName,
                            WarehouseCode = result.Result.data[i].WarehouseCode,
                        });
                    }
                    if (MySettings.CodeKho != "")
                    {
                        SelectedKho1 = Kho1s.Where(a => a.WarehouseCode == MySettings.CodeKho).FirstOrDefault();
                        if (SelectedKho1 != null)
                        {
                            WarehouesName1 = SelectedKho1.WarehouesName;
                            WarehouesCode1 = SelectedKho1.WarehouseCode;
                        }
                    }
                });
            }
        }

        public void LoadDataCombobox(WarehouseBPLModel model_)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if(_loai_kho=="1")
                {
                    SelectedKho1 = model_;
                    WarehouesName1 = SelectedKho1.WarehouesName;
                    WarehouesCode1 = SelectedKho1.WarehouseCode;
                }
                else if (_loai_kho == "2")
                {
                    SelectedKho2 = model_;
                    WarehouesName2 = SelectedKho2.WarehouesName;
                    WarehouesCode2 = SelectedKho2.WarehouseCode;
                }    
            });
        }
        public string _loai_kho = "1";
        public void LoadComboxSoLoai()
        {
            MySettings.Title = "Chọn kho";
            var page = new T_ComboboxPage(Kho1s, null);
            page._DC_ChoKhoPageModel = this;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async void LuuLais()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    List<CKDCModel> CKDCModel_ = App.Dblocal.GetTransactionHistory_CKDC(WarehouesCode1+"_"+ WarehouesCode2, WarehouesCode1, WarehouesCode2);
                    
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                                                (Constaint.ServiceAddress, Constaint.APIurl.inserthistory,
                                                CKDCModel_);
                    if (result != null && result.Result != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (result.Result.data == 1)
                            {
                                App.Dblocal.DeleteHistoryAsyncWithKey(_No);
                                 
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

    }
}
