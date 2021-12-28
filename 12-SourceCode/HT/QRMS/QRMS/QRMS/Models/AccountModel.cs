using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class AccountModel
    {
        private string FullName;
        public int ID { get; set; }
        public string CODE { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD_HASH { get; set; }
        public Nullable<int> ACCOUNT_TYPE_ID { get; set; }
        public string ACCOUNT_TYPE_NAME { get; set; }
        public Nullable<int> INSURANCE_AGENT_ID { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public Nullable<int> AGENT_TYPE_ID { get; set; }
        public string ACCOUNT_TYPE_CODE { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public string ROLE_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public string ROLE_SHORT_NAME { get; set; }
        public Nullable<int> DEPART_ID { get; set; }
        public string DEPART_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string DEPARTMENT_SHORT_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FULL_NAME { get { return string.IsNullOrEmpty(FullName) ? $"{LAST_NAME} {FIRST_NAME}" : FullName; } set { FullName = value; } }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string TEL { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string COUNTRY_SHORT_NAME { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string PROVINCE_NAME { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public string DISTRICT_CODE { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string DISTRICT_SHORT_NAME { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public string WARD_CODE { get; set; }
        public string WARD_NAME { get; set; }
        public string WARD_SHORT_NAME { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string IDENTITY_NO { get; set; }
    }
}
