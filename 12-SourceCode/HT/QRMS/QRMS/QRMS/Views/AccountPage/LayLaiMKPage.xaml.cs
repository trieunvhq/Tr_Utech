 
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels;
using QRMS.ViewModels.Account;
using QRMS.ViewModels.BH_DuLich.DL_NNNTVN;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace QRMS.Views.AccountPage
{
    public partial class LayLaiMKPage : ContentPage
    {
        LayLaiMKViewmodel ViewModel = new LayLaiMKViewmodel(); 
        public LayLaiMKPage()
        { 
            MySettings.Title = AppResources.LayLaiMK;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel.BackCommand = new Command(() =>
            {
                ViewModel.ExecuteBackPage();
            });
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);

        }


        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;

                if (ViewModel.DiaChiEmailSDT==null || ViewModel.DiaChiEmailSDT == "")
                {
                    isError = true;
                    inputDiaChiSDT.IsError = true; 

                }
                if (ViewModel.DiaChiEmailSDT.Contains("@"))
                {
                    if (MobileLib.IsEmail(ViewModel.DiaChiEmailSDT) == false)
                    {
                        inputDiaChiSDT.ErrorMessage = AppResources.EmailKhongDung;
                        inputDiaChiSDT.IsError = true;
                        isError = true;
                    }
                }
                else
                {
                    if (ViewModel.DiaChiEmailSDT.Length == 10)
                    { }
                    else
                    {
                        inputDiaChiSDT.ErrorMessage = AppResources.SDTKhongDung;
                        inputDiaChiSDT.IsError = true;
                        isError = true;
                    }
                }

                if(!isError)
                {
                    ViewModel.ExecuteNextPage();
                }

                //

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
