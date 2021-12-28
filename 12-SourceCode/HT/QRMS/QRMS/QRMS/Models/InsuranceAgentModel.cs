using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class InsuranceAgentModel
    {
        public int ID { get; set; }
        public Nullable<int> AGENT_TYPE_ID { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string CONTRACT_NO { get; set; }
        public string IDENTIFY_NO { get; set; }
        public string IDENTIFY_TYPE { get; set; }
        public Nullable<int> PJC_MANAGE_ACCOUNT_ID { get; set; }
        public Nullable<int> MANAGE_DEPARTMENT_ID { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public Nullable<System.DateTime> CONTRACT_DATE { get; set; }
        public string RANK { get; set; }
        public Nullable<int> PARENT_AGENT_ID { get; set; }
        public string MOBILE { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string TAX_NO { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCE_ID { get; set; }
        public Nullable<int> DISTRICT_ID { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public string PIC_NAME { get; set; }
        public string PIC_EMAIL { get; set; }
        public string PIC_MOBILE { get; set; }
        public string REMARK { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<int> DIVISION_ID { get; set; }
        public string AGENT_TYPE_CODE { get; set; }
        public Nullable<int> DIVISION_GRP_ID { get; set; }
        public string DIVISION_GRP_CODE { get; set; }
        public string DIVISION_GRP_NAME { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string DISTRICT_CODE { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string WARD_CODE { get; set; }
        public string WARD_NAME { get; set; }
        public string AGENT_TYPE_NAME { get; set; }
        public string PARENT_AGENT_NAME { get; set; }
        public string MANAGE_DEPARTMENT_NAME { get; set; }
        public string DIVISION_CODE { get; set; }
        public string DIVISION_NAME { get; set; }
        public string PJC_MANAGE_ACCOUNT_CODE { get; set; }
        public string PJC_MANAGE_ACCOUNT_NAME { get; set; }
        public string PJC_MANAGE_ACCOUNT_MOBILE { get; set; }
        public string PJC_MANAGE_ACCOUNT_EMAIL { get; set; }
        public string PERSONAL_ORGANIZATION_NAME { get; set; }
        public string PIC_NO { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public Nullable<int> PARENT_AGENT_ACCOUNT_ID { get; set; }
        public string PARENT_AGENT_ACCOUNT_NAME { get; set; }
        public string PARENT_AGENT_ACCOUNT_CODE { get; set; }
    }
}
