using Newtonsoft.Json;
using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public partial class CartedContractDKBSModel : Notifiable
    {
        public int ID { get; set; }
        public Nullable<int> CARTED_CONTRACT_ID { get; set; }
        public Nullable<int> MATERIAL_DKBS_INS_FEE_ID { get; set; }
        public string DKBS_CODE { get; set; }
        public string DKBS_NAME { get; set; }
        public Nullable<decimal> PERCEN_FEE { get; set; }
        public Nullable<decimal> FEE { get; set; }
        public Nullable<decimal> DEDUCTIBLE_RANK { get; set; }
        public Nullable<decimal> FEE_VALUE { get; set; }
        public string REMARK { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<decimal> INC_DESC_FEE_RATIO { get; set; }
        public Nullable<decimal> INC_DESC_FEE { get; set; }
    }
}