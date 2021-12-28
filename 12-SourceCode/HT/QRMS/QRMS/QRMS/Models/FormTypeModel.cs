using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public static class FormTypeModel
    {
        public static int UserID { get; set; }
        public static string Status { get; set; }
        public static string InsurProductCode { get; set; }
        public static string VehicleInsuranceType { get; set; }
        public static string VehicleType { get; set; }
        public static string Page { get; set; }
        public static string Key { get; set; }
        public static string InsuranceStatus { get; set; }
        public static double VAT { get; set; }
        public static string IssueType { get; set; }
        public static string ContractID { get; set; }
        public static int AgentID { get; set; }
        public static Models.InsuranceAgentModel AgentModel {get;set;}
         
        public static Nullable<int> T_NDBH_ID{ get; set; }
        public static string T_InsurMode { get; set; }
        public static int T_SNDBH { get; set; }
        public static string T_Mode { get; set; }
        public static string T_Page { get; set; }
        public class VehicleIns
        {
            public static string purposeCode;
            public static string vehType;
            public static short? seatNumber;
            public static decimal? weightTon;

        }
    }
}
