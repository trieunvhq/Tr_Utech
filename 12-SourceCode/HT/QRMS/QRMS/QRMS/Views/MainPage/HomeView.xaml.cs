using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.Home;
using QRMS.Resources;
using QRMS.ViewModels.MainPage; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.MainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{
		public HomeView ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = new HomeViewModel(this);
			FormTypeModel.T_Mode = "";
			FormTypeModel.T_Page = ""; 

        } 
	}
}