
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.interfaces;
using QRMS.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }



        private void EntryPass_Unfocused(object sender, FocusEventArgs e)
        {
            framePass.BorderColor = Color.FromHex("#DCE0E2");
        }

        private void EntryPass_Focused(object sender, FocusEventArgs e)
        {
            framePass.BorderColor = Color.FromHex("#F49A0E");
            framePass.BackgroundColor = Color.Default;
        }
        private void EntryUser_Unfocused(object sender, FocusEventArgs e)
        {
            frameUser.BorderColor = Color.FromHex("#DCE0E2");
        }

        private void EntryUser_Focused(object sender, FocusEventArgs e)
        {
            frameUser.BorderColor = Color.FromHex("#F49A0E");
            frameUser.BackgroundColor = Color.Default;
        }

        void btnLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                btnScan_Clicked();
                return;
                _ = Controls.LoadingUtility.ShowAsync().ContinueWith(bb =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    { 

                        lblUserEmptyError.IsVisible = false;
                        lblPassEmptyError.IsVisible = false;

                        lblUserEmptyError.Text = "Nhập tài khoản";
                        lblPassEmptyError.Text = "Nhập mật khẩu";
                        Controls.LoadingUtility.Show();
                        MySettings.FULL_NAME = "";

                        var isError = false;
                        if (string.IsNullOrWhiteSpace(txtUserName.Text))
                        {
                            frameUser.BackgroundColor = Color.FromHex("#FEEEEF");
                            frameUser.BorderColor = Color.FromHex("#F5323C");

                            lblUserEmptyError.IsVisible = true;
                            isError = true;
                        }
                        else
                        {
                            frameUser.BorderColor = Color.FromHex("#F49A0E");
                            frameUser.BackgroundColor = Color.Default;
                        }
                        if (string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            framePass.BackgroundColor = Color.FromHex("#FEEEEF");
                            framePass.BorderColor = Color.FromHex("#F5323C");
                            lblPassEmptyError.IsVisible = true;
                            isError = true;
                        }
                        else
                        {
                            framePass.BorderColor = Color.FromHex("#F49A0E");
                            framePass.BackgroundColor = Color.Default;
                        } 
                        if (isError)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.Hide();
                            });
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                            {
                                bool exception = false;
                                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                                var FCMTockenValue = String.Empty;
                                if (FCMToken)
                                {
                                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                                }
                                var token = FCMTockenValue;
                                //var account_id = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNoKey).Result;

                                var ipAddress = MobileInfo.GetIP();
                                var deviceName = MobileInfo.GetDeviceInfo();
                                string UserName_ = txtUserName.Text.ToLower();
                                var submit = APIHelper.PostObjectToAPIAsync<BaseModel<User>>
                                           (Constaint.ServiceAddress, Constaint.APIurl.login,
                                           new
                                           {
                                               IpAddress = ipAddress,
                                               DeviceName = deviceName,
                                               UserName = UserName_,
                                               Password = txtPassword.Text,
                                               Token = token
                                           });
                                _ = submit.ContinueWith(next =>
                                {
                                    if (submit != null && submit.Result != null)
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            if (submit.Result.data != null
                                          && submit.Result.ErrorCode == "0")
                                            {
                                                Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                            }
                                            else
                                            { }
                                        });
                                    } 
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            Controls.LoadingUtility.Hide();
                                        });
                                    }
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Controls.LoadingUtility.Hide();
                                });
                            }
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.Hide();
                });

                DependencyService.Get<ILogger>().Log(ex.ToString());

                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var token = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var namespaceInFile = GetType().Namespace;
                var className = GetType().Name;
                var methodName = "Login_Clicked";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
            }
        }
        private async void btnScan_Clicked()
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtUserName.Text = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
            });
        }
    }
}