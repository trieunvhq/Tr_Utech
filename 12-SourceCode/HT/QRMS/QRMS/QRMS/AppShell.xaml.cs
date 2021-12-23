using QRMS.Constants;
using QRMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(Views.InformationPage.NewsView), typeof(Views.InformationPage.NewsView));
            BindingContext = new AppShellViewModel();

            if(MySettings.Temp1 == "")
            {
                Renewals.Icon = "icon_TB_1";
            }
            else
            {
                Renewals.Icon = "icon_TB_2";
            }    
        }
        public void Load()
        {

        }
    }
}
