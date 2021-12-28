 
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
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

                _ = Controls.LoadingUtility.ShowAsync().ContinueWith(bb =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Application.Current.MainPage.Navigation.PushAsync(new HomePage());

                        Controls.LoadingUtility.Hide();
                        return;

                        lblUserEmptyError.IsVisible = false;
                        lblPassEmptyError.IsVisible = false;

                        lblUserEmptyError.Text = "Nhập tài khoản";
                        lblPassEmptyError.Text = "Nhập mật khẩu";
                        Controls.LoadingUtility.Show();
                        MySettings.FULL_NAME = "";

                        var isError = false;
                        if (string.IsNullOrEmpty(txtUserName.Text))
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
                        if (string.IsNullOrEmpty(txtPassword.Text))
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
                        if (string.IsNullOrWhiteSpace(txtUserName.Text))
                        {
                            frameUser.BackgroundColor = Color.FromHex("#FEEEEF");
                            frameUser.BorderColor = Color.FromHex("#F5323C");
                            lblUserEmptyError.IsVisible = true;
                            isError = true;
                        }
                        else
                        {
                            if (txtUserName.Text.Contains("@"))
                            {
                                if (MobileLib.IsEmail(txtUserName.Text) == false)
                                {
                                    lblUserEmptyError.Text = "Tài khoản không đúng";

                                    frameUser.BackgroundColor = Color.FromHex("#FEEEEF");
                                    frameUser.BorderColor = Color.FromHex("#F5323C");
                                    lblUserEmptyError.IsVisible = true;
                                    isError = true;
                                }
                            }
                            else if (!string.IsNullOrEmpty(txtUserName.Text))
                            {
                                if (txtUserName.Text.Length == 10)
                                { }
                                else
                                {
                                    lblUserEmptyError.Text = "Tài khoản không đúng";

                                    frameUser.BackgroundColor = Color.FromHex("#FEEEEF");
                                    frameUser.BorderColor = Color.FromHex("#F5323C");
                                    lblUserEmptyError.IsVisible = true;
                                    isError = true;
                                }
                            }
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
                                var submit = APIHelper.PostObjectToAPIAsync<BaseModel<CusConfigModel>>
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
                                    if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            switch (submit.Result.ErrorCode)
                                            {
                                                case "0":
                                                    break;
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
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), null);
            }
        }
    }
}