using System;
namespace QRMS.Models
{
    public class InsuredPersonInfoModel
    {
        public string NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<decimal> PACKAGE_VALUE { get; set; }
    }
}
