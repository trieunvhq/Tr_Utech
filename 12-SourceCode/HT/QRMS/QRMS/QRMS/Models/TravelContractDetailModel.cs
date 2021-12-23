using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class TravelContractDetailModel : Notifiable
    {
        public int ID { get; set; }
        public string CONTRACT_CODE { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string CUST_TYPE { get; set; }
        public string CUST_NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        public string CUST_PHONE { get; set; }
        public string CUST_EMAIL { get; set; }
        public string CUST_ADDRESS { get; set; }
        public Nullable<int> CUST_COUNTRY_ID { get; set; }
        public Nullable<int> CUST_PROVINCE_ID { get; set; }
        public Nullable<int> CUST_DISTRICT_ID { get; set; }
        public Nullable<int> CUS_WARD_ID { get; set; }
        public string CUST_COUNTRY_CODE { get; set; }
        public string CUST_PROVINCE_CODE { get; set; }
        public string CUST_DISTRICT_CODE { get; set; }
        public string CUS_WARD_CODE { get; set; }
        public string CUST_COUNTRY_NAME { get; set; }
        public string CUST_PROVINCE_NAME { get; set; }
        public string CUST_DISTRICT_NAME { get; set; }
        public string CUS_WARD_NAME { get; set; }
        public string CUST_SEX { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public Nullable<DateTime> IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public string BEN_NAME { get; set; }
        public string BEN_IDENTITY_ID { get; set; }
        public string BEN_PHONE { get; set; }
        public string BEN_EMAIL { get; set; }
        public string BEN_ADDRESS { get; set; }
        public Nullable<int> BEN_COUNTRY_ID { get; set; }
        public Nullable<int> BEN_PROVINCE_ID { get; set; }
        public Nullable<int> BEN_DISTRICT_ID { get; set; }
        public Nullable<int> BEN_WARD_ID { get; set; }
        public string BEN_SEX { get; set; }
        public string BEN_COUNTRY_CODE { get; set; }
        public string BEN_PROVINCE_CODE { get; set; }
        public string BEN_DISTRICT_CODE { get; set; }
        public string BEN_WARD_CODE { get; set; }
        public string BEN_COUNTRY_NAME { get; set; }
        public string BEN_PROVINCE_NAME { get; set; }
        public string BEN_DISTRICT_NAME { get; set; }
        public string BEN_WARD_NAME { get; set; }
        public Nullable<System.DateTime> BEN_DOB { get; set; }
        public Nullable<DateTime> BEN_IDENTITY_ISSUE_DATE { get; set; }
        public string BEN_IDENTITY_ISSUE_OFFICE { get; set; }
        public string BEN_NATIONALITY { get; set; }
        public Nullable<DateTime> CONTRACT_ISSUED_DATE { get; set; }
        public Nullable<DateTime> CONTRACT_START_DATE { get; set; }
        public Nullable<DateTime> CONTRACT_EXPIRE_DATE { get; set; }
        public string CONTRACT_ISSUE_TYPE { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL_IDENTITY_NO { get; set; }
        public Nullable<int> BILL_COUNTRY_ID { get; set; }
        public Nullable<int> BILL_PROVINCE_ID { get; set; }
        public Nullable<int> BILL_DISTRICT_ID { get; set; }
        public Nullable<int> BILL_WARD_ID { get; set; }
        public string BILL_COUNTRY_CODE { get; set; }
        public string BILL_PROVINCE_CODE { get; set; }
        public string BILL_DISTRICT_CODE { get; set; }
        public string BILL_WARD_CODE { get; set; }
        public string BILL_COUNTRY_NAME { get; set; }
        public string BILL_PROVINCE_NAME { get; set; }
        public string BILL_DISTRICT_NAME { get; set; }
        public string BILL_WARD_NAME { get; set; }
        public string BILL_ADDRESS { get; set; }
        public string BILL_COMPANY { get; set; }
        public string BILL_TAXNO { get; set; }
        public string Bill_COMPANY_ADDRESS { get; set; }
        public Nullable<int> CONTRACT_STATUS { get; set; }
        public Nullable<int> TRAVEL_INS_PROGRAM_ID { get; set; }
        public string TRAVEL_INS_PROGRAM_CODE { get; set; }
        public string TRAVEL_INS_PROGRAM_NAME { get; set; }
        public Nullable<decimal> TRAVEL_INS_PROGRAM_VALUE { get; set; }
        public Nullable<int> TRAVEL_AREA_FROM_ID { get; set; }
        public string TRAVEL_AREA_FROM_NAME { get; set; }
        public Nullable<int> TRAVEL_AREA_TO_ID { get; set; }
        public string TRAVEL_AREA_TO_NAME { get; set; }
        public Nullable<System.DateTime> TRAVEL_DEPART_DATE { get; set; }
        public Nullable<System.DateTime> TRAVEL_RETURN_DATE { get; set; }
        public string TRAVEL_IMAGE_IDENTITY_FRONT { get; set; }
        public string TRAVEL_IMAGE_IDENTITY_END { get; set; }
        public Nullable<decimal> TRAVEL_EXCHANGE_RATE { get; set; }
        public Nullable<decimal> TRAVEL_PRICE { get; set; }
        public Nullable<decimal> TRAVEL_DE_PRICE { get; set; }
        public Nullable<decimal> TRAVEL_DE_RATIO { get; set; }
        public Nullable<decimal> TRAVEL_VAT { get; set; }
        public Nullable<decimal> TRAVEL_TOTAL_PRICE { get; set; }
        public Nullable<int> CONTRACT_ID { get; set; }
        public Nullable<int> CARTED_CONTRACT_ID { get; set; }
        public Nullable<int> INSUR_PRODUCT_ID { get; set; }
        public string INSUR_PRODUCT_CODE { get; set; }
        public string INSUR_PRODUCT_NAME { get; set; }
        public string REPRESENTATIVE_NAME { get; set; }
        public Nullable<int> ALTER_STATUS { get; set; }
        public Nullable<short> ALTER_TIMES { get; set; }
        public string Remark { get; set; }



        public List<InsuredPersonDetailModel> listPerson { get; set; } = new List<InsuredPersonDetailModel>();
    }
}
