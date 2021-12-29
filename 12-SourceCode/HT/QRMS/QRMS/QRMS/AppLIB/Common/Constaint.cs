using QRMS.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QRMS.AppLIB.Common
{
    public class Constaint
    {
        #region AddressService 
        public const string ServiceAddress = "https://localhost:44392/"; 
        #endregion

        public class APIurl
        { 
            public const string login = "api-ht/account/login";
        }

        #region AccountLogin
        public const string UserNoKey = "UserNo";
        public const string UserNameKey = "UserName";
        public const string PasswordKey = "Password";
        public const string isUserLoggedKey = "isLogged";
        public const string InsuranceAgentID = "InsuranceAgentID";
        public const string InsuranceAgentCode = "InsuranceAgentCode";
        public const string InsuranceAgentName = "InsuranceAgentName";
        #endregion

        #region AccountStatus
        public const string AccountDeleted = "D";
        public const string AccountLocked = "L";
        #endregion

        #region VerityType
        public const string EmailType = "M";
        public const string PhoneNumberType = "P";
        #endregion

        #region Role
        public const string TDL = "140";    //Trưởng đại lý
        public const string CBDL = "150";  //Cán bộ đại lý
        public const string ADMIN = "110";  //Admin
        #endregion

        #region ObjectType
        public class CustomerType
        {
            public static string PersonalType = "1";
            public static string EnterpriseType = "2";
            public static bool IsPersonal(string type)
            {
                return type?.Trim() == PersonalType;
            }
            public static bool IsEnterprise(string type)
            {
                return type?.Trim() == EnterpriseType;
            }
        }
        public const string PersonalType = "1";
        public const string EnterpriseType = "2";
        #endregion

        #region LoaiPhuPhiDuLich
        public class PBHDL
        {
            public const string TripType = "T";
            public const string PointType = "P";
            public const string PersonalType = "I";
            public const string GroupType = "C";
            public const string Selected_A = "Y";
            public const string Selected_B = "N";
            public const string Selected_Yes = "Y";
            public const string Selected_No = "N";
        }
        #endregion

        #region Insurance Product
        public class Insurance_Product
        {
            public static Dictionary<string, string> InsuranceName => new Dictionary<string, string>
            {
                { CarCompulsory , AppResources.CarCompulsory },
                { CarMaterial , AppResources.CarMaterial },
                { MotoCompulsory , AppResources.MotoCompulsory },
                { Health_Cancer , AppResources.SK_Caner },
                { HealthFatalDisease , AppResources.SK_Fatal },
                { TravelInternational , AppResources.TravelInternationalInsurance },
                { TravelDomestic , AppResources.TravelDomesticInsurance },
                { TravelForeignerDomestic , AppResources.TravelForeignerInsurance },
                { TravelVietnameseAbbroad , AppResources.TravelVietnameseInsurance },
                { HomeInsurance , AppResources.BHNhaTuNhan },
            };

            public const string CarCompulsory = "5101";
            public const string CarMaterial = "5106";
            public const string MotoCompulsory = "5201";
            public const string MotoMaterial = "5204";
            public const string Health_Cancer = "6105";
            public const string HealthFatalDisease = "6106";
            public const string TravelDomestic = "6501";
            public const string TravelForeignerDomestic = "6502";
            public const string TravelVietnameseAbbroad = "6503";
            public const string TravelInternational = "6504";
            public const string HomeInsurance = "3104";


            public const string Car_ProductCode = "510";
            public const string Moto_ProductCode = "520";
            public const string Health_ProductCode = "610";
            public const string Travel_ProductCode = "650";
            public const string Home_ProductCode = "310";

            public const string Car_BannerCode = "B003";
            public const string Moto_BannerCode = "B001";
            public const string Health_BannerCode = "B005";
            public const string Travel_BannerCode = "B002";
            public const string Home_BannerCode = "B004";

            #region kiểm tra bảo hiểm sức khỏe
            public static bool IsHealthIns(string ProductCode)
            {
                return IsCancer(ProductCode) || IsFatalDisease(ProductCode);
            }
            public static bool IsFatalDisease(string ProductCode)
            {
                return ProductCode == HealthFatalDisease;
            }
            public static bool IsCancer(string ProductCode)
            {
                return ProductCode == Health_Cancer;
            }
            public static string HealthInsPackage(string insurProductCode)
            {
                if (IsCancer(insurProductCode)) return "C";
                else if (IsFatalDisease(insurProductCode)) return "F";
                else return string.Empty;
            }
            #endregion

            #region kiểm tra bảo hiểm xe cơ giới
            public static bool IsVehIns(string ProductCode)
            {
                return IsCar(ProductCode)
                    || IsMoto(ProductCode);
            }
            public static bool IsCar(string ProductCode)
            {
                return ProductCode == CarCompulsory
                    || ProductCode == CarMaterial;
            }
            public static bool IsMoto(string ProductCode)
            {
                return ProductCode == MotoCompulsory
                    || ProductCode == MotoMaterial;
            }
            public static bool IsCompulsory(string ProductCode)
            {
                return ProductCode == MotoCompulsory
                    || ProductCode == CarCompulsory;
            }
            public static bool IsMaterial(string ProductCode)
            {
                return ProductCode == MotoMaterial
                    || ProductCode == CarMaterial;
            }
            #endregion

            #region Kiểm tra bảo hiểm du lịch
            public static bool IsTravelIns(string ProductCode)
            {
                return IsTravelInternational(ProductCode) || IsTravelDomestic(ProductCode) || IsTravelVietnameseAbbroad(ProductCode) || IsTravelForeignerDomestic(ProductCode);
            }
            public static bool IsTravelInternational(string ProductCode)
            {
                return ProductCode == TravelInternational;
            }
            public static bool IsTravelDomestic(string ProductCode)
            {
                return ProductCode == TravelDomestic;
            }
            public static bool IsTravelVietnameseAbbroad(string ProductCode)
            {
                return ProductCode == TravelVietnameseAbbroad;
            }
            public static bool IsTravelForeignerDomestic(string ProductCode)
            {
                return ProductCode == TravelForeignerDomestic;
            }
            #endregion

            #region Kiểm tra bảo hiểm nhà tư nhân
            public static bool IsHinsIns(string ProductCode)
            {
                return ProductCode == HomeInsurance;
            }
            #endregion
        }
        #endregion

        #region Product_Cost_Basic
        public class Product_Cost_Basic
        {
            public const string FEE_TNDSBB_BBMOTO = "520101";       //Phí bắt buộc xe máy
            public const string FEE_TNDSBB_TNNNTXMOTO = "520102";   //Phí tai nạn người ngồi trên xe máy
            public const string FEE_TNDSBB_TNDSTTMOTO = "520103";   //Phí TNDS tăng thêm xe máy
            public const string FEE_TNDSBB_BBAUTO = "510101";       //Phí bắt buộc ô tô
            public const string FEE_TNDSBB_TNLPAUTO = "510102";     //Phí tai nạn lái phụ ô tô
            public const string FEE_TNDSBB_NNTXAUTO = "510103";     //Phí người ngồi trên ô tô
            public const string FEE_TNDSBB_TNDSTTAUTO = "510104";   //Phí TNDS tăng thêm ô tô
            public const string FEE_TNDSBB_TNDSHHAUTO = "510105";   //Phí TNDS chủ xe và hàng hóa trên ô tô
            public const string FEE_VC_DKBSAUTO = "510601";         //Phí điều khoản bổ sung ô tô
            public const string FEE_VC_TNLPAUTO = "510602";         //Phí tai nạn lái phụ ô tô
            public const string FEE_VC_NNTXAUTO = "510603";         //Phí người ngồi trên ô tô
            public const string FEE_VC_TNDSTTAUTO = "510604";       //Phí TNDS tăng thêm ô tô
            public const string FEE_VC_TNDSHHAUTO = "510605";       //Phí TNDS chủ xe và hàng hóa trên ô tô
            public const string FEE_VC_AUTO = "510606";             //Phí điều khoản bổ sung ô tô
            public const string FEE_VC_BBMOTO = "520401";           //Phí bắt buộc xe máy
            public const string FEE_VC_TNNNTXMOTO = "520402";       //Phí tai nạn người ngồi trên xe máy
            public const string FEE_VC_TNDSTTMOTO = "520403";       //Phí TNDS tăng thêm xe máy
            public const string FEE_DL_QT = "530101";               //Phí du lịch quốc tế
            public const string FEE_DL_TN = "540101";               //Phí du lịch trong nước
            public const string FEE_DL_NVTNN = "650301";            //Phí du lịch người việt tại nước ngoài
            public const string FEE_DL_NNNTVN = "650201";           //Phí du lịch người nước ngoài tại VN
            public static string Fee_Health_Cancer = $"{Insurance_Product.Health_Cancer}01";                //Phí du lịch quốc tế
            public static string Fee_Health_Disease = $"{Insurance_Product.HealthFatalDisease}01";          //Phí du lịch quốc tế
        }
        #endregion

        #region VehicleInsuranceType
        public class VehicleInsuranceType
        {
            public const string CarCompulsory = "5101";
            public const string CarMaterial = "5106";
            public const string MotoCompulsory = "5201";
            public const string MotoMaterial = "5204";

            public static bool IsVehIns(string ProductCode)
            {
                return ProductCode == CarCompulsory
                    || ProductCode == CarMaterial
                    || ProductCode == MotoCompulsory
                    || ProductCode == MotoMaterial;
            }

            public static bool IsCar(string ProductCode)
            {
                return ProductCode == CarCompulsory
                    || ProductCode == CarMaterial;
            }
            public static bool IsMoto(string ProductCode)
            {
                return ProductCode == MotoCompulsory
                    || ProductCode == MotoMaterial;
            }
        }
        #endregion

        #region CommonType
        public class CommonType
        {
            public const string Car = "VEH_CN_C";
            public const string Moto = "VEH_CN_M";
            public const string CarFreight = "VEH_HH";
            public const string SameOwner = "VEH_HH_CX";
            public const string CarMaterial = "24";
            public const string Origin = "18";
            public const string Purpose = "P";
            public const string TravelInsuredPerson = "TRAVEL_INTER";
            public const string TravelArea = "TRAVEL_AREA";
            public const string TravelBen = "TRAVEL_BEN";
        }
        public class CommonValue
        {
            public const string VAT = "VAT";
            public const double MaxAlter = 15;
        }
        public class CommonCode
        {
            public const string Domestic = "1801";
            public const string Foreign = "1802";
        }
        #endregion

        #region Identity_Type
        public class Identity_Type
        {
            public const string IDCard = "1";
            public const string TaxNo = "2";
        }
        #endregion

        #region ContractStatus
        public class Contract_Status
        {
            public const int NoStatus = 0;
            public const int InComplete = 1;
            public const int WaitConfirm = 2;
            public const int NoConfirm = 3;
            public const int Confirmed = 4;
            public const int Payed = 5;
            public const int Completed = 6;
            public const int Updated = 7;
            public const int Cancelled = 8;
            public const int Renewed = 9;
            public const int RequestUpdate = 10;
            public const int RequestCancel = 11;
            public const int WaitRenew = 12;

            public static string GetStatusText(string code)
            {
                try
                {
                    return ContractStatus[code];
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region Notify View Status
        public class Notify_View
        {
            public const string Watched = "Y";
            public const string NotSeen = "N";
        }
        #endregion

        #region ContractType
        public class Contract_Type
        {
            public const string NewContractType = "N";
            public const string RenewalsContractType = "R";
            public const string UpdateContractType = "U";
            public const string ApprovalContractType = "A";
        }
        //loại đơn bảo hiểm theo database (đớn nháp-đơn chính)
        public class ContractDBType
        {
            public static string Cart = "carted";
            public static string Contract = "contract";
            public static string AlterHistory = "alter";
            public static string Renewal = "renew";

            public static bool IsCart(string dbtype)
            {
                return dbtype.ToLower() == Cart.ToLower();
            }
            public static bool IsContract(string dbtype)
            {
                return dbtype.ToLower() == Contract.ToLower();
            }
            public static bool IsRenewal(string dbtype)
            {
                return dbtype.ToLower() == Renewal.ToLower();
            }
        }
        #endregion

        public static IDictionary<string, string> ContractStatus
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "1" , AppResources.Incomplete },
                    { "2" , AppResources.WaitConfirm },
                    { "3" , AppResources.NoConfirm},
                    { "4" , AppResources.Confirmed },
                    { "5" , AppResources.Payed  },
                    { "6" , AppResources.Completed  },
                    { "7" , AppResources.Updated },
                    { "8" , AppResources.Cancelled  },
                    { "9" , AppResources.Renewed },
                    { "10" , "Yêu cầu sửa đổi bổ sung" },
                    { "11" , "Yêu cầu hủy" },
                    { "12" , "Chờ tái tục" },
                };
            }
        }

        public class ContractIssueType
        {
            public const string IssueNew = "N";//cấp mới
            public const string Renewal = "R";//tái tục
            public const string Altered = "U";//SDBS
            public const string Cancel = "D";//Hủy

            public static string GetIssueText(string code)
            {
                try
                {
                    return ContractType[code];
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public static IDictionary<string, string> ContractType
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { ContractIssueType.IssueNew ,AppResources.IssueNew },
                    { ContractIssueType.Renewal ,AppResources.Renewal },
                    { ContractIssueType.Altered ,AppResources.Alter },
                    { ContractIssueType.Cancel ,AppResources.CancelContract },
                };
            }
        }

        #region page
        public class PageApp
        {
            public const string HomePage = "Home";
            public const string HistoryPage = "History";
            public const string DraftPage = "Draft";
            public const string ApprovalPage = "Approval";
            public const string RenewalsPage = "Renewals";
            public const string NotifyPage = "Notify";
            public const string PayPage = "Pay";
        }
        #endregion

        #region Sex
        public class SEX
        {
            public const string Male = "1";
            public const string Female = "2";
        }
        #endregion

        #region Action
        public class TabAction
        {
            public const string New_Tab = "N";
            public const string Renewals_Tab = "R";
            public const string History_Tab = "H";
            public const string Approval_Tab = "A";
        }
        public class ContractAct
        {
            public const string New = "N";
            public const string Renewals = "R";
            public const string History = "H";
            public const string Approval = "A";
            public const string Update = "U";
            public const string Cancel = "C";
            public const string Pay = "C";
        }
        #endregion

        #region TNDSTNType
        public class TNDSTNType
        {
            public const int Fix = 1;
            public const int Free = 2;
        }
        #endregion

        #region VehType
        public class VehicleType
        {
            public const string AutoChoNguoi = "LX004.1";
            public const string AutoChoHang = "LX005.1";
            public const string AutoPickup = "LX006.1";
            public const string AutoCuuThuong = "LX007.1";
            public const string AutoChoTien = "LX008.1";
            public const string AutoDauKeo = "LX009.1";
            public const string AutoChuyendungKhac = "LX010.1";
            public const string AutoTapLai = "LX012.1";
            public const string AutoTaxi = "LX013.1";
            public const string AutoBus = "LX014.1";
            public const string AutoChuyendung = "LX011.2";
            public const string MotoDuoi50cc = "LX001.1";
            public const string MotoTren50cc = "LX001.2";
            public const string Moto3Banh = "LX002.1";
            public const string MotoDien = "LX003.1";
            public const string MotoXeGanMay = "LX003.2";
            public const string Type09 = "09";
        }
        #endregion

        #region Notify Type
        public class NotifyType
        {
            public const string Professional = "1"; // thông báo đơn
            public const string System = "0";    // thông báo hệ thống
        }
        #endregion

        public static CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");

        #region người thụ hưởng là ?
        public class IsBen
        {
            public const string IsBuyer = "1";
            public const string IsInsuredPerson = "2";
            public const string IsOther = "3";
        }
        #endregion

        #region tiền tệ
        public class CurrentType
        {
            public const string USD = "USD";
            public const string VND = "VNĐ";
            public const string EUR = "EUR";
        }
        #endregion

        #region App_Type
        public class App_Type
        {
            public const string Agent = "A";
        }
        #endregion
    }

    public static class GeneralExtendMethod
    {
        public static decimal? Abs(this decimal? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static float? Abs(this float? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static double? Abs(this double? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static int? Abs(this int? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static long? Abs(this long? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static short? Abs(this short? dec)
        {
            if ((dec ?? 0) == 0)
                return null;
            else
                return Math.Abs(dec ?? 0);
        }
        public static decimal Percent(this decimal dec)
        {
            return dec / 100;
        }
        public static float Percent(this float dec)
        {
            return dec / 100;
        }

        public static decimal Limit(this decimal val, decimal limit)
        {
            if (val < limit) return val;
            else return limit;
        }
        public static float Limit(this float val, float limit)
        {
            if (val < limit) return val;
            else return limit;
        }
        public static int? ToInt(this string val)
        {
            val = val.Replace(" ", "");
            int _result = 0;
            if (int.TryParse(val, out _result))
            {
                return _result;
            }
            else return null;
        }

        public static int Age(this DateTime DOB, DateTime EndPoint)
        {
            int diffYear = EndPoint.Year - DOB.Year;
            int diffMonth = EndPoint.Month - DOB.Month;
            int diffDay = EndPoint.Day - DOB.Day;

            if (diffMonth < 0) diffYear--;
            else if (diffDay < 0) diffYear--;
            return diffYear;
        }
    }
}
