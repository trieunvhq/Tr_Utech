using Acr.UserDialogs;
using FFImageLoading.Svg.Forms;
using QRMS.Constants;
using QRMS.interfaces;
using QRMS.Resources;
using QRMS.ViewModels.Account; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountView : ContentPage
    {
        AccountViewModel ViewModel;
        public AccountView(string screenType, string user, string pass)
        {
            InitializeComponent();
            abs.Children.Remove(grid_popup);
            abs.Children.Remove(lbNen);
            abs.Children.Remove(grid_ThoatUD);
            ViewModel = new AccountViewModel(screenType, user, pass, this);
            ViewModel._AccountView = this;
            BindingContext = ViewModel;
        }

        public AccountView()
        {
            InitializeComponent();
            abs.Children.Remove(grid_popup);
            abs.Children.Remove(lbNen);
            abs.Children.Remove(grid_ThoatUD);
            ViewModel = new AccountViewModel(MySettings.screenType, MySettings.UserName, MySettings.Password, this);
            ViewModel._AccountView = this;
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            Console.WriteLine("AccountView");
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.SetTabBarIsVisible(this, false);
            });
            lbChuaChonXepHang.IsVisible = false;
            if (!abs.Children.Contains(lbNen))
                abs.Children.Add(lbNen);
            if (!abs.Children.Contains(grid_popup))
                abs.Children.Add(grid_popup);
        }

        void BtnBoQuaClicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.SetTabBarIsVisible(this, true);
            });
            abs.Children.Remove(grid_popup);
            abs.Children.Remove(lbNen);
        }

        void OnChiaSeDanhGiaButtonClicked(System.Object sender, System.EventArgs e)
        {
            var IsError = false;
            lbChuaChonXepHang.IsVisible = false;

            if (string.IsNullOrEmpty(ViewModel.FEEDBACK_STAR))
            {
                IsError = true;
                lbChuaChonXepHang.IsVisible = true;
            }
            if (!IsError)
                ViewModel.ChiaSeCamNghi();
        }
        public void XoaDanhGia(bool tt)
        {
            if (tt)
            {
                row1.Height = 0;
                row2.Height = 0;
                row3.Height = 0;
            }
            else
            {
                row1.Height = 150;
                row2.Height = 80;
                row3.Height = 60;
            }
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            lbChuaChonXepHang.IsVisible = false;
            string id_ = ((SvgCachedImage)sender).ClassId;
            ViewModel.FEEDBACK_STAR = id_;
            if (id_ == "1")
            {
                img1.Source = "star_cam.png";

                img2.Source = "star_den.png";
                img3.Source = "star_den.png";
                img4.Source = "star_den.png";
                img5.Source = "star_den.png";
            }
            else if (id_ == "2")
            {
                img1.Source = "star_cam.png";
                img2.Source = "star_cam.png";

                img3.Source = "star_den.png";
                img4.Source = "star_den.png";
                img5.Source = "star_den.png";
            }
            else if (id_ == "3")
            {
                img1.Source = "star_cam.png";
                img2.Source = "star_cam.png";
                img3.Source = "star_cam.png";

                img4.Source = "star_den.png";
                img5.Source = "star_den.png";
            }
            else if (id_ == "4")
            {
                img1.Source = "star_cam.png";
                img2.Source = "star_cam.png";
                img3.Source = "star_cam.png";
                img4.Source = "star_cam.png";

                img5.Source = "star_den.png";
            }
            else if (id_ == "5")
            {
                img1.Source = "star_cam.png";
                img2.Source = "star_cam.png";
                img3.Source = "star_cam.png";
                img4.Source = "star_cam.png";
                img5.Source = "star_cam.png";
            }
        }

        void UserViewcell_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new Views.AccountPage.AccountDetailView(ViewModel.IDAccount);
            ////Preferences.Set("ModelAnimation", true);
            ////Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void MatKhauVaBaoMat_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new Views.AccountPage.BaoMatPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void QLTB_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new QLTBPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void CHTG_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new CHTGPage("F");
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void DieuKienVaChinhSach_Tapped(System.Object sender, System.EventArgs e)
        {
            //    var page = new CHTGPage("P");
            //    Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void TTNHD_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new DS_TTNHDPage(0, "");
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void HoTro_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new HoTroPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void NgonNgu_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new NgonNguPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void ThongTinUngDung_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new TTUDPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void LichSu_Tapped(System.Object sender, System.EventArgs e)
        {
            //var page = new LichSuPage();
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void DonChuaHoanThanh_Tapped(System.Object sender, System.EventArgs e)
        {
            //    var page = new DCHTPage();
            //    Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void ThoatApp_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.SetTabBarIsVisible(this, false);
            });
            if (!abs.Children.Contains(lbNen))
                abs.Children.Add(lbNen);
            if (!abs.Children.Contains(grid_ThoatUD))
                abs.Children.Add(grid_ThoatUD);
            //var answer = await UserDialogs.Instance.ConfirmAsync("", AppResources.ThoatUngDung, AppResources.Co, AppResources.Khong);
            //if (answer)
            //{
            //    var closer = DependencyService.Get<ICloseApplication>();
            //    closer?.closeApplication();
            //}
        }

        void BtnKhongClicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.SetTabBarIsVisible(this, true);
            });
            abs.Children.Remove(grid_ThoatUD);
            abs.Children.Remove(lbNen);
        }

        void OnDangXuatClicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}