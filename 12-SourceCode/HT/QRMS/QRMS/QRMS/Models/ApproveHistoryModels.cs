using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class ApproveHistoryModels
    {
        public int ID { get; set; }
        public int? SUB_USER_ID { get; set; }
        public int? SUB_AGENT_ID { get; set; }
        public DateTime? SUB_DATE { get; set; }
        public int? SUB_CONTRACT_ID { get; set; }
        public int? SUB_CART_CONTRACT_ID { get; set; }
        public int? CONTRACT_STATUS { get; set; }
        public int? APPROVE_USER_ID { get; set; }
        public int? APPROVE_DIVN_ID { get; set; }
        public int? APPROVE_DEPART_ID { get; set; }
        public DateTime? APPROVE_DATE { get; set; }
        public string REMARK { get; set; }
        public string SUB_MSG { get; set; }
        public string FEEDBACK_MSG { get; set; }
        public string CONTRACT_ISSUE_TYPE { get; set; }
    }
}
