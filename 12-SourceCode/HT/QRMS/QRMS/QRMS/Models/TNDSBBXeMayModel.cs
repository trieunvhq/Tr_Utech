using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class TNDSBBXeMayModel
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
        public string VehicleType { get; set; }
        public string polIssueDate { get; set; }
        public string polStartDate { get; set; }
        public string polEndDate { get; set; }
        public int PolLcVehPremium { get; set; }
        public int PolLcVehVAT { get; set; }
        public int PolLcVehTotal { get; set; }
        public int IsPassagerPol { get; set; }
        public int PassagerLevelAmount { get; set; }
        public int NumOfPassagers { get; set; }
        public int polPassagerPremium { get; set; }
        public int polPassagerVat { get; set; }
        public int polPassagerAmount { get; set; }
        public int polLcPremium { get; set; }
        public int polVat { get; set; }
        public int polAmount { get; set; }
        public string PhHasInvoice { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceVatNumber { get; set; }
        public string InvoiceBusinessAddr { get; set; }
        public string InvoiceReceiveAddr { get; set; }
        public string CertReceiveAddr { get; set; }
        public string partnerPassword { get; set; }
        public int polLcSi_NN { get; set; }
        public int polLcSi_TS { get; set; }
        public int IsTNPol { get; set; }
        public int polLcSi_TNNN { get; set; }
        public int polLcSi_TNTS { get; set; }
        public int PolLcVehTNPremium { get; set; }
        public int PolLcVehTNVAT { get; set; }
        public int PolLcVehTNTotal { get; set; }
        public int polFeeAmountDiscount { get; set; }
        public decimal polFeeRateDiscount { get; set; }
        public int polPassagerAmountDiscount { get; set; }
        public decimal polPassagerRateDiscount { get; set; }
        public int polTNAmountDiscount { get; set; }
        public decimal polTNRateDiscount { get; set; }
    }
}
