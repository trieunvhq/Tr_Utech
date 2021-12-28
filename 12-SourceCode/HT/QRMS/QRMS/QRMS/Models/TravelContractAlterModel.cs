 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMS.Models
{
    public class TravelContractAlterModel
    {
        public int OLD_ID { get; set; }
        public string OLD_CONTRACT_CODE { get; set; }
        public int? OLD_CUSTOMER_ID { get; set; }
        public string OLD_CUST_TYPE { get; set; }
        public string OLD_CUST_NAME { get; set; }
        public string OLD_IDENTITY_NO { get; set; }
        public string OLD_CUST_PHONE { get; set; }
        public string OLD_CUST_EMAIL { get; set; }
        public string OLD_CUST_ADDRESS { get; set; }
        public int? OLD_CUST_COUNTRY_ID { get; set; }
        public int? OLD_CUST_PROVINCE_ID { get; set; }
        public int? OLD_CUST_DISTRICT_ID { get; set; }
        public int? OLD_CUS_WARD_ID { get; set; }
        public string OLD_CUST_COUNTRY_CODE { get; set; }
        public string OLD_CUST_PROVINCE_CODE { get; set; }
        public string OLD_CUST_DISTRICT_CODE { get; set; }
        public string OLD_CUS_WARD_CODE { get; set; }
        public string OLD_CUST_COUNTRY_NAME { get; set; }
        public string OLD_CUST_PROVINCE_NAME { get; set; }
        public string OLD_CUST_DISTRICT_NAME { get; set; }
        public string OLD_CUS_WARD_NAME { get; set; }
        public string OLD_CUST_SEX { get; set; }
        public DateTime? OLD_DOB { get; set; }
        public DateTime? OLD_IDENTITY_ISSUE_DATE { get; set; }
        public string OLD_IDENTITY_ISSUE_OFFICE { get; set; }
        public string OLD_NATIONALITY { get; set; }
        public string OLD_BEN_NAME { get; set; }
        public string OLD_BEN_IDENTITY_ID { get; set; }
        public string OLD_BEN_PHONE { get; set; }
        public string OLD_BEN_EMAIL { get; set; }
        public string OLD_BEN_ADDRESS { get; set; }
        public int? OLD_BEN_COUNTRY_ID { get; set; }
        public int? OLD_BEN_PROVINCE_ID { get; set; }
        public int? OLD_BEN_DISTRICT_ID { get; set; }
        public int? OLD_BEN_WARD_ID { get; set; }
        public string OLD_BEN_SEX { get; set; }
        public string OLD_BEN_COUNTRY_CODE { get; set; }
        public string OLD_BEN_PROVINCE_CODE { get; set; }
        public string OLD_BEN_DISTRICT_CODE { get; set; }
        public string OLD_BEN_WARD_CODE { get; set; }
        public string OLD_BEN_COUNTRY_NAME { get; set; }
        public string OLD_BEN_PROVINCE_NAME { get; set; }
        public string OLD_BEN_DISTRICT_NAME { get; set; }
        public string OLD_BEN_WARD_NAME { get; set; }
        public Nullable<System.DateTime> OLD_BEN_DOB { get; set; }
        public DateTime? OLD_BEN_IDENTITY_ISSUE_DATE { get; set; }
        public string OLD_BEN_IDENTITY_ISSUE_OFFICE { get; set; }
        public string OLD_BEN_NATIONALITY { get; set; }
        public DateTime? OLD_CONTRACT_ISSUED_DATE { get; set; }
        public DateTime? OLD_CONTRACT_START_DATE { get; set; }
        public DateTime? OLD_CONTRACT_EXPIRE_DATE { get; set; }
        public string OLD_CONTRACT_ISSUE_TYPE { get; set; }
        public int? OLD_CONTRACT_STATUS { get; set; }
        public Nullable<int> OLD_TRAVEL_INS_PROGRAM_ID { get; set; }
        public string OLD_TRAVEL_INS_PROGRAM_CODE { get; set; }
        public string OLD_TRAVEL_INS_PROGRAM_NAME { get; set; }
        public Nullable<decimal> OLD_TRAVEL_INS_PROGRAM_VALUE { get; set; }
        public Nullable<int> OLD_TRAVEL_AREA_FROM_ID { get; set; }
        public string OLD_TRAVEL_AREA_FROM_NAME { get; set; }
        public Nullable<int> OLD_TRAVEL_AREA_TO_ID { get; set; }
        public string OLD_TRAVEL_AREA_TO_NAME { get; set; }
        public Nullable<System.DateTime> OLD_TRAVEL_DEPART_DATE { get; set; }
        public Nullable<System.DateTime> OLD_TRAVEL_RETURN_DATE { get; set; }
        public string OLD_TRAVEL_IMAGE_IDENTITY_FRONT { get; set; }
        public string OLD_TRAVEL_IMAGE_IDENTITY_END { get; set; }
        public Nullable<int> OLD_TRAVEL_EXCHANGE_RATE { get; set; }
        public Nullable<decimal> OLD_TRAVEL_PRICE { get; set; }
        public Nullable<decimal> OLD_TRAVEL_DE_PRICE { get; set; }
        public Nullable<decimal> OLD_TRAVEL_DE_RATIO { get; set; }
        public Nullable<decimal> OLD_TRAVEL_TOTAL_PRICE { get; set; }
        public Nullable<int> OLD_INSUR_PRODUCT_ID { get; set; }
        public string OLD_INSUR_PRODUCT_CODE { get; set; }
        public string OLD_INSUR_PRODUCT_NAME { get; set; }
        public string OLD_REPRESENTATIVE_NAME { get; set; }
        public List<InsuredPersonDetailModel> OLD_listPerson { get; set; } = new List<InsuredPersonDetailModel>();

        public int NEW_ID { get; set; }
        public string NEW_CONTRACT_CODE { get; set; }
        public int? NEW_CUSTOMER_ID { get; set; }
        public string NEW_CUST_TYPE { get; set; }
        public string NEW_CUST_NAME { get; set; }
        public string NEW_IDENTITY_NO { get; set; }
        public string NEW_CUST_PHONE { get; set; }
        public string NEW_CUST_EMAIL { get; set; }
        public string NEW_CUST_ADDRESS { get; set; }
        public int? NEW_CUST_COUNTRY_ID { get; set; }
        public int? NEW_CUST_PROVINCE_ID { get; set; }
        public int? NEW_CUST_DISTRICT_ID { get; set; }
        public int? NEW_CUS_WARD_ID { get; set; }
        public string NEW_CUST_COUNTRY_CODE { get; set; }
        public string NEW_CUST_PROVINCE_CODE { get; set; }
        public string NEW_CUST_DISTRICT_CODE { get; set; }
        public string NEW_CUS_WARD_CODE { get; set; }
        public string NEW_CUST_COUNTRY_NAME { get; set; }
        public string NEW_CUST_PROVINCE_NAME { get; set; }
        public string NEW_CUST_DISTRICT_NAME { get; set; }
        public string NEW_CUS_WARD_NAME { get; set; }
        public string NEW_CUST_SEX { get; set; }
        public DateTime? NEW_DOB { get; set; }
        public DateTime? NEW_IDENTITY_ISSUE_DATE { get; set; }
        public string NEW_IDENTITY_ISSUE_OFFICE { get; set; }
        public string NEW_NATIONALITY { get; set; }
        public string NEW_BEN_NAME { get; set; }
        public string NEW_BEN_IDENTITY_ID { get; set; }
        public string NEW_BEN_PHONE { get; set; }
        public string NEW_BEN_EMAIL { get; set; }
        public string NEW_BEN_ADDRESS { get; set; }
        public int? NEW_BEN_COUNTRY_ID { get; set; }
        public int? NEW_BEN_PROVINCE_ID { get; set; }
        public int? NEW_BEN_DISTRICT_ID { get; set; }
        public int? NEW_BEN_WARD_ID { get; set; }
        public string NEW_BEN_SEX { get; set; }
        public string NEW_BEN_COUNTRY_CODE { get; set; }
        public string NEW_BEN_PROVINCE_CODE { get; set; }
        public string NEW_BEN_DISTRICT_CODE { get; set; }
        public string NEW_BEN_WARD_CODE { get; set; }
        public string NEW_BEN_COUNTRY_NAME { get; set; }
        public string NEW_BEN_PROVINCE_NAME { get; set; }
        public string NEW_BEN_DISTRICT_NAME { get; set; }
        public string NEW_BEN_WARD_NAME { get; set; }
        public Nullable<System.DateTime> NEW_BEN_DOB { get; set; }
        public DateTime? NEW_BEN_IDENTITY_ISSUE_DATE { get; set; }
        public string NEW_BEN_IDENTITY_ISSUE_OFFICE { get; set; }
        public string NEW_BEN_NATIONALITY { get; set; }
        public DateTime? NEW_CONTRACT_ISSUED_DATE { get; set; }
        public DateTime? NEW_CONTRACT_START_DATE { get; set; }
        public DateTime? NEW_CONTRACT_EXPIRE_DATE { get; set; }
        public string NEW_CONTRACT_ISSUE_TYPE { get; set; }
        public int? NEW_CONTRACT_STATUS { get; set; }
        public Nullable<int> NEW_TRAVEL_INS_PROGRAM_ID { get; set; }
        public string NEW_TRAVEL_INS_PROGRAM_CODE { get; set; }
        public string NEW_TRAVEL_INS_PROGRAM_NAME { get; set; }
        public Nullable<decimal> NEW_TRAVEL_INS_PROGRAM_VALUE { get; set; }
        public Nullable<int> NEW_TRAVEL_AREA_FROM_ID { get; set; }
        public string NEW_TRAVEL_AREA_FROM_NAME { get; set; }
        public Nullable<int> NEW_TRAVEL_AREA_TO_ID { get; set; }
        public string NEW_TRAVEL_AREA_TO_NAME { get; set; }
        public Nullable<System.DateTime> NEW_TRAVEL_DEPART_DATE { get; set; }
        public Nullable<System.DateTime> NEW_TRAVEL_RETURN_DATE { get; set; }
        public string NEW_TRAVEL_IMAGE_IDENTITY_FRONT { get; set; }
        public string NEW_TRAVEL_IMAGE_IDENTITY_END { get; set; }
        public Nullable<int> NEW_TRAVEL_EXCHANGE_RATE { get; set; }
        public Nullable<decimal> NEW_TRAVEL_PRICE { get; set; }
        public Nullable<decimal> NEW_TRAVEL_DE_PRICE { get; set; }
        public Nullable<decimal> NEW_TRAVEL_DE_RATIO { get; set; }
        public Nullable<decimal> NEW_TRAVEL_TOTAL_PRICE { get; set; }
        public Nullable<int> NEW_INSUR_PRODUCT_ID { get; set; }
        public string NEW_INSUR_PRODUCT_CODE { get; set; }
        public string NEW_INSUR_PRODUCT_NAME { get; set; }
        public string NEW_REPRESENTATIVE_NAME { get; set; }
        public List<InsuredPersonDetailModel> NEW_listPerson { get; set; } = new List<InsuredPersonDetailModel>();
    }
}
