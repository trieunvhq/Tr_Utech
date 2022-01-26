  
using QRMS.Constants;
using QRMS.Views.CK;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class NK_HOMEPage : ContentPage
    {
        public NK_HOMEPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);

            row_trencung.Height = 20;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (MySettings.h_QRMS >= 812)
                    {
                        row_trencung.Height = 40;
                    }
                    else
                    {
                    }
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }


            if (MySettings.Index_Page == 1)
            {
                lbTieuDe.Text = "NHẬP KHO";
                lbDungCu.Text = "1. NHẬP KHO DỤNG CỤ";
                lbNguyenLieu.Text = "2. NHẬP KHO NGUYÊN LIỆU";
                lbThanhPham.Text = "3. NHẬP KHO THÀNH PHẨM";
           
            }
            else if (MySettings.Index_Page == 2)
            {
                lbTieuDe.Text = "XUẤT KHO";
                lbDungCu.Text = "1. XUẤT KHO DỤNG CỤ";
                lbNguyenLieu.Text = "2. XUẤT KHO NGUYÊN LIỆU";
                lbThanhPham.Text = "3. XUẤT KHO THÀNH PHẨM"; 
            }
            else if (MySettings.Index_Page == 3)
            {
                lbTieuDe.Text = "CHUYỂN KHO";
                lbDungCu.Text = "1. CHUYỂN KHO DỤNG CỤ";
                lbNguyenLieu.Text = "2. CHUYỂN KHO NGUYÊN LIỆU";
                lbThanhPham.Text = "3. CHUYỂN KHO THÀNH PHẨM"; 
            }

        }


        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        async void BtnDungCu_CLicked(System.Object sender, System.EventArgs e)
        {
            if (MySettings.Index_Page == 1 || MySettings.Index_Page == 2)
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NK_CPPage());

            }
            else if (MySettings.Index_Page == 3)
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CKDC_CPPage());
            }

        }

        async void BtnNguyenLieu_CLicked(System.Object sender, System.EventArgs e)
        {
            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ChonKhoKiemKePage());

        }

        async void BtnThanhPham_CLicked(System.Object sender, System.EventArgs e)
        {
            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ChonKhoKiemKe2Page());

        }
    }
}
