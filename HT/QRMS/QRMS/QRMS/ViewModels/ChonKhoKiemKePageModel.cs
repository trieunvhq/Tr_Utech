﻿  
using PIAMA.Views.Shared;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Models; 
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq; 
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class ChonKhoKiemKePageModel : BaseViewModel
    {
        public ObservableCollection<ComboModel> Khos { get; set; } = new ObservableCollection<ComboModel>();
        public ComboModel SelectedKho { get; set; }

        public string Name { get; set; }

        public ChonKhoKiemKePageModel()
        {
            LoadModels();
        }


        public void LoadModels()
        {
            var result = APIHelper.GetObjectFromAPIAsync<BaseModel<List<ComboModel>>>
                                              (Constaint.ServiceAddress, Constaint.APIurl.getlistwarehouses, null);
            if (result != null && result.Result != null && result.Result.data != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Khos = new ObservableCollection<ComboModel>();

                    for (int i = 0; i < result.Result.data.Count; ++i)
                    {
                        Khos.Add(new ComboModel
                        {
                            ID = result.Result.data[i].ID,
                            Name = result.Result.data[i].Name,
                        });
                    }
                    if (MySettings.IDKho != "")
                    {
                        SelectedKho = Khos.Where(a => a.ID == MySettings.IDKho).FirstOrDefault();
                        Name = SelectedKho.Name;
                    }
                });
            }
        }

        public void LoadDataCombobox(ComboModel model_, int tt)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (tt)
                {
                    case 3:
                        SelectedKho = model_;
                        Name = SelectedKho.Name;
                        break;
                }

            });
        }
        public void LoadComboxSoLoai()
        {
            var page = new T_ComboboxPage(Khos, null, 3, null,this);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}