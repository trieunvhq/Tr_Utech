using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class TNVNInsFeeModel
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string INTENT_BUSINESS_CODE { get; set; }
        public Nullable<int> COMMON_ID { get; set; }
        public string COMMON_CODE { get; set; }
        public Nullable<decimal> INS_MONEY_F { get; set; }
        public Nullable<decimal> INS_MONEY_T { get; set; }
        public Nullable<decimal> TNDSBB_FEE { get; set; }
        public Nullable<decimal> PERCENT_FEE { get; set; }
        public Nullable<decimal> VAT { get; set; }
    }
}
