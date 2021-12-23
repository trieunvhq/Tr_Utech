using Acr.UserDialogs;
using Microsoft.EntityFrameworkCore;
using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class AccountLanguageViewModel : BaseViewModel
    {
        private static string exdisconnect = "An exception occurred during the operation, making the result invalid";

        #region biến
        public DatabaseContext conn;
        private ContentPage Page;
        //public ObservableCollection<ListContractModel> ListContract { get; set; }
        public Command FilterCommand { get; }
        //public Command lblInsuranceTypeCommand { get; }
        private int pageNumber { get; set; }
        public Command BackCommand { get; }

        public ViewCell TiengvietViewcell;
        public ViewCell EnglishViewcell;
        public Image IconViet;
        public Image IconEng;
        #endregion

        #region hiển thị
        private string IDAccount;
        public string lblAccount;
        private string _lblBack;
        public string lblBack
        {
            get => _lblBack;
            set
            {
                _lblBack = value;
                OnPropertyChanged();
            }
        }
        private string _lblTiengviet;
        public string lblTiengviet
        {
            get => _lblTiengviet;
            set
            {
                _lblTiengviet = value;
                OnPropertyChanged();
            }
        }
        private string _lblEnglish;
        public string lblEnglish
        {
            get => _lblEnglish;
            set
            {
                _lblEnglish = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region helper
        public IEnumerable<LanguageModel> languages()
        {
            yield return new LanguageModel() { ScreenCode = "LG01", ScreenName = "AccountLanguage", ItemType = "Label", TypeName = "lblBack", LangVi = "〈 Quay lại", LangEn = "〈 Back", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "LG01", ScreenName = "AccountLanguage", ItemType = "Label", TypeName = "lblTiengviet", LangVi = "Tiếng Việt", LangEn = "Tiếng Việt", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "LG01", ScreenName = "AccountLanguage", ItemType = "Label", TypeName = "lblEnglish", LangVi = "English", LangEn = "English", Remark = "" };
        }
        #endregion

        #region main
        private DateTime? start = null;
        private DateTime? end = null;
        private TimeSpan? time = null;

        public AccountLanguageViewModel(ContentPage page)
        {
            //start = DateTime.Now;
            //DependencyService.Get<ILogger>().Log("Start_Load: " + start.ToString());
            Page = page;
            //var callAccount = LoadForm();
            conn = new DatabaseContext();

            var getLang = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            if (getLang != null)
            {
                var lstProp = GetType().GetProperties();
                //var lstLangTxt = conn.languageModels.Where(a => a.ScreenCode.Equals("LG01")).ToList();
                var lstLangTxt = this.languages();
                var lstTypeName = new Dictionary<string, string>();
                if (getLang.Value.ToLower() == "vi")
                {
                    lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangVi);
                    IconViet = Page.FindByName<Image>("IconViet");
                    IconViet.IsVisible = true;
                    IconEng = Page.FindByName<Image>("IconEng");
                    IconEng.IsVisible = false;
                }    
                if (getLang.Value.ToLower() == "en")
                {
                    lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangEn);
                    IconViet = Page.FindByName<Image>("IconViet");
                    IconViet.IsVisible = false;
                    IconEng = Page.FindByName<Image>("IconEng");
                    IconEng.IsVisible = true;
                }   

                foreach (var item in lstProp)
                {
                    if (lstTypeName.ContainsKey(item.Name))
                        item.SetValue(this, lstTypeName[item.Name]);
                    else
                    {
                        var langModel = this.languages().FirstOrDefault(a => a.TypeName.ToLower() == item.Name.ToLower());
                        if (langModel != null)
                        {
                            conn.languageModels.Add(langModel);
                            conn.SaveChangesAsync();
                            item.SetValue(this, (getLang.Value.ToLower() == "vi") ? langModel?.LangVi : langModel?.LangEn);
                        }
                    }
                }
            }
            BackCommand = new Command(Back_Clicked);

            TiengvietViewcell = Page.FindByName<ViewCell>("TiengvietViewcell");
            TiengvietViewcell.Tapped += TiengvietViewcell_Tapped; ;

            EnglishViewcell = Page.FindByName<ViewCell>("EnglishViewcell");
            EnglishViewcell.Tapped += EnglishViewcell_Tapped;
        }

        private void EnglishViewcell_Tapped(object sender, EventArgs e)
        {
            var i = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            if (i != null)
            {
                i.Value = "En";
                conn.Entry(i).State = EntityState.Modified;
                conn.SaveChanges();
            }
            else
            {
                List<SettingModel> lstSetting = new List<SettingModel>()
                {
                    //new SettingModel() {Key = "Account" },
                    new SettingModel() {Key = "Language", Value = "En" },
                };

                conn.settingModels.AddRange(lstSetting);
                conn.SaveChanges();
            }
            Resources.AppResources.Culture = new System.Globalization.CultureInfo("en");
            Application.Current.MainPage = new QRMS.Views.LoginPage.LoginView();
        }

        private void TiengvietViewcell_Tapped(object sender, EventArgs e)
        {
            var i = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            if (i != null)
            {
                i.Value = "Vi";
                conn.Entry(i).State = EntityState.Modified;
                conn.SaveChanges();
            }
            else
            {
                List<SettingModel> lstSetting = new List<SettingModel>()
                {
                    //new SettingModel() {Key = "Account" },
                    new SettingModel() {Key = "Language", Value = "Vi" },
                };

                conn.settingModels.AddRange(lstSetting);
                conn.SaveChanges();
            }
            Resources.AppResources.Culture = new System.Globalization.CultureInfo("vi");
            Application.Current.MainPage = new QRMS.Views.LoginPage.LoginView();
        }

        private void Back_Clicked(object obj)
        {
            //Page.TranslateTo(300, 0, 500);

            //Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            //{
            //    // Do something

            //    Application.Current.MainPage.Navigation.PopModalAsync(false);

            //    return false; // True = Repeat again, False = Stop the timer
            //});
            Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        #region Loading
        private async Task LoadForm()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(exdisconnect))
                {
                    await Application.Current.MainPage.DisplayAlert("Thông báo", "Lỗi kết nối Service", "OK");
                }
                DependencyService.Get<ILogger>().Log(ex.ToString());
            }
            finally
            {
                UserDialogs.Instance.HideLoading();

                end = DateTime.Now;
                DependencyService.Get<ILogger>().Log("End_Load: " + end.ToString());

                time = end - start;
                DependencyService.Get<ILogger>().Log("Time_Load: " + time.ToString());
            }
        }
        #endregion

    }
}
