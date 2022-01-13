 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMS.Constants;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class HeThongPage : ContentPage
    {
        public HeThongPage(bool isThietLapLai)
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);

            lbThietlapLai.IsVisible = isThietLapLai;
            if(isThietLapLai)
            {
                lbThietlapLai.Text = "Vui lòng thiết lập lại Service";
                lbThietlapLai.TextColor = Color.Red;
            }    
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
            txtTenMay.Text = MySettings.TenMay;
            txtService.Text = MySettings.Service;
        }

       

        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

        }

        void BtnHeThong_CLicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnKho_CLicked(System.Object sender, System.EventArgs e)
        {
        }

        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            MySettings.TenMay = txtTenMay.Text;
            MySettings.Service = txtService.Text;

            lbThietlapLai.IsVisible = true;
            lbThietlapLai.Text = "Lưu lại thành công!";
            lbThietlapLai.TextColor = Color.Green;

            await Task.Delay(1000);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
