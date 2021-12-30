 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public NhapKhoDungCuPage(string id)
        {
            InitializeComponent();
             
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new NhapKhoDungCuPageModel(id);
            ViewModel._Page = this;
            ViewModel.Initialize();
            BindingContext = ViewModel;
 
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

        public void ThongBao_time(string str, int time,bool IsThanhCong)
        {
            if (str == null || str == "") return;
            Device.BeginInvokeOnMainThread(async () =>
            {
                _ = Controls.LoadingUtility.HideAsync();
                await Task.Delay(TimeSpan.FromMilliseconds(time));
                lbThongBao.IsVisible = false;  
            });
            if (IsThanhCong)
                lbThongBao.TextColor = Color.Green;
            else
                lbThongBao.TextColor = Color.Red;
            lbThongBao.Text = str; 
            lbThongBao.IsVisible = true; 
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
            row.Height = 100;
            
            lbNen.IsVisible = true;
            scanView.IsVisible = true;
            btnDongQuet.IsVisible = true;
        }

        async void scanView_OnScanResult(ZXing.Result result)
        {
            ViewModel.ScanComplate(result.Text);
        }

        void btnDongQuet_Clicked(System.Object sender, System.EventArgs e)
        {
            row.Height = 50;
            lbNen.IsVisible = false;
            scanView.IsVisible = false;
            btnDongQuet.IsVisible = false;
        }
    }
}
