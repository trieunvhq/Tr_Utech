using System;
namespace QRMS.Models
{
    public class CusCustomerInfoModel
    {
        public string FULL_NAME { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> GENDER_ID { get; set; }
        public string GENDER_NAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string IDENTIFY_NO { get; set; }
        public Nullable<System.DateTime> IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public string PROVINCE_NAME { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public string DISTRICT_NAME { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public string WARD_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string USE_PASSWORD { get; set; }
        public string USE_FINGER { get; set; }
        public string USE_FACE { get; set; }
        public string RECEIVE_NOTIFY { get; set; }
        public string RECEIVE_NOTIFY_MAIN_SCREEN { get; set; }
    }
}
