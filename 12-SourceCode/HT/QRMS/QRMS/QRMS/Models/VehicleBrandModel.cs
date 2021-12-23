using QRMS.Helper;

namespace QRMS.Models
{
    public class VehicleBrandModel : Notifiable
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string NAME { get; set; }

        public string SHORT_NAME { get; set; }

        public string STATUS_RECORD { get; set; }

        public int PageTotal { get; set; }
    }
}