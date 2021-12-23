using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class CommonValueModel : Notifiable
    {
        public int ID { get; set; }

        public Nullable<int> COMMON_TYPE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public string STATUS_RECORD { get; set; }

        public string COMMON_TYPE_CODE { get; set; }

        public string TYPE_NAME { get; set; }

        public string VALUE_CODE { get; set; }

        public string VALUE_NAME { get; set; }

        public string VALUE { get; set; }

        public int PageTotal { get; set; }
    }
}