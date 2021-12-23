 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.Home; 
using QRMS.Resources;
using QRMS.ViewModels.MainPage; 
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace QRMS.Views.MainPage
{
    public partial class HomeMorePage : ContentPage
    {
        public List<CusInsuranceTypeModel> menuModels = new List<CusInsuranceTypeModel>();
        public List<CusUtilityModel> cusUtilityModel = new List<CusUtilityModel>();
        
        List<SvgCachedImage> list_CachedImage = new List<SvgCachedImage>();
        List<Label> list_Label = new List<Label>();
        List<Button> list_Button = new List<Button>();

        private bool _SP_TT;
        public HomeMorePageModel ViewModel { get; set; } = new HomeMorePageModel();
        public HomeMorePage(bool SP_TT, List<CusInsuranceTypeModel> menuModels_, List<CusUtilityModel> cusUtilityModel_)
        {
            menuModels = menuModels_;
            cusUtilityModel = cusUtilityModel_;
            _SP_TT = SP_TT;
            if (SP_TT)
            {
                MySettings.Title = AppResources.SanPhamBaoHiem;
            }
            else
            {
                MySettings.Title = AppResources.TienIchBaoHiem;
            }    
            
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
             
            //
             
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel.BackCommand = new Command(() =>
            {
                Xamarin.Forms.Application.Current.MainPage = new QRMS.AppShell();
            });

        }

        private bool T_Clicked = false;
        protected override void OnAppearing()
        {
            T_Clicked = false;
            base.OnAppearing();
            Load_Data();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Load_Data()
        {
            grid_main.Children.Clear();
            int tt_ = 0;
            if (_SP_TT)
            {
                tt_ = menuModels.Count;
                for (int i=0;i< menuModels.Count;++i)
                {
                    SvgCachedImage img = new SvgCachedImage {
                        Source = "code_0" + menuModels[i].CODE,
                        Margin = new Thickness(10, 10, 10, 0),
                        BackgroundColor = Color.Transparent
                    };
                    Label lb = new Label {
                        Text = menuModels[i].NAME,
                        TextColor = Color.FromHex("#1D1D1F"),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Start,
                        Margin = new Thickness(5, 4, 5, 0),
                        FontSize = 13
                    };
                    Button button = new Button {
                        ClassId = menuModels[i].CODE,
                        Padding = 0,
                        BackgroundColor = Color.Transparent,

                    };
                    button.Clicked += BH_Clicked;
                    int x1, x2, y1, y2;
                    if(i%3==0)
                    {
                        x1 = 1;;y1 = 1 + (4 * (i % 3));
                        x2 = 0; ; y2 = 3 + (4 * (i % 3));
                    }
                    else if (i % 3 == 1)
                    {
                        x1 = 5; ; y1 = 1 + (4 * (i % 3));
                        x2 = 4; ; y2 = 3 + (4 * (i % 3));
                    }
                    else
                    {
                        x1 = 9; ; y1 = 1 + (4 * (i % 3));
                        x2 = 8; ; y2 = 3 + (4 * (i % 3));
                    }
                    grid_main.Children.Add(img, x1, x1+2, y1, y1+2);
                    grid_main.Children.Add(lb, x2, x2 + 4, y2, y2 + 1);
                    grid_main.Children.Add(button, x1, x1 + 2, y1, y1 + 2); 
                } 
            }
            else
            {
                tt_ = cusUtilityModel.Count;
                for (int i = 0; i < cusUtilityModel.Count; ++i)
                {
                    SvgCachedImage img = new SvgCachedImage {
                        Source = "code_2" + cusUtilityModel[i].CODE,
                        Margin = new Thickness(10, 10, 10, 0),
                        BackgroundColor = Color.Transparent
                    };
                    Label lb = new Label {
                        Text = cusUtilityModel[i].NAME,
                        TextColor = Color.FromHex("#1D1D1F"),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Start,
                        Margin = new Thickness(5, 4, 5, 0),
                        FontSize = 13
                    };
                    Button button = new Button {
                        ClassId = cusUtilityModel[i].CODE,
                        Padding = 0,
                        BackgroundColor = Color.Transparent,
                    };
                    button.Clicked += TI_Clicked;
                    int x1, x2, y1, y2;
                    if (i % 3 == 0)
                    {
                        x1 = 1; ; y1 = 1 + (4 * (i / 3));
                        x2 = 0; ; y2 = 3 + (4 * (i / 3));
                    }
                    else if (i % 3 == 1)
                    {
                        x1 = 5; ; y1 = 1 + (4 * (i / 3));
                        x2 = 4; ; y2 = 3 + (4 * (i / 3));
                    }
                    else
                    {
                        x1 = 9; ; y1 = 1 + (4 * (i / 3));
                        x2 = 8; ; y2 = 3 + (4 * (i / 3));
                    }
                    grid_main.Children.Add(img, x1, x1 + 2, y1, y1 + 2);
                    grid_main.Children.Add(lb, x2, x2 + 4, y2, y2 + 1);
                    grid_main.Children.Add(button, x1, x1 + 2, y1, y1 + 2);
                }
            }
            if (tt_ > 9 && tt_ < 13)
            {
                row1_1.Height = 40;
                row1_2.Height = 30;
                row1_3.Height = 40;
                row1_4.Height = 8;

                row2_1.Height = 0;
                row2_2.Height = 0;
                row2_3.Height = 0;
                row2_4.Height = 0;

                row3_1.Height = 0;
                row3_2.Height = 0;
                row3_3.Height = 0;
                row3_4.Height = 0;
            }
            else if (tt_ > 12 && tt_ < 16)
            {
                row1_1.Height = 40;
                row1_2.Height = 30;
                row1_3.Height = 40;
                row1_4.Height = 8;

                row2_1.Height = 40;
                row2_2.Height = 30;
                row2_3.Height = 40;
                row2_4.Height = 8;

                row3_1.Height = 0;
                row3_2.Height = 0;
                row3_3.Height = 0;
                row3_4.Height = 0;
            }
            else if (tt_ > 15)
            {
                row1_1.Height = 40;
                row1_2.Height = 30;
                row1_3.Height = 40;
                row1_4.Height = 8;

                row2_1.Height = 40;
                row2_2.Height = 30;
                row2_3.Height = 40;
                row2_4.Height = 8;

                row3_1.Height = 40;
                row3_2.Height = 30;
                row3_3.Height = 40;
                row3_4.Height = 8;
            }
        }
        private void BH_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (T_Clicked)
                    return;
                T_Clicked = true;

                string code_ = "";
                switch (((Button)sender).ClassId)
                {
                    //case "06":
                    //    ((Button)sender).ClassId = "";
                    //    code_ = Constaint.Insurance_Product.Car_ProductCode;
                    //    await Application.Current.MainPage.Navigation.PushAsync
                    //                    (new DSSPPage(code_), false);
                    //    break;
                    //case "01":
                    //    ((Button)sender).ClassId = "";
                    //    code_ = Constaint.Insurance_Product.Moto_ProductCode;
                    //    await Application.Current.MainPage.Navigation.PushAsync
                    //                    (new DSSPPage(code_), false);
                    //    break;
                    //case "03":
                    //    ((Button)sender).ClassId = "";
                    //    code_ = Constaint.Insurance_Product.Home_ProductCode;
                    //    await Application.Current.MainPage.Navigation.PushAsync
                    //                    (new DSSPPage(code_), false);
                    //    break;
                    //case "04":
                    //    ((Button)sender).ClassId = "";
                    //    code_ = Constaint.Insurance_Product.Health_ProductCode;
                    //    await Application.Current.MainPage.Navigation.PushAsync
                    //                    (new DSSPPage(code_), false);
                    //    break;
                    //case "02":
                    //    ((Button)sender).ClassId = "";
                    //    code_ = Constaint.Insurance_Product.Travel_ProductCode;
                    //    await Application.Current.MainPage.Navigation.PushAsync
                    //                    (new DSSPPage(code_), false);
                    //    break;
                }
            });
        }
        private void TI_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (T_Clicked)
                    return;
                T_Clicked = true;
                string code_ = ((Button)sender).ClassId;
                switch (code_)
                {
                    //case "01":
                    //    ((Button)sender).ClassId = "";
                    //    MySettings.Title = AppResources.GiayChungNhanBaoHiem;
                    //    await Application.Current.MainPage.Navigation.PushAsync(new TraCuuGCNPage());
                    //    break;
                    //case "02":
                    //    ((Button)sender).ClassId = "";
                    //    MySettings.Title = AppResources.MangLuoiPjico;
                    //    await Application.Current.MainPage.Navigation.PushAsync(new TraCuuMLPijcoPage(), false);
                    //    break;
                    //case "03":
                    //    ((Button)sender).ClassId = "";
                    //    MySettings.Title = AppResources.Alter;
                    //    var page = new OtoBB_DS_SDBSPage(1, "");
                    //    await Application.Current.MainPage.Navigation.PushAsync(page, false);
                    //    break;
                    //case "04":
                    //    ((Button)sender).ClassId = "";
                    //    MySettings.Title = AppResources.MangLuoiGara;
                    //    await Application.Current.MainPage.Navigation.PushAsync(new TraCuuGara(), false);
                    //    break;
                    //case "05":
                    //    ((Button)sender).ClassId = "";
                    //    MySettings.Title = AppResources.MangLuoiBenhVien;
                    //    await Application.Current.MainPage.Navigation.PushAsync(new TraCuuMangLuoiCSYT(), false);
                    //    break;
                    //case "06":
                    //    ((Button)sender).ClassId = "";
                    //    await TCCayxang_ClickedAsync();
                    //    ((Button)sender).ClassId = code_;
                    //    T_Clicked = false;
                    //    break;
                    //case "07":
                    //    //Tra Cuu Boi Thuong
                    //    break;
                    //case "08":
                    //    //Tái tục
                    //    ((Button)sender).ClassId = ""; 
                    //    await Application.Current.MainPage.Navigation.PushAsync(new DS_TTPage());
                    //    break;
                    //case "09":
                    //    //Khai báo tổn thất
                    //    break;

                }
            });
        }
        private async Task TCCayxang_ClickedAsync()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var link = APIHelper.GetObjectFromAPI<BaseModel<string>>
                        (Constaint.ServiceAddress, Constaint.APIurl.getlinkgasstation, null);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Controls.LoadingUtility.HideAsync();
                        if (!string.IsNullOrEmpty(link.data))
                        {
                            Device.OpenUri(new Uri(link.data));
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<ILogger>().Log(ex.ToString());
                    // Log ex to db
                    var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                    var FCMTockenValue = String.Empty;
                    if (FCMToken)
                    {
                        FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                    }
                    var token = FCMTockenValue;
                    var appType = Constaint.App_Type.Agent;
                    var osType = Device.OS.ToString();
                    var namespaceInFile = GetType().Namespace;
                    var className = GetType().Name;
                    var methodName = "TCCayxang_Clicked";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
            //var page = new Views.ErrorPage.UnderConstructionView();
            //_ = PopupNavigation.Instance.PushAsync(page);
        } 
    }
}
