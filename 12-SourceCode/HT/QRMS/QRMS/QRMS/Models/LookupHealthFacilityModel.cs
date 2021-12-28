using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class LookupHealthFacilityModel: Notifiable
    {
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int PageTotal { get; set; }
    }
}
