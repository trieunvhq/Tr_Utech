 
using System;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class ChonPhieuNhapPage : ContentPage
    {
        //MyScan _MyScan; 

        public ChonPhieuNhapPageModel ViewModel { get; set; }
        public ChonPhieuNhapPage()
        { 
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new ChonPhieuNhapPageModel();
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._ChonPhieuNhapPage = this;
            //_MyScan = new MyScan();
            //_MyScan._ChonPhieuNhapPageModel = ViewModel;

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
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                    await Controls.LoadingUtility.HideAsync();
                });
            });

        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                { 
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NhapKhoDungCuPage(
                        ViewModel._PurchaseOrderNo, ViewModel._WarehouseCode, ViewModel._PurchaseOrderDate ,ViewModel._WarehouseName));
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }
         
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        } 
    }
}
