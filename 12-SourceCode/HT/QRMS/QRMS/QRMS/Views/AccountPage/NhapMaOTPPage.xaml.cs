 
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Resources;
using QRMS.ViewModels.Account;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    public partial class NhapMaOTPPage : ContentPage
    {
        NhapMaOTPPagemodel ViewModel = new NhapMaOTPPagemodel();
        public NhapMaOTPPage(string email_sdt, CusGetOtpForgotPasswordModel model)
        {
            MySettings.Title = AppResources.XacNhanMatKhau;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel.BackCommand = new Command(() =>
            {
                ViewModel.ExecuteBackPage();
            });
            ViewModel.EmailSDT = email_sdt;
            ViewModel._model = model;
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);

            if(model.DestinationReceiveOtp== "Email")
            {
                span1.Text = AppResources.VuiLongXacNhanMaBaoMatOTP_2_;
            }
            else
            {
                span1.Text = AppResources.VuiLongXacNhanMaBaoMatOTP_;
            }    
        }


        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;

                if (ViewModel.OTP == null || ViewModel.OTP == "")
                {
                    isError = true;
                    inputOTP.IsError = true;

                }
                else
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
