
using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Views.AccountPage;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class NhapMaOTPPagemodel : BaseViewModel
    {
        public CusGetOtpForgotPasswordModel _model;
        public string OTP { get; set; }
        public string EmailSDT { get; set; }
        public string ThoiGian { get; set; } = "59";
        
        public override void OnAppearing()
        {
            base.OnAppearing();
            StartDemThoiGianGGS();
        }
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private int _time = 59;
        private void StartDemThoiGianGGS()
        {
            StopDemThoiGianGGS();
            CancellationTokenSource cts = this.cancellation;

            Device.StartTimer(TimeSpan.FromSeconds(1),
                  () =>
                  {
                      if (cts.IsCancellationRequested) return false;
                      if(_time>=0)
                        ThoiGian = _time.ToString();
                      else
                      {
                          return false;
                      }
                      --_time;

                      return true;
                  });
        }
        private void StopDemThoiGianGGS()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
          
        public void ExecuteNextPage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (OTP == _model.OtpCode)
                {
                    var page = new TaoMKMoiPage();
                    Application.Current.MainPage.Navigation.PushAsync(page);
                } 
            }); 
        }

    }
}
