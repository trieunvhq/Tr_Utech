using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class ApprovalLimit
    {
        public int ID { get; set; }
        public int? GROUP_ID { get; set; }
        public int? PRODUCT_ID { get; set; }
        public decimal? LMT_VALUE { get; set; }
        public decimal? FEE_INCREASE_RATIO_F { get; set; }
        public decimal? FEE_INCREASE_RATIO_T { get; set; }
        public decimal? FEE_DECREASE_RATIO_F { get; set; }
        public decimal? FEE_DECREASE_RATIO_T { get; set; }
        public DateTime? ACTIVE_DATE_F { get; set; }
        public DateTime? ACTIVE_DATE_T { get; set; }
    }
}
