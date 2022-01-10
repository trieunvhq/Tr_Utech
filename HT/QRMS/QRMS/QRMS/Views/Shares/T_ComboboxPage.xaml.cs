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
        private int _tt;
        public KhoPageModel _ViewModel;
        public ChonDonMuaHangPageModel _ViewModel2;
        public ChonKhoKiemKePageModel _ViewModel3;
        public ChonChiThiXuatHangViewModel _ViewModel4;
        public XK_CCTXHPageModel _ViewModel5;
        public T_ComboboxPage(int tt
            , ObservableCollection<WarehouseBPLModel> ModelKhos_
            , ObservableCollection<ComboModel> ModelDonHangs_

            , KhoPageModel viewModel_
            , ChonDonMuaHangPageModel ViewModel2_
            , ChonKhoKiemKePageModel ViewModel3_
            , ChonChiThiXuatHangViewModel ViewModel4_
            , XK_CCTXHPageModel ViewModel5_)
        {
            _tt = tt; 
            _ViewModel = viewModel_;
            _ViewModel2 = ViewModel2_;
            _ViewModel3 = ViewModel3_;
            _ViewModel4 = ViewModel4_;
            _ViewModel5 = ViewModel5_;


            switch (tt)
            {
                case 1:
                    MySettings.Title = "Chọn kho";
                    break;
                case 2:
                    MySettings.Title = "Chọn đơn hàng";
                    break;
                case 3:
                    MySettings.Title = "Chọn kho";
                    break;
                case 4:
                    MySettings.Title = "Chọn chỉ thị xuất hàng";
                    break;
            }    
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
                switch(_tt)
                {
                    case 1:
                        _ViewModel.LoadDataCombobox(((WarehouseBPLModel)e.Item), _tt);
                        break;
                    case 2:
                        _ViewModel2.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
                    case 3:
                        _ViewModel3.LoadDataCombobox(((WarehouseBPLModel)e.Item), _tt);
                        break;
                    case 4:
                        _ViewModel4.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
                    case 5:
                        _ViewModel5.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
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
