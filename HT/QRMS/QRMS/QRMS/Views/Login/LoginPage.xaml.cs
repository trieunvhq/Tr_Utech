
using Acr.UserDialogs;
using LIB;
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
            txtPassword.Text = MySettings.Password;
            txtUserName.Text = MySettings.UserName;
        }



        private void EntryPass_Unfocused(object sender, FocusEventArgs e)
        {
            framePass.BorderColor = Color.FromHex("#DCE0E2");
        }

        private void EntryPass_Focused(object sender, FocusEventArgs e)
        {
            framePass.BorderColor = Color.FromHex("#F49A0E");
            framePass.BackgroundColor = Color.Default;
            lblPassEmptyError.IsVisible = false;
        }
        private void EntryUser_Unfocused(object sender, FocusEventArgs e)
        {
            frameUser.BorderColor = Color.FromHex("#DCE0E2");
        }

        private void EntryUser_Focused(object sender, FocusEventArgs e)
        {
            frameUser.BorderColor = Color.FromHex("#F49A0E");
            frameUser.BackgroundColor = Color.Default;
            lblUserEmptyError.IsVisible = false;
        }

        void btnLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                _ = Controls.LoadingUtility.ShowAsync().ContinueWith(bb =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
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
                            txtUserName.Text = txtUserName.Text.TrimStart().TrimEnd().Trim();

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
                            txtPassword.Text = txtPassword.Text.TrimStart().TrimEnd().Trim();
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
                                else
                                    FCMTockenValue = MobileInfo.GetIP();

                                var token = FCMTockenValue;
                                //var account_id = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNoKey).Result;
                                MySettings.Token = token;
                                var ipAddress = MobileInfo.GetIP();
                                var deviceName = MobileInfo.GetDeviceInfo();
                                string UserName_ = txtUserName.Text.ToLower();

                                if(string.IsNullOrWhiteSpace(MySettings.Service))
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Application.Current.MainPage.Navigation.PushAsync(new HeThongPage(true));
                                        Controls.LoadingUtility.Hide();
                                    });
                                }
                                else
                                {
                                    MySettings.Service = MySettings.Service.TrimStart().TrimEnd().Trim();
                                    Constaint.ServiceAddress = MySettings.Service;
                                    var _result = await APICaller.PostObjectToAPImodelAsync<BaseModel<User>>
                                    (Constaint.ServiceAddress, Constaint.APIurl.login,
                                    new
                                    {
                                        IpAddress = ipAddress,
                                        DeviceName = deviceName,
                                        UserName = UserName_,
                                        Password = txtPassword.Text,
                                        Token = token
                                    }).ConfigureAwait(false);
                                    //
                                    if (_result.data != null && _result.data != null)
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            if (_result.RespondCode == 200)
                                            { 
                                                if (_result.data != null
                                              && _result.data.ErrorCode == "0")
                                                {
                                                    MySettings.UserName = txtUserName.Text;
                                                    MySettings.Password = txtPassword.Text;

                                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                                    Controls.LoadingUtility.Hide();
                                                }
                                                else
                                                {
                                                    lblPassEmptyError.Text = "Sai tài khoản hoặc mật khẩu";
                                                    framePass.BackgroundColor = Color.FromHex("#FEEEEF");
                                                    framePass.BorderColor = Color.FromHex("#F5323C");
                                                    lblPassEmptyError.IsVisible = true;
                                                    Controls.LoadingUtility.Hide();
                                                }
                                            }
                                            else
                                            {
                                                await Application.Current.MainPage.Navigation.PushAsync(new HeThongPage(true));
                                                Controls.LoadingUtility.Hide();
                                            }
                                        });
                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await Application.Current.MainPage.Navigation.PushAsync(new HeThongPage(true));
                                            Controls.LoadingUtility.Hide();
                                        });
                                    }

                                    //var submit = APIHelper.PostObjectToAPIAsync<BaseModel<User>>
                                    //         (Constaint.ServiceAddress, Constaint.APIurl.login,
                                    //         new
                                    //         {
                                    //             IpAddress = ipAddress,
                                    //             DeviceName = deviceName,
                                    //             UserName = UserName_,
                                    //             Password = txtPassword.Text,
                                    //             Token = token
                                    //         });
                                    //_ = _result.ContinueWith(next =>
                                    //{
                                        
                                    //});
                                }    

                                   
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new HeThongPage(true));
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
    }
}