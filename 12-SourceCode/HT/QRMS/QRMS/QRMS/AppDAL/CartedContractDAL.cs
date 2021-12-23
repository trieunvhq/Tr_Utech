using QRMS.Helper;
using QRMS.Models;
//using PISAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QRMS.AppDAL
{
    public class CartedContractDAL
    {
        private static DatabaseContext conn = new DatabaseContext();

        //thông tin đơn bảo hiểm
        public static int AddCartedContract(CartedContractModel obj)
        {
            try
            {
                var item = conn.cartedContractModelLocals.Where(a => (!string.IsNullOrEmpty(obj.IDENTITY_NO) && a.IDENTITY_NO.Equals(obj.IDENTITY_NO))).FirstOrDefault();
                if (item != null)
                {
                    item.CUSTOMER_ID = obj.CUSTOMER_ID == null ? null : obj.CUSTOMER_ID;
                    item.CUST_TYPE = string.IsNullOrEmpty(obj.CUST_TYPE) ? string.Empty : obj.CUST_TYPE;
                    item.CUST_NAME = string.IsNullOrEmpty(obj.CUST_NAME) ? string.Empty : obj.CUST_NAME;
                    item.IDENTITY_NO = string.IsNullOrEmpty(obj.IDENTITY_NO) ? string.Empty : obj.IDENTITY_NO;
                    item.CUST_PHONE = string.IsNullOrEmpty(obj.CUST_PHONE) ? string.Empty : obj.CUST_PHONE;
                    item.CUST_EMAIL = string.IsNullOrEmpty(obj.CUST_EMAIL) ? string.Empty : obj.CUST_EMAIL;
                    item.CUST_ADDRESS = string.IsNullOrEmpty(obj.CUST_ADDRESS) ? string.Empty : obj.CUST_ADDRESS;
                    item.CUST_COUNTRY_ID = obj.CUST_COUNTRY_ID == null ? null : obj.CUST_COUNTRY_ID;
                    item.CUST_PROVINCE_ID = obj.CUST_PROVINCE_ID == null ? null : obj.CUST_PROVINCE_ID;
                    item.CUST_DISTRICT_ID = obj.CUST_DISTRICT_ID == null ? null : obj.CUST_DISTRICT_ID;
                    item.CUS_WARD_ID = obj.CUS_WARD_ID == null ? null : obj.CUS_WARD_ID;
                    item.BILL_NAME = string.IsNullOrEmpty(obj.BILL_NAME) ? string.Empty : obj.BILL_NAME;
                    item.BILL_IDENTITY_NO = obj.BILL_IDENTITY_NO == null ? null : obj.BILL_IDENTITY_NO;
                    item.BILL_COUNTRY_ID = obj.BILL_COUNTRY_ID == null ? null : obj.BILL_COUNTRY_ID;
                    item.BILL_PROVINCE_ID = obj.BILL_PROVINCE_ID == null ? null : obj.BILL_PROVINCE_ID;
                    item.BILL_DISTRICT_ID = obj.BILL_DISTRICT_ID == null ? null : obj.BILL_DISTRICT_ID;
                    item.BILL_WARD_ID = obj.BILL_WARD_ID == null ? null : obj.BILL_WARD_ID;
                    item.BILL_ADDRESS = string.IsNullOrEmpty(obj.BILL_ADDRESS) ? string.Empty : obj.BILL_ADDRESS;
                    item.BEN_NAME = string.IsNullOrEmpty(obj.BEN_NAME) ? string.Empty : obj.BEN_NAME;
                    item.BEN_IDENTITY_ID = obj.BEN_IDENTITY_ID == null ? null : obj.BEN_IDENTITY_ID;
                    item.BEN_PHONE = string.IsNullOrEmpty(obj.BEN_PHONE) ? string.Empty : obj.BEN_PHONE;
                    item.VEH_PLATE_NO = string.IsNullOrEmpty(obj.VEH_PLATE_NO) ? string.Empty : obj.VEH_PLATE_NO;
                    item.VEH_FRAME_NO = string.IsNullOrEmpty(obj.VEH_FRAME_NO) ? string.Empty : obj.VEH_FRAME_NO;
                    item.VEH_ENGINE_NO = string.IsNullOrEmpty(obj.VEH_ENGINE_NO) ? string.Empty : obj.VEH_ENGINE_NO;
                    item.VEH_BRAND_ID = obj.VEH_BRAND_ID == null ? null : obj.VEH_BRAND_ID;
                    item.VEH_MODEL_ID = obj.VEH_MODEL_ID == null ? null : obj.VEH_MODEL_ID;
                    item.VEH_TYPE_ID = obj.VEH_TYPE_ID == null ? null : obj.VEH_TYPE_ID;
                    item.VEH_CUBE_CAPACITY = obj.VEH_CUBE_CAPACITY == null ? null : obj.VEH_CUBE_CAPACITY;
                    item.VEH_MAX_LOAD = obj.VEH_MAX_LOAD == null ? null : obj.VEH_MAX_LOAD;
                    item.VEH_SEAT_NO = obj.VEH_SEAT_NO == null ? null : obj.VEH_SEAT_NO;
                    item.VEH_ORIGIN = obj.VEH_ORIGIN == null ? null : obj.VEH_ORIGIN;
                    item.VEH_FIRST_REG_DATE = obj.VEH_FIRST_REG_DATE == null ? null : obj.VEH_FIRST_REG_DATE;
                    item.VEH_PURPOSE_ID = obj.VEH_PURPOSE_ID == null ? null : obj.VEH_PURPOSE_ID;
                    item.VEH_VALUE = obj.VEH_VALUE == null ? null : obj.VEH_VALUE;
                    item.SALE_ACCOUNT_ID = obj.SALE_ACCOUNT_ID == null ? null : obj.SALE_ACCOUNT_ID;
                    item.CUST_ACCOUNT_ID = obj.CUST_ACCOUNT_ID == null ? null : obj.CUST_ACCOUNT_ID;
                    item.SALE_PERSONEL_CODE = string.IsNullOrEmpty(obj.SALE_PERSONEL_CODE) ? string.Empty : obj.SALE_PERSONEL_CODE;
                    item.CONTRACT_CERT_NO = string.IsNullOrEmpty(obj.CONTRACT_CERT_NO) ? string.Empty : obj.CONTRACT_CERT_NO;
                    item.CONTRACT_PRINT_NO = string.IsNullOrEmpty(obj.CONTRACT_PRINT_NO) ? string.Empty : obj.CONTRACT_PRINT_NO;
                    item.CONTRACT_ISSUED_DATE = obj.CONTRACT_ISSUED_DATE == null ? null : obj.CONTRACT_ISSUED_DATE;
                    item.CONTRACT_START_DATE = obj.CONTRACT_START_DATE == null ? null : obj.CONTRACT_START_DATE;
                    item.CONTRACT_EXPIRE_DATE = obj.CONTRACT_EXPIRE_DATE == null ? null : obj.CONTRACT_EXPIRE_DATE;
                    item.CONTRACT_ISSUE_TYPE = string.IsNullOrEmpty(obj.CONTRACT_ISSUE_TYPE) ? string.Empty : obj.CONTRACT_ISSUE_TYPE;
                    item.INSURED_TOTAL_VAL = obj.INSURED_TOTAL_VAL == null ? null : obj.INSURED_TOTAL_VAL;
                    item.DEDUCT_TOTAL_VAL = obj.DEDUCT_TOTAL_VAL == null ? null : obj.DEDUCT_TOTAL_VAL;
                    item.TOTAL_PRICE = obj.TOTAL_PRICE == null ? null : obj.TOTAL_PRICE;
                    item.TOTAL_DEDUCT_PRICE = obj.TOTAL_DEDUCT_PRICE == null ? null : obj.TOTAL_DEDUCT_PRICE;
                    item.TOTAL_DEDUCT_RATIO = obj.TOTAL_DEDUCT_RATIO == null ? null : obj.TOTAL_DEDUCT_RATIO;
                    item.TOTAL_VAT = obj.TOTAL_VAT == null ? null : obj.TOTAL_VAT;
                    item.PAY_METHOD = obj.PAY_METHOD == null ? null : obj.PAY_METHOD;
                    item.PAY_DUE_DATE = obj.PAY_DUE_DATE == null ? null : obj.PAY_DUE_DATE;
                    item.TNDSBB_AUTO_PRICE = obj.TNDSBB_AUTO_PRICE == null ? null : obj.TNDSBB_AUTO_PRICE;
                    item.TNDSBB_AUTO_DE_PRICE = obj.TNDSBB_AUTO_DE_PRICE == null ? null : obj.TNDSBB_AUTO_DE_PRICE;
                    item.TNSDBB_AUTO_VAT = obj.TNSDBB_AUTO_VAT == null ? null : obj.TNSDBB_AUTO_VAT;
                    item.TNDSBB_MOTO_PRICE = obj.TNDSBB_MOTO_PRICE == null ? null : obj.TNDSBB_MOTO_PRICE;
                    item.TNDSBB_MOTO_DE_PRICE = obj.TNDSBB_MOTO_DE_PRICE == null ? null : obj.TNDSBB_MOTO_DE_PRICE;
                    item.TNDSBB_MOTO_VAT = obj.TNDSBB_MOTO_VAT == null ? null : obj.TNDSBB_MOTO_VAT;
                    item.TNVN_AUTO_CREW_NO = obj.TNVN_AUTO_CREW_NO == null ? null : obj.TNVN_AUTO_CREW_NO;
                    item.TNVN_AUTO_CREW_PCLAIM_ID = obj.TNVN_AUTO_CREW_PCLAIM_ID == null ? null : obj.TNVN_AUTO_CREW_PCLAIM_ID;
                    item.TNVN_AUTO_CREW_PCLAIM = obj.TNVN_AUTO_CREW_PCLAIM == null ? null : obj.TNVN_AUTO_CREW_PCLAIM;
                    item.TNVN_AUTO_PSGR_NO = obj.TNVN_AUTO_PSGR_NO == null ? null : obj.TNVN_AUTO_PSGR_NO;
                    item.TNVN_AUTO_PSGR_PCLAIM_ID = obj.TNVN_AUTO_PSGR_PCLAIM_ID == null ? null : obj.TNVN_AUTO_PSGR_PCLAIM_ID;
                    item.TNVN_AUTO_PSGR_PCLAIM = obj.TNVN_AUTO_PSGR_PCLAIM == null ? null : obj.TNVN_AUTO_PSGR_PCLAIM;
                    item.TNVN_AUTO_PRICE = obj.TNVN_AUTO_PRICE == null ? null : obj.TNVN_AUTO_PRICE;
                    item.TNVN_AUTO_DE_PRICE = obj.TNVN_AUTO_DE_PRICE == null ? null : obj.TNVN_AUTO_DE_PRICE;
                    item.TNVN_AUTO_DE_RATIO = obj.TNVN_AUTO_DE_RATIO == null ? null : obj.TNVN_AUTO_DE_RATIO;
                    item.TNVN_AUTO_VAT = obj.TNVN_AUTO_VAT == null ? null : obj.TNVN_AUTO_VAT;
                    item.TNDSTN_ADD_AUTO_TYPE = obj.TNDSTN_ADD_AUTO_TYPE == null ? null : obj.TNDSTN_ADD_AUTO_TYPE;
                    item.TNDSTN_ADD_AUTO_PCLAIM_ID = obj.TNDSTN_ADD_AUTO_PCLAIM_ID == null ? null : obj.TNDSTN_ADD_AUTO_PCLAIM_ID;
                    item.TNDSTN_ADD_AUTO_ASSEST = obj.TNDSTN_ADD_AUTO_ASSEST == null ? null : obj.TNDSTN_ADD_AUTO_ASSEST;
                    item.TNDSTN_ADD_AUTO_CREW = obj.TNDSTN_ADD_AUTO_CREW == null ? null : obj.TNDSTN_ADD_AUTO_CREW;
                    item.TNDSTN_ADD_AUTO_PRICE = obj.TNDSTN_ADD_AUTO_PRICE == null ? null : obj.TNDSTN_ADD_AUTO_PRICE;
                    item.TNDSTN_ADD_AUTO_DE_PRICE = obj.TNDSTN_ADD_AUTO_DE_PRICE == null ? null : obj.TNDSTN_ADD_AUTO_DE_PRICE;
                    item.TNDSTN_ADD_AUTO_VAT = obj.TNDSTN_ADD_AUTO_VAT == null ? null : obj.TNDSTN_ADD_AUTO_VAT;
                    item.TNDSTN_ADD_AUTO_PSGR = obj.TNDSTN_ADD_AUTO_PSGR == null ? null : obj.TNDSTN_ADD_AUTO_PSGR;
                    item.TNDS_AUTO_LOAD_TYPE = obj.TNDS_AUTO_LOAD_TYPE == null ? null : obj.TNDS_AUTO_LOAD_TYPE;
                    item.TNDS_AUTO_LOAD_LMT = obj.TNDS_AUTO_LOAD_LMT == null ? null : obj.TNDS_AUTO_LOAD_LMT;
                    item.TNDS_AUTO_LOAD_DEDUCT = obj.TNDS_AUTO_LOAD_DEDUCT == null ? null : obj.TNDS_AUTO_LOAD_DEDUCT;
                    item.TNDS_AUTO_LOAD_OWN = string.IsNullOrEmpty(obj.TNDS_AUTO_LOAD_OWN) ? string.Empty : obj.TNDS_AUTO_LOAD_OWN;
                    item.TNDS_AUTO_LOAD_PRICE = obj.TNDS_AUTO_LOAD_PRICE == null ? null : obj.TNDS_AUTO_LOAD_PRICE;
                    item.TNDS_AUTO_LOAD_DE_PRICE = obj.TNDS_AUTO_LOAD_DE_PRICE == null ? null : obj.TNDS_AUTO_LOAD_DE_PRICE;
                    item.TNDS_AUTO_LOAD_VAT = obj.TNDS_AUTO_LOAD_VAT == null ? null : obj.TNDS_AUTO_LOAD_VAT;
                    item.VC_AUTO_VEH_VALUE = obj.VC_AUTO_VEH_VALUE == null ? null : obj.VC_AUTO_VEH_VALUE;
                    item.VS_AUTO_INSURE_VAL = obj.VS_AUTO_INSURE_VAL == null ? null : obj.VS_AUTO_INSURE_VAL;
                    item.VC_AUTO_DE_PRICE = obj.VC_AUTO_DE_PRICE == null ? null : obj.VC_AUTO_DE_PRICE;
                    item.VC_AUTO_DE_PRICE_RATIO = obj.VC_AUTO_DE_PRICE_RATIO == null ? null : obj.VC_AUTO_DE_PRICE_RATIO;
                    item.VC_AUTO_PRICE = obj.VC_AUTO_PRICE == null ? null : obj.VC_AUTO_PRICE;
                    item.VC_AUTO_VAT = obj.VC_AUTO_VAT == null ? null : obj.VC_AUTO_VAT;
                    item.TNVN_MOTO_PCLAIM_ID = obj.TNVN_MOTO_PCLAIM_ID == null ? null : obj.TNVN_MOTO_PCLAIM_ID;
                    item.TNVN_MOTO_PCLAIM = obj.TNVN_MOTO_PCLAIM == null ? null : obj.TNVN_MOTO_PCLAIM;
                    item.TNVN_MOTO_PRICE = obj.TNVN_MOTO_PRICE == null ? null : obj.TNVN_MOTO_PRICE;
                    item.TNVN_MOTO_DE_PRICE = obj.TNVN_MOTO_DE_PRICE == null ? null : obj.TNVN_MOTO_DE_PRICE;
                    item.TNVN_MOTO_VAT = obj.TNVN_MOTO_VAT == null ? null : obj.TNVN_MOTO_VAT;
                    item.VC_MOTO_VEH_VALUE = obj.VC_MOTO_VEH_VALUE == null ? null : obj.VC_MOTO_VEH_VALUE;
                    item.VC_MOTO_INSURE_VAL = obj.VC_MOTO_INSURE_VAL == null ? null : obj.VC_MOTO_INSURE_VAL;
                    item.VC_MOTO_CLAIM_DEDUCT = obj.VC_MOTO_CLAIM_DEDUCT == null ? null : obj.VC_MOTO_CLAIM_DEDUCT;
                    item.VC_AUTO_CLAIM_DEDUCT = obj.VC_AUTO_CLAIM_DEDUCT == null ? null : obj.VC_AUTO_CLAIM_DEDUCT;
                    item.VC_MOTO_PRICE = obj.VC_MOTO_PRICE == null ? null : obj.VC_MOTO_PRICE;
                    item.VC_MOTO_DE_PRICE = obj.VC_MOTO_DE_PRICE == null ? null : obj.VC_MOTO_DE_PRICE;
                    item.VC_MOTO_DE_RATIO = obj.VC_MOTO_DE_RATIO == null ? null : obj.VC_MOTO_DE_RATIO;
                    item.VC_MOTO_VAT = obj.VC_MOTO_VAT == null ? null : obj.VC_MOTO_VAT;
                    item.TNDSTN_ADD_MOTO_TYPE = obj.TNDSTN_ADD_MOTO_TYPE == null ? null : obj.TNDSTN_ADD_MOTO_TYPE;
                    item.TNDSTN_ADD_MOTO_PCLAIM_ID = obj.TNDSTN_ADD_MOTO_PCLAIM_ID == null ? null : obj.TNDSTN_ADD_MOTO_PCLAIM_ID;
                    item.TNDSTN_ADD_MOTO_ASSEST = obj.TNDSTN_ADD_MOTO_ASSEST == null ? null : obj.TNDSTN_ADD_MOTO_ASSEST;
                    item.TNDSTN_ADD_MOTO_CREW = obj.TNDSTN_ADD_MOTO_CREW == null ? null : obj.TNDSTN_ADD_MOTO_CREW;
                    item.TNDSTN_ADD_MOTO_PSGR = obj.TNDSTN_ADD_MOTO_PSGR == null ? null : obj.TNDSTN_ADD_MOTO_PSGR;
                    item.TNDSTN_ADD_PRICE = obj.TNDSTN_ADD_PRICE == null ? null : obj.TNDSTN_ADD_PRICE;
                    item.TNDSTN_ADD_DE_PRICE = obj.TNDSTN_ADD_DE_PRICE == null ? null : obj.TNDSTN_ADD_DE_PRICE;
                    item.TNDSTN_ADD_VAT = obj.TNDSTN_ADD_VAT == null ? null : obj.TNDSTN_ADD_VAT;
                    item.INPUTING_USER_ID = obj.INPUTING_USER_ID == null ? null : obj.INPUTING_USER_ID;

                    conn.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    conn.SaveChanges();
                }
                else
                {
                    CartedContractModelLocal objItem = new CartedContractModelLocal()
                    {
                        CUSTOMER_ID = obj.CUSTOMER_ID == null ? null : obj.CUSTOMER_ID,
                        CUST_TYPE = string.IsNullOrEmpty(obj.CUST_TYPE) ? string.Empty : obj.CUST_TYPE,
                        CUST_NAME = string.IsNullOrEmpty(obj.CUST_NAME) ? string.Empty : obj.CUST_NAME,
                        IDENTITY_NO = string.IsNullOrEmpty(obj.IDENTITY_NO) ? string.Empty : obj.IDENTITY_NO,
                        CUST_PHONE = string.IsNullOrEmpty(obj.CUST_PHONE) ? string.Empty : obj.CUST_PHONE,
                        CUST_EMAIL = string.IsNullOrEmpty(obj.CUST_EMAIL) ? string.Empty : obj.CUST_EMAIL,
                        CUST_ADDRESS = string.IsNullOrEmpty(obj.CUST_ADDRESS) ? string.Empty : obj.CUST_ADDRESS,
                        CUST_COUNTRY_ID = obj.CUST_COUNTRY_ID == null ? null : obj.CUST_COUNTRY_ID,
                        CUST_PROVINCE_ID = obj.CUST_PROVINCE_ID == null ? null : obj.CUST_PROVINCE_ID,
                        CUST_DISTRICT_ID = obj.CUST_DISTRICT_ID == null ? null : obj.CUST_DISTRICT_ID,
                        CUS_WARD_ID = obj.CUS_WARD_ID == null ? null : obj.CUS_WARD_ID,
                        BILL_NAME = string.IsNullOrEmpty(obj.BILL_NAME) ? string.Empty : obj.BILL_NAME,
                        BILL_IDENTITY_NO = string.IsNullOrEmpty(obj.BEN_IDENTITY_ID) ? string.Empty : obj.BILL_IDENTITY_NO,
                        BILL_COUNTRY_ID = obj.BILL_COUNTRY_ID == null ? null : obj.BILL_COUNTRY_ID,
                        BILL_PROVINCE_ID = obj.BILL_PROVINCE_ID == null ? null : obj.BILL_PROVINCE_ID,
                        BILL_DISTRICT_ID = obj.BILL_DISTRICT_ID == null ? null : obj.BILL_DISTRICT_ID,
                        BILL_WARD_ID = obj.BILL_WARD_ID == null ? null : obj.BILL_WARD_ID,
                        BILL_ADDRESS = string.IsNullOrEmpty(obj.BILL_ADDRESS) ? string.Empty : obj.BILL_ADDRESS,
                        BEN_NAME = string.IsNullOrEmpty(obj.BEN_NAME) ? string.Empty : obj.BEN_NAME,
                        BEN_IDENTITY_ID = obj.BEN_IDENTITY_ID == null ? null : obj.BEN_IDENTITY_ID,
                        BEN_PHONE = string.IsNullOrEmpty(obj.BEN_PHONE) ? string.Empty : obj.BEN_PHONE,
                    };

                    conn.cartedContractModelLocals.Add(objItem);
                    conn.SaveChanges();
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
