using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class BaseContractModel : Notifiable
    {
        public int ID { get; set; }

        #region thông tin khách hàng
        public int? CUSTOMER_ID { get; set; }
        public string CUST_CODE { get; set; }
        public int? CUST_ACCOUNT_ID { get; set; }
        public string CUST_TYPE { get; set; }
        public string CUST_NAME { get; set; }
        public string REPRESENTATIVE_NAME { get; set; }
        public string IDENTITY_NO { get; set; }
        public string TAX_NO { get; set; }
        public string CUST_PHONE { get; set; }//  SĐT
        public string CUST_EMAIL { get; set; }// Email
        public string CUST_ADDRESS { get; set; }
        public int? CUST_COUNTRY_ID { get; set; }
        public string CUST_COUNTRY_NAME { get; set; }
        public int? CUST_PROVINCE_ID { get; set; }
        public string CUST_PROVINCE_NAME { get; set; }
        public int? CUST_DISTRICT_ID { get; set; }
        public string CUST_DISTRICT_NAME { get; set; }
        public int? CUS_WARD_ID { get; set; }
        public string CUST_WARD_NAME { get; set; }
        public string IDENTITY_TYPE { get; set; }
        public DateTime? IDENTITY_ISSUE_DATE { get; set; }
        public string IDENTITY_ISSUE_OFFICE { get; set; }
        public string NATIONALITY { get; set; }
        public string CUST_SEX { get; set; }
        public DateTime? DOB { get; set; }
        public string CUST_COUNTRY_CODE { get; set; }
        public string CUST_PROVINCE_CODE { get; set; }
        public string CUST_DISTRICT_CODE { get; set; }
        public string CUS_WARD_CODE { get; set; }
        public string CUS_WARD_NAME { get; set; }

        #endregion

        #region thông tin người thụ hưởng
        public string BEN_NAME { get; set; }
        public string BEN_IDENTITY_ID { get; set; }
        public DateTime? BEN_IDENTITY_ISSUE_DATE { get; set; }
        public string BEN_IDENTITY_ISSUE_OFFICE { get; set; }
        public string BEN_NATIONALITY { get; set; }
        public DateTime? BEN_DOB { get; set; }
        public string BEN_PHONE { get; set; }
        public string BEN_EMAIL { get; set; }
        public string BEN_ADDRESS { get; set; }
        public int? BEN_COUNTRY_ID { get; set; }
        public int? BEN_PROVINCE_ID { get; set; }
        public int? BEN_DISTRICT_ID { get; set; }
        public int? BEN_WARD_ID { get; set; }
        public string BEN_SEX { get; set; }
        public string BEN_COUNTRY_CODE { get; set; }
        public string BEN_COUNTRY_NAME { get; set; }
        public string BEN_PROVINCE_CODE { get; set; }
        public string BEN_PROVINCE_NAME { get; set; }
        public string BEN_DISTRICT_CODE { get; set; }
        public string BEN_DISTRICT_NAME { get; set; }
        public string BEN_WARD_CODE { get; set; }
        public string BEN_WARD_NAME { get; set; }
        public int? BEN_RELATIONSHIP_ID { get; set; }
        public string BEN_RELATIONSHIP_CODE { get; set; }
        public string BEN_RELATIONSHIP_NAME { get; set; }

        #endregion

        #region thông tin người nhận hóa đơn
        public int? INPUTING_USER_ID { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL_IDENTITY_NO { get; set; }
        public string BILL_ADDRESS { get; set; }
        public string BILL_IDENTITY_TYPE { get; set; }
        public string BILL_COMPANY { get; set; }
        public string BILL_TAXNO { get; set; }
        public string BILL_COMPANY_ADDRESS { get; set; }
        public int? BILL_COUNTRY_ID { get; set; }
        public string BILL_COUNTRY_CODE { get; set; }
        public string BILL_COUNTRY_NAME { get; set; }
        public int? BILL_PROVINCE_ID { get; set; }
        public string BILL_PROVINCE_CODE { get; set; }
        public string BILL_PROVINCE_NAME { get; set; }
        public int? BILL_DISTRICT_ID { get; set; }
        public string BILL_DISTRICT_CODE { get; set; }
        public string BILL_DISTRICT_NAME { get; set; }
        public int? BILL_WARD_ID { get; set; }
        public string BILL_WARD_CODE { get; set; }
        public string BILL_WARD_NAME { get; set; }

        #endregion

        #region thông tin CBBH và Đại lý
        public int? SALE_ACCOUNT_ID { get; set; }
        public string SALE_ACCOUNT_CODE { get; set; }
        public string SALE_ACCOUNT_NAME { get; set; }
        public string SALE_PERSONEL_CODE { get; set; }
        public int? AGENT_ID { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public int? APPROVAL_AGENT_ID { get; set; }
        public string APPROVAL_AGENT_CODE { get; set; }
        public string APPROVAL_AGENT_NAME { get; set; }
        public int? DIVN_ID { get; set; }
        public string DIVN_CODE { get; set; }
        public string DIVN_NAME { get; set; }
        public int? DEPT_ID { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }

        #endregion

        #region thông tin chung bảo hiểm

        #region thông tin hiệu lực
        public string CONTRACT_CERT_NO { get; set; }
        public string CONTRACT_PRINT_NO { get; set; }
        public DateTime? CONTRACT_ISSUED_DATE { get; set; }
        public DateTime? CONTRACT_START_DATE { get; set; }
        public DateTime? CONTRACT_EXPIRE_DATE { get; set; }
        public string CONTRACT_ISSUE_TYPE { get; set; }
        #endregion

        #region thông tin tình trạng đơn
        public int? INSUR_PRODUCT_ID { get; set; }
        public string INSUR_PRODUCT_CODE { get; set; }
        public string INSUR_PRODUCT_NAME { get; set; }
        public string INSUR_PRODUCT_TYPE { get; set; }

        public int? CONTRACT_STATUS { get; set; }
        public int? CONTRACT_STATUS_ID { get; set; }
        public string CONTRACT_STATUS_CODE { get; set; }
        public string CONTRACT_STATUS_NAME { get; set; }
        public string CONTRACT_CODE { get; set; }
        public string CONTRACT_PARENTS { get; set; }
        public short? ALTER_STATUS { get; set; }
        public short? ALTER_TIMES { get; set; }


        #region thông tin đơn nháp được clone từ đơn chính
        public int? CartContractID { get; set; }
        public string CartContractIssueType { get; set; }
        public int? CartContractStatus { get; set; }
        #endregion
        #endregion

        #region thông tin phí và giá trị hợp đồng
        public decimal? INSURED_TOTAL_VAL { get; set; }
        public decimal? DEDUCT_TOTAL_VAL { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public decimal? TOTAL_DEDUCT_PRICE { get; set; }
        public decimal? TOTAL_DEDUCT_RATIO { get; set; }
        public float? TOTAL_VAT { get; set; }
        public decimal? TOTAL_ALTERED_PRICE { get; set; }
        public decimal? TOTAL_FINAL_PRICE { get; set; }
        public int? PAY_METHOD { get; set; }
        public DateTime? PAY_DUE_DATE { get; set; }
        #endregion

        #endregion

        #region chương trình khuyến mãi
        public decimal? DISCOUNT { get; set; }
        public int? DISCOUNT_PROG_ID { get; set; }
        public string DISCOUNT_PROG_NAME { get; set; }
        public decimal? DISCOUNT_RATIO { get; set; }
        #endregion

        #region thông tin bản ghi
        public string STATUS_RECORD { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public int? UPDATE_USER_ID { get; set; }
        #endregion

    }

    public class BaseAlterContract : Notifiable
    {

        public bool ID { get; set; } = false;   
        public Color ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(ID); } }

        #region thông tin khách hàng
        public bool CUSTOMER_ID { get; set; } = false;
        public bool CUST_CODE { get; set; } = false;
        public bool CUST_ACCOUNT_ID { get; set; } = false;
        public bool CUST_TYPE { get; set; } = false;
        public bool CUST_NAME { get; set; } = false;
        public bool REPRESENTATIVE_NAME { get; set; } = false;
        public bool IDENTITY_NO { get; set; } = false;
        public bool TAX_NO { get; set; } = false;
        public bool CUST_PHONE { get; set; } = false;//  SĐT
        public bool CUST_EMAIL { get; set; } = false;// Email
        public bool CUST_ADDRESS { get; set; } = false;
        public bool CUST_COUNTRY_NAME { get; set; } = false;
        public bool CUST_PROVINCE_ID { get; set; } = false;
        public bool CUST_PROVINCE_NAME { get; set; } = false;
        public bool CUST_DISTRICT_ID { get; set; } = false;
        public bool CUST_DISTRICT_NAME { get; set; } = false;
        public bool CUS_WARD_ID { get; set; } = false;
        public bool CUST_WARD_NAME { get; set; } = false;
        public bool IDENTITY_TYPE { get; set; } = false;
        public bool IDENTITY_ISSUE_DATE { get; set; } = false;
        public bool IDENTITY_ISSUE_OFFICE { get; set; } = false;
        public bool NATIONALITY { get; set; } = false;
        public bool CUST_SEX { get; set; } = false;
        public bool DOB { get; set; } = false;
        public bool CUST_COUNTRY_CODE { get; set; } = false;
        public bool CUST_PROVINCE_CODE { get; set; } = false;
        public bool CUST_DISTRICT_CODE { get; set; } = false;
        public bool CUS_WARD_CODE { get; set; } = false;
        public bool CUS_WARD_NAME { get; set; } = false;

        #endregion

        #region thông tin người thụ hưởng
        public bool BEN_NAME { get; set; } = false;
        public bool BEN_IDENTITY_ID { get; set; } = false;
        public bool BEN_IDENTITY_ISSUE_DATE { get; set; } = false;
        public bool BEN_IDENTITY_ISSUE_OFFICE { get; set; } = false;
        public bool BEN_NATIONALITY { get; set; } = false;
        public bool BEN_DOB { get; set; } = false;
        public bool BEN_PHONE { get; set; } = false;
        public bool BEN_EMAIL { get; set; } = false;
        public bool BEN_ADDRESS { get; set; } = false;
        public bool BEN_COUNTRY_ID { get; set; } = false;
        public bool BEN_PROVINCE_ID { get; set; } = false;
        public bool BEN_DISTRICT_ID { get; set; } = false;
        public bool BEN_WARD_ID { get; set; } = false;
        public bool BEN_SEX { get; set; } = false;
        public bool BEN_COUNTRY_CODE { get; set; } = false;
        public bool BEN_COUNTRY_NAME { get; set; } = false;
        public bool BEN_PROVINCE_CODE { get; set; } = false;
        public bool BEN_PROVINCE_NAME { get; set; } = false;
        public bool BEN_DISTRICT_CODE { get; set; } = false;
        public bool BEN_DISTRICT_NAME { get; set; } = false;
        public bool BEN_WARD_CODE { get; set; } = false;
        public bool BEN_WARD_NAME { get; set; } = false;
        public bool BEN_RELATIONSHIP_ID { get; set; } = false;
        public bool BEN_RELATIONSHIP_CODE { get; set; } = false;
        public bool BEN_RELATIONSHIP_NAME { get; set; } = false;

        #endregion

        #region thông tin người nhận hóa đơn
        public bool INPUTING_USER_ID { get; set; } = false;
        public bool BILL_NAME { get; set; } = false;
        public bool BILL_IDENTITY_NO { get; set; } = false;
        public bool BILL_ADDRESS { get; set; } = false;
        public bool BILL_IDENTITY_TYPE { get; set; } = false;
        public bool BILL_COMPANY { get; set; } = false;
        public bool BILL_TAXNO { get; set; } = false;
        public bool BILL_COMPANY_ADDRESS { get; set; } = false;
        public bool BILL_COUNTRY_ID { get; set; } = false;
        public bool BILL_COUNTRY_CODE { get; set; } = false;
        public bool BILL_COUNTRY_NAME { get; set; } = false;
        public bool BILL_PROVINCE_ID { get; set; } = false;
        public bool BILL_PROVINCE_CODE { get; set; } = false;
        public bool BILL_PROVINCE_NAME { get; set; } = false;
        public bool BILL_DISTRICT_ID { get; set; } = false;
        public bool BILL_DISTRICT_CODE { get; set; } = false;
        public bool BILL_DISTRICT_NAME { get; set; } = false;
        public bool BILL_WARD_ID { get; set; } = false;
        public bool BILL_WARD_CODE { get; set; } = false;
        public bool BILL_WARD_NAME { get; set; } = false;

        #endregion

        #region thông tin CBBH và Đại lý
        public bool SALE_ACCOUNT_ID { get; set; } = false;
        public bool SALE_ACCOUNT_CODE { get; set; } = false;
        public bool SALE_ACCOUNT_NAME { get; set; } = false;
        public bool SALE_PERSONEL_CODE { get; set; } = false;
        public bool AGENT_ID { get; set; } = false;
        public bool AGENT_CODE { get; set; } = false;
        public bool AGENT_NAME { get; set; } = false;
        public bool APPROVAL_AGENT_ID { get; set; } = false;
        public bool APPROVAL_AGENT_CODE { get; set; } = false;
        public bool APPROVAL_AGENT_NAME { get; set; } = false;
        public bool DIVN_ID { get; set; } = false;
        public bool DIVN_CODE { get; set; } = false;
        public bool DIVN_NAME { get; set; } = false;
        public bool DEPT_ID { get; set; } = false;
        public bool DEPT_CODE { get; set; } = false;
        public bool DEPT_NAME { get; set; } = false;

        #endregion

        #region thông tin chung bảo hiểm

        #region thông tin hiệu lực
        public bool CONTRACT_CERT_NO { get; set; } = false;
        public bool CONTRACT_PRbool_NO { get; set; } = false;
        public bool CONTRACT_ISSUED_DATE { get; set; } = false;
        public bool CONTRACT_START_DATE { get; set; } = false;
        public bool CONTRACT_EXPIRE_DATE { get; set; } = false;
        public bool CONTRACT_ISSUE_TYPE { get; set; } = false;
        #endregion

        #region thông tin tình trạng đơn
        public bool INSUR_PRODUCT_ID { get; set; } = false;
        public bool INSUR_PRODUCT_CODE { get; set; } = false;
        public bool INSUR_PRODUCT_NAME { get; set; } = false;
        public bool INSUR_PRODUCT_TYPE { get; set; } = false;

        public bool CONTRACT_STATUS { get; set; } = false;
        public bool CONTRACT_STATUS_ID { get; set; } = false;
        public bool CONTRACT_STATUS_CODE { get; set; } = false;
        public bool CONTRACT_STATUS_NAME { get; set; } = false;
        public bool CONTRACT_CODE { get; set; } = false;
        public bool CONTRACT_PARENTS { get; set; } = false;
        public bool ALTER_STATUS { get; set; } = false;
        public bool ALTER_TIMES { get; set; } = false;


        #region thông tin đơn nháp được clone từ đơn chính
        public bool CartContractID { get; set; } = false;
        public bool CartContractIssueType { get; set; } = false;
        public bool CartContractStatus { get; set; } = false;
        #endregion
        #endregion

        #region thông tin phí và giá trị hợp đồng
        public bool INSURED_TOTAL_VAL { get; set; } = false;
        public bool DEDUCT_TOTAL_VAL { get; set; } = false;
        public bool TOTAL_PRICE { get; set; } = false;
        public bool TOTAL_DEDUCT_PRICE { get; set; } = false;
        public bool TOTAL_DEDUCT_RATIO { get; set; } = false;
        public bool TOTAL_VAT { get; set; } = false;
        public bool TOTAL_ALTERED_PRICE { get; set; } = false;
        public bool TOTAL_FINAL_PRICE { get; set; } = false;
        public bool PAY_METHOD { get; set; } = false;
        public bool PAY_DUE_DATE { get; set; } = false;
        #endregion

        #endregion

        #region chương trình khuyến mãi
        public bool DISCOUNT { get; set; } = false;
        public bool DISCOUNT_PROG_ID { get; set; } = false;
        public bool DISCOUNT_PROG_NAME { get; set; } = false;
        public bool DISCOUNT_RATIO { get; set; } = false;
        #endregion

        #region thông tin bản ghi
        public bool STATUS_RECORD { get; set; } = false;
        public bool CREATE_DATE { get; set; } = false;
        public bool CREATE_USER_ID { get; set; } = false;
        public bool UPDATE_DATE { get; set; } = false;
        public bool UPDATE_USER_ID { get; set; } = false;
        #endregion

        #region color
        public Color CUSTOMER_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUSTOMER_ID); } }
        public Color CUST_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_CODE); } }
        public Color CUST_ACCOUNT_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_ACCOUNT_ID); } }
        public Color CUST_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_TYPE); } }
        public Color CUST_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_NAME); } }
        public Color REPRESENTATIVE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(REPRESENTATIVE_NAME); } }
        public Color IDENTITY_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(IDENTITY_NO); } }
        public Color TAX_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TAX_NO); } }
        public Color CUST_PHONE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_PHONE); } }
        public Color CUST_EMAIL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_EMAIL); } }
        public Color CUST_ADDRESS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_ADDRESS); } }
        public Color CUST_COUNTRY_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_COUNTRY_NAME); } }
        public Color CUST_PROVINCE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_PROVINCE_NAME); } }
        public Color CUST_DISTRICT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_DISTRICT_NAME); } }
        public Color CUST_WARD_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_WARD_NAME); } }
        public Color IDENTITY_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(IDENTITY_TYPE); } }
        public Color IDENTITY_ISSUE_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(IDENTITY_ISSUE_DATE); } }
        public Color IDENTITY_ISSUE_OFFICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(IDENTITY_ISSUE_OFFICE); } }
        public Color NATIONALITY_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(NATIONALITY); } }
        public Color CUST_SEX_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUST_SEX); } }
        public Color DOB_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DOB); } }
        public Color CUS_WARD_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CUS_WARD_NAME); } }




        public Color BEN_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_NAME); } }
        public Color BEN_IDENTITY_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_IDENTITY_ID); } }
        public Color BEN_IDENTITY_ISSUE_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_IDENTITY_ISSUE_DATE); } }
        public Color BEN_IDENTITY_ISSUE_OFFICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_IDENTITY_ISSUE_OFFICE); } }
        public Color BEN_NATIONALITY_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_NATIONALITY); } }
        public Color BEN_DOB_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_DOB); } }
        public Color BEN_PHONE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_PHONE); } }
        public Color BEN_EMAIL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_EMAIL); } }
        public Color BEN_ADDRESS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_ADDRESS); } }
        public Color BEN_WARD_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_WARD_ID); } }
        public Color BEN_SEX_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_SEX); } }
        public Color BEN_COUNTRY_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_COUNTRY_NAME); } }
        public Color BEN_PROVINCE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_PROVINCE_NAME); } }
        public Color BEN_DISTRICT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_DISTRICT_NAME); } }
        public Color BEN_WARD_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_WARD_NAME); } }
        public Color BEN_RELATIONSHIP_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BEN_RELATIONSHIP_NAME); } }


        public Color INPUTING_USER_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INPUTING_USER_ID); } }
        public Color BILL_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_NAME); } }
        public Color BILL_IDENTITY_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_IDENTITY_NO); } }
        public Color BILL_ADDRESS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_ADDRESS); } }
        public Color BILL_IDENTITY_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_IDENTITY_TYPE); } }
        public Color BILL_COMPANY_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_COMPANY); } }
        public Color BILL_TAXNO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_TAXNO); } }
        public Color BILL_COMPANY_ADDRESS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_COMPANY_ADDRESS); } }
        public Color BILL_COUNTRY_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_COUNTRY_CODE); } }
        public Color BILL_COUNTRY_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_COUNTRY_NAME); } }
        public Color BILL_PROVINCE_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_PROVINCE_CODE); } }
        public Color BILL_PROVINCE_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_PROVINCE_NAME); } }
        public Color BILL_DISTRICT_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_DISTRICT_CODE); } }
        public Color BILL_DISTRICT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_DISTRICT_NAME); } }
        public Color BILL_WARD_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_WARD_CODE); } }
        public Color BILL_WARD_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(BILL_WARD_NAME); } }




        public Color SALE_ACCOUNT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(SALE_ACCOUNT_NAME); } }
        public Color AGENT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(AGENT_NAME); } }
        public Color APPROVAL_AGENT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(APPROVAL_AGENT_NAME); } }
        public Color DIVN_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DIVN_NAME); } }
        public Color DEPT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DEPT_NAME); } }






        public Color CONTRACT_CERT_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_CERT_NO); } }
        public Color CONTRACT_PRbool_NO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_PRbool_NO); } }
        public Color CONTRACT_ISSUED_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_ISSUED_DATE); } }
        public Color CONTRACT_START_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_START_DATE); } }
        public Color CONTRACT_EXPIRE_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_EXPIRE_DATE); } }
        public Color CONTRACT_ISSUE_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_ISSUE_TYPE); } }



        public Color INSUR_PRODUCT_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSUR_PRODUCT_ID); } }
        public Color INSUR_PRODUCT_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSUR_PRODUCT_CODE); } }
        public Color INSUR_PRODUCT_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSUR_PRODUCT_NAME); } }
        public Color INSUR_PRODUCT_TYPE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSUR_PRODUCT_TYPE); } }

        public Color CONTRACT_STATUS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_STATUS); } }
        public Color CONTRACT_STATUS_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_STATUS_ID); } }
        public Color CONTRACT_STATUS_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_STATUS_CODE); } }
        public Color CONTRACT_STATUS_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_STATUS_NAME); } }
        public Color CONTRACT_CODE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_CODE); } }
        public Color CONTRACT_PARENTS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CONTRACT_PARENTS); } }
        public Color ALTER_STATUS_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(ALTER_STATUS); } }
        public Color ALTER_TIMES_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(ALTER_TIMES); } }



        public Color CartContractID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CartContractID); } }
        public Color CartContractIssueType_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CartContractIssueType); } }
        public Color CartContractStatus_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(CartContractStatus); } }




        public Color INSURED_TOTAL_VAL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(INSURED_TOTAL_VAL); } }
        public Color DEDUCT_TOTAL_VAL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DEDUCT_TOTAL_VAL); } }
        public Color TOTAL_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_PRICE); } }
        public Color TOTAL_DEDUCT_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_DEDUCT_PRICE); } }
        public Color TOTAL_DEDUCT_RATIO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_DEDUCT_RATIO); } }
        public Color TOTAL_VAT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_VAT); } }
        public Color TOTAL_ALTERED_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_ALTERED_PRICE); } }
        public Color TOTAL_FINAL_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(TOTAL_FINAL_PRICE); } }
        public Color PAY_METHOD_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(PAY_METHOD); } }
        public Color PAY_DUE_DATE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(PAY_DUE_DATE); } }





        public Color DISCOUNT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DISCOUNT); } }
        public Color DISCOUNT_PROG_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DISCOUNT_PROG_ID); } }
        public Color DISCOUNT_PROG_NAME_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DISCOUNT_PROG_NAME); } }
        public Color DISCOUNT_RATIO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(DISCOUNT_RATIO); } }

        #endregion

        #region MyRegion
        public BaseAlterContract() { }
        public BaseAlterContract(BaseContractModel oldContract, BaseContractModel newContract)
        {
            try
            {
                var lstProp = GetType().GetProperties();
                for (int i = 0; i < lstProp.Length; i++)
                {
                    if (lstProp[i].SetMethod == null) { continue; }
                    var _oldValue = oldContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(oldContract);
                    var _newValue = newContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(newContract);

                    var isChange = _oldValue?.ToString() != _newValue?.ToString();
                    lstProp[i].SetValue(this, isChange);
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion
    }

    public class ContractModel: BaseContractModel
    {
        #region
        public string VEH_TYPE_NAME { get; set; }
        public string VEH_ORIGIN_NAME { get; set; }
        public string TNDS_AUTO_LOAD_TYPE_NAME { get; set; }
        public decimal? TNDS_AUTO_MAX_INS_VAL { get; set; }

        #endregion

        public string VEH_PLATE_NO { get; set; }
        public string VEH_FRAME_NO { get; set; }
        public string VEH_ENGINE_NO { get; set; }
        public int? VEH_BRAND_ID { get; set; }
        public string VEH_BRAND_NAME { get; set; }
        public int? VEH_MODEL_ID { get; set; }
        public string VEH_MODEL_NAME { get; set; }
        public int? VEH_TYPE_ID { get; set; }
        public decimal? VEH_CUBE_CAPACITY { get; set; }
        public decimal? VEH_MAX_LOAD { get; set; }
        public short? VEH_SEAT_NO { get; set; }
        public int? VEH_ORIGIN { get; set; }
        public DateTime? VEH_FIRST_REG_DATE { get; set; }
        public int? VEH_PURPOSE_ID { get; set; }
        public string VEH_PURPOSE { get; set; }
        public decimal? VEH_VALUE { get; set; }
        public decimal? TNDSBB_AUTO_PRICE { get; set; }
        public decimal? TNDSBB_AUTO_DE_PRICE { get; set; }
        public decimal? TNSDBB_AUTO_VAT { get; set; }
        public decimal? TNDSBB_MOTO_PRICE { get; set; }
        public decimal? TNDSBB_MOTO_DE_PRICE { get; set; }
        public decimal? TNDSBB_MOTO_VAT { get; set; }
        public short? TNVN_AUTO_CREW_NO { get; set; }
        public int? TNVN_AUTO_CREW_PCLAIM_ID { get; set; }
        public decimal? TNVN_AUTO_CREW_PCLAIM { get; set; }
        public short? TNVN_AUTO_PSGR_NO { get; set; }
        public int? TNVN_AUTO_PSGR_PCLAIM_ID { get; set; }
        public decimal? TNVN_AUTO_PSGR_PCLAIM { get; set; }
        public decimal? TNVN_AUTO_PRICE { get; set; }
        public decimal? TNVN_AUTO_DE_PRICE { get; set; }
        public float? TNVN_AUTO_DE_RATIO { get; set; }
        public decimal? TNVN_AUTO_VAT { get; set; }
        public int? TNDSTN_ADD_AUTO_TYPE { get; set; }
        public int? TNDSTN_ADD_AUTO_PCLAIM_ID { get; set; }
        public decimal? TNDSTN_ADD_AUTO_ASSEST { get; set; }
        public decimal? TNDSTN_ADD_AUTO_CREW { get; set; }
        public decimal? TNDSTN_ADD_AUTO_PRICE { get; set; }
        public decimal? TNDSTN_ADD_AUTO_DE_PRICE { get; set; }
        public decimal? TNDSTN_ADD_AUTO_VAT { get; set; }
        public decimal? TNDSTN_ADD_AUTO_PSGR { get; set; }
        public int? TNDS_AUTO_LOAD_TYPE { get; set; }
        public decimal? TNDS_AUTO_LOAD_LMT { get; set; }
        public decimal? TNDS_AUTO_LOAD_DEDUCT { get; set; }
        public string TNDS_AUTO_LOAD_OWN { get; set; }
        public decimal? TNDS_AUTO_LOAD_PRICE { get; set; }
        public decimal? TNDS_AUTO_LOAD_DE_PRICE { get; set; }
        public decimal? TNDS_AUTO_LOAD_VAT { get; set; }
        public decimal? VC_AUTO_VEH_VALUE { get; set; }
        public decimal? VS_AUTO_INSURE_VAL { get; set; }
        public decimal? VC_AUTO_DE_PRICE { get; set; }
        public decimal? VC_AUTO_DE_PRICE_RATIO { get; set; }
        public decimal? VC_AUTO_PRICE { get; set; }
        public decimal? VC_AUTO_VAT { get; set; }
        public int? TNVN_MOTO_PCLAIM_ID { get; set; }
        public decimal? TNVN_MOTO_PCLAIM { get; set; }
        public decimal? TNVN_MOTO_PRICE { get; set; }
        public decimal? TNVN_MOTO_DE_PRICE { get; set; }
        public decimal? TNVN_MOTO_VAT { get; set; }
        public decimal? VC_MOTO_VEH_VALUE { get; set; }
        public decimal? VC_MOTO_INSURE_VAL { get; set; }
        public decimal? VC_MOTO_CLAIM_DEDUCT { get; set; }
        public decimal? VC_AUTO_CLAIM_DEDUCT { get; set; }
        public decimal? VC_MOTO_PRICE { get; set; }
        public decimal? VC_MOTO_DE_PRICE { get; set; }
        public decimal? VC_MOTO_DE_RATIO { get; set; }
        public decimal? VC_MOTO_VAT { get; set; }
        public int? TNDSTN_ADD_MOTO_TYPE { get; set; }
        public int? TNDSTN_ADD_MOTO_PCLAIM_ID { get; set; }
        public decimal? TNDSTN_ADD_MOTO_ASSEST { get; set; }
        public decimal? TNDSTN_ADD_MOTO_CREW { get; set; }
        public decimal? TNDSTN_ADD_MOTO_PSGR { get; set; }
        public decimal? TNDSTN_ADD_PRICE { get; set; }
        public decimal? TNDSTN_ADD_DE_PRICE { get; set; }
        public decimal? TNDSTN_ADD_VAT { get; set; }
        public string TNDS_AUTO_LOAD_TYPE_CODE { get; set; }
        public string TNDS_AUTO_LOAD_NAME { get; set; }
        public decimal? TNDSTN_ADD_AUTO_PCLAIM { get; set; }
        public decimal? LOAD_WEIGHT { get; set; }
        public string VEH_PUPOSE_CODE { get; set; }
        public decimal? TNVN_AUTO_PSGR_VAT { get; set; }
        public decimal? TNVN_AUTO_PSGR_DE_PRICE { get; set; }
        public decimal? TNVN_AUTO_PSGR_PRICE { get; set; }
        public decimal? TNVN_AUTO_CREW_VAT { get; set; }
        public decimal? TNVN_AUTO_CREW_DE_PRICE { get; set; }
        public decimal? TNVN_AUTO_CREW_PRICE { get; set; }
        public decimal? TNDS_AUTO_LOAD_PTON { get; set; }
        public int? TNDS_AUTO_LOAD_LVL_ID { get; set; }
        public decimal? TNDSBB_AUTO_DE_RATIO { get; set; }
        public decimal? TNDSBB_MOTO_DE_RATIO { get; set; }
        public decimal? TNDSTN_ADD_AUTO_DE_RATIO { get; set; }
        public decimal? TNDSTN_ADD_MOTO_DE_RATIO { get; set; }
        public decimal? TNDS_AUTO_LOAD_DE_RATIO { get; set; }
        public decimal? TNVN_MOTO_DE_RATIO { get; set; }
        public decimal? TNVN_AUTO_CREW_DE_RATIO { get; set; }
        public decimal? TNVN_AUTO_PSGR_DE_RATIO { get; set; }
        public int? HEALTH_CANCER_PACKAGE_ID { get; set; }
        public decimal? HEALTH_CANCER_INSURED_LVL { get; set; }
        public decimal? HEALTH_CANCER_INSURED_VAL { get; set; }
        public decimal? HEALTH_CANCER_DEDUCT { get; set; }
        public decimal? HEALTH_CANCER_PRICE { get; set; }
        public decimal? HEALTH_CANCER_DE_PRICE { get; set; }
        public decimal? HEALTH_CANCER_DE_RATIO { get; set; }
        public decimal? HEALTH_CANCER_VAT { get; set; }
        public decimal? HEALTH_DISEASE_PACKAGE_ID { get; set; }
        public decimal? HEALTH_DISEASE_INSURED_LVL { get; set; }
        public decimal? HEALTH_DISEASE_INSURED_VAL { get; set; }
        public decimal? HEALTH_DISEASE_DEDUCT { get; set; }
        public decimal? HEALTH_DISEASE_PRICE { get; set; }
        public decimal? HEALTH_DISEASE_DE_PRICE { get; set; }
        public decimal? HEALTH_DISEASE_DE_RATIO { get; set; }
        public decimal? HEALTH_DISEASE_VAT { get; set; }
        public int? TRAVEL_INS_PROGRAM_ID { get; set; }
        public string TRAVEL_INS_PROGRAM_CODE { get; set; }
        public string TRAVEL_INS_PROGRAM_NAME { get; set; }
        public decimal? TRAVEL_INS_PROGRAM_VALUE { get; set; }
        public int? TRAVEL_AREA_FROM_ID { get; set; }
        public string TRAVEL_AREA_FROM_NAME { get; set; }
        public int? TRAVEL_AREA_TO_ID { get; set; }
        public string TRAVEL_AREA_TO_NAME { get; set; }
        public DateTime? TRAVEL_DEPART_DATE { get; set; }
        public DateTime? TRAVEL_RETURN_DATE { get; set; }
        public string TRAVEL_IMAGE_IDENTITY_FRONT { get; set; }
        public string TRAVEL_IMAGE_IDENTITY_END { get; set; }
        public decimal? TRAVEL_EXCHANGE_RATE { get; set; }
        public decimal? TRAVEL_PRICE { get; set; }
        public decimal? TRAVEL_DE_PRICE { get; set; }
        public decimal? TRAVEL_DE_RATIO { get; set; }
        public decimal? TRAVEL_VAT { get; set; }
        public decimal? TRAVEL_TOTAL_PRICE { get; set; }
        public decimal? TRAVEL_PRICE_ORIGIN { get; set; }
        public string TRAVEL_MODEL { get; set; }
        public string TRAVEL_GAME { get; set; }
        public string TRAVEL_DANGER_GAME { get; set; }
        public string VEH_REGISTRATION_IMAGE { get; set; }
        public string VEH_FRONT_IMAGE { get; set; }
        public string VEH_BACK_IMAGE { get; set; }
        public string VEH_LEFT_IMAGE { get; set; }
        public string VEH_RIGHT_IMAGE { get; set; }
        public int? NUMBER_START { get; set; }
        public int? NUMBER_END { get; set; }
        public int? NUMBER_CURENT { get; set; }
        public string HINS_OWNERSHIP { get; set; }
        public string HINS_USINGTIME { get; set; }
        public string HINS_ADDRESS { get; set; }
        public string HINS_AUTOFIREFIGHTINGSYS { get; set; }
        public string HINS_BANKMORTGAGE { get; set; }
        public string HINS_NAME_OF_BANK { get; set; }
        public string HINS_PHONE_OF_BANK { get; set; }
        public string HINS_EMAIL_OF_BANK { get; set; }
        public string HINS_ADDRESS_OF_BANK { get; set; }
        public Nullable<int> CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_NAME { get; set; }
        public Nullable<decimal> AUTO_BB_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_VN_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_ADD_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> AUTO_LOAD_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_BB_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_VN_VAT_BEFORE_ALTER { get; set; }
        public Nullable<decimal> MOTO_ADD_VAT_BEFORE_ALTER { get; set; }

        public static implicit operator ContractModel(CartedContractModel v)
        {
            throw new NotImplementedException();
        }
    }


}

