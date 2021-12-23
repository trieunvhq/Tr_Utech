using System;
namespace QRMS.Models.QMK
{
    public class CusGetOtpForgotPasswordModel
    {
        public string DestinationReceiveOtp { get; set; }
        public string OtpCode { get; set; }
        public int AccountId { get; set; }
    }
}
