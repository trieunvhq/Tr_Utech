namespace QRMSWeb.Constants
{
    public class Constants
    {
        public const string AGENT_TYPE_CODE_DIVISION = "202";
        public  const string DEPARTMENT_INDIVIDUAL_TYPE = "102";
        public  const string DEPARTMENT_GROUP_TYPE = "101";

        public  const string PERMISSION_ACTION_COMMON_TYPE_CODE = "AC";

        public  const string LIMIT_TYPE_CODE = "HM";

        public const string COMMON_TYPE_CODE_TRAVEL_AREA = "TRAVEL_AREA";
        public  const string COMMON_TYPE_CODE_CAR = "VEH_CN_C";
        public const string COMMON_TYPE_CODE_MOTO = "VEH_CN_M";
        public const string COMMON_TYPE_CODE_HH = "VEH_HH";
        public const string SAME_OWNER = "VEH_HH_CX";
        

        public const string COMMON_TYPE_CODE_DOMESTIC_NAME = "19";

        public const string Health_CancerFee = "C";
        public const string Health_FatalDiseaseFee = "F";
        public const string CURR_CODE_VND = "VNĐ";
        public const string CURR_NAME_VND = "VIETNAM DONG";
        public const string COMON_TYPE_CAR_MATERIAL = "24";
        public const string DKBS001_CODE = "001";
        public const string VEH_TYPE_CODE_09 = "09";

        public const string DEFAULT_COUNTRY_CODE = "VN";

        public const int MAX_UPLOAD_FILE = 209715200;
    }
    public class HisEffectDateTable
    {
        public const string HisTable = "HIS";
        public const string MainTable = "MAIN";
    }
    public class RecordStatus
    {
        public const string Delete = "D";
        public const string Update = "U";
        public const string Create = "N";
        public const string Locked = "L";
    }
    public class NotifyType
    {
        public const int SYSTEM_NOTIFY = 0;
        public const int BUSINESS_NOTIFY = 1;

    }
    public class NotifyCategory
    {
        public const string NewNews = "NC_01";
        public const string COMMON = "NC_02";
        public const string SYSTEM = "NC_03";
    }
}