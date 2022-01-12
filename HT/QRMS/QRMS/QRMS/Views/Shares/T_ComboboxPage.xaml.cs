using System;
using System.Collections.ObjectModel;
using QRMS.Constants;
using QRMS.Models;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace PIAMA.Views.Shared
{
    public partial class T_ComboboxPage : ContentPage
    {
        public ObservableCollection<WarehouseBPLModel> _ModelKhos { get; set; } = new ObservableCollection<WarehouseBPLModel>();
        public ObservableCollection<ComboModel> _ModelDonHangs { get; set; } = new ObservableCollection<ComboModel>();
       
        public KhoPageModel _KhoPageModel;
        public ChonDonMuaHangPageModel _ChonDonMuaHangPageModel;
        public ChonKhoKiemKePageModel _ChonKhoKiemKePageModel;
        public ChonChiThiXuatHangViewModel _ChonChiThiXuatHangViewModel;
        public XK_CCTXHPageModel _XK_CCTXHPageModel;
        public XKDC_CKKKPageModel _XKDC_CKKKPageModel;
        public DC_ChoKhoPageModel _DC_ChoKhoPageModel;
        public KK_ChonKhoPageModel _KK_ChonKhoPageModel;
        public T_ComboboxPage(ObservableCollection<WarehouseBPLModel> ModelKhos_
            , ObservableCollection<ComboModel> ModelDonHangs_ )
        { 
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            _ModelKhos = ModelKhos_;
            _ModelDonHangs = ModelDonHangs_;
            if(_ModelKhos!=null)
            {
                for(int i=0;i<ModelKhos_.Count;++i)
                {
                    _ModelKhos[i].Name = _ModelKhos[i].WarehouesName;
                }    
                lst_combobox.ItemsSource = _ModelKhos;
            }    
            else if (_ModelDonHangs != null)
                lst_combobox.ItemsSource = _ModelDonHangs;

        }
        
        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
        }
 
        private void LoadComboboxAsync_timkiem()
        {
            lst_combobox.ItemsSource = null;
            if (_ModelKhos != null)
            {
                ObservableCollection<WarehouseBPLModel> model_ = new ObservableCollection<WarehouseBPLModel>();
                for (int i = 0; i < _ModelKhos.Count; ++i)
                {
                    if (_ModelKhos[i].WarehouesName.ToLower().Contains(txtTimKiem_combobox.Text.ToLower().TrimEnd().TrimStart()))
                    {
                        model_.Add(_ModelKhos[i]);
                    }
                }
                lst_combobox.ItemsSource = model_;
            }
            else if (_ModelDonHangs != null)
            {
                ObservableCollection<ComboModel> model_ = new ObservableCollection<ComboModel>();
                for (int i = 0; i < _ModelDonHangs.Count; ++i)
                {
                    if (_ModelDonHangs[i].Name.ToLower().Contains(txtTimKiem_combobox.Text.ToLower().TrimEnd().TrimStart()))
                    {
                        model_.Add(_ModelDonHangs[i]);
                    }
                }
                lst_combobox.ItemsSource = model_;
            }
        }

        private void txtTimKiem_combobox_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        { 
            if (txtTimKiem_combobox.Text == "")
                imgClose_combobox.IsVisible = false;
            else
                imgClose_combobox.IsVisible = true; 

            LoadComboboxAsync_timkiem(); 
        }
        private void btnClose_TimKiem_combobox_Clicked(System.Object sender, System.EventArgs e)
        {
            txtTimKiem_combobox.Text = "";
            imgClose_combobox.IsVisible = false;
            LoadComboboxAsync_timkiem();
        }
        private async void Lbcombobox_Tapped(System.Object sender, System.EventArgs e)
        {
            string ma_ = ((Label)sender).ClassId; 

        }

        async void lst_combobox_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        { 
            if (e.Item != null)
            {
                if(_KhoPageModel!=null)
                {
                    _KhoPageModel.LoadDataCombobox(((WarehouseBPLModel)e.Item));
                }
                else if (_ChonDonMuaHangPageModel != null)
                {
                    _ChonDonMuaHangPageModel.LoadDataCombobox(((ComboModel)e.Item));
                }
                else if (_ChonKhoKiemKePageModel != null)
                {
                    _ChonKhoKiemKePageModel.LoadDataCombobox(((WarehouseBPLModel)e.Item));
                }
                else if (_ChonChiThiXuatHangViewModel != null)
                {
                    _ChonChiThiXuatHangViewModel.LoadDataCombobox(((ComboModel)e.Item));
                }
                else if (_XK_CCTXHPageModel != null)
                {
                    _XK_CCTXHPageModel.LoadDataCombobox(((ComboModel)e.Item));
                }
                else if (_XKDC_CKKKPageModel != null)
                {
                    _XKDC_CKKKPageModel.LoadDataCombobox(((WarehouseBPLModel)e.Item));
                }
                else if (_KK_ChonKhoPageModel != null)
                {
                    _KK_ChonKhoPageModel.LoadDataCombobox(((WarehouseBPLModel)e.Item));
                }
                else if (_DC_ChoKhoPageModel != null)
                {
                    _DC_ChoKhoPageModel.LoadDataCombobox(((WarehouseBPLModel)e.Item));
                }
                
               await Application.Current.MainPage.Navigation.PopAsync();
            } 
        }

        void BtnBack_combobox_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
