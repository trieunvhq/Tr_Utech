 
using System;
using System.Collections.Generic;
using QRMS.Constants;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class ChonDonMuaHangPage : ContentPage
    {
        public ChonDonMuaHangPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);

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
        }

        void BtnLayDonMuaHang_CLicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
