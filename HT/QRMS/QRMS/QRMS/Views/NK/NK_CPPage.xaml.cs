  
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class NK_CPPage : ContentPage
    {
        //MyScan _MyScan; 

        public NK_CPPageModel ViewModel { get; set; }
        public NK_CPPage()
        {
            InitializeComponent();

            if(MySettings.Index_Page==1)
            {
                lbTieuDe.Text = "Chọn phiếu nhập";
            }
            else if (MySettings.Index_Page == 2)
            {
                lbTieuDe.Text = "Chọn phiếu xuất";
            }
            else if (MySettings.Index_Page == 3)
            {
                lbTieuDe.Text = "Chọn phiếu chuyển";
            }
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new NK_CPPageModel();
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._NK_CPPage = this;
            //_MyScan = new MyScan();
            //_MyScan._NK_CPPageModel = ViewModel;

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
        }

        protected override bool OnBackButtonPressed()
        {
            BtnQuayLai_CLicked(null, null);
            return true;
        }
        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                    await Controls.LoadingUtility.HideAsync();
                });
            });

        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!string.IsNullOrWhiteSpace(ViewModel._PurchaseOrderNo))
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NhapKhoDungCuPage(
                         ViewModel._PurchaseOrderNo, ViewModel._WarehouseCode, ViewModel._PurchaseOrderDate, ViewModel._WarehouseName));
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}
