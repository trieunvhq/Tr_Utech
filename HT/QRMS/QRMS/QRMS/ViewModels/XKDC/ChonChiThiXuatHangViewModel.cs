  
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
    public class ChonChiThiXuatHangViewModel : BaseViewModel
    {
        public ObservableCollection<ComboModel> DonHangs { get; set; } = new ObservableCollection<ComboModel>();
        public ComboModel SelectedDonHang { get; set; }


        public DateTime TuNgay { get; set; } = DateTime.Now;
        public DateTime DenNgay { get; set; } = DateTime.Now;
        public string Name { get; set; }

        public ChonChiThiXuatHangViewModel()
        {
            LoadModels(false);
        }


        public void LoadModels(bool tt)
        {
            var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransferInstructionModel>>>
                                              (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstruction, new {
                                                  from_day = TuNgay.Date,
                                                  to_day = DenNgay.Date
                                              });
            if (result != null && result.Result != null && result.Result.data != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DonHangs = new ObservableCollection<ComboModel>();

                    for (int i = 0; i < result.Result.data.Count; ++i)
                    {
                        DonHangs.Add(new ComboModel
                        {
                            ID = result.Result.data[i].ID.ToString(),
                            Name = result.Result.data[i].TransferOrderNo,
                            PurchaseOrderDate = Convert.ToDateTime(result.Result.data[i].InstructionDate)
                        });
                    }
                  
                    if(tt)
                    { 
                        var page = new T_ComboboxPage(DonHangs, null, 4, null, null, this);
                        Application.Current.MainPage.Navigation.PushAsync(page);
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
                    case 4:
                        SelectedDonHang = model_;
                        Name = SelectedDonHang.Name;
                        break;
                }

            });
        }
        public void LoadComboxSoLoai()
        {
            LoadModels(true);
        }

    }
}
