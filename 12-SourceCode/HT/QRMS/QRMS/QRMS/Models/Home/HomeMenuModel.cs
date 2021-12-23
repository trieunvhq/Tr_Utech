using System;
using System.Collections.Generic;

namespace QRMS.Models.Home
{
    public class HomeMenuModel
    {
        public int NotifyCount { get; set; }
        public List<CusBannerModel> CusBanner { get; set; }
        public List<CusInsuranceTypeModel> CusProduct { get; set; }
        public List<CusMenuUtilityModel> CusUtility { get; set; } 
    }
}
