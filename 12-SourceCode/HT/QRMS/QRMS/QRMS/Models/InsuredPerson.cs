using QRMS.Helper;
using QRMS.AppLIB;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class InsuredPerson : Notifiable
    {
        #region hien thi
        public int? ListViewIndex { get; set; }
        #endregion


        public int ID { get; set; }
        public int? CONTRACT_ID { get; set; }
        public int? CARTED_CONTRACT_ID { get; set; }
        public string NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        public DateTime? IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public DateTime? DOB { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public int? COUNTRY_ID { get; set; }
        public int? PROVINCE_ID { get; set; }
        public int? DISTRICT_ID { get; set; }
        public int? WARD_ID { get; set; }
        public int? CUSTTOMER_RELATE_TYPE_ID { get; set; }
        public string CUSTTOMER_RELATE_TYPE_CODE { get; set; }
        public string CUSTTOMER_RELATE_TYPE { get; set; }
        public string STATUS_RECORD { get; set; }
        public int? GENDER_ID { get; set; }
        public string GENDER_NAME { get; set; }
        public string PRODUCT_CODE { get; set; }
        public int? PACKAGE_ID { get; set; }
        public string PACKAGE_CODE { get; set; }
        public string PACKAGE_NAME { get; set; }
        public decimal? PACKAGE_VALUE { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public int? UPDATE_USER_ID { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public string IMAGE_IDCARD_FRONT { get; set; }
        public string IMAGE_IDCARD_BACK { get; set; }
        public int? CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_NAME { get; set; }
        public decimal? EXCHANGE_RATE_CURRENCY { get; set; }
        public decimal? PERSON_PRICE { get; set; }
        public Nullable<decimal> LUGGAGE { get; set; }
        public string STT { get; set; }
    }
}
