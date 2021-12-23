using System;
using Acr.UserDialogs;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels;
using QRMS.ViewModels.BH_DuLich.DL_NNNTVN;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml; 
using System.Collections.Generic;
using Page = Xamarin.Forms.Page;
using Application = Xamarin.Forms.Application;

namespace QRMS.Views.AccountPage
{
    public partial class DK_TK_TCPage : ContentPage
    {
        public DK_TK_TCPage()
        {
            MySettings.Title = AppResources.TaoMatKhauDangNhap;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);
            
        }


        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {   
                List<Page> stack = new List<Page>();
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
                    stack = Application.Current.MainPage.Navigation.ModalStack.Skip(1).ToList();
                else if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                    stack = Application.Current.MainPage.Navigation.NavigationStack.Skip(1).ToList();

                Application.Current.MainPage.Navigation.PushAsync(new QRMS.Views.LoginPage());

                foreach (var item in stack)
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.RemovePage(item);
                }
                //Xamarin.Forms.Application.Current.MainPage = new QRMS.Views.LoginPage.LoginView();
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
#if DEBUG
                UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
            }
        }


    }
}
