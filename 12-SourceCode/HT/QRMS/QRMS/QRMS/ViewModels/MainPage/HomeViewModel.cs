using Acr.UserDialogs;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Newtonsoft.Json;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.Home;
using QRMS.Resources;
using QRMS.Views;
using QRMS.Views.MainPage; 
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRMS.ViewModels.MainPage
{
    public class HomeViewModel : BaseViewModel
    {
        #region bien
        private bool T_Clicked = false;

        public DatabaseContext conn;
        public ContentPage Page;

        public Command BHCommand { get; }
        public Command BHKhacCommand { get; }
        public Command TICommand { get; }
        public Command TIKhacCommand { get; }
        public Command BHOToCommand { get; }
        public Command BHXeMayCommand { get; }
        public Command BHSucKhoeCommand { get; }
        public Command BHTuNhanCommand { get; }
        public Command BHDuLichCommand { get; }
        public Command TCGCNBHCommand { get; }
        public Command TCGaraCommand { get; }
        public Command TCCayXangCommand { get; }
        public Command TCCSYTCommand { get; }
        public Command TCPJICOCommand { get; }
        public Command TapAccountCommand { get; } 
        private ObservableCollection<CarouselViewHome_model> Carousel_model = new ObservableCollection<CarouselViewHome_model>();

        private string IDAccount;

        private string _NEW_BELL;
        public string NEW_BELL
        {
            get => _NEW_BELL;
            set
            {
                _NEW_BELL = value;
                OnPropertyChanged();
            }
        }

        private string _OLD_BELL;
        public string OLD_BELL
        {
            get => _OLD_BELL;
            set
            {
                _OLD_BELL = value;
                OnPropertyChanged();
            }
        }

        public Xamarin.Forms.CarouselView CarouselView_Main;
        public SvgCachedImage Banner2;

        public List<Button> ds_btnAction1;
        public List<Button> ds_btnAction2;
        public List<SvgCachedImage> ds_btn1;
        public List<Label> ds_lb1;
        public List<SvgCachedImage> ds_btn2;
        public List<Label> ds_lb2;

        public List<Image> ds_btn_TB;
        public List<Label> ds_lb;
        public static bool IsLoadedMain { get; set; }
        #endregion

        #region hienthi
        private string UserName;
        private string Password;

        private string _img_btn3;
        public string img_btn3
        {
            get => _img_btn3;
            set
            {
                _img_btn3 = value;
                OnPropertyChanged();
            }
        }

        private string _imgBHXeMay;
        public string imgBHXeMay
        {
            get => _imgBHXeMay;
            set
            {
                _imgBHXeMay = value;
                OnPropertyChanged();
            }
        }

        private string _imgBHSucKhoe;
        public string imgBHSucKhoe
        {
            get => _imgBHSucKhoe;
            set
            {
                _imgBHSucKhoe = value;
                OnPropertyChanged();
            }
        }

        private string _imgBHTuNhan;
        public string imgBHTuNhan
        {
            get => _imgBHTuNhan;
            set
            {
                _imgBHTuNhan = value;
                OnPropertyChanged();
            }
        }

        private string _imgBHDuLich;
        public string imgBHDuLich
        {
            get => _imgBHDuLich;
            set
            {
                _imgBHDuLich = value;
                OnPropertyChanged();
            }
        }

        private string _imgTCGCNBH;
        public string imgTCGCNBH
        {
            get => _imgTCGCNBH;
            set
            {
                _imgTCGCNBH = value;
                OnPropertyChanged();
            }
        }

        private string _imgTCGara;
        public string imgTCGara
        {
            get => _imgTCGara;
            set
            {
                _imgTCGara = value;
                OnPropertyChanged();
            }
        }

        private string _imgTCCayXang;
        public string imgTCCayXang
        {
            get => _imgTCCayXang;
            set
            {
                _imgTCCayXang = value;
                OnPropertyChanged();
            }
        }

        private string _imgTCCSYT;
        public string imgTCCSYT
        {
            get => _imgTCCSYT;
            set
            {
                _imgTCCSYT = value;
                OnPropertyChanged();
            }
        }

        private string _imgTCPJICO;
        public string imgTCPJICO
        {
            get => _imgTCPJICO;
            set
            {
                _imgTCPJICO = value;
                OnPropertyChanged();
            }
        }

        private string _imgApproval;
        public string imgApproval
        {
            get => _imgApproval;
            set
            {
                _imgApproval = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region helper
        public IEnumerable<LanguageModel> languages()
        {
            yield return new LanguageModel() { ScreenCode = "code_006", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm \nÔ tô", LangEn = "Auto Insurance", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_001", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm \nXe máy", LangEn = "Moto Insurance", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_004", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm \nSức khỏe", LangEn = "Health Insurance", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_003", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm \nnnhà tư nhân", LangEn = "Home Insurance", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_002", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm du lịch", LangEn = "Travel Insurance", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_0061", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm động vật", LangEn = "Bảo hiểm\nđộng vật", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_007", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm\nhàng không", LangEn = "Bảo hiểm\nhàng không", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_008", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm tàu hàng", LangEn = "Bảo hiểm tàu hàng", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_009", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm Container", LangEn = "Bảo hiểm Container", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_010", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm kho hàng", LangEn = "Bảo hiểm kho hàng", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_011", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm\nthang máy", LangEn = "Bảo hiểm thang máy", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_012", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm\ntiền gửi", LangEn = "Bảo hiểm tiền gửi", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_013", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm hỗ trợ\nviện phí", LangEn = "Bảo hiểm hỗ trợ viện phí", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_014", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm\nsốt xuất huyết", LangEn = "Bảo hiểm sốt xuất huyết", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_015", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm\nnông nghiệp", LangEn = "Bảo hiểm nông nghiệp", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_016", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm hưu trí", LangEn = "Bảo hiểm hưu trí", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_017", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm Virus", LangEn = "Bảo hiểm Virus", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_018", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Bảo hiểm cho con", LangEn = "Bảo hiểm cho con", Remark = "" };

            yield return new LanguageModel() { ScreenCode = "code_101", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Giấy chứng\nnhận bảo hiểm", LangEn = "Giấy chứng nhận bảo hiểm", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_102", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Tái tục", LangEn = "Tái tục", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_103", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Yêu cầu sửa đổi", LangEn = "Yêu cầu sửa đổi", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_104", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Khai báo tổn thất", LangEn = "Khai báo tổn thất", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_105", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Tra cứu bồi thường", LangEn = "Tra cứu bồi thường", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_106", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Mạng lưới Gara", LangEn = "Mạng lưới Gara", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_107", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Mạng lưới Pjico", LangEn = "Mạng lưới Pjico", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_108", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Mạng lưới bệnh viện", LangEn = "Mạng lưới bệnh viện", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "code_109", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Mạng lưới cây xăng", LangEn = "Mạng lưới cây xăng", Remark = "" };

            //yield return new LanguageModel() { ScreenCode = "code_001", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Sản phẩm khác", LangEn = "Sản phẩm khác", Remark = "" };
            //yield return new LanguageModel() { ScreenCode = "code_001", ScreenName = "MainPage", ItemType = "Button", TypeName = "", LangVi = "Tiện ích khác", LangEn = "Tiện ích khác", Remark = "" };
        }
        #endregion

        public TabBar shellbar;
        List<CusInsuranceTypeModel> menuModels = new List<CusInsuranceTypeModel>();
        List<CusMenuUtilityModel> utilityModels = new List<CusMenuUtilityModel>();
        List<CusBannerModel> cusBannerModels = new List<CusBannerModel>();
        Image btn_TB1;
        Image btn_TB2;
        Image btn_TB3;
        Image btn_TB4;
        SvgCachedImage btn7, btn8, btn9, btn10, btn11;
        public bool isIndicator { get; set; } = false;
        public HomeViewModel(ContentPage page)
        {
            MySettings.screenType = "account";

            Page = page;
            ds_btnAction1 = new List<Button>();
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction1"));
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction2"));
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction3"));
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction4"));
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction5"));
            ds_btnAction1.Add(Page.FindByName<Button>("BtnAction6"));
            ds_btnAction1[0].Clicked += BH_Clicked; ds_btnAction1[1].Clicked += BH_Clicked; ds_btnAction1[2].Clicked += BH_Clicked;
            ds_btnAction1[3].Clicked += BH_Clicked; ds_btnAction1[4].Clicked += BH_Clicked;

            ds_btnAction2 = new List<Button>();
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction1_2"));
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction2_2"));
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction3_2"));
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction4_2"));
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction5_2"));
            ds_btnAction2.Add(Page.FindByName<Button>("BtnAction6_2"));
            ds_btnAction2[0].Clicked += TI_Clicked; ds_btnAction2[1].Clicked += TI_Clicked; ds_btnAction2[2].Clicked += TI_Clicked;
            ds_btnAction2[3].Clicked += TI_Clicked; ds_btnAction2[4].Clicked += TI_Clicked;
            ds_btn1 = new List<SvgCachedImage>();
            ds_btn2 = new List<SvgCachedImage>();
            ds_lb1 = new List<Label>();
            ds_lb2 = new List<Label>();

            ds_btn1.Add(Page.FindByName<SvgCachedImage>("btn1"));
            ds_btn1.Add(Page.FindByName<SvgCachedImage>("btn2"));
            ds_btn1.Add(Page.FindByName<SvgCachedImage>("btn3"));
            ds_btn1.Add(Page.FindByName<SvgCachedImage>("btn4"));
            ds_btn1.Add(Page.FindByName<SvgCachedImage>("btn5"));

            ds_lb1.Add(Page.FindByName<Label>("lb1"));
            ds_lb1.Add(Page.FindByName<Label>("lb2"));
            ds_lb1.Add(Page.FindByName<Label>("lb3"));
            ds_lb1.Add(Page.FindByName<Label>("lb4"));
            ds_lb1.Add(Page.FindByName<Label>("lb5"));

            btn7 = Page.FindByName<SvgCachedImage>("btn7");
            btn8 = Page.FindByName<SvgCachedImage>("btn8");
            btn9 = Page.FindByName<SvgCachedImage>("btn9");
            btn10 = Page.FindByName<SvgCachedImage>("btn10");
            btn11 = Page.FindByName<SvgCachedImage>("btn11");
            ds_btn2.Add(btn7); ds_btn2.Add(btn8); ds_btn2.Add(btn9);
            ds_btn2.Add(btn10); ds_btn2.Add(btn11);

            ds_lb2.Add(Page.FindByName<Label>("lb7"));
            ds_lb2.Add(Page.FindByName<Label>("lb8"));
            ds_lb2.Add(Page.FindByName<Label>("lb9"));
            ds_lb2.Add(Page.FindByName<Label>("lb10"));
            ds_lb2.Add(Page.FindByName<Label>("lb11"));



            ds_lb = new List<Label>();
            ds_lb.Add(Page.FindByName<Label>("lb7_2"));
            ds_lb.Add(Page.FindByName<Label>("lb8_2"));
            ds_lb.Add(Page.FindByName<Label>("lb9_2"));
            ds_lb.Add(Page.FindByName<Label>("lb10_2"));
            ds_lb.Add(Page.FindByName<Label>("lb11_2"));

            ds_btn_TB = new List<Image>();

            Image btn_TB1 = Page.FindByName<Image>("btn7_2");
            Image btn_TB2 = Page.FindByName<Image>("btn8_2");
            Image btn_TB3 = Page.FindByName<Image>("btn9_2");
            Image btn_TB4 = Page.FindByName<Image>("btn10_2");
            ds_btn_TB.Add(btn_TB1);
            ds_btn_TB.Add(btn_TB2);
            ds_btn_TB.Add(btn_TB3);
            ds_btn_TB.Add(btn_TB4);
            //
            CarouselView_Main = Page.FindByName<Xamarin.Forms.CarouselView>("CarouselView_Main");
            CarouselView_Main.ItemsSource = null;
            Banner2 = Page.FindByName<SvgCachedImage>("Banner2");
            Banner2.Source = null;
            //

             
            TapAccountCommand = new Command(TapAccount_Clicked);
            BHOToCommand = new Command(BHOto_Clicked);
            BHXeMayCommand = new Command(BHXemay_Clicked);
            BHSucKhoeCommand = new Command(BHSuckhoe_Clicked);
            BHTuNhanCommand = new Command(BHTunhan_Clicked);
            BHDuLichCommand = new Command(BHDulich_Clicked); 
             

            //_ = Load_Notify();

            UserName = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNameKey).Result;
            Password = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.PasswordKey).Result;
            MySettings.UserName = UserName;
            MySettings.Password = Password;

            //_ = GetBanner();
            _ = GetVAT();


            Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {

                var submit = APIHelper.PostObjectToAPIAsync<BaseModel<HomeMenuModel>>
                                       (Constaint.ServiceAddress, Constaint.APIurl.getmainmenu, new
                                       {
                                           accountId = FormTypeModel.UserID
                                       });
                _ = submit.ContinueWith(next =>
                {
                    if (submit.Result.RespondCode != null && submit.Result.RespondCode != "")
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        { 
                            if(submit.Result.data!=null)
                            {
                                menuModels = submit.Result.data.CusProduct;
                                utilityModels = submit.Result.data.CusUtility;
                                cusBannerModels = submit.Result.data.CusBanner;
                                int tempp_ = submit.Result.data.NotifyCount;

                                tempp_ = 8;
                                if (tempp_ > 0)
                                {
                                    if (MySettings.Temp1 == "" || MySettings.Temp1 == null)
                                    {
                                        MySettings.Temp1 = "1";
                                        Application.Current.MainPage = new QRMS.AppShell();
                                    }
                                }
                                else
                                {
                                    if (MySettings.Temp1 == "")
                                    {
                                    }
                                    else
                                    {
                                        MySettings.Temp1 = "";
                                        Application.Current.MainPage = new QRMS.AppShell();
                                    }
                                }

                                Carousel_model = new ObservableCollection<CarouselViewHome_model>();

                                for (int i = 0; i < cusBannerModels.Count; ++i)
                                {
                                    if (cusBannerModels[i].Position == 1)
                                    {
                                        if (cusBannerModels[i].lstImage != null && cusBannerModels[i].lstImage.Count > 0)
                                        {
                                            for (int j = 0; j < cusBannerModels[i].lstImage.Count; ++j)
                                            {
                                                ImageSource source = null;
                                                try
                                                {
                                                    //stream = new MemoryStream(result.Result.data[i].IMAGE);
                                                    //source = ImageSource.FromStream(() => stream);
                                                    source = new UriImageSource { Uri = new Uri($"{cusBannerModels[i].lstImage[j].ImageBase64}") };
                                                }
                                                catch { } 

                                                //imageSources.Add(ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(cusBannerModels[i].lstImage[j].ImageBase64))));
                                                Carousel_model.Add(new CarouselViewHome_model
                                                {
                                                    Position = 0,
                                                    ImageBase64 = source
                                                });
                                            }
                                        }     
                                    }
                                    else
                                    {
                                        if(cusBannerModels[i].lstImage!=null && cusBannerModels[i].lstImage.Count>0)
                                        { 
                                            try
                                            { 
                                                Banner2.Source = new UriImageSource { Uri = new Uri($"{cusBannerModels[i].lstImage[0].ImageBase64}") }; 
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                CarouselView_Main.ItemsSource = Carousel_model;
                                if(Carousel_model.Count>1)
                                {
                                    isIndicator = true;
                                }
                                else
                                {
                                    isIndicator = false;
                                }
                                //
                                //var lstProp = GetType().GetProperties();
                                //var lstLangTxt = this.languages();
                                //var lstTypeName = new Dictionary<string, string>();
                                //if (MySettings.Vi_En)
                                //    lstTypeName = lstLangTxt.ToDictionary(a => a.ScreenCode, a => a.LangVi);
                                //else
                                //    lstTypeName = lstLangTxt.ToDictionary(a => a.ScreenCode, a => a.LangEn);
                                int max_ = 5;
                                if (menuModels.Count < 5)
                                    max_ = menuModels.Count;

                                for (int i = 0; i < max_; ++i)
                                {
                                    ds_lb1[i].Text = menuModels[i].NAME;
                                    ds_btn1[i].Source = "code_0" + menuModels[i].CODE;
                                    ds_btnAction1[i].ClassId = menuModels[i].CODE;
                                }
                                if (menuModels.Count == 6)
                                {
                                    ds_lb1[5].Text = menuModels[5].NAME;
                                    ds_btn1[5].Source = "code_0" + menuModels[5].CODE;
                                    ds_btnAction1[5].ClassId = menuModels[5].CODE;
                                    ds_btnAction1[5].Clicked += BHKhac_Clicked;
                                }
                                else if (menuModels.Count > 6)
                                { 
                                    ds_lb1[5].Text = AppResources.SanPhamKhac;
                                    ds_btn1[5].Source = "code_000";
                                    ds_btnAction1[5].ClassId = menuModels[5].CODE;
                                    ds_btnAction1[5].Clicked += BH_Clicked;
                                }



                                //Page.FindByName<SvgCachedImage>("btn6").Source = "iconKhac";
                                //Page.FindByName<Label>("lb6").Text = AppResources.SanPhamKhac;
                                //Page.FindByName<SvgCachedImage>("btn6").Source = "code_201";
                                //Page.FindByName<Label>("lb6").Text = AppResources.Renewal;
                                //
                                int index_taituc_ = 0;
                                //max_ = 5;
                                //if (utilityModels[0].cusUtilityModel.Count < 5)
                                //    max_ = utilityModels[0].cusUtilityModel.Count;
                                //for (int i = 0; i < max_; ++i)
                                //{
                                //    ds_lb2[i].Text = utilityModels[0].cusUtilityModel[i].NAME;
                                //    ds_btn2[i].Source = "code_2" + utilityModels[0].cusUtilityModel[i].CODE;
                                //    ds_btnAction2[i].ClassId = utilityModels[0].cusUtilityModel[i].CODE;
                                //    if (utilityModels[0].cusUtilityModel[i].CODE == "01")
                                //        index_taituc_ = i;
                                //}
                                //Page.FindByName<SvgCachedImage>("btn12").Source = "iconKhac";
                                //Page.FindByName<Label>("lb12").Text = AppResources.TienIchKhac;
                                //
                                max_ = 5;
                                if (utilityModels[0].cusUtilityModel.Count < 5)
                                    max_ = utilityModels[0].cusUtilityModel.Count;
                                //Page.FindByName<SvgCachedImage>("btn12").Source = "iconKhac";
                                //Page.FindByName<Label>("lb12").Text = AppResources.TienIchKhac;
                                ds_lb2.Add(Page.FindByName<Label>("lb12"));
                                ds_btn2.Add(Page.FindByName<SvgCachedImage>("btn12"));


                                for (int i = 0; i < max_; ++i)
                                {
                                    ds_lb2[i].Text = utilityModels[0].cusUtilityModel[i].NAME;
                                    ds_btn2[i].Source = "code_2" + utilityModels[0].cusUtilityModel[i].CODE;
                                    ds_btnAction2[i].ClassId = utilityModels[0].cusUtilityModel[i].CODE;
                                    if (utilityModels[0].cusUtilityModel[i].CODE == "01")
                                        index_taituc_ = i;
                                }
                                if(utilityModels[0].cusUtilityModel.Count==6)
                                {
                                    ds_lb2[5].Text = utilityModels[0].cusUtilityModel[5].NAME;
                                    ds_btn2[5].Source = "code_2" + utilityModels[0].cusUtilityModel[5].CODE;
                                    ds_btnAction2[5].ClassId = utilityModels[0].cusUtilityModel[5].CODE;
                                    if (utilityModels[0].cusUtilityModel[5].CODE == "01")
                                        index_taituc_ = 5;
                                    ds_btnAction2[5].Clicked += TI_Clicked; 
                                }
                                else if (utilityModels[0].cusUtilityModel.Count > 6)
                                {
                                    ds_lb2[5].Text = AppResources.TienIchKhac;
                                    ds_btn2[5].Source = "code_000";
                                    ds_btnAction2[5].ClassId = utilityModels[0].cusUtilityModel[5].CODE;
                                    if (utilityModels[0].cusUtilityModel[5].CODE == "01")
                                        index_taituc_ = 5; 
                                    ds_btnAction2[5].Clicked += TIKhac_Clicked;
                                }

                                //
                                string str_ = utilityModels[0].COUNT.ToString();
                                if (str_.Length > 1)
                                    str_ = "9+";
                                //
                                tempp_ = utilityModels[0].COUNT;
                                //  tempp_ = 8;
                                str_ = "";
                                if (tempp_ > 0)
                                {
                                    //ds_btn_TB[index_taituc_].IsVisible = true;
                                    //ds_lb[index_taituc_].IsVisible = true;
                                    //ds_lb[index_taituc_].Text = str_;
                                    for (int i = 0; i < ds_btn_TB.Count; ++i)
                                    {
                                        if (i != index_taituc_)
                                        {
                                            ds_btn_TB[i].IsVisible = false;
                                            ds_lb[i].IsVisible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < ds_btn_TB.Count; ++i)
                                    {
                                        ds_btn_TB[i].IsVisible = false;
                                        ds_lb[i].IsVisible = false;
                                    }
                                }
                            }    
                            Controls.LoadingUtility.Hide();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Controls.LoadingUtility.Hide();
                        });
                    }
                });
            });
            //
            //using (var conn = new DatabaseContext())
            //{
            //var getLang = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            //if (getLang != null)
            //{
            //    var lstProp = GetType().GetProperties();
            //    var lstLangTxt = this.languages();
            //    var lstTypeName = new Dictionary<string, string>();
            //    if (getLang.Value.ToLower() == "vi")
            //        lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangVi);
            //    if (getLang.Value.ToLower() == "en")
            //        lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangEn);

            //    foreach (var item in lstProp)
            //    {
            //        if (lstTypeName.ContainsKey(item.Name))
            //            item.SetValue(this, lstTypeName[item.Name]);
            //        else
            //        {
            //            var langModel = this.languages().FirstOrDefault(a => a.TypeName.ToLower() == item.Name.ToLower());
            //            if (langModel != null)
            //            {
            //                conn.languageModels.Add(langModel);
            //                conn.SaveChangesAsync();
            //                item.SetValue(this, (getLang.Value.ToLower() == "vi") ? langModel?.LangVi : langModel?.LangEn);
            //            }
            //        }
            //    }
            //}

            //if (FilterViewModel.ListCartedContract != null && FilterViewModel.ListCartedContract.Count > 0 || FilterViewModel.ListInsurContract != null && FilterViewModel.ListInsurContract.Count > 0)
            //{
            //    FilterViewModel.isFilter = false;
            //    FilterViewModel.namePage = string.Empty;
            //    FilterViewModel.ListCartedContract.Clear();
            //    FilterViewModel.ListInsurContract.Clear();
            //}


            //}
        }
        private HttpClient _client;
        private async void LayAPI()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            _client = new HttpClient(clientHandler);
            Uri uri = new Uri(string.Format(Constaint.ServiceAddress + "/" + Constaint.APIurl.getmainmenu + "?accountId=" + 29));

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<BaseModel<HomeMenuModel>>(content);

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    
                }
            });
        }
        private void BHKhac_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new HomeMorePage(true, menuModels, utilityModels[0].cusUtilityModel), false);
        }
        public override void OnAppearing()
        {
            T_Clicked = false;
            base.OnAppearing();
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
                    

                }
            });
        }
        private void TIKhac_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new HomeMorePage(false, menuModels, utilityModels[0].cusUtilityModel), false);
            

            //string linkCayXang = "";
            ////Application.Current.MainPage.Navigation.PushAsync(new TraCuuGCNPage(), false);
            //try
            //{
            //    var result = APIHelper.GetObjectFromAPI<BaseModel<object>>
            //          (Constaint.ServiceAddress, Constaint.APIurl.getlinkgasstation, null);
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        Controls.LoadingUtility.HideAsync();
            //        if (result.data != null)
            //        {
            //            //linkCayXang = object.
            //        }
            //        else
            //        {
            //            Application.Current.MainPage.DisplayAlert("Thông báo", "Không tìm thấy dữ liệu", "OK");
            //        }
            //    });
            //} 
            //catch
            //{

            //}


        }
       

        private async Task GetVAT()
        {
            try
            {
                var taskVAT = CommonValueAPI.GetVATRatio();
                FormTypeModel.VAT = taskVAT;
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif                    
                });
            }
        }

        private async Task GetBanner()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var lstItem = BannerAPI.GetImage("A");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (lstItem != null && lstItem.Count > 0)
                        {
                            //Banner1.Source = lstItem.Where(p => p.Position == 1).FirstOrDefault()?.ImageBase64;
                            Banner2.Source = lstItem.Where(p => p.Position == 2).FirstOrDefault()?.ImageBase64;
                            Carousel_model = new ObservableCollection<CarouselViewHome_model>();
                            Carousel_model.Add(new CarouselViewHome_model
                            {
                                //id = 1,
                                // noiDung = lstItem.Where(p => p.Position == 1).FirstOrDefault()?.ImageBase64,
                            });
                            //Carousel_model.Add(new CarouselViewHome_model
                            //{
                            //    id = 2,
                            //    noiDung = lstItem.Where(p => p.Position == 2).FirstOrDefault()?.ImageBase64,
                            //});
                            CarouselView_Main.ItemsSource = Carousel_model;
                        }
                        Controls.LoadingUtility.HideAsync();
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }

        
        private void TapAccount_Clicked()
        {
            var page = new Views.AccountPage.AccountView("account", UserName, Password);
            //Preferences.Set("ModelAnimation", true);
            //Application.Current.MainPage.Navigation.PushModalAsync(page, false);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void BHOto_Clicked()
        { 
        }

        private void BHXemay_Clicked()
        { 
        }

        private void BHSuckhoe_Clicked()
        { 
        }

        private void BHTunhan_Clicked()
        { 
        }

        private void BHDulich_Clicked()
        { 
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
        private async Task Load_Notify()
        {
            try
            {
                IDAccount = Xamarin.Essentials.SecureStorage.GetAsync(Constaint.UserNoKey).Result;
                var result = NotificationsAPI.CheckNotify(Convert.ToInt32(IDAccount));
                if (result == "true")
                {
                    NEW_BELL = "true";
                    OLD_BELL = "false";
                }
                else
                {
                    NEW_BELL = "false";
                    OLD_BELL = "true";
                }
            }
            catch (Exception ex)
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
                var methodName = "Load_Notify";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);

                await UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
            }
        }
    }
}
