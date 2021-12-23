using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppBLL;
using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Views.AccountPage;
using QRMS.Views.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class AccountViewModel : BaseViewModel
    {
        private static string exdisconnect = "An exception occurred during the operation, making the result invalid";

        #region bien
        public DatabaseContext conn;
        private ContentPage Page;
        public AccountView _AccountView;
        //public ObservableCollection<ListContractModel> ListContract { get; set; }
        public Command FilterCommand { get; }
        //public Command lblInsuranceTypeCommand { get; }
        private int pageNumber { get; set; }

        private string _Search;
        public string Search
        {
            get => _Search;
            set
            {
                _Search = value;
                OnPropertyChanged();
            }
        }

        private ListAccountModel _ItemAccount = null;
        public ListAccountModel ItemAccount
        {
            get => _ItemAccount;
            set
            {
                _ItemAccount = value;
                OnPropertyChanged();
                Account_Clicked();
            }
        }
        public Command BackCommand { get; }
        //public Command cmdAccount { get; }
         
        #endregion

        #region hienthi

        public string IDAccount;
        private string UserName;
        private string Password;

        private string _lblAccount;
        public string lblAccount
        {
            get => _lblAccount;
            set
            {
                _lblAccount = value;
                OnPropertyChanged();
            }
        }
        private string _lblBack;
        public string lblBack
        {
            get => _lblBack;
            set
            {
                _lblBack = value;
                OnPropertyChanged();
            }
        } 
        public string lblName { get; set; }


        private string _lblChangePassword;
        public string lblChangePassword
        {
            get => _lblChangePassword;
            set
            {
                _lblChangePassword = value;
                OnPropertyChanged();
            }
        }

        private string _lblSetting;
        public string lblSetting
        {
            get => _lblSetting;
            set
            {
                _lblSetting = value;
                OnPropertyChanged();
            }
        }

        private string _lblLanguage;
        public string lblLanguage
        {
            get => _lblLanguage;
            set
            {
                _lblLanguage = value;
                OnPropertyChanged();
            }
        }

        private string _lblLogout;
        public string lblLogout
        {
            get => _lblLogout;
            set
            {
                _lblLogout = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region helper
        public IEnumerable<LanguageModel> languages()
        {
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblChangePassword", LangVi = "Thay đổi mật khẩu", LangEn = "Change Password", Remark = "Thay đổi mật khẩu" };
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblSetting", LangVi = "Cài đặt", LangEn = "Setting", Remark = "Cài đặt tài khoản" };
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblLanguage", LangVi = "Ngôn ngữ", LangEn = "Language", Remark = "Ngôn ngữ" };
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblLogout", LangVi = "Đăng xuất", LangEn = "Log out", Remark = "Đăng xuất" };
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblBack", LangVi = "〈 Quay lại", LangEn = "〈 Back", Remark = "Trở lại" };
            yield return new LanguageModel() { ScreenCode = "A01", ScreenName = "Account", ItemType = "Label", TypeName = "lblAccount", LangVi = "Tài khoản", LangEn = "Account", Remark = "Tài khoản" };
        }
        #endregion

        #region main

        private DateTime? start = null;
        private DateTime? end = null;
        private TimeSpan? time = null;
        public string SoLuong_DCHT { get; set; } = "";
        public AccountViewModel(string screenType, string user, string pass, ContentPage page)
        {
            try
            {
                //start = DateTime.Now;
                //DependencyService.Get<ILogger>().Log("Start_Load: " + start.ToString());
                Page = page;
                //listitem = new ObservableCollection<ListAccountModel>();
                UserName = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNameKey).Result;
                Password = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.PasswordKey).Result;
                IDAccount = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNoKey).Result;
                //var callAccount = LoadForm();
                conn = new DatabaseContext();
                var getLang = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
                if (getLang != null)
                {
                    var lstProp = GetType().GetProperties();
                    //var lstLangTxt = conn.languageModels.Where(a => a.ScreenCode.Equals("A01")).ToList();
                    var lstLangTxt = this.languages();
                    var lstTypeName = new Dictionary<string, string>();
                    if (getLang.Value.ToLower() == "vi")
                        lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangVi);
                    if (getLang.Value.ToLower() == "en")
                        lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangEn);

                    foreach (var item in lstProp)
                    {
                        if (lstTypeName.ContainsKey(item.Name))
                            item.SetValue(this, lstTypeName[item.Name]);
                        else
                        {
                            var langModel = this.languages().FirstOrDefault(a => a.TypeName.ToLower() == item.Name.ToLower());
                            if (langModel != null)
                            {
                                conn.languageModels.Add(langModel);
                                conn.SaveChangesAsync();
                                item.SetValue(this, (getLang.Value.ToLower() == "vi") ? langModel?.LangVi : langModel?.LangEn);
                            }
                        }
                    }
                }
                BackCommand = new Command(Back_Clicked); 

            }
            catch (Exception ex)
            {
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
                var methodName = "AccountViewModel";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID; 
            }
        }
        public bool IsChuaDanhGia { get; set; }
        public bool IsDanhGia { get; set; } = true;
        public string star1 { get; set; } = "star_den.svg";
        public string star2 { get; set; } = "star_den.svg";
        public string star3 { get; set; } = "star_den.svg";
        public string star4 { get; set; } = "star_den.svg";
        public string star5 { get; set; } = "star_den.svg";
        public override void OnAppearing()
        {
            base.OnAppearing();
            CallThread();
        }
        public bool IsSL { get; set; } = false;
        private async Task CallThread()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(ab =>
                { 
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<CusAccountTabInfoModel>>
                                    (Constaint.ServiceAddress, Constaint.APIurl.getaccounttabinfo,
                                    new
                                    {
                                        ACCOUNT_ID = FormTypeModel.UserID,
                                        //CUST_BYER_TYPE
                                    });
                    _ = submit.ContinueWith(next =>
                    {
                        if ((submit.Result.RespondCode != null && submit.Result.RespondCode != "")
                        ||(submit.Result.ErrorCode != null && submit.Result.ErrorCode != ""))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();

                                if (submit.Result.RespondCode == "0" || submit.Result.ErrorCode == "0")
                                { 
                                    lblName = submit.Result.data.CustomerName;
                                    if(submit.Result.data.LastFeedbackStar>0)
                                    {
                                        IsChuaDanhGia = false;
                                        _AccountView.XoaDanhGia(true);
                                        if(submit.Result.data.LastFeedbackStar>=1)
                                        { star1 = "star_cam.svg"; }
                                        if (submit.Result.data.LastFeedbackStar >= 2)
                                        { star2 = "star_cam.svg"; }
                                        if (submit.Result.data.LastFeedbackStar == 3)
                                        { star3 = "star_cam.svg"; }
                                        if (submit.Result.data.LastFeedbackStar == 4)
                                        { star4 = "star_cam.svg"; }
                                        if (submit.Result.data.LastFeedbackStar == 5)
                                        { star5 = "star_cam.svg"; }

                                       
                                    }
                                    else
                                    {
                                        IsChuaDanhGia = true;
                                        _AccountView.XoaDanhGia(false);
                                    }
                                    if (submit.Result.data.QuantityIncompleteCartedContract > 0
                                       && submit.Result.data.QuantityIncompleteCartedContract < 10)
                                    {
                                        IsSL = true;
                                        SoLuong_DCHT = submit.Result.data.QuantityIncompleteCartedContract.ToString();
                                    }
                                    else if (submit.Result.data.QuantityIncompleteCartedContract <= 0)
                                    {
                                        IsSL = false;
                                        SoLuong_DCHT = "";
                                    }
                                    else
                                    {
                                        IsSL = true;
                                        SoLuong_DCHT = "9+";
                                    }
                                }
                                else
                                {
                                    Application.Current.MainPage.DisplayAlert("ErrorCode: " + submit.Result.ErrorCode, submit.Result.Message, "OK");
                                }
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                            });
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();

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
                    var methodName = "CallThread";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID; 
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }

        public string FEEDBACK_STAR { get; set; }
        public string FEEDBACK_CONTENT { get; set; }
        public void ChiaSeCamNghi()
        {
            try
            {
                Controls.LoadingUtility.ShowAsync().ContinueWith(a =>
                {
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                                    (Constaint.ServiceAddress, Constaint.APIurl.savefeedback,
                                    new
                                    {
                                        ACCOUNT_ID = FormTypeModel.UserID,
                                        FEEDBACK_STAR = FEEDBACK_STAR,
                                        FEEDBACK_CONTENT = FEEDBACK_CONTENT
                                    });
                    _ = submit.ContinueWith(next =>
                    {
                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                                if (submit.Result.RespondCode == "0" || submit.Result.ErrorCode == "0")
                                {
                                    IsDanhGia = false;
                                    IsChuaDanhGia = false;
                                    _AccountView.XoaDanhGia(true);
                                }
                                else
                                {
                                    Application.Current.MainPage.DisplayAlert("ErrorCode: " + submit.Result.ErrorCode, submit.Result.Message, "OK");
                                }
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                            });
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();

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
                    var methodName = "ExecuteNextPage";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }
        private void LanguageViewcell_Tapped(object sender, EventArgs e)
        {
            var page = new Views.AccountPage.AccountLanguageView();
            //Preferences.Set("ModelAnimation", true);
            //Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        
        private void ChangePasswordViewcell_Tapped(object sender, EventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void UserViewcell_Tapped(object sender, EventArgs e)
        {
            var page = new Views.AccountPage.AccountDetailView(IDAccount);
            //Preferences.Set("ModelAnimation", true);
            //Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        //private void cmdChangePassword_click(object obj)
        //{
        //    var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
        //    Application.Current.MainPage.Navigation.PushModalAsync(page);
        //}

        //private void cmdAccount_click(object obj)
        //{
        //    var page = new Views.AccountPage.AccountDetailView(IDAccount);
        //    Application.Current.MainPage.Navigation.PushModalAsync(page);
        //}

        private void Back_Clicked(object obj)
        {
            //Page.TranslateTo(300, 0, 500);

            //Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            //{
            //    // Do something

            //    Application.Current.MainPage.Navigation.PopModalAsync(false);

            //    return false; // True = Repeat again, False = Stop the timer
            //});
            Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        #region Focus
        private void LabelChangePassword_Focused(object sender, FocusEventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void GridChangePassword_Focused(object sender, FocusEventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void GridName_Focused(object sender, FocusEventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void FrameChangePassword_Focused(object sender, FocusEventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void FrameName_Focused(object sender, FocusEventArgs e)
        {
            //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region action
        private void Account_Clicked()
        {
            //ItemContract = obj as ListContractModel;
            if (ItemAccount != null)
            {

                if (ItemAccount.ItemAccount == "Name")
                {
                }
                else if (ItemAccount.ItemAccount == "ChangePassword")
                {
                    //var page = new Views.ForgotPasswordPage.ForgotPassword3View("ChangePassword", UserName, Password);
                    ////Preferences.Set("ModelAnimation", true);
                    ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
                    //Application.Current.MainPage.Navigation.PushAsync(page);
                }
            }
        }
        #endregion
         
    }
}
