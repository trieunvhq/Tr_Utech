 
using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.KKDC;
using QRMS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific; 

namespace QRMS.Views
{
    public partial class KK_SCANPage : ContentPage
    {
        //MyScan _MyScan; 

        public KK_SCANPageModel ViewModel { get; set; }

        public string WarehouseCode;
        public string WarehouseName;
        private bool _isDisconnect = true;

        public KK_SCANPage(string WarehouseCode_, string WarehouseName_)
        {
            InitializeComponent();

            WarehouseCode = WarehouseCode_;
            WarehouseName = WarehouseName_;

            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new KK_SCANPageModel(this);
            ViewModel._WarehouesCode = WarehouseCode_;
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._KK_SCANPage = this;
            //_MyScan = new MyScan(1, ViewModel);
            //
            //_MyScan = new MyScan();
            //_MyScan._KK_SCANPageModel = ViewModel;
            row_trencung.Height = 20;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (MySettings.h_QRMS >= 812)
                    {
                        row_trencung.Height = 40;
                    }
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _isDisconnect = true;
                DisplayAlert("Thông báo", "Mất kết nối!", "OK");
            }
            else
            {
                _isDisconnect = false;
            }
        }

        protected override void OnAppearing()
        {
            txtMaKK.Completed += (sender, e) => EntryCompleted(sender, e);

            base.OnAppearing();
            ViewModel.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        }

        void EntryCompleted(object sender, EventArgs e)
        {
            ViewModel.LoadModels();
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                _isDisconnect = true;
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
            }
            else
                _isDisconnect = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            BtnQuayLai_CLicked(null, null);
            return true;
        }


        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ViewModel._Historys_NewScan.Count > 0)
                        await Load_popup_DangXuat("Chưa lưu dữ liệu quét. Bạn có muốn quay lại không?", "Có", "Huỷ bỏ");
                    else
                    {
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                        await Controls.LoadingUtility.HideAsync();
                    }    
                });
            });

        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                { 
                    ViewModel.SaveDBlocal();
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }
         

        public async Task Load_popup_DangXuat(string tieude, string nutdongy, string huybo)
        {
            await Controls.LoadingUtility.HideAsync();
            if (huybo == "")
            {
                btnHuyBo_absPopup.IsVisible = false;
            }
            else
            {
                btnHuyBo_absPopup.IsVisible = true;
            }
            //
            btnDongY_absPopup.Text = nutdongy;
            btnHuyBo_absPopup.Text = huybo;
            lbTieuDe_absPopup.Text = tieude;
            if (!grid.Children.Contains(absPopup_DangXuat))
                grid.Children.Add(absPopup_DangXuat);
            grid.RaiseChild(absPopup_DangXuat);
            await absPopup_DangXuat.FadeTo(1, 200);
            grid.RaiseChild(absPopup_DangXuat);
        }


        private async void BtnDongY_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);
            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn quay lại không?")
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }

            if (MySettings.To_Page == "homepage")
            {
                MySettings.To_Page = "";
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else if (MySettings.To_Page == "KKDC_CLPage")
            {
                MySettings.To_Page = "";
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new KKDC_CLPage(WarehouseCode, WarehouseName));
            }
        }


        private async void BtnHuyBo_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn quay lại không?")
            {
            }
        }

        void txtTest_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ViewModel.ScanComplate(txtTest.Text);
        }

        void txtMaKK_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            
        }
    }
}
