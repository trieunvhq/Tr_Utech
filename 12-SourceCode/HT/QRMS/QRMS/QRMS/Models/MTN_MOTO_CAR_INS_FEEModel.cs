using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class MTN_MOTO_CAR_INS_FEEModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string INS_MONEY_MAXCOMMON_TYPE_NAME { get; set; }
        public Nullable<int> COMMON_ID { get; set; }
        public string COMMON_CODE { get; set; }
        public string COMMON_NAME { get; set; }
        public string MTN_RANK_NAME { get; set; }
        public Nullable<decimal> INS_MONEY_PERSON { get; set; }
        public Nullable<decimal> INS_MONEY_ASSET { get; set; }
        public Nullable<decimal> INS_MONEY_MAX { get; set; }
        public string REMARK { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<decimal> INS_MONEY_LOAD { get; set; }
        public string sINS_MONEY_LOAD { get; set; }
        public string DISPLAY_NAME { get; set; }
    }
}