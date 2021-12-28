using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class DistrictModel : Notifiable
    {
        public int ID { get; set; }

        public Nullable<int> PROVINCE_ID { get; set; }

        public string PROVINCE_CODE { get; set; }

        public string CODE { get; set; }

        public string NAME { get; set; }

        public string SHORT_NAME { get; set; }

        public string STATUS_RECORD { get; set; }

        public int PageTotal { get; set; }
    }
}