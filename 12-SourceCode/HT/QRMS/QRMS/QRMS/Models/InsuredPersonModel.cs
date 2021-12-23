using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class InsuredPersonModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> CONTRACT_ID { get; set; }
        public Nullable<int> CARTED_CONTRACT_ID { get; set; }
        public string NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        public Nullable<System.DateTime> IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public Nullable<int> CUSTTOMER_RELATE_TYPE_ID { get; set; }
        public string CUSTTOMER_RELATE_TYPE_CODE { get; set; }
        public string CUSTTOMER_RELATE_TYPE { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> GENDER_ID { get; set; }
        public string GENDER_NAME { get; set; }
        public string PRODUCT_CODE { get; set; }
        public Nullable<int> PACKAGE_ID { get; set; }
        public string PACKAGE_CODE { get; set; }
        public string PACKAGE_NAME { get; set; }
        public Nullable<decimal> PACKAGE_VALUE { get; set; }
        public string IMAGE_IDCARD_FRONT { get; set; }
        public string IMAGE_IDCARD_BACK { get; set; }
        public Nullable<int> CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_NAME { get; set; }
        public Nullable<decimal> EXCHANGE_RATE_CURRENCY { get; set; }
        public int? NUMBER { get; set; }
        public string STT { get; set; }
    }
}
