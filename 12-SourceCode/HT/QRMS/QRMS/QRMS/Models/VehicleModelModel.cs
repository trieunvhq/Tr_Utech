using QRMS.Helper;
using System;

namespace QRMS.Models
{
    public class VehicleModelModel : Notifiable
    {
        public int ID { get; set; }

        public Nullable<int> BRAND_ID { get; set; }

        public string CODE { get; set; }

        public string NAME { get; set; }

        public Nullable<int> VEHICLE_TYPE { get; set; }

        public Nullable<decimal> PRICE { get; set; }

        public string SHORT_NAME { get; set; }

        public string STATUS_RECORD { get; set; }

        public int PageTotal { get; set; }
    }
}