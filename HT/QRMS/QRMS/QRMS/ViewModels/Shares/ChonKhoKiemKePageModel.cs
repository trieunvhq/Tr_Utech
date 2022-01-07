  
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
        public ObservableCollection<WarehouseBPLModel> Khos { get; set; } = new ObservableCollection<WarehouseBPLModel>();
        public WarehouseBPLModel SelectedKho { get; set; }

        public string WarehouesName { get; set; }

        public ChonKhoKiemKePageModel()
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
                    Khos = new ObservableCollection<WarehouseBPLModel>();

                    for (int i = 0; i < result.Result.data.Count; ++i)
                    {
                        Khos.Add(new WarehouseBPLModel
                        {
                            ID = result.Result.data[i].ID,
                            WarehouesName = result.Result.data[i].WarehouesName,
                        });
                    }
                    if (MySettings.CodeKho != "")
                    {
                        SelectedKho = Khos.Where(a => a.ID == MySettings.CodeKho).FirstOrDefault();
                        WarehouesName = SelectedKho.WarehouesName;
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
                    case 3:
                        SelectedKho = model_;
                        WarehouesName = SelectedKho.WarehouesName;
                        break;
                }

            });
        }
        public void LoadComboxSoLoai()
        {
            var page = new T_ComboboxPage(3, Khos, null, null, null,this, null);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
