using QRMS.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountDetailView : ContentPage
    {
        AccountDetailViewModel ViewModel;
        public AccountDetailView(string IDAccount)
        {
            InitializeComponent();
             ViewModel = new AccountDetailViewModel(IDAccount, this);
            BindingContext = ViewModel;
        }
         
        void OnChinhSuaVaCapNhatButonClicked(System.Object sender, System.EventArgs e)
        {
            //var page = new Views.AccountPage.TK_ChinhSuaPage(); 
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }
        protected override void OnAppearing()
        {
            Console.WriteLine("TK_ChinhSuaPage");
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
         

        void Back_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}