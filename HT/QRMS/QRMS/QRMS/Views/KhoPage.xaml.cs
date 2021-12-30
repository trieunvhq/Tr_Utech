 
using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class KhoPage : ContentPage
    {
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
            if(ViewModel.SelectedKho != null)
            {
                MySettings.MaKho = ViewModel.SelectedKho.Name;
                MySettings.IDKho = ViewModel.SelectedKho.ID;
            } 
            await UserDialogs.Instance.ConfirmAsync("", "Thành công!", "Đồng ý");
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
