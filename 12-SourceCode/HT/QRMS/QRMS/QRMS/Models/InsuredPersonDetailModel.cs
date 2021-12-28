using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using QRMS.Helper;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class InsuredPersonDetailModel:Notifiable
    {
        public GridLength row1 { get; set; }
        public  bool isVisible_row1 { get; set; }
        public string stringFormat { get; set; }
        public string sINSURED_PERSON_PACKAGE_VALUE { get; set; }

        public int INSURED_PERSON_ID { get; set; }
        public string INSURED_PERSON_NAME { get; set; }
        public string INSURED_PERSON_IDENTITY_NO { get; set; }
        public DateTime? INSURED_PERSON_IDENTITY_ISSUE_DATE { get; set; }
        public string INSURED_PERSON_IDENTITY_ISSUE_OFFICE { get; set; }
        public string INSURED_PERSON_NATIONALITY { get; set; }
        public DateTime? INSURED_PERSON_DOB { get; set; }
        public string INSURED_PERSON_PHONE { get; set; }
        public string INSURED_PERSON_EMAIL { get; set; }
        public string INSURED_PERSON_ADDRESS { get; set; }
        public int? INSURED_PERSON_COUNTRY_ID { get; set; }
        public int? INSURED_PERSON_PROVINCE_ID { get; set; }
        public int? INSURED_PERSON_DISTRICT_ID { get; set; }
        public int? INSURED_PERSON_WARD_ID { get; set; }
        public string INSURED_PERSON_COUNTRY_CODE { get; set; }
        public string INSURED_PERSON_PROVINCE_CODE { get; set; }
        public string INSURED_PERSON_DISTRICT_CODE { get; set; }
        public string INSURED_PERSON_WARD_CODE { get; set; }
        public string INSURED_PERSON_COUNTRY_NAME { get; set; }
        public string INSURED_PERSON_PROVINCE_NAME { get; set; }
        public string INSURED_PERSON_DISTRICT_NAME { get; set; }
        public string INSURED_PERSON_WARD_NAME { get; set; }
        public int? INSURED_PERSON_CUSTTOMER_RELATE_TYPE_ID { get; set; }
        public string INSURED_PERSON_CUSTTOMER_RELATE_TYPE { get; set; }
        public int? INSURED_PERSON_GENDER_ID { get; set; }
        public string INSURED_PERSON_GENDER_NAME { get; set; }
        public string INSURED_PERSON_PRODUCT_CODE { get; set; }
        public int? INSURED_PERSON_PACKAGE_ID { get; set; }
        public string INSURED_PERSON_PACKAGE_CODE { get; set; }
        public string INSURED_PERSON_PACKAGE_NAME { get; set; }
        public decimal? INSURED_PERSON_PACKAGE_VALUE { get; set; }
        public string INSURED_PERSON_CUSTTOMER_RELATE_TYPE_CODE { get; set; }
        public int? INSURED_PERSON_CURRENCY_ID { get; set; }
        public string INSURED_PERSON_CURRENCY_CODE { get; set; }
        public string INSURED_PERSON_CURRENCY_NAME { get; set; }
        public decimal? INSURED_PERSON_EXCHANGE_RATE_CURRENCY { get; set; }


        public InsuredPersonDetailAlterModel AlterInfo { get; set; } = new InsuredPersonDetailAlterModel();
    }


    public class InsuredPersonDetailAlterModel : Notifiable
    {
        public bool INSURED_PERSON_ID { get; set; } = false;
        public bool INSURED_PERSON_NAME { get; set; } = false;
        public bool INSURED_PERSON_IDENTITY_NO { get; set; } = false;
        public bool INSURED_PERSON_IDENTITY_ISSUE_DATE { get; set; } = false;
        public bool INSURED_PERSON_IDENTITY_ISSUE_OFFICE { get; set; } = false;
        public bool INSURED_PERSON_NATIONALITY { get; set; } = false;
        public bool INSURED_PERSON_DOB { get; set; } = false;
        public bool INSURED_PERSON_PHONE { get; set; } = false;
        public bool INSURED_PERSON_EMAIL { get; set; } = false;
        public bool INSURED_PERSON_ADDRESS { get; set; } = false;
        public bool INSURED_PERSON_COUNTRY_NAME { get; set; } = false;
        public bool INSURED_PERSON_PROVINCE_NAME { get; set; } = false;
        public bool INSURED_PERSON_DISTRICT_NAME { get; set; } = false;
        public bool INSURED_PERSON_WARD_NAME { get; set; } = false;
        public bool INSURED_PERSON_CUSTTOMER_RELATE_TYPE_ID { get; set; } = false;
        public bool INSURED_PERSON_CUSTTOMER_RELATE_TYPE { get; set; } = false;
        public bool INSURED_PERSON_GENDER_ID { get; set; } = false;
        public bool INSURED_PERSON_GENDER_NAME { get; set; } = false;
        public bool INSURED_PERSON_PRODUCT_CODE { get; set; } = false;
        public bool INSURED_PERSON_PACKAGE_ID { get; set; } = false;
        public bool INSURED_PERSON_PACKAGE_CODE { get; set; } = false;
        public bool INSURED_PERSON_PACKAGE_NAME { get; set; } = false;
        public bool INSURED_PERSON_PACKAGE_VALUE { get; set; } = false;
        public bool INSURED_PERSON_CUSTTOMER_RELATE_TYPE_CODE { get; set; } = false;
        public bool INSURED_PERSON_CURRENCY_ID { get; set; } = false;
        public bool INSURED_PERSON_CURRENCY_CODE { get; set; } = false;
        public bool INSURED_PERSON_CURRENCY_NAME { get; set; } = false;
        public bool INSURED_PERSON_EXCHANGE_RATE_CURRENCY { get; set; } = false;

        public Color INSURED_PERSON_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_ID); } }
        public Color INSURED_PERSON_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_NAME); } }
        public Color INSURED_PERSON_IDENTITY_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_IDENTITY_NO); } }
        public Color INSURED_PERSON_IDENTITY_ISSUE_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_IDENTITY_ISSUE_DATE); } }
        public Color INSURED_PERSON_IDENTITY_ISSUE_OFFICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_IDENTITY_ISSUE_OFFICE); } }
        public Color INSURED_PERSON_NATIONALITY_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_NATIONALITY); } }
        public Color INSURED_PERSON_DOB_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_DOB); } }
        public Color INSURED_PERSON_PHONE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PHONE); } }
        public Color INSURED_PERSON_EMAIL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_EMAIL); } }
        public Color INSURED_PERSON_ADDRESS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_ADDRESS); } }
        public Color INSURED_PERSON_COUNTRY_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_COUNTRY_NAME); } }
        public Color INSURED_PERSON_PROVINCE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PROVINCE_NAME); } }
        public Color INSURED_PERSON_DISTRICT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_DISTRICT_NAME); } }
        public Color INSURED_PERSON_WARD_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_WARD_NAME); } }
        public Color INSURED_PERSON_CUSTTOMER_RELATE_TYPE_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CUSTTOMER_RELATE_TYPE_ID); } }
        public Color INSURED_PERSON_CUSTTOMER_RELATE_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CUSTTOMER_RELATE_TYPE); } }
        public Color INSURED_PERSON_GENDER_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_GENDER_ID); } }
        public Color INSURED_PERSON_GENDER_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_GENDER_NAME); } }
        public Color INSURED_PERSON_PRODUCT_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PRODUCT_CODE); } }
        public Color INSURED_PERSON_PACKAGE_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PACKAGE_ID); } }
        public Color INSURED_PERSON_PACKAGE_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PACKAGE_CODE); } }
        public Color INSURED_PERSON_PACKAGE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PACKAGE_NAME); } }
        public Color INSURED_PERSON_PACKAGE_VALUE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_PACKAGE_VALUE); } }
        public Color INSURED_PERSON_CUSTTOMER_RELATE_TYPE_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CUSTTOMER_RELATE_TYPE_CODE); } }
        public Color INSURED_PERSON_CURRENCY_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CURRENCY_ID); } }
        public Color INSURED_PERSON_CURRENCY_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CURRENCY_CODE); } }
        public Color INSURED_PERSON_CURRENCY_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_CURRENCY_NAME); } }
        public Color INSURED_PERSON_EXCHANGE_RATE_CURRENCY_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_PERSON_EXCHANGE_RATE_CURRENCY); } }

        #region constructor
        public InsuredPersonDetailAlterModel() : base() { }
        public InsuredPersonDetailAlterModel(InsuredPersonDetailModel oldContract, InsuredPersonDetailModel newContract)
        {
            try
            {
                PropertyInfo[] lstProp = typeof(InsuredPersonDetailAlterModel).GetProperties();
                for (int i = 0; i < lstProp.Length; i++)
                {
                    if (lstProp[i].SetMethod == null) { continue; }
                    var _oldValue = oldContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(oldContract);
                    var _newValue = newContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(newContract);

                    var isChange = _oldValue?.ToString() != _newValue?.ToString();
                    lstProp[i].SetValue(this, isChange);
                }
            }
            catch { }
        }
        #endregion
    }
}
