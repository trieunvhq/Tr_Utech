using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QRMS.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        #region bien
        public DatabaseContext conn;
        private Tab tabHome;
        private Tab tabHistory;
        private Tab tabApproval;
        private Tab tabRenewals;
        private Tab tabDraft;
        private ContentPage Page;
        #endregion

        #region hienthi
        private string _lblHomePage;
        public string lblHomePage
        {
            get => _lblHomePage;
            set
            {
                _lblHomePage = value;
                OnPropertyChanged();
            }
        }
         
        private string _lblApprovalPage;
        public string lblApprovalPage
        {
            get => _lblApprovalPage;
            set
            {
                _lblApprovalPage = value;
                OnPropertyChanged();
            }
        }

        private string _lblTinTucPage;
        public string lblTinTucPage
        {
            get => _lblTinTucPage;
            set
            {
                _lblTinTucPage = value;
                OnPropertyChanged();
            }
        }

        private string _lblThongBaoPage;
        public string lblThongBaoPage
        {
            get => _lblThongBaoPage;
            set
            {
                _lblThongBaoPage = value;
                OnPropertyChanged();
            }
        }

        private string _lblTaiKhoanPage;
        public string lblTaiKhoanPage
        {
            get => _lblTaiKhoanPage;
            set
            {
                _lblTaiKhoanPage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region helper
        public IEnumerable<LanguageModel> languages()
        {
            yield return new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblHomePage", LangVi = "Trang chủ", LangEn = "Home", Remark = "Trang chủ" }; 
            yield return new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblTinTucPage", LangVi = "Tin tức", LangEn = "News", Remark = "Tin tức" };
            yield return new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblThongBaoPage", LangVi = "Thông báo", LangEn = "Notify", Remark = "Thông báo" };
            yield return new LanguageModel() { ScreenCode = "Shell", ScreenName = "Shell", ItemType = "Label", TypeName = "lblTaiKhoanPage", LangVi = "Tài khoản", LangEn = "Account", Remark = "Tài khoản" };
        }
        #endregion

        public AppShellViewModel()
        {
            conn = new DatabaseContext();
            var getLang = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            if (getLang != null)
            {
                var lstProp = GetType().GetProperties();
                var lstLangTxt = this.languages();
                var lstTypeName = new Dictionary<string, string>();
                if (getLang.Value.ToLower() == "vi")
                    lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangVi);
                if (getLang.Value.ToLower() == "en")
                    lstTypeName = lstLangTxt.ToDictionary(a => a.TypeName, a => a.LangEn);

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
        }
    }
}
