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
        public ObservableCollection<ComboModel> _Models { get; set; } = new ObservableCollection<ComboModel>();
        private int _tt;
        public KhoPageModel _ViewModel;
        public ChonDonMuaHangPageModel _ViewModel2;
        public ChonKhoKiemKePageModel _ViewModel3;
        public T_ComboboxPage(ObservableCollection<ComboModel> models, KhoPageModel viewModel_,int tt
            , ChonDonMuaHangPageModel ViewModel2_
            , ChonKhoKiemKePageModel ViewModel3_)
        {
            _tt = tt; 
            _ViewModel = viewModel_;
            _ViewModel2 = ViewModel2_;
            _ViewModel3 = ViewModel3_;
            MySettings.Title = "";
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            _Models = models;
            lst_combobox.ItemsSource = models; 
        }
        
        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
        }
 
        private void LoadComboboxAsync_timkiem()
        {
            lst_combobox.ItemsSource = null;
            ObservableCollection<ComboModel> model_ = new ObservableCollection<ComboModel>();
            for (int i = 0; i < _Models.Count; ++i)
            {
                if (_Models[i].Name.ToLower().Contains(txtTimKiem_combobox.Text.ToLower().TrimEnd().TrimStart()))
                {
                    model_.Add(_Models[i]);
                }
            }
            lst_combobox.ItemsSource = model_;
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
                        _ViewModel.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
                    case 2:
                        _ViewModel2.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
                    case 3:
                        _ViewModel3.LoadDataCombobox(((ComboModel)e.Item), _tt);
                        break;
                }    
                Application.Current.MainPage.Navigation.PopAsync();
            } 
        }
    }
}
