 
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
    public partial class CK_LayPhieuPage : ContentPage
    {
        //MyScan _MyScan; 

        public CK_LayPhieuPageModel ViewModel { get; set; }
        public CK_LayPhieuPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new CK_LayPhieuPageModel();
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._CK_LayPhieuPage = this;
            //_MyScan = new MyScan();
            //_MyScan._CK_LayPhieuPageModel = ViewModel;

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
                    if (!string.IsNullOrWhiteSpace(ViewModel.TransferOrderNo))
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new DC_SCANPage(
                         ViewModel.TransferOrderNo, ViewModel.InstructionDate
                         , ViewModel.WarehouseCode_From, ViewModel.WarehouseName_From
                         , ViewModel.WarehouseCode_To, ViewModel.WarehouseName_To));
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
