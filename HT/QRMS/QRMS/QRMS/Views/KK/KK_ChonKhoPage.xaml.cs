 
using System;
using System.Collections.Generic;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class KK_ChonKhoPage : ContentPage
    {
        private bool _isDisconnect = true;
        public KK_ChonKhoPageModel ViewModel { get; set; }
        public KK_ChonKhoPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);


            ViewModel = new KK_ChonKhoPageModel();
            ViewModel.Initialize();
            BindingContext = ViewModel;


            row_trencung.Height = 20;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (MySettings.h_QRMS >= 812)
                    {
                        row_trencung.Height = 40;
                    }
                    else
                    {
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
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                _isDisconnect = true;
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
            }
            else
            {
                _isDisconnect = false;
            }    
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            if (_isDisconnect)
            {
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
                return;
            }

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new KKDC_CLPage(ViewModel.WarehouesCode, ViewModel.WarehouesName));
        }

        async void SoLoai_Tapped(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ViewModel.LoadComboxSoLoai();
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }
    }
}
