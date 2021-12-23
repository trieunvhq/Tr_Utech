using System;
using System.Collections.Generic;

namespace QRMS.Models
{
    public class PrivateHouseAlterHistoryModel
    {
        public int ID { get; set; }
        public string OLD_CONTRACT_CODE { get; set; } // Thông tin chung
        public string NEW_CONTRACT_CODE { get; set; }
        public int? OLD_CONTRACT_STATUS { get; set; }
        public int? NEW_CONTRACT_STATUS { get; set; }
        public DateTime? OLD_CONTRACT_ISSUED_DATE { get; set; }
        public DateTime? NEW_CONTRACT_ISSUED_DATE { get; set; }
        public DateTime? OLD_CONTRACT_START_DATE { get; set; }
        public DateTime? NEW_CONTRACT_START_DATE { get; set; }
        public DateTime? OLD_CONTRACT_EXPIRE_DATE { get; set; }
        public DateTime? NEW_CONTRACT_EXPIRE_DATE { get; set; }
        public string OLD_CUST_NAME { get; set; } // Thông tin khách hàng
        public string NEW_CUST_NAME { get; set; }
        public string OLD_IDENTITY_NO { get; set; }
        public string NEW_IDENTITY_NO { get; set; }
        public string OLD_CUST_PHONE { get; set; }
        public string NEW_CUST_PHONE { get; set; }
        public string OLD_CUST_EMAIL { get; set; }
        public string NEW_CUST_EMAIL { get; set; }
        public string OLD_CUST_PROVINCE_NAME { get; set; }
        public string NEW_CUST_PROVINCE_NAME { get; set; }
        public string OLD_CUST_DISTRICT_NAME { get; set; }
        public string NEW_CUST_DISTRICT_NAME { get; set; }
        public string OLD_CUST_WARD_NAME { get; set; }
        public string NEW_CUST_WARD_NAME { get; set; }
        public string OLD_CUST_ADDRESS { get; set; }
        public string NEW_CUST_ADDRESS { get; set; }
        public List<InsuredPersonInfoModel> OLD_LIST_INSURED_PERSON { get; set; } // Người được bảo hiểm
        public List<InsuredPersonInfoModel> NEW_LIST_INSURED_PERSON { get; set; }
        public string OLD_BEN_NAME { get; set; } // Người thụ hưởng
        public string NEW_BEN_NAME { get; set; }
        public string OLD_BEN_IDENTITY_ID { get; set; }
        public string NEW_BEN_IDENTITY_ID { get; set; }
        public string OLD_BEN_PHONE { get; set; }
        public string NEW_BEN_PHONE { get; set; }
        public string OLD_BEN_EMAIL { get; set; }
        public string NEW_BEN_EMAIL { get; set; }
        public string OLD_BEN_ADDRESS { get; set; }
        public string NEW_BEN_ADDRESS { get; set; }
        public decimal? OLD_HINS_HOME_INSUR_VALUE { get; set; } // Nhà tư nhân // cho ngôi nhà
        public decimal? NEW_HINS_HOME_INSUR_VALUE { get; set; }
        // phạm vi

        // điều khoản

        public decimal? OLD_HINS_INSUR_WITHOUT_VAT { get; set; }
        public decimal? NEW_HINS_INSUR_WITHOUT_VAT { get; set; }
        public decimal? OLD_HINS_DEDUCTION_VALUE { get; set; }
        public decimal? NEW_HINS_DEDUCTION_VALUE { get; set; }
        // vat

        public decimal? OLD_HINS_ASSETS_VALUE { get; set; } // cho tài sản ngôi nhà
        public decimal? NEW_HINS_ASSETS_VALUE { get; set; }
        public decimal? OLD_HINS_ASSETS_WITHOUT_VAT { get; set; }
        public decimal? NEW_HINS_ASSETS_WITHOUT_VAT { get; set; }
        //public decimal? OLD_HINS_DEDUCTION_VALUE { get; set; }
        //public decimal? NEW_HINS_DEDUCTION_VALUE { get; set; }
        // vat

        public decimal? OLD_INSURED_TOTAL_VAL { get; set; } // Tổng
        public decimal? NEW_INSURED_TOTAL_VAL { get; set; }
        public decimal? OLD_TOTAL_DEDUCT_PRICE { get; set; }
        public decimal? NEW_TOTAL_DEDUCT_PRICE { get; set; }
        public decimal? OLD_DEDUCT_TOTAL_VAL { get; set; }
        public decimal? NEW_DEDUCT_TOTAL_VAL { get; set; }
        public float? OLD_TOTAL_VAT { get; set; }
        public float? NEW_TOTAL_VAT { get; set; }
        public decimal? OLD_TOTAL_PRICE { get; set; } // Tổng số tiền
        public decimal? NEW_TOTAL_PRICE { get; set; }


    }
}
