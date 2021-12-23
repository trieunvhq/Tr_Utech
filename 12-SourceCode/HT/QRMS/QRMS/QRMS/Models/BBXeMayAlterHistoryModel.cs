using System;
namespace QRMS.Models
{
    public class BBXeMayAlterHistoryModel
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
        public string OLD_VEH_PLATE_NO { get; set; } // Thông tin xe
        public string NEW_VEH_PLATE_NO { get; set; }
        public string OLD_VEH_FRAME_NO { get; set; }
        public string NEW_VEH_FRAME_NO { get; set; }
        public string OLD_VEH_ENGINE_NO { get; set; }
        public string NEW_VEH_ENGINE_NO { get; set; }
        public string OLD_VEH_TYPE_NAME { get; set; }
        public string NEW_VEH_TYPE_NAME { get; set; }
        public string OLD_VEH_PURPOSE_NAME { get; set; }
        public string NEW_VEH_PURPOSE_NAME { get; set; }
        public string OLD_BILL_NAME { get; set; } // Thông tin hóa đơn
        public string NEW_BILL_NAME { get; set; }
        public string OLD_BILL_COMPANY { get; set; }
        public string NEW_BILL_COMPANY { get; set; }
        public string OLD_BILL_TAXNO { get; set; }
        public string NEW_BILL_TAXNO { get; set; }
        public string OLD_BILL_PROVINCE_NAME { get; set; }
        public string NEW_BILL_PROVINCE_NAME { get; set; }
        public string OLD_BILL_DISTRICT_NAME { get; set; }
        public string NEW_BILL_DISTRICT_NAME { get; set; }
        public string OLD_BILL_WARD_NAME { get; set; }
        public string NEW_BILL_WARD_NAME { get; set; }
        public string OLD_BILL_ADDRESS { get; set; }
        public string NEW_BILL_ADDRESS { get; set; }
        public decimal? OLD_TNDSBB_MOTO_PRICE { get; set; } // tnds bắt buộc
        public decimal? NEW_TNDSBB_MOTO_PRICE { get; set; }
        public decimal? OLD_TNVN_MOTO_PRICE { get; set; } // tai nạn người ngồi
        public decimal? NEW_TNVN_MOTO_PRICE { get; set; }
        public short? OLD_VEH_SEAT_NO { get; set; }
        public short? NEW_VEH_SEAT_NO { get; set; }
        public decimal? OLD_TNDSTN_ADD_MOTO_CREW { get; set; } // tndstn tăng thêm
        public decimal? NEW_TNDSTN_ADD_MOTO_CREW { get; set; }
        public decimal? OLD_TNDSTN_ADD_MOTO_ASSEST { get; set; }
        public decimal? NEW_TNDSTN_ADD_MOTO_ASSEST { get; set; }
        //public decimal? OLD_TNDSBB_MOTO_PRICE { get; set; } // Phí bảo hiểm
        //public decimal? NEW_TNDSBB_MOTO_PRICE { get; set; }
        public decimal? OLD_TNDSBB_MOTO_DE_PRICE { get; set; }
        public decimal? NEW_TNDSBB_MOTO_DE_PRICE { get; set; }
        public decimal? OLD_TNDSBB_MOTO_VAT { get; set; }
        public decimal? NEW_TNDSBB_MOTO_VAT { get; set; }
        //public decimal? OLD_TNVN_MOTO_PRICE { get; set; }
        //public decimal? NEW_TNVN_MOTO_PRICE { get; set; }
        public decimal? OLD_TNDSTN_ADD_PRICE { get; set; }
        public decimal? NEW_TNDSTN_ADD_PRICE { get; set; }
        public decimal? OLD_TNDSTN_ADD_DE_PRICE { get; set; }
        public decimal? NEW_TNDSTN_ADD_DE_PRICE { get; set; }
        public decimal? OLD_TNDSTN_ADD_VAT { get; set; }
        public decimal? NEW_TNDSTN_ADD_VAT { get; set; }
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
