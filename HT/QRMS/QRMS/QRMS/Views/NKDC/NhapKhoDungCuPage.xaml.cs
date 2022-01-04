 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class NhapKhoDungCuPage : ContentPage
    {
        MyScan _MyScan;
        public NhapKhoDungCuPageModel ViewModel { get; set; }
        public NhapKhoDungCuPage(string id, string no, DateTime d)
        {
            InitializeComponent();
             
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new NhapKhoDungCuPageModel(id, no, d); 
            ViewModel.Initialize();
            BindingContext = ViewModel;
            _MyScan = new MyScan(1, ViewModel);
            //
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
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    ViewModel.LuuLais();
                    await Controls.LoadingUtility.HideAsync();
                });
            }); 
        } 

        void lst_combobox_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        void BtnQuet_CLicked(System.Object sender, System.EventArgs e)
        {
            _MyScan.OpenBarcodeReader(); 
        } 
    }
}
