using System;
namespace QRMS.Models
{
    public class CusRegisterOutputModel
    {
        public bool IsExistEmail { get;set; }
        public bool IsExistMobile { get; set; }
        public bool IsExistIdentityNo { get; set; }
        public int AccountId { get; set; } 
    }
}
