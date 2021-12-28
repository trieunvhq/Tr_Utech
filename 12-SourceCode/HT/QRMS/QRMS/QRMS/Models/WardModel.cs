using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class WardModel : Notifiable
    {
        public int ID { get; set; }

        public Nullable<int> DISTRICT_ID { get; set; }

        public string DISTRICT_CODE { get; set; }

        public string CODE { get; set; }

        public string NAME { get; set; }

        public string SHORT_NAME { get; set; }

        public string STATUS_RECORD { get; set; }

        public int PageTotal { get; set; }
    }
}