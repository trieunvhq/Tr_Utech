using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class CurrencyModel : Notifiable
    {
        public int ID { get; set; }
        public string CURR_CODE { get; set; }
        public string CURR_NAME { get; set; }
        public string CURR_SHORT_NAME { get; set; }
        public string CURR_UNIT_NAME { get; set; }
        public string REMARK { get; set; }
        public decimal EXCHANGE_RATE { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
    }
}
