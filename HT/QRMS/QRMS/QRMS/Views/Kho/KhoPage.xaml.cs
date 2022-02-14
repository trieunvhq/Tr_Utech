 
using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class KhoPage : ContentPage
    {
        private bool _isDisconnect = true;
        public KhoPageModel ViewModel { get; set; }
        public KhoPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new KhoPageModel();
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
            if (!string.IsNullOrWhiteSpace(inputModel.Text.Trim()))
            {
                MySettings.NameKho = inputModel.Text.Trim();
                MySettings.CodeKho = inputModel.Text.Trim();

                await UserDialogs.Instance.ConfirmAsync("", "Thành công!", "Đồng ý", "");
            }
            //if(ViewModel.SelectedKho != null)
            //{
            //    MySettings.NameKho = ViewModel.SelectedKho.WarehouesName;
            //    MySettings.CodeKho = ViewModel.SelectedKho.WarehouseCode;

            //    await UserDialogs.Instance.ConfirmAsync("", "Thành công!", "Đồng ý", "");
            //}
        }


        async void SoLoai_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_isDisconnect)
            {
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
                return;
            }

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
