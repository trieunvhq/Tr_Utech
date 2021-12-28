using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class ASSEST_CAR_INS_FEEModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string INTENT_BUSINESS_CODE { get; set; }
        public string INTENT_BUSINESS_NAME { get; set; }
        public Nullable<int> COMMON_ID { get; set; }
        public string COMMON_CODE { get; set; }
        public string COMMON_NAME { get; set; }
        public Nullable<decimal> INS_MONEY_F { get; set; }
        public Nullable<decimal> INS_MONEY_T { get; set; }
        public Nullable<int> YEARS_USED_F { get; set; }
        public Nullable<int> YEARS_USED_T { get; set; }
        public Nullable<decimal> PERCENT_FEE { get; set; }
        public Nullable<decimal> DEDUCTIBLE_RANK { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
    }
}