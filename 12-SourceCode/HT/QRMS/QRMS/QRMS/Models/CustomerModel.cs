using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FULL_NAME { get; set; }
        public Nullable<int> ACCOUNT_ID { get; set; }
        public string USERNAME { get; set; }
        public string CTYPE { get; set; }
        public string IDENTIFY_NO { get; set; }
        public string IDENTITFY_TYPE { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string TAX_NO { get; set; }
        public Nullable<int> COUNTRY_OB_ID { get; set; }
        public Nullable<int> PROVINCE_OB_ID { get; set; }
        public Nullable<int> DISTRIC_OB_ID { get; set; }
        public Nullable<int> WARD_OB_ID { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public Nullable<System.DateTime> IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public Nullable<int> GENDER_ID { get; set; }
        public string GENDER_NAME { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string REPRESENTATIVE_NAME { get; set; }
        public string STATUS_RECORD { get; set; }
    }
}
