using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class FreightInsFeeModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string COMMON_TYPE_NAME { get; set; }
        public Nullable<int> COMMON_ID { get; set; }
        public string COMMON_CODE { get; set; }
        public string COMMON_NAME { get; set; }
        public Nullable<decimal> PERCEN_FEE { get; set; }
        public Nullable<decimal> DEDUCTIBLE_PERCEN { get; set; }
        public Nullable<decimal> MIN_VALUE { get; set; }
        public Nullable<decimal> MAX_VALUE { get; set; }
        public string REMARK { get; set; }
        public Nullable<decimal> VAT { get; set; }
    }
}