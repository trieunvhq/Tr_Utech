using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class TNDSBBOtoModel
    {
        public string custType { get; set; }
        public string custName { get; set; }
        public string custDob { get; set; }
        public string custId { get; set; }
        public string cusIdType { get; set; }
        public string custIdIssueDt { get; set; }
        public string custIdIssuePlace { get; set; }
        public string custSex { get; set; }
        public string custNationality { get; set; }
        public string custAddress { get; set; }
        public string custAddrCountry { get; set; }
        public string custAddrProvince { get; set; }
        public string custAddrDistrict { get; set; }
        public string custPhone { get; set; }
        public string custEmail { get; set; }
        public string OriginOfProduction { get; set; }
        public string HasRegnNo { get; set; }
        public string RegnNo { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string MakeNo { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public short? SeatingCapacity { get; set; }
        public int VehWeight { get; set; }
        public string VehUseType { get; set; }
        public string YearOfManufactory { get; set; }
        public string polIssueDate { get; set; }
        public string polStartDate { get; set; }
        public string polEndDate { get; set; }
        public int polLcSi_NN { get; set; }
        public int? polLcSi_HK { get; set; }
        public int polLcSi_TS { get; set; }
        public int PolLcVehPremium { get; set; }
        public int PolLcVehVAT { get; set; }
        public int PolLcVehTotal { get; set; }
        public int IsVoluntaryPol { get; set; }
        public int polLcSi_TNNN { get; set; }
        public int polLcSi_TNHK { get; set; }
        public int polLcSi_TNTS { get; set; }
        public int PolLcTNMaxSI { get; set; }
        public int PolLcVehTNPremium { get; set; }
        public int PolLcVehTNVAT { get; set; }
        public int PolLcVehTNTotal { get; set; }
        public int IsPAforCoDriverPol { get; set; }
        public int PALevelAmount { get; set; }
        public int NumOfDrivers { get; set; }
        public int polPAPremium { get; set; }
        public int polPAVat { get; set; }
        public int polPAAmount { get; set; }
        public decimal polPAFeeRate { get; set; }
        public int IsPassagerPol { get; set; }
        public int PassagerLevelAmount { get; set; }
        public int NumOfPassagers { get; set; }
        public int polPassagerPremium { get; set; }
        public int polPassagerVat { get; set; }
        public int polPassagerAmount { get; set; }
        public decimal polPassagerFeeRate { get; set; }
        public int IsCargoPol { get; set; }
        public int CargoLevelAmount { get; set; }
        public int CargoLevelMaxAmount { get; set; }
        public int CargoWeight { get; set; }
        public string CargoVerhType { get; set; }
        public int polCargoPremium { get; set; }
        public int polCargoVat { get; set; }
        public int polCargoAmount { get; set; }
        public decimal polCargoFeeRate { get; set; }
        public int polLcPremium { get; set; }
        public int polVat { get; set; }
        public int polAmount { get; set; }
        public string PhHasInvoice { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceVatNumber { get; set; }
        public string InvoiceBusinessAddr { get; set; }
        public string InvoiceReceiveAddr { get; set; }
        public decimal polPAFeeRateDiscount { get; set; }
        public int polPAFeeAmountDiscount { get; set; }
        public decimal polPassagerFeeRateDiscount { get; set; }
        public int polPassagerFeeAmountDiscount { get; set; }
        public decimal polCargoFeeRateDiscount { get; set; }
        public int polCargoFeeAmountDiscount { get; set; }
        public decimal polTNFeeRateDiscount { get; set; }
        public int polTNFeeAmountDiscount { get; set; }
        public string partnerPassword { get; set; }
    }

    public class TNDSBBdataModel
    {
        public int ID { get; set; }
        public Nullable<int> COMMON_TYPE_ID { get; set; }
        public string COMMON_TYPE_CODE { get; set; }
        public string INTENT_BUSINESS_CODE { get; set; }
        public string INTENT_BUSINESS_NAME { get; set; }
        public Nullable<int> COMMON_ID { get; set; }
        public string COMMON_CODE { get; set; }
        public string COMMON_NAME { get; set; }
        public Nullable<int> NUMBER_OF_SEATS_F { get; set; }
        public Nullable<int> NUMBER_OF_SEATS_T { get; set; }
        public Nullable<decimal> WEIGHT_TON_F { get; set; }
        public Nullable<decimal> WEIGHT_TON_T { get; set; }
        public Nullable<decimal> TNDSBB_FEE { get; set; }
        public Nullable<decimal> CREW_PERCENT_FEE { get; set; }
        public Nullable<decimal> PSGR_PERCENT_FEE { get; set; }
        public Nullable<decimal> ASSET_PERCENT_FEE { get; set; }
        public string DESCRIPTION { get; set; }
        public string COMMON_TYPE_NAME { get; set; }
        public Nullable<decimal> VAT_BB { get; set; }
        public Nullable<decimal> VAT_TNTT { get; set; }
        public Nullable<int> NUMBER_OF_PEOPLE_EXCLUDED { get; set; }
    }
}
