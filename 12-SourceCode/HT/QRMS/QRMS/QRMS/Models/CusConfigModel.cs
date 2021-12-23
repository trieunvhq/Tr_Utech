using System;
namespace QRMS.Models
{
    public class CusConfigModel
    {
        public bool IsMaintain { get; set; }
        public string MessageLogin { get; set; }
        public CusLoginAccountModel objAccount { get; set; }
        public string TokenAccess { get; set; }
    }
}
