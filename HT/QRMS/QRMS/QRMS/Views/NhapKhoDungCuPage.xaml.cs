 
using System;
using System.Collections.Generic;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class NhapKhoDungCuPage : ContentPage
    {
        public NhapKhoDungCuPageModel ViewModel { get; set; }
        public NhapKhoDungCuPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new NhapKhoDungCuPageModel();
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


        void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }


        void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            if (ViewModel.SelectedKho != null)
            {
                MySettings.MaKho = ViewModel.SelectedKho.Name;
                MySettings.IDKho = ViewModel.SelectedKho.ID;
            }
        } 

        void lst_combobox_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        void BtnQuet_CLicked(System.Object sender, System.EventArgs e)
        {
            scanView.IsVisible = true;
        }

        void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ViewModel.LoadModels(result.Text.Split());
                await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
            });
        }
    }
}
