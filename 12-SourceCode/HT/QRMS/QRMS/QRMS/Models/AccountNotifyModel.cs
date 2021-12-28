using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class AccountNotifyModel
    {
        //public AccountNotifyModel()
        //{
        //    List_Professional = new List<ListProfessionalNotifyModel>();
        //    List_System = new List<ListSystemNotifyModel>();
        //}

        //public List<ListProfessionalNotifyModel> List_Professional { get; set; }
        //public List<ListSystemNotifyModel> List_System { get; set; }
        public decimal ID { get; set; }
        public decimal NOTIFY_ID { get; set; }
        public decimal ACCOUNT_ID { get; set; }
        public decimal NOTIFY_TYPE { get; set; }
        public Nullable<decimal> NEWS_ID { get; set; }
        public string NEWS_CODE { get; set; }
        public string NEWS_NAME { get; set; }
        public Nullable<decimal> CONTRACT_ID { get; set; }
        public string CONTRACT_CODE { get; set; }
        public string CONTRACT_TYPE { get; set; }
        public string STATUS_CONTRACT { get; set; }
        public string NOTIFY { get; set; }
        public string IMAGE { get; set; }
        public Nullable<System.DateTime> PUBLIC_DATE { get; set; }
        public string SEND { get; set; }
        public string VIEWED { get; set; }
        public string REMARK { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<decimal> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<decimal> UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<short> SENT_COUNT { get; set; }
        public string CONTRACT_ISSUE_TYPE { get; set; }
        public string INSUR_PRODUCT_CODE { get; set; }
        public int? AGENT_ID { get; set; }
    }
}
