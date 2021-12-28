using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using Xamarin.Forms;

namespace QRMS.API
{
    public class PermiaAPI
    {
        public static string GetPolicyMoto(CartedContractModel obj)
        {
            try
            {
                TNDSBBXeMayModel objItem = new TNDSBBXeMayModel()
                {
                    custType = string.IsNullOrEmpty(obj.CUST_TYPE) ? string.Empty : obj.CUST_TYPE,
                    custName = string.IsNullOrEmpty(obj.CUST_NAME) ? string.Empty : obj.CUST_NAME,
                    custDob = string.Empty,
                    custId = string.IsNullOrEmpty(obj.IDENTITY_NO) ? string.Empty : obj.IDENTITY_NO,
                    cusIdType = string.IsNullOrEmpty(obj.IDENTITY_TYPE) ? string.Empty : obj.IDENTITY_TYPE,
                    custIdIssueDt = string.Empty,
                    custIdIssuePlace = string.Empty,
                    custSex = string.IsNullOrEmpty(obj.CUST_SEX) ? "1" : obj.CUST_SEX,
                    custNationality = "Viet Nam",
                    custAddress = string.IsNullOrEmpty(obj.CUST_ADDRESS) ? string.Empty : obj.CUST_ADDRESS,
                    custAddrCountry = string.IsNullOrEmpty(obj.CUST_COUNTRY_CODE) ? "VN" : obj.CUST_COUNTRY_CODE,
                    custAddrProvince = string.IsNullOrEmpty(obj.CUST_PROVINCE_CODE) ? string.Empty : obj.CUST_PROVINCE_CODE,
                    custAddrDistrict = string.IsNullOrEmpty(obj.CUST_DISTRICT_CODE) ? string.Empty : obj.CUST_DISTRICT_CODE,
                    custPhone = string.IsNullOrEmpty(obj.CUST_PHONE) ? string.Empty : obj.CUST_PHONE,
                    custEmail = string.IsNullOrEmpty(obj.CUST_EMAIL) ? string.Empty : obj.CUST_EMAIL,
                    OriginOfProduction = obj.VEH_ORIGIN == null ? "T" : "N",
                    HasRegnNo = string.IsNullOrEmpty(obj.VEH_PLATE_NO) ? "N" : "Y",
                    RegnNo = string.IsNullOrEmpty(obj.VEH_PLATE_NO) ? string.Empty : obj.VEH_PLATE_NO,
                    ChassisNo = string.IsNullOrEmpty(obj.VEH_FRAME_NO) ? string.Empty : obj.VEH_FRAME_NO,
                    EngineNo = string.IsNullOrEmpty(obj.VEH_ENGINE_NO) ? string.Empty : obj.VEH_ENGINE_NO,
                    VehicleType = string.IsNullOrEmpty(obj.VEH_TYPE_CODE) ? string.Empty : obj.VEH_TYPE_CODE /*"1"*/,
                    polIssueDate = obj.CONTRACT_ISSUED_DATE.Value.ToString("dd/MM/yyyy"),
                    polStartDate = obj.CONTRACT_START_DATE.Value.ToString("dd/MM/yyyy"),
                    polEndDate = obj.CONTRACT_EXPIRE_DATE.Value.ToString("dd/MM/yyyy"),
                    PolLcVehPremium = (int)(obj.TNDSBB_MOTO_PRICE ?? 0),
                    PolLcVehVAT = (int)(obj.TNDSBB_MOTO_VAT ?? 0),
                    PolLcVehTotal =(int)(obj.TNDSBB_MOTO_PRICE ?? 0) + (int)(obj.TNDSBB_MOTO_VAT ?? 0),
                    IsPassagerPol = obj.TNVN_MOTO_PCLAIM == 0 ? 0 : 1,
                    PassagerLevelAmount = (int)(obj.TNVN_MOTO_PCLAIM ?? 0),
                    NumOfPassagers = (int)(obj.TNVN_MOTO_PCLAIM_ID ?? 0),
                    polPassagerPremium =(int)(obj.TNVN_MOTO_PRICE ?? 0),
                    polPassagerVat = (int)(obj.TNVN_MOTO_VAT ?? 0),
                    polPassagerAmount = (int)(obj.TNVN_MOTO_PRICE ?? 0) + (int)(obj.TNVN_MOTO_VAT ?? 0),
                    polLcPremium = (int)(obj.TNDSBB_MOTO_PRICE ?? 0) + (int)(obj.TNVN_MOTO_PRICE ?? 0),
                    polVat = (int)(obj.TNDSBB_MOTO_VAT ?? 0) + (int)(obj.TNVN_MOTO_VAT ?? 0),
                    polAmount = ((int)(obj.TNDSBB_MOTO_PRICE ?? 0) + (int)(obj.TNVN_MOTO_PRICE ?? 0)) + ((int)(obj.TNDSBB_MOTO_VAT ?? 0) + (int)(obj.TNVN_MOTO_VAT ?? 0)),
                    PhHasInvoice = string.IsNullOrEmpty(obj.BILL_IDENTITY_NO) ? "0" : "1",
                    InvoiceName = string.IsNullOrEmpty(obj.BILL_NAME) ? string.Empty : obj.BILL_NAME,
                    InvoiceVatNumber = string.IsNullOrEmpty(obj.BILL_IDENTITY_NO) ? string.Empty : obj.BILL_IDENTITY_NO,
                    InvoiceBusinessAddr = string.IsNullOrEmpty(obj.BILL_ADDRESS) ? string.Empty : obj.BILL_ADDRESS,
                    InvoiceReceiveAddr = string.Empty,
                    partnerPassword = "test@123",
                    CertReceiveAddr = string.Empty,
                    polLcSi_NN = 150000000,
                    polLcSi_TS = 50000000,
                    IsTNPol = obj.TNDSTN_ADD_MOTO_TYPE == 0 ? 0 : 1,
                    polLcSi_TNNN = (int)(obj.TNDSTN_ADD_MOTO_PSGR ?? 0),
                    polLcSi_TNTS = (int)(obj.TNDSTN_ADD_MOTO_ASSEST ?? 0),
                    PolLcVehTNPremium = (int)(obj.TNDSTN_ADD_PRICE ?? 0),
                    PolLcVehTNVAT = (int)(obj.TNDSTN_ADD_VAT ?? 0),
                    PolLcVehTNTotal = (int)(obj.TNDSTN_ADD_PRICE ?? 0) + (int)(obj.TNDSTN_ADD_VAT ?? 0),
                    polFeeAmountDiscount = (int)(obj.TNDSBB_MOTO_DE_PRICE ?? 0)*(-1),
                    polFeeRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1)) / 100,
                    polPassagerAmountDiscount = (int)(obj.TNVN_MOTO_DE_PRICE ?? 0)*(-1),
                    polPassagerRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1)) / 100,
                    polTNAmountDiscount = (int)(obj.TNDSTN_ADD_DE_PRICE ?? 0) * (-1),
                    polTNRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1)) / 100
                };

                var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, Constaint.APIurl.CallPemiaMoto, objItem);

                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.PermiaAPI.GetPolicyMoto()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static string GetPolicyAuto(CartedContractModel obj)
        {
            try
            {
                decimal feerate = 0;
                if (obj.LOAD_WEIGHT > 0)
                {
                    feerate = (obj.TNDS_AUTO_LOAD_PRICE ?? 0) / ((obj.TNDS_AUTO_LOAD_LMT ?? 0) * (obj.LOAD_WEIGHT ?? 0));
                }
                else if((obj.LOAD_WEIGHT == 0 || obj.LOAD_WEIGHT == null) && obj.VEH_MAX_LOAD > 0)
                {
                    feerate = (obj.TNDS_AUTO_LOAD_PRICE ?? 0) / ((obj.TNDS_AUTO_LOAD_LMT ?? 0)* (obj.VEH_MAX_LOAD ?? 0));
                }
                else if ((obj.LOAD_WEIGHT == 0 && obj.VEH_MAX_LOAD == 0) || (obj.LOAD_WEIGHT == null && obj.VEH_MAX_LOAD == 0) || (obj.LOAD_WEIGHT == null && obj.VEH_MAX_LOAD == null) || (obj.LOAD_WEIGHT == 0 && obj.VEH_MAX_LOAD == null))
                {
                    feerate = (obj.TNDS_AUTO_LOAD_PRICE ?? 0) / ((obj.TNDS_AUTO_LOAD_LMT ?? 0));
                }
                decimal checkcrew = 0;
                decimal psrg = 0;
                if((obj.TNVN_AUTO_CREW_NO ?? 0) > 0)
                {
                    checkcrew = (obj.TNVN_AUTO_CREW_PRICE ?? 0) / ((obj.TNVN_AUTO_CREW_PCLAIM ?? 0) * (obj.TNVN_AUTO_CREW_NO ?? 0));
                }
                if ((obj.TNVN_AUTO_PSGR_NO ?? 0) > 0)
                {
                    psrg = (obj.TNVN_AUTO_PSGR_PRICE ?? 0) / ((obj.TNVN_AUTO_PSGR_PCLAIM ?? 0) * (obj.TNVN_AUTO_PSGR_NO ?? 0));
                }

                switch (obj.VEH_TYPE_CODE)
                {
                    case "C01":
                        obj.VEH_TYPE_CODE = "LX004.1";
                        break;
                    case "C02":
                        obj.VEH_TYPE_CODE = "LX005.1";
                        break;
                    case "C03":
                        obj.VEH_TYPE_CODE = "LX006.1";
                        break;
                    case "C04":
                        obj.VEH_TYPE_CODE = "LX007.1";
                        break;
                    case "C05":
                        obj.VEH_TYPE_CODE = "LX008.1";
                        break;
                    case "C06":
                        obj.VEH_TYPE_CODE = "LX009.1";
                        break;
                    case "C07":
                        obj.VEH_TYPE_CODE = "LX010.1";
                        break;
                    case "C08":
                        obj.VEH_TYPE_CODE = "LX012.1";
                        break;
                    case "C09":
                        obj.VEH_TYPE_CODE = "LX013.1";
                        break;
                    case "C10":
                        obj.VEH_TYPE_CODE = "LX014.1";
                        break;
                    case "C11":
                        obj.VEH_TYPE_CODE = "LX011.2";
                        break;
                    //case "C12":
                    //    obj.VEH_TYPE_CODE = "LX004.1";
                    //    break;
                    //case "C13":
                    //    obj.VEH_TYPE_CODE = "LX004.1";
                    //    break;
                    //case "C14":
                    //    obj.VEH_TYPE_CODE = "LX004.1";
                    //    break;
                    //case "C15":
                    //    obj.VEH_TYPE_CODE = "LX004.1";
                    //    break;
                    //case "C16":
                    //    obj.VEH_TYPE_CODE = "LX004.1";
                    //    break;
                }

                TNDSBBOtoModel objItem = new TNDSBBOtoModel()
                {
                    custType = string.IsNullOrEmpty(obj.CUST_TYPE) ? string.Empty : obj.CUST_TYPE,
                    custName = string.IsNullOrEmpty(obj.CUST_NAME) ? string.Empty : obj.CUST_NAME,
                    custDob = string.Empty,
                    custId = string.IsNullOrEmpty(obj.IDENTITY_NO) ? string.Empty : obj.IDENTITY_NO,
                    cusIdType = string.IsNullOrEmpty(obj.IDENTITY_TYPE) ? string.Empty : obj.IDENTITY_TYPE,
                    custIdIssueDt = string.Empty,
                    custIdIssuePlace = string.Empty,
                    custSex = string.IsNullOrEmpty(obj.CUST_SEX) ? "1" : obj.CUST_SEX,
                    custNationality = string.Empty,
                    custAddress = string.IsNullOrEmpty(obj.CUST_ADDRESS) ? string.Empty : obj.CUST_ADDRESS,
                    custAddrCountry = string.IsNullOrEmpty(obj.CUST_COUNTRY_CODE) ? string.Empty : obj.CUST_COUNTRY_CODE,
                    custAddrProvince = string.IsNullOrEmpty(obj.CUST_PROVINCE_CODE) ? string.Empty : obj.CUST_PROVINCE_CODE,
                    custAddrDistrict = string.IsNullOrEmpty(obj.CUST_DISTRICT_CODE) ? string.Empty : obj.CUST_DISTRICT_CODE,
                    custPhone = string.IsNullOrEmpty(obj.CUST_PHONE) ? string.Empty : obj.CUST_PHONE,
                    custEmail = string.IsNullOrEmpty(obj.CUST_EMAIL) ? string.Empty : obj.CUST_EMAIL,
                    OriginOfProduction = obj.VEH_ORIGIN == null ? "T" : "N",
                    HasRegnNo = string.IsNullOrEmpty(obj.VEH_PLATE_NO) ? "N" : "Y",
                    RegnNo = string.IsNullOrEmpty(obj.VEH_PLATE_NO) ? string.Empty : obj.VEH_PLATE_NO,
                    ChassisNo = string.IsNullOrEmpty(obj.VEH_FRAME_NO) ? string.Empty : obj.VEH_FRAME_NO,
                    EngineNo = string.IsNullOrEmpty(obj.VEH_ENGINE_NO) ? string.Empty : obj.VEH_ENGINE_NO,
                    MakeNo = string.IsNullOrEmpty(obj.VEH_BRAND_CODE) ? "014" : obj.VEH_BRAND_CODE,
                    Model = string.IsNullOrEmpty(obj.VEH_MODEL_CODE) ? "0140103010" : obj.VEH_MODEL_CODE,
                    VehicleType = string.IsNullOrEmpty(obj.VEH_TYPE_CODE) ? string.Empty : obj.VEH_TYPE_CODE,
                    SeatingCapacity = obj.VEH_SEAT_NO ?? 0,
                    VehWeight = (int)(obj.VEH_MAX_LOAD ?? 0),
                    VehUseType = string.IsNullOrEmpty(obj.VEH_PURPOSE_CODE) ? string.Empty : obj.VEH_PURPOSE_CODE,
                    YearOfManufactory = obj.VEH_FIRST_REG_DATE == null ? "14/03/2019" : obj.VEH_FIRST_REG_DATE.Value.ToString("dd/MM/yyyy"),
                    polIssueDate = obj.CONTRACT_ISSUED_DATE.Value.ToString("dd/MM/yyyy"),
                    polStartDate = obj.CONTRACT_START_DATE.Value.ToString("dd/MM/yyyy"),
                    polEndDate = obj.CONTRACT_EXPIRE_DATE.Value.ToString("dd/MM/yyyy"),
                    polLcSi_NN = 150000000,    //Mức trách nhiệm DSBB về người
                    polLcSi_HK = 100000000,    //Mức trách nhiệm DSBB về tài sản
                    polLcSi_TS = 100000000,    //Mức trách nhiệm DSBB về hành khách
                    PolLcVehPremium = (int)(obj.TNDSBB_AUTO_PRICE ?? 0),
                    PolLcVehVAT = (int)(obj.TNSDBB_AUTO_VAT ?? 0),
                    PolLcVehTotal = (int)(obj.TNDSBB_AUTO_PRICE ?? 0 + obj.TNSDBB_AUTO_VAT ?? 0),
                    IsVoluntaryPol = obj.TNDSTN_ADD_AUTO_TYPE == 0 ? 0 : 1,//Mua/ko mua TNDSTN tăng thêm
                    polLcSi_TNNN = (int)(obj.TNDSTN_ADD_AUTO_CREW ?? 0),  //???
                    polLcSi_TNHK = (int)(obj.TNDSTN_ADD_AUTO_PSGR ?? 0),  //???
                    polLcSi_TNTS = (int)(obj.TNDSTN_ADD_AUTO_ASSEST ?? 0),  //???
                    PolLcTNMaxSI = 1000000000,  //???
                    PolLcVehTNPremium = (int)(obj.TNDSTN_ADD_AUTO_PRICE ?? 0),  //Phí bảo hiểm trách nhiệm dân sự tự nguyện xe  
                    PolLcVehTNVAT = (int)(obj.TNDSTN_ADD_AUTO_VAT ?? 0),  //Vat phí bảo hiểm trách nhiệm dân sự tự nguyện xe 
                    PolLcVehTNTotal = (int)(obj.TNDSTN_ADD_AUTO_PRICE ?? 0) + (int)(obj.TNDSTN_ADD_AUTO_VAT ?? 0),  //Tổng phí bảo hiểm trách nhiệm dân sự tự nguyện xe  
                    IsPAforCoDriverPol = obj.TNVN_AUTO_CREW_NO == 0 ? 0 : 1, //Tai nạn lái phụ xe
                    PALevelAmount = (int)(obj.TNVN_AUTO_CREW_PCLAIM ?? 0), //mưc trách nhiệm lái phụ xe
                    NumOfDrivers = obj.TNVN_AUTO_CREW_NO ?? 0,
                    polPAPremium = (int)(obj.TNVN_AUTO_CREW_PCLAIM ?? 0),
                    polPAVat = (int)(obj.TNVN_AUTO_VAT ?? 0),
                    polPAAmount = (int)(obj.TNVN_AUTO_CREW_PCLAIM ?? 0) + (int)(obj.TNVN_AUTO_VAT ?? 0),
                    polPAFeeRate = checkcrew, //tỉ lệ phí tai nạn lái phụ xe
                    IsPassagerPol = obj.TNVN_AUTO_PSGR_NO == 0 ? 0 : 1,//có mua bảo hiểm cho người ngồi trên xe
                    PassagerLevelAmount = (int)(obj.TNVN_AUTO_PSGR_PCLAIM ?? 0),
                    NumOfPassagers = obj.TNVN_AUTO_PSGR_NO ?? 0,
                    polPassagerPremium = (int)(obj.TNVN_AUTO_PRICE ?? 0),
                    polPassagerVat = (int)(obj.TNVN_AUTO_VAT ?? 0),
                    polPassagerAmount = (int)(obj.TNVN_AUTO_PRICE ?? 0) + (int)(obj.TNVN_AUTO_VAT ?? 0),
                    polPassagerFeeRate = psrg, //Tỉ lệ Phí bảo hiểm người ngồi trên xe 
                    IsCargoPol = obj.TNDS_AUTO_LOAD_TYPE == 0 ? 0 : 1,//Có mua Bảo hiểm cho người ngồi trên xe
                    CargoLevelAmount = (int)(obj.TNDS_AUTO_LOAD_LMT ?? 0),
                    CargoLevelMaxAmount = (int)(obj.TNDS_AUTO_LOAD_LMT ?? 0),  //???
                    CargoWeight = (int)(obj.VEH_MAX_LOAD ?? 0),// tải trọng hàng hóa
                    CargoVerhType = obj.TNDS_AUTO_LOAD_TYPE == null ? string.Empty : obj.TNDS_AUTO_LOAD_TYPE.ToString(),
                    polCargoPremium = (int)(obj.TNDS_AUTO_LOAD_PRICE ?? 0),
                    polCargoVat = (int)(obj.TNDS_AUTO_LOAD_VAT ?? 0),
                    polCargoAmount = (int)(obj.TNDS_AUTO_LOAD_VAT ?? 0) + (int)(obj.TNDS_AUTO_LOAD_PRICE ?? 0),
                    polCargoFeeRate = feerate, //Tỉ lệ phí bảo hiểm trách nhiệm tài sản trên xe
                    polLcPremium = (int)(obj.TOTAL_PRICE ?? 0),
                    polVat = (int)(obj.TOTAL_VAT ?? 0),
                    polAmount = (int)(obj.TOTAL_FINAL_PRICE ?? 0),
                    PhHasInvoice = string.IsNullOrEmpty(obj.BILL_IDENTITY_NO) ? "0" : "1",
                    InvoiceName = string.Empty,
                    InvoiceVatNumber = string.Empty,
                    InvoiceBusinessAddr = string.Empty,
                    InvoiceReceiveAddr = string.Empty,
                    polPAFeeRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0)*(-1))/100,
                    polPAFeeAmountDiscount = (int)(obj.TNVN_AUTO_CREW_DE_PRICE ?? 0)*(-1),
                    polPassagerFeeRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1))/ 100,
                    polPassagerFeeAmountDiscount = (int)(obj.TNVN_AUTO_PSGR_DE_PRICE ?? 0)*(-1),
                    polCargoFeeRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1)) / 100,
                    polCargoFeeAmountDiscount = (int)(obj.TNDS_AUTO_LOAD_DE_PRICE ?? 0)*(-1),
                    polTNFeeAmountDiscount = (int)(obj.TNDSTN_ADD_AUTO_DE_PRICE ?? 0)*(-1),
                    polTNFeeRateDiscount = ((obj.TOTAL_DEDUCT_RATIO ?? 0) * (-1)) / 100,
                    partnerPassword = "Pjico@123",
                };

                var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, "/api/tndsbboto/getpolicynumber", objItem);

                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.PermiaAPI.GetPolicyAuto()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        //public static CustomerModel GetCustomerByPermia(CustomerModel obj)
        //{
        //    try
        //    {
        //        obj.CODE = "KH" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
