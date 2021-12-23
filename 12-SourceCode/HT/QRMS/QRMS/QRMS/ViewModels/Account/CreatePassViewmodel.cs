  
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class CreatePassViewmodel : BaseViewModel
    { 
        public override void OnAppearing()
        {
            base.OnAppearing(); 

        } 
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
         
    }
}
