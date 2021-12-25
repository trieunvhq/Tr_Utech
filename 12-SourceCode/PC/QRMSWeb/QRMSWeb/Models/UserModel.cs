using System;

namespace QRMSWeb.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD_HASH { get; set; }
        public Nullable<int> ACCOUNT_TYPE_ID { get; set; }
        public Nullable<int> INSURANCE_AGENT_ID { get; set; }
        public string FULL_NAME { get; set; }
        public Nullable<int> DEPARTMENT_ID { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string IDENTITY_NO { get; set; }
        public string ACCOUNT_TYPE_CODE { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public string ROLE_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARMENT_NAME { get; set; }
        public string INSURANCE_AGENT_CODE { get; set; }
        public string INSURANCE_AGENT_NAME { get; set; }
        public Nullable<int> DIVISION_ID  { get; set; }
        public Nullable<int> AGENT_TYPE_ID { get; set; }
        public string AGENT_TYPE_CODE { get; set; }
        public string AGENT_TYPE_NAME { get; set; }
        public Nullable<System.DateTime> IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public Nullable<int> GENDER_ID { get; set; }
        public string GENDER_NAME { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }

        public string ACCOUNT_GROUP { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string USE_PASSWORD { get; set; }
        public string USE_FINGER { get; set; }
        public string USE_FACE { get; set; }
        public string RECEIVE_NOTIFY { get; set; }
        public string RECEIVE_NOTIFY_MAIN_SCREEN { get; set; }


        public string PJC_CODE { get; set; }
        //extend
        public bool IsLocked
        {
            get
            {
                return Constants.RecordStatus.Locked.Equals(STATUS_RECORD);
            }
        }
        public bool IsInUsing { get; set; } = false;
    }
}