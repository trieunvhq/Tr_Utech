using Newtonsoft.Json;
using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public partial class MATERIAL_DKBS_INS_FEEModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string COMMON_TYPE_NAME { get; set; }
        public string DKBS_CODE { get; set; }
        public string DKBS_NAME { get; set; }
        public Nullable<decimal> PERCEN_FEE { get; set; }
        public Nullable<decimal> FEE { get; set; }
        public Nullable<decimal> DEDUCTIBLE_RANK { get; set; }
        public string REMARK { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<decimal> INS_MONEY_LOAD { get; set; }
    }

    public partial class MATERIAL_DKBS_INS_FEEModel
    {
        [JsonIgnore]
        public bool IsChecked { get; set; }
        [JsonIgnore]
        public bool IsEnabled { get; set; } = true;
    }
}