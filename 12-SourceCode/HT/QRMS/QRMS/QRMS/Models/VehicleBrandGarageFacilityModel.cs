using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class VehicleBrandGarageFacilityModel
    {
        public decimal ID { get; set; }
        public int VEHICLE_BRAND_ID { get; set; }
        public string VEHICLE_BRAND_NAME { get; set; }
        public string GARAGE_FACILITY_NAME { get; set; }
        public string GARAGE_FACILITY_ADDRESS { get; set; }
        public string GARAGE_FACILITY_TEL { get; set; }
        public int PageTotal { get; set; }
    }
}
