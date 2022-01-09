﻿ 
using Acr.UserDialogs;
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq; 
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class KhoPageModel : BaseViewModel
    { 
        public ObservableCollection<WarehouseBPLModel> Khos { get; set; } = new ObservableCollection<WarehouseBPLModel>(); 
        public WarehouseBPLModel SelectedKho { get; set; }

        public string WarehouesName { get; set; }
        public string WarehouseCode { get; set; }


        public KhoPageModel()
        {
            LoadModels();
        } 


        public void LoadModels()
        {
            var result = APIHelper.GetObjectFromAPIAsync<BaseModel<List<WarehouseBPLModel>>>
                                              (Constaint.ServiceAddress, Constaint.APIurl.getlistwarehouses,null);
            if (result != null && result.Result != null && result.Result.data != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Khos = new ObservableCollection<WarehouseBPLModel>();

                    for (int i = 0; i < result.Result.data.Count; ++i)
                    {
                        Khos.Add(new WarehouseBPLModel
                        {
                            ID = result.Result.data[i].ID, 
                            WarehouesName = result.Result.data[i].WarehouesName,
                            WarehouseCode = result.Result.data[i].WarehouseCode,
                        });
                    }
                    if(MySettings.CodeKho!="")
                    {
                        SelectedKho = Khos.Where(a => a.ID == MySettings.CodeKho).FirstOrDefault();
                        WarehouesName = SelectedKho.WarehouesName;
                        WarehouseCode = SelectedKho.WarehouseCode;
                    } 
                });
            }
        }

        public void LoadDataCombobox(WarehouseBPLModel model_, int tt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (tt)
                {
                    case 1:
                        SelectedKho = model_;
                        WarehouesName = SelectedKho.WarehouesName;
                        WarehouseCode = SelectedKho.WarehouseCode;
                        break; 
                }

            });
        } 
        public void LoadComboxSoLoai()
        {
            var page = new T_ComboboxPage(1,Khos,null, this,null, null, null);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
        
    }
}