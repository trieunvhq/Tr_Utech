 
using Acr.UserDialogs; 
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper; 
using QRMS.Resources; 
using QRMS.ViewModels.Account; 
using System; 
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific; 

namespace QRMS.Views.AccountPage
{
    public partial class BaoMatPage : ContentPage
    {
        BaoMatPageModel ViewModel = new BaoMatPageModel();
        public BaoMatPage()
        {
            MySettings.Title = AppResources.BaoMat;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel.BackCommand = new Command(() =>
            {
                ViewModel.ExecuteBackPage();
            });
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);

        }

        protected override void OnAppearing()
        {
            Console.WriteLine("BaoMatPage");
            base.OnAppearing();
            ViewModel.OnAppearing();
        }


        void ThayDoiMK_Clicked(System.Object sender, System.EventArgs e)
        { 
            var page = new DoiMKPage();
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(page);
        }

        void SwitchCustom1_CheckedChanged(System.Object sender, System.Boolean e)
        {
            ViewModel.ExecuteNextPage(1);
        }
        void SwitchCustom2_CheckedChanged(System.Object sender, System.Boolean e)
        { 
            ViewModel.ExecuteNextPage(2);
        }
        void SwitchCustom3_CheckedChanged(System.Object sender, System.Boolean e)
        { 
            ViewModel.ExecuteNextPage(3);
        }
    }
}
