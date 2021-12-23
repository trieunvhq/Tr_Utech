

using QRMS.Constants;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class TaoMKMoiPagemodel : BaseViewModel
    {
        public string MatKhau { get; set; }
        public string MatKhauMoi { get; set; }
        public string MoTa { get; set; }
        public override void OnAppearing()
        {
            base.OnAppearing();
            if (MySettings.Vi_En)
            {
                MoTa = "Mật khẩu mới của bạn không được trùng với những mật khẩu đã sử dụng trước đây. Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/thường,số và ký tự đặc biệt như: !, @, #, $, %, &...";
            }
            else
            {
                MoTa = "Mật khẩu mới của bạn không được trùng với những mật khẩu đã sử dụng trước đây. Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/thường,số và ký tự đặc biệt như: !, @, #, $, %, &...";
            }
        }
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
