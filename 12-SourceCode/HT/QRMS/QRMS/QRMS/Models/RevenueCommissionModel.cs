using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class RevenueCommissionModel
    {
        public Nullable<int> INSUR_PRODUCT_ID { get; set; }
        public string INSUR_PRODUCT_CODE { get; set; }
        public string INSUR_PRODUCT_NAME { get; set; }
        public Nullable<decimal> TOTAL_REVENUE { get; set; }
        public Nullable<decimal> TOTAL_COMMISSION { get; set; }
        public Nullable<int> CONTRACT_QUANTITY { get; set; }
        public Nullable<decimal> PERCENT { get; set; }
    }
}
