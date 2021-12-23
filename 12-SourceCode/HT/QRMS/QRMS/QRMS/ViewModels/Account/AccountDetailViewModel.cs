using Acr.UserDialogs;
using LIB.Common;
using QRMS.API;
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
    public class AccountDetailViewModel : BaseViewModel
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
        private string _lblFullName;
        public string lblFullName
        {
            get => _lblFullName;
            set
            {
                _lblFullName = value;
                OnPropertyChanged();
            }
        }
        private string _lblPhone;
        public string lblPhone
        {
            get => _lblPhone;
            set
            {
                _lblPhone = value;
                OnPropertyChanged();
            }
        }
        private string _lblIDCard;
        public string lblIDCard
        {
            get => _lblIDCard;
            set
            {
                _lblIDCard = value;
                OnPropertyChanged();
            }
        }
        private string _lblIssueDate;
        public string lblIssueDate
        {
            get => _lblIssueDate;
            set
            {
                _lblIssueDate = value;
                OnPropertyChanged();
            }
        }
        private string _lblIssueLoca;
        public string lblIssueLoca
        {
            get => _lblIssueLoca;
            set
            {
                _lblIssueLoca = value;
                OnPropertyChanged();
            }
        }
        private string _lblGender;
        public string lblGender
        {
            get => _lblGender;
            set
            {
                _lblGender = value;
                OnPropertyChanged();
            }
        }
        private string _lblTaxNo;
        public string lblTaxNo
        {
            get => _lblTaxNo;
            set
            {
                _lblTaxNo = value;
                OnPropertyChanged();
            }
        }
        private string _lblInsuranceAgent;
        public string lblInsuranceAgent
        {
            get => _lblInsuranceAgent;
            set
            {
                _lblInsuranceAgent = value;
                OnPropertyChanged();
            }
        }
        private string _lblDateOB;
        public string lblDateOB
        {
            get => _lblDateOB;
            set
            {
                _lblDateOB = value;
                OnPropertyChanged();
            }
        }
        private string _lblAddress;
        public string lblAddress
        {
            get => _lblAddress;
            set
            {
                _lblAddress = value;
                OnPropertyChanged();
            }
        }
        private string _lblEmail;
        public string lblEmail
        {
            get => _lblEmail;
            set
            {
                _lblEmail = value;
                OnPropertyChanged();
            }
        }
        private string _lblCustomer;
        public string lblCustomer
        {
            get => _lblCustomer;
            set
            {
                _lblCustomer = value;
                OnPropertyChanged();
            }
        }

        private string _FullName;
        public string FullName
        {
            get => _FullName;
            set
            {
                _FullName = value;
                OnPropertyChanged();
            }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string _IDCard;
        public string IDCard
        {
            get => _IDCard;
            set
            {
                _IDCard = value;
                OnPropertyChanged();
            }
        }
        private string _IssueDate;
        public string IssueDate
        {
            get => _IssueDate;
            set
            {
                _IssueDate = value;
                OnPropertyChanged();
            }
        }
        private string _IssueLoca;
        public string IssueLoca
        {
            get => _IssueLoca;
            set
            {
                _IssueLoca = value;
                OnPropertyChanged();
            }
        }
        private string _Gender;
        public string Gender
        {
            get => _Gender;
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }
        private string _TaxNo;
        public string TaxNo
        {
            get => _TaxNo;
            set
            {
                _TaxNo = value;
                OnPropertyChanged();
            }
        }
        private string _InsuranceAgent;
        public string InsuranceAgent
        {
            get => _InsuranceAgent;
            set
            {
                _InsuranceAgent = value;
                OnPropertyChanged();
            }
        }
        private string _DateOB;
        public string DateOB
        {
            get => _DateOB;
            set
            {
                _DateOB = value;
                OnPropertyChanged();
            }
        }
        private string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }
        private string _Customer;
        public string Customer
        {
            get => _Customer;
            set
            {
                _Customer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region helper
        public IEnumerable<LanguageModel> languages()
        {
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblBack", LangVi = "〈 Quay lại", LangEn = "Back", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblFullName", LangVi = "Họ và tên", LangEn = "Full name", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "Ad01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblPhone", LangVi = "Số điện thoại", LangEn = "Phone number", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblIDCard", LangVi = "Số CMT/CCCD", LangEn = "ID Card", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblIssueDate", LangVi = "Ngày cấp", LangEn = "Issue Date", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblIssueLoca", LangVi = "Nơi cấp", LangEn = "Issue Location", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblGender", LangVi = "Giới tính", LangEn = "Gender", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblTaxNo", LangVi = "Mã số thuế", LangEn = "Tax No", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblInsuranceAgent", LangVi = "Mã đại lý", LangEn = "Agent No", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblDateOB", LangVi = "Ngày sinh", LangEn = "Date of birth", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblAddress", LangVi = "Địa chỉ", LangEn = "Address", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblEmail", LangVi = "Email", LangEn = "Email", Remark = "" };
            yield return new LanguageModel() { ScreenCode = "AD01", ScreenName = "AccountDetail", ItemType = "Label", TypeName = "lblCustomer", LangVi = "Cán bộ/Tổ chức quản lý", LangEn = "Officers/Management organization", Remark = "" };
        }
        #endregion

        #region main
        private DateTime? start = null;
        private DateTime? end = null;
        private TimeSpan? time = null;

        public AccountDetailViewModel(string IdAccount, ContentPage page)
        {
            Page = page;
            IDAccount = IdAccount;
            //var callAccount = LoadForm();
            conn = new DatabaseContext();

            var getLang = conn.settingModels.Where(a => a.Key.Equals("Language")).FirstOrDefault();
            if (getLang != null)
            {
                var lstProp = GetType().GetProperties();
                //var lstLangTxt = conn.languageModels.Where(a => a.ScreenCode.Equals("AD01")).ToList();
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
            BackCommand = new Command(Back_Clicked);

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            CallThread();

        }
       // public string FullName { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
       // public string Email { get; set; }
        public string SDT { get; set; }
        public string SCMT { get; set; }
        public string NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string TinhThanh { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public string DiaChi { get; set; } 
        private async void CallThread()
        {
            try
            {
                await Task.Delay(1000);
                Controls.LoadingUtility.Show();
                CusCustomerInfoModel result = new CusCustomerInfoModel();
                await Task.Run(async () =>
                {
                    result = AccountAPI.GetAccountByID(id: IDAccount);
                }).ContinueWith(a =>
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (result != null)
                    {
                        FullName = result.FULL_NAME;
                        NgaySinh = result.DOB == null ? DateTime.Now.ToString("dd/MM/yyyy") : result.DOB.Value.Date.ToString("dd/MM/yyyy");
                        GioiTinh = result.GENDER_NAME;
                        Email = result.EMAIL;
                        SDT = result.MOBILE;
                        SCMT = result.IDENTIFY_NO;
                        NgayCap = result.IDENTITY_ISSUE_DATE == null ? DateTime.Now.ToString("dd/MM/yyyy") : result.IDENTITY_ISSUE_DATE.Value.Date.ToString("dd/MM/yyyy");

                        NoiCap = result.IDENTITY_ISSUE_OFFICE;
                        TinhThanh = result.PROVINCE_NAME;
                        QuanHuyen = result.DISTRICT_NAME;
                        PhuongXa = result.WARD_NAME;
                        DiaChi = result.ADDRESS;
                    }
                    Controls.LoadingUtility.Hide();
                }));
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
                var appType = QRMS.AppLIB.Common.Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var namespaceInFile = GetType().Namespace;
                var className = GetType().Name;
                var methodName = "CallThread";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);

                Controls.LoadingUtility.Hide();
                await UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK").ConfigureAwait(false);
            }
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
    }
}
