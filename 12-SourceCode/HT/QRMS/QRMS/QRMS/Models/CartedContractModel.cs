using Newtonsoft.Json;
using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Resources;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public partial class CartedContractModel : Notifiable
    {
        public int ID { get; set; }
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
        public string BEN_NAME { get; set; }
        public string BEN_IDENTITY_ID { get; set; }
        public string BEN_PHONE { get; set; }
        public string VEH_PLATE_NO { get; set; }
        public string VEH_FRAME_NO { get; set; }
        public string VEH_ENGINE_NO { get; set; }
        public Nullable<int> VEH_BRAND_ID { get; set; }
        public Nullable<int> VEH_MODEL_ID { get; set; }
        public Nullable<int> VEH_TYPE_ID { get; set; }
        public Nullable<decimal> VEH_CUBE_CAPACITY { get; set; }
        public Nullable<decimal> VEH_MAX_LOAD { get; set; }
        public Nullable<short> VEH_SEAT_NO { get; set; }
        public Nullable<int> VEH_ORIGIN { get; set; }
        public Nullable<System.DateTime> VEH_FIRST_REG_DATE { get; set; }
        public Nullable<int> VEH_PURPOSE_ID { get; set; }
        public string VEH_PURPOSE_CODE { get; set; }
        public Nullable<decimal> VEH_VALUE { get; set; }
        public Nullable<int> SALE_ACCOUNT_ID { get; set; }
        public Nullable<int> CUST_ACCOUNT_ID { get; set; }
        public string SALE_PERSONEL_CODE { get; set; }
        public string CONTRACT_CERT_NO { get; set; }
        public string CONTRACT_PRINT_NO { get; set; }
        public Nullable<System.DateTime> CONTRACT_ISSUED_DATE { get; set; }
        public Nullable<System.DateTime> CONTRACT_START_DATE { get; set; }
        public Nullable<System.DateTime> CONTRACT_EXPIRE_DATE { get; set; }
        public string CONTRACT_ISSUE_TYPE { get; set; }
        public Nullable<decimal> INSURED_TOTAL_VAL { get; set; }
        public Nullable<decimal> DEDUCT_TOTAL_VAL { get; set; }
        public Nullable<decimal> TOTAL_PRICE { get; set; }
        public Nullable<decimal> TOTAL_DEDUCT_PRICE { get; set; }
        public Nullable<decimal> TOTAL_DEDUCT_RATIO { get; set; }
        public Nullable<float> TOTAL_VAT { get; set; }
        public Nullable<int> PAY_METHOD { get; set; }
        public Nullable<System.DateTime> PAY_DUE_DATE { get; set; }
        public Nullable<decimal> TNDSBB_AUTO_PRICE { get; set; }
        public Nullable<decimal> TNDSBB_AUTO_DE_PRICE { get; set; }
        public Nullable<decimal> TNSDBB_AUTO_VAT { get; set; }
        public Nullable<decimal> TNDSBB_MOTO_PRICE { get; set; }
        public Nullable<decimal> TNDSBB_MOTO_DE_PRICE { get; set; }
        public Nullable<decimal> TNDSBB_MOTO_VAT { get; set; }
        public Nullable<short> TNVN_AUTO_CREW_NO { get; set; }
        public Nullable<int> TNVN_AUTO_CREW_PCLAIM_ID { get; set; }
        public Nullable<decimal> TNVN_AUTO_CREW_PCLAIM { get; set; }
        public Nullable<short> TNVN_AUTO_PSGR_NO { get; set; }
        public Nullable<int> TNVN_AUTO_PSGR_PCLAIM_ID { get; set; }
        public Nullable<decimal> TNVN_AUTO_PSGR_PCLAIM { get; set; }
        public Nullable<decimal> TNVN_AUTO_PRICE { get; set; }
        public Nullable<decimal> TNVN_AUTO_DE_PRICE { get; set; }
        public Nullable<float> TNVN_AUTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNVN_AUTO_VAT { get; set; }
        public Nullable<int> TNDSTN_ADD_AUTO_TYPE { get; set; }
        public Nullable<int> TNDSTN_ADD_AUTO_PCLAIM_ID { get; set; }
        public string VEH_PUPOSE_CODE { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_ASSEST { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_CREW { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_PRICE { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_DE_PRICE { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_VAT { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_PSGR { get; set; }
        public Nullable<int> TNDS_AUTO_LOAD_TYPE { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_LMT { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_DEDUCT { get; set; }
        public string TNDS_AUTO_LOAD_OWN { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_PRICE { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_DE_PRICE { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_VAT { get; set; }
        public Nullable<decimal> VC_AUTO_VEH_VALUE { get; set; }
        public Nullable<decimal> VS_AUTO_INSURE_VAL { get; set; }
        public Nullable<decimal> VC_AUTO_DE_PRICE { get; set; }
        public Nullable<decimal> VC_AUTO_DE_PRICE_RATIO { get; set; }
        public Nullable<decimal> VC_AUTO_PRICE { get; set; }
        public Nullable<decimal> VC_AUTO_VAT { get; set; }
        public Nullable<int> TNVN_MOTO_PCLAIM_ID { get; set; }
        public Nullable<decimal> TNVN_MOTO_PCLAIM { get; set; }
        public Nullable<decimal> TNVN_MOTO_PRICE { get; set; }
        public Nullable<decimal> TNVN_MOTO_DE_PRICE { get; set; }
        public Nullable<decimal> TNVN_MOTO_VAT { get; set; }
        public Nullable<decimal> VC_MOTO_VEH_VALUE { get; set; }
        public Nullable<decimal> VC_MOTO_INSURE_VAL { get; set; }
        public Nullable<decimal> VC_MOTO_CLAIM_DEDUCT { get; set; }
        public Nullable<decimal> VC_AUTO_CLAIM_DEDUCT { get; set; }
        public Nullable<decimal> VC_MOTO_PRICE { get; set; }
        public Nullable<decimal> VC_MOTO_DE_PRICE { get; set; }
        public Nullable<decimal> VC_MOTO_DE_RATIO { get; set; }
        public Nullable<decimal> VC_MOTO_VAT { get; set; }
        public string STATUS_RECORD { get; set; }
        public Nullable<int> TNDSTN_ADD_MOTO_TYPE { get; set; }
        public Nullable<int> TNDSTN_ADD_MOTO_PCLAIM_ID { get; set; }
        public Nullable<decimal> TNDSTN_ADD_MOTO_ASSEST { get; set; }
        public Nullable<decimal> TNDSTN_ADD_MOTO_CREW { get; set; }
        public Nullable<decimal> TNDSTN_ADD_MOTO_PSGR { get; set; }
        public Nullable<decimal> TNDSTN_ADD_PRICE { get; set; }
        public Nullable<decimal> TNDSTN_ADD_DE_PRICE { get; set; }
        public Nullable<decimal> TNDSTN_ADD_VAT { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER_ID { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> UPDATE_USER_ID { get; set; }
        public Nullable<int> INPUTING_USER_ID { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL_IDENTITY_NO { get; set; }
        public Nullable<int> BILL_COUNTRY_ID { get; set; }
        public Nullable<int> BILL_PROVINCE_ID { get; set; }
        public Nullable<int> BILL_DISTRICT_ID { get; set; }
        public Nullable<int> BILL_WARD_ID { get; set; }
        public string BILL_ADDRESS { get; set; }
        public Nullable<int> CONTRACT_STATUS { get; set; }
        public string BILL_IDENTITY_TYPE { get; set; }
        public string IDENTITY_TYPE { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public Nullable<int> DISCOUNT_PROG_ID { get; set; }
        public string DISCOUNT_PROG_NAME { get; set; }
        public Nullable<decimal> DISCOUNT_RATIO { get; set; }
        public string TAX_NO { get; set; }
        public string CONTRACT_CODE { get; set; }
        public Nullable<int> AGENT_ID { get; set; }
        public string AGENT_NAME { get; set; }
        public Nullable<int> INSUR_PRODUCT_ID { get; set; }
        public string INSUR_PRODUCT_CODE { get; set; }
        public string INSUR_PRODUCT_NAME { get; set; }
        public string AGENT_CODE { get; set; }
        public Nullable<int> APPROVAL_AGENT_ID { get; set; }
        public string APPROVAL_AGENT_CODE { get; set; }
        public string APPROVAL_AGENT_NAME { get; set; }
        public Nullable<int> DIVN_ID { get; set; }
        public string DIVN_NAME { get; set; }
        public Nullable<int> AGENT_ID_C_1 { get; set; }
        public Nullable<int> AGENT_ID_C_2 { get; set; }
        public Nullable<int> AGENT_ID_C_3 { get; set; }
        public Nullable<int> AGENT_ID_C_4 { get; set; }
        public Nullable<int> AGENT_ID_C_5 { get; set; }
        public Nullable<int> AGENT_ID_C_6 { get; set; }
        public Nullable<int> AGENT_ID_C_7 { get; set; }
        public Nullable<int> AGENT_ID_C_8 { get; set; }
        public Nullable<int> AGENT_ID_C_9 { get; set; }
        public Nullable<int> AGENT_ID_C_10 { get; set; }
        public string DIVN_CODE { get; set; }
        public string CONTRACT_STATUS_CODE { get; set; }
        public Nullable<decimal> TOTAL_ALTERED_PRICE { get; set; }
        public Nullable<decimal> TOTAL_FINAL_PRICE { get; set; }
        public string SALE_ACCOUNT_CODE { get; set; }
        public string SALE_ACCOUNT_NAME { get; set; }
        public string CUST_SEX { get; set; }
        public string CUST_COUNTRY_CODE { get; set; }
        public string CUST_PROVINCE_CODE { get; set; }
        public string CUST_DISTRICT_CODE { get; set; }
        public string CUS_WARD_CODE { get; set; }
        public string VEH_BRAND_CODE { get; set; }
        public string VEH_MODEL_CODE { get; set; }
        public string VEH_TYPE_CODE { get; set; }
        public string CONTRACT_PARENTS { get; set; }
        public string BILL_COMPANY { get; set; }
        public string BILL_TAXNO { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_PCLAIM { get; set; }
        public Nullable<decimal> LOAD_WEIGHT { get; set; }
        public Nullable<int> TNDS_AUTO_LOAD_LVL_ID { get; set; }
        public Nullable<short> ALTER_STATUS { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_PTON { get; set; }
        public Nullable<decimal> TNVN_AUTO_CREW_PRICE { get; set; }
        public Nullable<decimal> TNVN_AUTO_CREW_DE_PRICE { get; set; }
        public Nullable<decimal> TNVN_AUTO_CREW_VAT { get; set; }
        public Nullable<decimal> TNVN_AUTO_PSGR_PRICE { get; set; }
        public Nullable<decimal> TNVN_AUTO_PSGR_DE_PRICE { get; set; }
        public Nullable<decimal> TNVN_AUTO_PSGR_VAT { get; set; }
        public Nullable<short> ALTER_TIMES { get; set; } 
        private Nullable<System.DateTime> iDENTITY_ISSUE_DATE;
        public Nullable<System.DateTime> IDENTITY_ISSUE_DATE   // property
        {
            get { return iDENTITY_ISSUE_DATE; }   // get method
            set
            {
                iDENTITY_ISSUE_DATE = value;
                sIDENTITY_ISSUE_DATE = value?.ToString("dd/MM/yyyy");
            }  // set method
        }
        public string sIDENTITY_ISSUE_DATE { get; set; }

        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        private Nullable<System.DateTime> dOB;
        public Nullable<System.DateTime> DOB   // property
        {
            get { return dOB; }   // get method
            set
            {
                dOB = value;
                sDOB = value?.ToString("dd/MM/yyyy");
            }  // set method
        }
        public string sDOB { get; set; }//Ngày sinh
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
        public string sbEN_IDENTITY_ISSUE_DATE { get; set; }
        private Nullable<System.DateTime> bEN_IDENTITY_ISSUE_DATE;

        public Nullable<System.DateTime> BEN_IDENTITY_ISSUE_DATE
        {
            get
            {
                return bEN_IDENTITY_ISSUE_DATE;
            }
            set
            {
                bEN_IDENTITY_ISSUE_DATE = value;
                sbEN_IDENTITY_ISSUE_DATE = value?.ToString("dd/MM/yyyy");
            }
        }
        public string BEN_IDENTITY_ISSUE_OFFICE { get; set; }
        public string BEN_NATIONALITY { get; set; }
        public string sBEN_DOB { get; set; }

        private Nullable<System.DateTime> bEN_DOB;

        public Nullable<System.DateTime> BEN_DOB
        {
            get
            {
                return bEN_DOB;
            }
            set
            {
                bEN_DOB = value;
                sBEN_DOB = value?.ToString("dd/MM/yyyy");
            }
        }

        public Nullable<decimal> TNDSBB_AUTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNDSBB_MOTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNDSTN_ADD_AUTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNDSTN_ADD_MOTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNDS_AUTO_LOAD_DE_RATIO { get; set; }
        public Nullable<decimal> TNVN_MOTO_DE_RATIO { get; set; }
        public Nullable<decimal> TNVN_AUTO_CREW_DE_RATIO { get; set; }
        public Nullable<decimal> TNVN_AUTO_PSGR_DE_RATIO { get; set; }
        public Nullable<int> HEALTH_CANCER_PACKAGE_ID { get; set; }
        public Nullable<decimal> HEALTH_CANCER_INSURED_LVL { get; set; }
        public Nullable<decimal> HEALTH_CANCER_INSURED_VAL { get; set; }
        public Nullable<decimal> HEALTH_CANCER_DEDUCT { get; set; }
        public Nullable<decimal> HEALTH_CANCER_PRICE { get; set; }
        public Nullable<decimal> HEALTH_CANCER_DE_PRICE { get; set; }
        public Nullable<decimal> HEALTH_CANCER_DE_RATIO { get; set; }
        public Nullable<decimal> HEALTH_CANCER_VAT { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_PACKAGE_ID { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_INSURED_LVL { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_INSURED_VAL { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_DEDUCT { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_PRICE { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_DE_PRICE { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_DE_RATIO { get; set; }
        public Nullable<decimal> HEALTH_DISEASE_VAT { get; set; }
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
        public string REPRESENTATIVE_NAME { get; set; }
        public Nullable<decimal> TRAVEL_PRICE_ORIGIN { get; set; }
        public string CUST_CODE { get; set; }
        public string INSUR_PRODUCT_TYPE { get; set; }
        public Nullable<int> BEN_RELATIONSHIP_ID { get; set; }
        public string BEN_RELATIONSHIP_CODE { get; set; }
        public string BEN_RELATIONSHIP_NAME { get; set; }
        public string TRAVEL_MODEL { get; set; }
        public string TRAVEL_GAME { get; set; }
        public string TRAVEL_DANGER_GAME { get; set; }
        public string VEH_REGISTRATION_IMAGE { get; set; }
        public string VEH_FRONT_IMAGE { get; set; }
        public string VEH_BACK_IMAGE { get; set; }
        public string VEH_LEFT_IMAGE { get; set; }
        public string VEH_RIGHT_IMAGE { get; set; }
        public Nullable<int> DEPT_ID { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public Nullable<int> NUMBER_START { get; set; }
        public Nullable<int> NUMBER_END { get; set; }
        public Nullable<int> NUMBER_CURENT { get; set; }
        public string HINS_OWNERSHIP { get; set; }
        public string HINS_OWNERSHIP_NAME { get; set; }
        public string HINS_USINGTIME { get; set; }
        public string HINS_ADDRESS { get; set; }
        public string HINS_AUTOFIREFIGHTINGSYS { get; set; }
        public string HINS_BANKMORTGAGE { get; set; }
        public string HINS_NAME_OF_BANK { get; set; }
        public string HINS_PHONE_OF_BANK { get; set; }
        public string HINS_EMAIL_OF_BANK { get; set; }
        public string HINS_ADDRESS_OF_BANK { get; set; }
        public string BILL_COMPANY_ADDRESS { get; set; }
        public Nullable<int> CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_NAME { get; set; }
        public Nullable<decimal> HINS_HOME_VALUE { get; set; }
        public string sHINS_HOME_VALUE { get; set; }
        public Nullable<decimal> HINS_HOME_INSUR_VALUE { get; set; }
        public string sHINS_HOME_INSUR_VALUE { get; set; }
        public string HINS_JOIN_VOLUNTARY_FIRE { get; set; }
        public Nullable<decimal> HINS_ASSETS_VALUE { get; set; }
        public Nullable<decimal> HINS_DEDUCTION_VALUE { get; set; }
        public string VEH_TYPE_NAME { get; set; }
        public Nullable<int> INSUR_PRODUCT_TYPE_ID { get; set; }
        public string INSUR_PRODUCT_TYPE_NAME { get; set; }
        public Nullable<decimal> HINS_VALUE_FEE_RATIO { get; set; }
        public Nullable<decimal> TRAVEL_COMMISSION { get; set; }
        public string TRAVEL_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> HINS_HOME_PRICE { get; set; }
        public string sHINS_HOME_PRICE { get; set; }
        public Nullable<decimal> HINS_HOME_RATIO { get; set; }
        public Nullable<decimal> HINS_ASSETS_PRICE { get; set; }
        public Nullable<decimal> HINS_ASSETS_RATIO { get; set; }
        public Nullable<decimal> HINS_DAMAGE_VALUE { get; set; }
        public Nullable<decimal> HINS_DAMAGE_PRICE { get; set; }
        public Nullable<decimal> HINS_DAMAGE_RATIO { get; set; }
        public string HINS_LOAN_CONTRACT_NO { get; set; }
        public Nullable<decimal> HEALTH_COMMISSION { get; set; }
        public string HEALTH_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_BB_COMMISSION { get; set; }
        public string AUTO_BB_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_ADD_COMMISSION { get; set; }
        public string AUTO_ADD_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_CREW_COMMISSION { get; set; }
        public string AUTO_CREW_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_PSGR_COMMISSION { get; set; }
        public string AUTO_PSGR_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_LOAD_COMMISSION { get; set; }
        public string AUTO_LOAD_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> MOTO_BB_COMMISSION { get; set; }
        public string MOTO_BB_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> MOTO_ADD_COMMISSION { get; set; }
        public string MOTO_ADD_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> MOTO_VN_COMMISSION { get; set; }
        public string MOTO_VN_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> AUTO_BB_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_VN_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_ADD_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_LOAD_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_BB_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_VN_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_ADD_VAT_BEFORE_ALTER { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_1 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_2 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_3 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_4 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_5 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_6 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_7 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_8 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_9 { get; set; }
        public Nullable<int> AGENT_ACCOUNT_ID_C_10 { get; set; }
        public Nullable<decimal> AUTO_VC_COMMISSION { get; set; }
        public string AUTO_VC_COST_BASIC_CODE { get; set; }
        public Nullable<decimal> HINS_COMMISSION { get; set; }
        public string HINS_COST_BASIC_CODE { get; set; }
        public Nullable<int> VEH_TNDSTT_TYPE_ID { get; set; }
        public Nullable<int> HINS_PROVINCE { get; set; }
        public string HINS_PROVINCE_CODE { get; set; }
        public string HINS_PROVINCE_NAME { get; set; }
        public Nullable<int> HINS_DISTRICT { get; set; }
        public string HINS_DISTRICT_CODE { get; set; }
        public string HINS_DISTRICT_NAME { get; set; }
        public Nullable<int> HINS_WARD { get; set; }
        public string HINS_WARD_CODE { get; set; }
        public string HINS_WARD_NAME { get; set; }
        public string HINS_HOUSETYPE { get; set; }
        public string HINS_HOUSETYPE_NAME { get; set; }
        public string HINS_KINDOFHOUSE { get; set; }
        public string HINS_KINDOFHOUSE_NAME { get; set; }
        public Nullable<decimal> HINS_HOME_DE_RATIO { get; set; }
        public Nullable<decimal> HINS_HOME_DE_PRICE { get; set; }
        public Nullable<decimal> HINS_HOME_PRICE_AF_DE { get; set; }
        public Nullable<decimal> HINS_HOME_TOTAL_PRICE { get; set; }
        public Nullable<decimal> HINS_HOME_VAT { get; set; }
        public Nullable<decimal> HINS_ASSETS_DE_RATIO { get; set; }
        public Nullable<decimal> HINS_ASSETS_DE_PRICE { get; set; }
        public Nullable<decimal> HINS_ASSETS_PRICE_AF_DE { get; set; }
        public Nullable<decimal> HINS_ASSETS_TOTAL_PRICE { get; set; }
        public Nullable<decimal> HINS_ASSETS_VAT { get; set; }
        public Nullable<decimal> HINS_DAMAGE_DE_RATIO { get; set; }
        public Nullable<decimal> HINS_DAMAGE_DE_PRICE { get; set; }
        public Nullable<decimal> HINS_DAMAGE_PRICE_AF_DE { get; set; }
        public Nullable<decimal> HINS_DAMAGE_TOTAL_PRICE { get; set; }
        public Nullable<decimal> HINS_DAMAGE_VAT { get; set; }

        public int PageTotal { get; set; }
        public bool IsRequireImage { get; set; }
        public bool IsSameOwner { get; set; } 
        public string CUST_PROVINCE_NAME { get; set; }
        public string CUST_DISTRICT_NAME { get; set; }
        public string CUST_WARD_NAME { get; set; }  
        // 
        public IList<MATERIAL_DKBS_INS_FEEModel> DKBSList { get; set; }
        public IList<CartedContractDKBSModel> CartedDKBSList { get; set; }
        public IList<TNVNInsFeeModel> TNVNList { get; set; }
        public IList<MTN_MOTO_CAR_INS_FEEModel> MTNFixedList { get; set; }
        public IList<MTN_MOTO_CAR_INS_FEEModel> MTNGoodsList { get; set; }
        public IList<FreightInsFeeModel> FreightList { get; set; }
        public IList<AGENCY_PRODUCT_ASC_DESC_SUBFEEModel> SubfeeList { get; set; }
        
        public FreightInsFeeModel SelectedVehicleType { get; set; }
        public CommonValueModel SelectedType { get; set; }
        public VehicleBrandModel SelectedBrand { get; set; }
        public VehicleModelModel SelectedModel { get; set; }
        public CommonValueModel SelectedOrigin { get; set; }
        public CommonValueModel SelectedPurpose { get; set; }
        public int? CartContractID { get; set; }
        public string CartContractIssueType { get; set; }
        public int? CartContractStatus { get; set; } 
    }

    public partial class CartedContractModel
    {
        [JsonIgnore]
        public decimal? ORIGINAL_TNDS_AUTO_LOAD_PRICE
        {
            get
            {
                decimal.TryParse(TNDS_AUTO_LOAD_OWN, out decimal value);
                return (TNDS_AUTO_LOAD_PRICE ?? 0) + value;
            }
        }

        [JsonIgnore]
        [DependsOn("TNDS_AUTO_LOAD_OWN")]
        public string IsSameOwnerString
        {
            get
            {
                return !string.IsNullOrEmpty(TNDS_AUTO_LOAD_OWN) ? AppResources.Yes : AppResources.No;
            }
        }

        [JsonIgnore]
        [DependsOn("CONTRACT_ISSUE_TYPE", "CONTRACT_STATUS")]
        public string CONTRACT_STATUS_STRING => MobileLib.GetContractStatusString(CONTRACT_ISSUE_TYPE, CONTRACT_STATUS);

        [JsonIgnore]
        [DependsOn("CONTRACT_STATUS")]
        public Color CONTRACT_STATUS_COLOR => MobileLib.GetColorContractStatus(CONTRACT_STATUS);

        [JsonIgnore]
        public string NextButtonText => CONTRACT_ISSUE_TYPE == Constaint.ContractIssueType.Altered ? AppResources.SendRequest : AppResources.PayConfirm;
    }
}