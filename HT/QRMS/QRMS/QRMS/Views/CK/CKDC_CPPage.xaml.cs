  
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views.CK
{
    public partial class CKDC_CPPage : ContentPage
    {
        //MyScan _MyScan; 

        public CKDC_CPPageModel ViewModel { get; set; }
        public CKDC_CPPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new CKDC_CPPageModel();
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
                    if (!string.IsNullOrWhiteSpace(ViewModel._No))
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NK_SCANPage(
                         ViewModel._No, ViewModel._Date
                         , ViewModel._WarehouseCode, ViewModel._WarehouseName
                         , ViewModel._WarehouseCode_To, ViewModel._WarehouseName_To));
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        void CustomEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ViewModel.ScanComplate(txtTest.Text);
        }
    }
}
