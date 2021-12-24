using Microsoft.EntityFrameworkCore;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("samantha.ttf", Alias ="FontAwesome2")]
[assembly: ExportFont("Mulish_Light.ttf", Alias ="FontAwesome")]
namespace QRMS
{
    public partial class App : Application
    {
        private DatabaseContext conn;
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public App()
        {
            // Todo resource language initial selection
            // QRMS.Resources.AppResources.Culture = new CultureInfo("vi");

            Thread.CurrentThread.CurrentCulture = Constaint.cultureInfo;
            Thread.CurrentThread.CurrentUICulture = Constaint.cultureInfo;
            if (Constaint.cultureInfo.Name.Contains("en"))
            {
                MySettings.Vi_En = false;
            }
            else
            {
                MySettings.Vi_En = true;
            }
            InitializeComponent();

            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "SwipeView_Experimental"
            });

            Application.Current.UserAppTheme = OSAppTheme.Light;
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            //conn = new DatabaseContext();

            using (var conn = new DatabaseContext())
            {
                
                //BaseModel<string> token = NotificationsAPI.GetToken(FCMTockenValue);
                var setting = conn.settingModels.AsNoTracking().FirstOrDefault();
                if (setting == null)
                {
                    List<SettingModel> lstSetting = new List<SettingModel>()
                    {
                        new SettingModel() {Key = "Language", Value = "Vi" },
                        //new SettingModel() {Key = "Version", Value = "V.001"},
                    };

                    conn.settingModels.AddRange(lstSetting);
                    conn.SaveChanges();
                }

                var language = conn.languageModels.AsNoTracking().FirstOrDefault();
                if (language == null)
                {
                    List<LanguageModel> lstItem = new List<LanguageModel>()
                    {
                        //#region ShellTemplate
                        //new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblHomePage", LangVi = "Trang chủ", LangEn = "Home", Remark="Trang chủ" },
                        //new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblHistoryPage", LangVi = "Tra cứu", LangEn = "Search", Remark="Lịch sử" },
                        //new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblApprovalPage", LangVi = "Phê duyệt", LangEn = "Approval", Remark="Phê duyệt" },
                        //new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblRenewalsPage", LangVi = "Tái tục", LangEn = "Renewals", Remark="Tái tục" },
                        //new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblDraftPage", LangVi = "Bản nháp", LangEn = "Draft", Remark="Bản nháp" },
                        //#endregion

                        //#region ForgotPassword
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblTutorial", LangVi = "Vui lòng nhập tên đăng nhập để nhận mã OTP lấy lại mật khẩu", LangEn = "Please enter user name", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblUserName", LangVi = "Tên đăng nhập", LangEn = "User name", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "TextBox", TypeName = "txtUserName", LangVi = "Nhập tên đăng nhập của bạn", LangEn = "Input your user name", Remark="Tên đăng nhập" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblUserEmptyError", LangVi = "Nhập tên đăng nhập", LangEn = "Input user name", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Button", TypeName = "btnOTP", LangVi = "Lấy mã OTP", LangEn = "Get OTP", Remark="OTP" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblForgotPassword", LangVi = "Bạn muốn cung cấp lại mật khẩu?", LangEn = "Forgot password?", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblMsgError", LangVi = "Tài khoản không tồn tại", LangEn = "Account doesn't exist", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblAccountLocked", LangVi = "Tài khoản đã bị khóa", LangEn = "Account is locked", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L02", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblBack", LangVi = "Quay lại", LangEn = "Back", Remark="Quay lại" },

                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblTutorialNumberPhone", LangVi = "Vui lòng xác nhận mã bảo mật OTP đã được gửi tới số điện thoại của bạn", LangEn = "Please enter OTP sent to your phone number", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblTutorialEmail", LangVi = "Vui lòng xác nhận mã bảo mật OTP đã được gửi tới email của bạn", LangEn = "Please enter OTP sent to your email", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOTP", LangVi = "Mã OTP", LangEn = "OTP", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "TextBox", TypeName = "txtOTP", LangVi = "Nhập mã OTP", LangEn = "Input OTP", Remark="nhập OTP" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Button", TypeName = "btnOTP", LangVi = "Xác nhận", LangEn = "Confirm", Remark="xác nhập OTP" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOTPConfirm", LangVi = "Xác nhận mã OTP để khôi phục lại mật khẩu", LangEn = "OTP confirm", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblMsgError", LangVi = "Mã OTP không đúng", LangEn = "OTP is incorrect", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOTPExpiredError", LangVi = "Mã OTP đã hết hạn", LangEn = "OTP has expired", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOTPAgain", LangVi = "Gửi lại mã OTP", LangEn = "Get OTP again", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOTPEmptyError", LangVi = "Nhập mã OTP", LangEn = "Input OTP", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L03", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblBack", LangVi = "Quay lại", LangEn = "Back", Remark="Quay lại" },

                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblChangePassword", LangVi = "Thay đổi mật khẩu mới", LangEn = "Change new password", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblTutorial", LangVi = "Thông tin mật khẩu của bạn phải chứa chữ cái in hoa, chữ thường, số, ký tự đặc biệt như: !, @, #, $, %, &...", LangEn = "Your password information must contain uppercase letters, lowercase letters, numbers, special characters like: !, @, #, $,%, &...", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOldPassword", LangVi = "Mật khẩu hiện tại", LangEn = "Current password", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "TextBox", TypeName = "txtOldPassword", LangVi = "Vui lòng nhập mật khẩu hiện tại của bạn", LangEn = "Please input your current password", Remark="Tên đăng nhập" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblOldPasswordEmptyError", LangVi = "Nhập mật khẩu hiện tại", LangEn = "Input current password", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblVerifyOldPasswordError", LangVi = "Mật khẩu hiện tại không đúng", LangEn = "Current password is incorrect", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblNewPassword", LangVi = "Mật khẩu mới", LangEn = "New password", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "TextBox", TypeName = "txtNewPassword", LangVi = "Vui lòng nhập mật khẩu mới", LangEn = "Please input new password", Remark="Tên đăng nhập" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblNewPasswordEmptyError", LangVi = "Nhập mật khẩu mới", LangEn = "Input new password", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblVerifyNewPassword", LangVi = "Xác nhận lại mật khẩu mới", LangEn = "Verify new password", Remark="Thông báo" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "TextBox", TypeName = "txtVerifyNewPassword", LangVi = "Vui lòng nhập lại chính xác mật khẩu mới", LangEn = "Please input verify new password", Remark="Tên đăng nhập" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblVerifyNewPasswordEmptyError", LangVi = "Xác nhận mật khẩu mới bắt buộc nhập", LangEn = "Verify new password is blank", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Button", TypeName = "btnPasswordChange", LangVi = "Thay đổi mật khẩu", LangEn = "Change password", Remark="thay đổi mật khẩu" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblMsgError", LangVi = "Mật khẩu xác nhận không đúng", LangEn = "Verify new password is incorrect", Remark="Thông báo lỗi" },
                        //new LanguageModel() { ScreenCode = "L04", ScreenName = "ForgotPassword", ItemType = "Label", TypeName = "lblBack", LangVi = "Quay lại", LangEn = "Back", Remark="Quay lại" },
                        //#endregion
                    };

                    conn.languageModels.AddRange(lstItem);
                    conn.SaveChanges();
                }

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
