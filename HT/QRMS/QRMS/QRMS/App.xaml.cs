﻿using Microsoft.EntityFrameworkCore;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("samantha.ttf", Alias ="FontAwesome2")]
[assembly: ExportFont("Mulish_Light.ttf", Alias ="FontAwesome")]
namespace QRMS
{
    public partial class App : Application
    {
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public App()
        {
            // Todo resource language initial selection
            // QRMS.Resources.AppResources.Culture = new CultureInfo("vi");

            Thread.CurrentThread.CurrentCulture = Constaint.cultureInfo;
            Thread.CurrentThread.CurrentUICulture = Constaint.cultureInfo;
            if (Constaint.cultureInfo.Name.Contains("en"))
            {
                MySettings.Vi_En = false;
            }
            else
            {
                MySettings.Vi_En = true;
            }
            InitializeComponent();

            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "SwipeView_Experimental"
            });

            Application.Current.UserAppTheme = OSAppTheme.Light;
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}