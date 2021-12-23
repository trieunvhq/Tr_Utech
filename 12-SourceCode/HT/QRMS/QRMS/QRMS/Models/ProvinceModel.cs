using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class ProvinceModel : Notifiable
    {
        public int ID { get; set; }

        public Nullable<int> COUNTRY_ID { get; set; }

        public string COUNTRY_CODE { get; set; }

        public string NAME { get; set; }

        public string CODE { get; set; }

        public string STATUS_RECORD { get; set; }

        public int PageTotal { get; set; }
    }
}