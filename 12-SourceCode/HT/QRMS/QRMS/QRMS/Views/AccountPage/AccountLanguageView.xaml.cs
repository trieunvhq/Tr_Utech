using QRMS.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountLanguageView : ContentPage
    {
        public AccountLanguageView()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            Shell.SetTabBarIsVisible(this, false);
            BindingContext = new AccountLanguageViewModel(this);
        }
    }
}