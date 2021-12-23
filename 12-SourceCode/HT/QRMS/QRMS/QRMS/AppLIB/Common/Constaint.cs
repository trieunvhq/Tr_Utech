using QRMS.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QRMS.AppLIB.Common
{
    public class Constaint
    {
        #region AddressService
        public const string APIAddress = "http://192.168.0.111:4165/";
        public const string APIlocal = "https://localhost:44392/";
        //public const string ServiceAddress = "https://113.20.126.218";
        //public const string ServiceAddress = "http://192.168.1.130:9083/";
        //public const string ServiceAddress = "http://192.168.1.10:6087";
        //public const string ServiceAddress = "http://10.0.2.2:8001/";
        //public const string ServiceAddress = "http://123.30.145.181:9082";
        //public const string ServiceAddress = "https://uat-agency.pjico.com.vn";
        public const string ServiceAddress4PDFhttps = "http://123.30.145.181:9082";
        public const string ServiceAddress = "http://118.70.132.5:8060";
        //public const string ServiceAddress = "http://192.168.108.103:6060";
        //public const string ServiceAddress = "http://192.168.0.116:8060";
        #endregion

        public class APIurl
        {
            public const string GetVehicleAlterHistory = "api/vehiclealterhistory/getvehiclebycontractcode";
            public const string GetTNDSBB_Fee_Data = "api/tndsbbfee/getfeedata";
            public const string GetAllFerightFeeData = "api/freightinsfee/getall";
            public const string LiabilityLevel = "api/tndsbbfee/getlimitedlevel";
            public const string TNVNLevel = "api/tnvnfee/getfeebytype";
            public const string ValueByType = "api/commonvalue/getvaluebykey";
            public const string ValueByCode = "api/commonvalue/getvaluebycode";
            public const string GetCartedContractByKey = "api/cartedcontract/getbykey";
            public const string GetCartedbyKeyType = "api/cartedcontract/getcartbykeytype";
            public const string AddCustomerCartedContract = "api/cartedcontract/addcustomer";
            public const string AddCarCustomerCartedContract = "api/cartedcontract/addcarcustomer";
            public const string AddVehicleCartedContract = "api/cartedcontract/addvehicle";
            public const string AddAgentCartedContract = "api/cartedcontract/addagency";
            public const string AddAgentAndFeeCartedContract = "api/cartedcontract/addfeenagent";
            public const string AddFeeCartedContract = "api/cartedcontract/addfee";
            public const string AddAlterCartedContract = "api/cartedcontract/addalter";
            public const string GetListCartedContract = "api/cartedcontract/getlist";
            public const string DeleteCartedContract = "api/cartedcontract/delete";
            public const string SubmitIDApprove = "api/cartedcontract/submitapprove";
            public const string SubmitCodeToApprove = "api/cartedcontract/submittoapprove";
            public const string ApproveCartContract = "api/cartedcontract/approve";
            public const string GetApproveCartID = "api/cartedcontract/getapprovalhistory";
            public const string GetContractAlter = "api/cartedcontract/getcontractalter";
            public const string GetCarMaterialContractAlter = "api/cartedcontract/getcarmaterialcontractalter";
            public const string UpdateHealthInsureds = "api/cartedcontract/addinsuredpersonhealth";
            public const string AddInsuranceContractSDBS = "api/insurancecontract/addinsurcontractsdbs";
            public const string GetApproveContractID = "api/insurancecontract/getapprovalhistory";
            public const string UpdateContract = "api/cartedcontract/updatecontract";
            public const string UpdateStatusCartedContract = "api/cartedcontract/updatestatus";
            public const string GetContractByKeyType = "api/inscontract/getbykeytype";
            public const string CopyToCart = "api/insurancecontract/copytocart";
            public const string CancelAlterRequest = "api/contract/cancelalterrequest";
            public const string AddInsuranceContract = "api/insurancecontract/add3";
            public const string CheckContractApprove = "api/approvelimit/checkcontractapprove";
            public const string GetApprLimit = "api/approvelimit/getlimit";
            public const string CallPemiaMoto = "api/tndsbbxemay/getpolicynumber";
            public const string GetNotifyByUser = "api/notify/getnotifybyuser";
            public const string GetInsurAgent = "api/insuranceagent/getbyid";
            public const string GetProvince = "/api/province/getall";
            public const string ChangeViewNotify = "api/notify/changeviewnotify";
            public const string CheckNotify = "api/notify/checknotify";
            public const string GetExpireTime = "api/config/expiretime";
            public const string GetNewVersion = "api/config/getnewversion";
            public const string GetImageBanner = "api/banner/getimage";
            public const string AddInsuredPerson = "api/cartedcontract/addinsuredpersontravel";
            public const string AddBenForCartedContract = "api/cartedcontract/addben";
            public const string GetListInsuredPerson = "api/insuredperson/getlistinsuredperson";
            public const string AddTripInformation = "api/cartedcontract/addtrip";
            public const string GetListProgram = "api/internationtravelprogram/getlistprogram";
            public const string GetInternationalTravelFee = "api/internationaltravelfee/getfee";
            public const string AddTravelFee = "api/cartedcontract/addtravelfee";
            public const string GetTravelContractDetail = "api/cartedcontract/gettravelcontractdetail";
            public const string UpdateContractTravel = "api/cartedcontract/updatecontracttravel";
            public const string AddBillTravel = "api/cartedcontract/addbilltravel";
            public const string AddImageIDCard = "api/cartedcontract/addimageidcard";
            public const string GetHealthInsFee = "api/healthfee/getfee";
            public const string GetInsPersonCart = "api/insuredperson/getinsuredpersoncarted";
            public const string GetInsPersonContract = "api/insuredperson/getinsuredpersoncontract";
            public const string CheckInsPerExist = "api/insuredperson/checkexist";
            public const string DeleteInsPerson = "api/insuredperson/delete";
            public const string DelInsPer = "api/insuredperson/delete-insured";
            public const string GCNpdfAPI = "api-wa/insurance-certificate";
            public const string GCNpdfAPIhttp = "api/certi_contract/gcn-https";
            public const string GetDomesticTravelFee = "api/domestictravelfee/getfee";
            public const string GetCartHealthContractDetail = "api/cartedcontract/gethealthcontractdetail";
            public const string GetHealthContractDetail2 = "api/insurancecontract/gethealthcontractdetail";
            public const string UpdateContractHealth = "api/cartedcontract/updatecontracthealth";
            public const string UploadImage = "api/cartedcontract/uploadimage";
            public const string AddCarMaterialCartedContract = "api/cartedcontract/addcarmaterial";
            public const string CheckCarMaterialFee = "api/cartedcontract/checkcarmaterialfee";
            public const string GetMaterialDKBS = "api/cartedcontract/getdkbs";
            public const string GetAscDescFee = "api/ap_ascdesc_subfee/getascdescfee";
            public const string GetListCurrency = "api/currency/getlistcurrency";
            public const string GetInternationalTravelForVNFee = "api/internationaltravelforvnfee/getfee";
            public const string GetVNTravelForForeignerFee = "api/vntravelforforeignerfee/getfee";
            public const string AddHomeInfoForCartedContract = "api/cartedcontract/addhomeinfor";
            public const string GetToken = "api/notify/getToken";
            public const string CheckAccountToken = "api/notify/CheckTokenAccount";
            public const string GetTravelContractDetailForInsur = "api/insurancecontract/gettravelcontractdetail";
            public const string GetTravelQTContractDetail = "api/insurancecontract/gettravelcontractdetail";
            public const string GetCarMaterialImages = "api/cartedcontract/getcarmaterialimages";
            public const string GetCarMaterialByKey = "api/cartedcontract/getcarmaterialbykey";
            public const string GetCarMaterialAlterByKey = "api/cartedcontract/getcarmaterialalterbykey";
            public const string GetCarMaterialContractByKey = "api/cartedcontract/getcarmaterialcontractbykey";
            public const string Gettravelcontractalter = "api/insurancecontract/gettravelcontractalter";
            public const string UpdateContractCarMaterial = "api/cartedcontract/updatecontractVCXCG";
            public const string GetCarMaterialInsuranceContractByKey = "api/insurancecontract/getcarmaterialcontractbykey";
            public const string Getdivisiondatabygroupdivision = "api/lookuppijiconetwork/getdivisiondatabygroupdivision";
            public const string Getprovince = "api/province/getall";
            public const string Getbyfilter = "api/lookuppijiconetwork/getbyfilter";
            public const string lookuphealthfacilitynetwork = "api/lookuphealthfacilitynetwork/getbyprovince";
            public const string agetnearest = "api/lookuphealthfacilitynetwork/getnearest";
            public const string Getall = "api/groupdivision/getall";
            public const string VehicleBrand = "api/vehiclebrandgaragefacility/searchbycarbrandandprovince";
            public const string GetListGaraNear = "api/vehiclebrandgaragefacility/lookupbycoordinates";
            public const string GCN = "api/insurancecontract/searchlist";
            public const string searchGCN = "cus-api/insurancecontract/searchGCN"; //Tra cứu GCN
            public const string getlinkgasstation = "api-cus/cusvehiclebrandgaragefacility/getlinkgasstation"; //Tra cứu Cây xăng

            public const string GetMotoCompulsoryAlterHistory = "api/vehiclealterhistory/getbbxemaybycontractcode";
            public const string GetCarCompulsoryAlterHistory = "api/vehiclealterhistory/getbbotobycontractcode";
            public const string GetTravelAlterHistory = "api/contractalterhistory/getinternationaltravellistbycode";
            public const string GetHealthCancerAlterHistory = "api/healthalterhistory/getcancerbycontractcode";
            public const string GetHealthFatalDiseaseAlterHistory = "api/healthalterhistory/getdiseasebycontractcode";
            public const string GetHomeInsuranceAlterHistory = "api/contractalterhistory/gethomeinsurancehistorybycontractcode";

            public const string GetListDistrictAll = "api/district/getall";
            public const string GetListWardAll = "api/ward/getall";
            public const string EditInsuredPerson = "api/insuredperson/edit";


            public const string Addhomeinsurfee = "api/cartedcontract/addhomeinsurfee";
            public const string GetListInsuranceContract = "api/insurancecontract/getlist";

            public const string AddLogEx = "api/logex/create";

            public const string Gethomeinsurcontractdetail = "api/cartedcontract/gethomeinsurcontractdetail";
            public const string addinsuredperson = "api/cartedcontract/addinsuredperson";
            public const string addhomeinsurDefee = "api/cartedcontract/addhomeinsurDefee";
            public const string addbillhomeinsur = "api/cartedcontract/addbillhomeinsur";
            public const string updatecontracthomeinsur = "api/cartedcontract/updatecontracthomeinsur";
            public const string getprivatehousecontractdetail = "api/insurancecontract/getprivatehousecontractdetail";
            public const string getprivatehousecontractdetailbycarted = "api/cartedcontract/getprivatehousecontractdetail";
            public const string GetProvinceAgentGroupDivision = "api/lookuppjiconetwork/getdivisiondatabygroupdivision";
            public const string GetListPJC = "api/lookuppjiconetwork/getbyfilter";
            public const string GetListPJCNear = "api/lookuppjiconetwork/getnearest";

            public const string getprivatehousecontractalter = "api/insurancecontract/getprivatehousecontractalter";
            public const string getlistagentchild = "api/insuranceagent/getlistagentchild";
            public const string getlistrevenuecommission = "api/insurancecontract/getlistrevenuecommission";
            public const string getlistproductbycode = "api/insuranceproduct/getlistproductbycode";
            public const string getproductbycode = "api/insuranceproduct/getproductbycode";
            public const string CheckFeeNullOrZero = "api/cartedcontract/checkfeenullorzero";
            public const string login = "api-cus/account/login";
            public const string create = "api-cus/account/create";
            public const string change_password = "api-cus/account/change-password";

            public const string GetMenuUtility = "api-cus/CusMenuUtility/GetMenuUtility";
            public const string getmainmenu = "cus-api/cusmenu/getmainmenu";
            public const string getlistproduct = "cus-api/cusmenu/getlistproduct";
            public const string getlistproductbycode2 = "cus-api/cusinsuranceproduct/getlistproductbycode";
            public const string getproductbycode2 = "api-cus/insuranceproduct/getproductbycode";
            public const string getfiledata = "api-cus/doc/getfiledata";

            public const string get_vehicle_information_data = "api-cus/tndsbb-auto/get-vehicle-information-data";
            public const string getbykey = "cus-api/cartedcontract/getbykey";
            public const string vehicle_info = "api-cus/tndsbb-auto/vehicle-info";
            public const string insurance_duration = "api-cus/tndsbb-auto/insurance-duration";
            public const string cus_addfee = "cus-api/cartedcontract/addfee";
            public const string getfeedata = "cus-api/tndsbbfee/getfeedata";
            public const string updatefee = "cus-api/cartedcontract/updatefee";
            public const string getlistgiftcode = "cus-api/giftcode/getlistgiftcode";
            public const string checkgiftcode = "cus-api/giftcode/checkgiftcode";
            public const string addcustomer = "cus-api/cartedcontract/addcustomer";
            public const string getcustomerincarted = "cus-api/cartedcontract/getcustomerincarted";
            public const string check_exist_info = "api-cus/account/check-exist-info";

            public const string getmotoinsurancedetailbycode = "cus-api/cusmotoinsurancedetail/getmotoinsurancedetailbycode";
            public const string getcarinsurancedetailbycode = "cus-api/cuscarinsurancedetail/getcarinsurancedetailbycode";
            public const string getListCusReceiveBill = "cus-api/customerreceive/getListCusReceiveBill";
            public const string InsertCusReceiveBill = "cus-api/customerreceive/InsertCusReceiveBill";
            public const string getCusReceiveByID = "cus-api/customerreceive/getCusReceiveByID";
            public const string DeleteCusReceiveBill = "cus-api/customerreceive/DeleteCusReceiveBill";
            public const string UpdateCusReceiveBill = "cus-api/customerreceive/UpdateCusReceiveBill";
            public const string getpaymentinfo = "cus-api/pay/getpaymentinfo";
            public const string getGCNOtoDemo = "cus-api/cartedcontract/getGCNOtoDemo";
            public const string getdieukhoanquytac = "cus-api/insuranceproduct/getdieukhoanquytac"; 
            public const string createtransactionlog = "cus-api/pay/createtransactionlog";
            public const string getlistsdbs = "cus-api/insurancecontract/getlistsdbs"; 
            public const string addrequest = "cus-api/customercontractrequest/addrequest"; 
            public const string get_vehicle_information_data_XM = "api-cus/tndsbb-moto/get-vehicle-information-data";
            public const string vehicle_info_XM = "api-cus/tndsbb-moto/vehicle-info"; 
            public const string insurance_duration_XM = "api-cus/tndsbb-moto/insurance-duration";

            public const string getvehicleinformationdata = "api-cus/automaterial/getvehicleinformationdata";
            public const string getbyvehiclebrand = "api-cus/vehiclemodel/getbyvehiclebrand";
            public const string vehicleinfoimage = "api-cus/automaterial/vehicleinfoimage";
            public const string vehicleinfo = "api-cus/automaterial/vehicleinfo";

            public const string choosebuyfor = "api-cus/cartedcontract/choosebuyfor";
            public const string getvehicleinfoimagedata = "api-cus/automaterial/getvehicleinfoimagedata";
            public const string insurance_duration_VC = "api-cus/auto-material/insurance-duration";

            public const string getdkbsinfo = "cus-api/automaterial/getdkbsinfo";
            public const string addmaterialfee = "cus-api/cartedcontract/addmaterialfee";
            public const string getDKBSbykey = "cus-api/cartedcontract/getDKBSbykey";

            public const string addbillcartedcontract = "cus-api/cartedcontract/addbillcartedcontract";
            public const string napasok = "cus-api/pay/napasok";
            public const string getGCN = "cus-api/insurancecontract/getGCN";
            public const string getGCNDemo = "cus-api/cartedcontract/getGCNDemo";

            public const string getcarvcinsurancedetailbycode = "cus-api/cuscarinsurancedetail/getcarvcinsurancedetailbycode";
            public const string getbyprovince = "api-cus/lookuphospitalnetwork/getbyprovince";
            public const string lookupbycoordinates = "api-cus/cusvehiclebrandgaragefacility/lookupbycoordinates"; //Gara gần nhất
            public const string searchbycarbrandandprovince = "api-cus/cusvehiclebrandgaragefacility/searchbycarbrandandprovince";
            public const string getdivisiondatabygroupdivision = "api-cus/lookuppjiconetwork/getdivisiondatabygroupdivision";
            public const string getbyfilter_ = "api-cus/lookuppjiconetwork/getbyfilter";

            public const string loaddatainsuredpersonforhealthinsurance = "api-cus/healthinsurance/loaddatainsuredpersonforhealthinsurance";
            public const string loaddatainspackagebyage = "api-cus/healthinsurance/loaddatainspackagebyage";
            public const string loaddatainsuredpersonbyaccountId = "api-cus/healthinsurance/loaddatainsuredpersonbyaccountId";


           // public const string addinsuredpersonforhealthinsurance = "cus-api/cartedcontract/addinsuredpersonforhealthinsurance";

            public const string getbeneficiaryinfodata = "api-cus/healthinsurance/getbeneficiaryinfodata";
            public const string beneficiaryinfo = "api-cus/healthinsurance/beneficiaryinfo";

            public const string addinsuredpersonforhealthinsurance = "api-cus/healthinsurance/addinsuredpersonforhealthinsurance";

            public const string getlistbycontractcode = "api-cus/insuredperson/getlistbycontractcode";
            public const string deletebyid = "api-cus/insuredperson/deletebyid";


            public const string insuranceduration = "api-cus/healthinsurance/insuranceduration";
            public const string getfee = "cus-api/healthinsurance/getfee";

            public const string gethealthcontractdetail = "cus-api/healthinsurance/gethealthcontractdetail";


            public const string saveinformationinsuraceperson = "api-cus/custravelinsurance/saveinformationinsuraceperson";
            public const string loaddatainsuredpersonfortravelinsurance = "api-cus/custravelinsurance/loaddatainsuredpersonfortravelinsurance";
            public const string loaddatatravelinsuredpersonbyaccountId = "api-cus/custravelinsurance/loaddatatravelinsuredpersonbyaccountId";

            public const string getlistrenewalbyid = "cus-api/cusautoinsurancepolicyrenewal/getlistrenewalbyid";
            public const string beneficiaryinfo_DL = "api-cus/travelinsurance/beneficiaryinfo";
            public const string getbeneficiaryinfodata_DL = "api-cus/travelinsurance/getbeneficiaryinfodata";

            public const string updatetripinfo = "api-cus/travelinsurance/updatetripinfo";

            public const string getinsurancepolicydetailstravel = "api-cus/travelinsurance/getinsurancepolicydetailstravel";

            public const string gettripinfodata = "api-cus/travelinsurance/gettripinfodata";

            public const string getfee_DL = "cus-api/travelinsurance/getfee";
            public const string gettripinfodata0 = "api-cus/travelinsurance/gettripinfodata0";


            public const string gettripinfodata2 = "api-cus/travelinsurance/gettripinfodata2";
            public const string GetNewVersionAppCustomer = "api-cus/cusconfig/getnewversion";
            public const string copycontract = "api-cus/cusinsurancecontract/copycontract";
            public const string checkrequest = "cus-api/customercontractrequest/checkrequest";
            public const string updatehouseinfo = "api-cus/houseinsurance/updatehouseinfo";
            public const string gethouseinfodata = "api-cus/houseinsurance/gethouseinfodata";


            public const string getinsurancefeeinfodata = "api-cus/houseinsurance/getinsurancefeeinfodata";
            public const string insurancefeeinfo = "api-cus/houseinsurance/insurancefeeinfo";
            public const string loaddatahomeinsuredperson = "api-cus/cushouseinsurance/loaddatahomeinsuredperson";
            public const string loaddatahouseinsuredpersonbyaccountId = "api-cus/custravelinsurance/loaddatahouseinsuredpersonbyaccountId";
            public const string savehouseinformationinsuraceperson = "api-cus/houseinsurance/savehouseinformationinsuraceperson";
            public const string getbeneficiaryinfodata_NTN = "api-cus/houseinsurance/getbeneficiaryinfodata";
            public const string beneficiaryinfo_NTN = "api-cus/houseinsurance/beneficiaryinfo"; 
            public const string detailsofeachfee = "api-cus/houseinsurance/detailsofeachfee"; 
            public const string getinsurancepolicydetailshouse = "api-cus/houseinsurance/getinsurancepolicydetailshouse"; 
            public const string insuranceduration_NTN = "api-cus/houseinsurance/insuranceduration"; 
            public const string getGCN_K = "cus-api/insurancecontract/getGCN_K";
            public const string getGCNDemo_K = "cus-api/cartedcontract/getGCNDemo_K"; 
            public const string getotpcodeforgotpassword = "api-cus/account/getotpcodeforgotpassword";
            public const string newpassword = "api-cus/account/newpassword"; 
            public const string getaccounttabinfo = "api-cus/account/getaccounttabinfo";


            public const string updateinfo = "api-cus/customer/updateinfo";
            public const string getbyaccountid = "api-cus/customer/getbyaccountid";
            public const string savefeedback = "api-cus/account/savefeedback";
            public const string getnotifybyuser = "api-cus/cusnotify/getnotifybyuser";
            public const string getnews = "cus-api/cusnews/getnews";
            public const string changepassword = "api-cus/account/changepassword";
            public const string toggleaccountsetting = "api-cus/account/toggleaccountsetting";
            public const string getlistfrequentlyaskedquestion = "api-cus/account/getlistfrequentlyaskedquestion";
            public const string getbylistconfigkey = "api-cus/config/getbylistconfigkey";
            public const string getbyconfigkey = "api-cus/config/getbyconfigkey";
            public const string getinsurancecontractbought = "api-cus/insurancecontract/getinsurancecontractbought";
            public const string getcartedcontractincomplete = "api-cus/cartedcontract/getcartedcontractincomplete";

            public const string checkfeenullorzero = "cus-api/cartedcontract/checkfeenullorzero";
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
