using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class HealthContractDetailModel : BaseContractModel
    {
        #region bộ phí của bảo hiểm bệnh ung thư
        public int? HEALTH_CANCER_PACKAGE_ID { get; set; }
        public decimal? HEALTH_CANCER_INSURED_LVL { get; set; }
        public decimal? HEALTH_CANCER_INSURED_VAL { get; set; }
        public decimal? HEALTH_CANCER_DEDUCT { get; set; }
        public decimal? HEALTH_CANCER_PRICE { get; set; }
        public decimal? HEALTH_CANCER_DE_PRICE { get; set; }
        public decimal? HEALTH_CANCER_DE_RATIO { get; set; }
        public decimal? HEALTH_CANCER_VAT { get; set; }
        #endregion

        #region bộ phí của bảo hiểm bệnh hiểm nghèo
        public decimal? HEALTH_DISEASE_PACKAGE_ID { get; set; }
        public decimal? HEALTH_DISEASE_INSURED_LVL { get; set; }
        public decimal? HEALTH_DISEASE_INSURED_VAL { get; set; }
        public decimal? HEALTH_DISEASE_DEDUCT { get; set; }
        public decimal? HEALTH_DISEASE_PRICE { get; set; }
        public decimal? HEALTH_DISEASE_DE_PRICE { get; set; }
        public decimal? HEALTH_DISEASE_DE_RATIO { get; set; }
        public decimal? HEALTH_DISEASE_VAT { get; set; }
        #endregion

        public List<InsuredPersonDetailModel> listPerson { get; set; } = new List<InsuredPersonDetailModel>();
    }

    public class HealthContractDetailAlterModel : BaseAlterContract
    {
        #region bộ phí của bảo hiểm bệnh ung thư
        public bool HEALTH_CANCER_PACKAGE_ID { get; set; } = false;
        public bool HEALTH_CANCER_INSURED_LVL { get; set; } = false;
        public bool HEALTH_CANCER_INSURED_VAL { get; set; } = false;
        public bool HEALTH_CANCER_DEDUCT { get; set; } = false;
        public bool HEALTH_CANCER_PRICE { get; set; } = false;
        public bool HEALTH_CANCER_DE_PRICE { get; set; } = false;
        public bool HEALTH_CANCER_DE_RATIO { get; set; } = false;
        public bool HEALTH_CANCER_VAT { get; set; } = false;
        public Color HEALTH_CANCER_PACKAGE_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_PACKAGE_ID); } }
        public Color HEALTH_CANCER_INSURED_LVL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_INSURED_LVL); } }
        public Color HEALTH_CANCER_INSURED_VAL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_INSURED_VAL); } }
        public Color HEALTH_CANCER_DEDUCT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_DEDUCT); } }
        public Color HEALTH_CANCER_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_PRICE); } }
        public Color HEALTH_CANCER_DE_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_DE_PRICE); } }
        public Color HEALTH_CANCER_DE_RATIO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_DE_RATIO); } }
        public Color HEALTH_CANCER_VAT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_CANCER_VAT); } }

        #endregion

        #region bộ phí của bảo hiểm bệnh hiểm nghèo
        public bool HEALTH_DISEASE_PACKAGE_ID { get; set; } = false;
        public bool HEALTH_DISEASE_INSURED_LVL { get; set; } = false;
        public bool HEALTH_DISEASE_INSURED_VAL { get; set; } = false;
        public bool HEALTH_DISEASE_DEDUCT { get; set; } = false;
        public bool HEALTH_DISEASE_PRICE { get; set; } = false;
        public bool HEALTH_DISEASE_DE_PRICE { get; set; } = false;
        public bool HEALTH_DISEASE_DE_RATIO { get; set; } = false;
        public bool HEALTH_DISEASE_VAT { get; set; } = false;
        public Color HEALTH_DISEASE_PACKAGE_ID_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_PACKAGE_ID); } }
        public Color HEALTH_DISEASE_INSURED_LVL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_INSURED_LVL); } }
        public Color HEALTH_DISEASE_INSURED_VAL_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_INSURED_VAL); } }
        public Color HEALTH_DISEASE_DEDUCT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_DEDUCT); } }
        public Color HEALTH_DISEASE_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_PRICE); } }
        public Color HEALTH_DISEASE_DE_PRICE_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_DE_PRICE); } }
        public Color HEALTH_DISEASE_DE_RATIO_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_DE_RATIO); } }
        public Color HEALTH_DISEASE_VAT_Color { get { return AppLIB.Common.MobileLib.GetColorChangeStatus(HEALTH_DISEASE_VAT); } }

        #endregion


        #region constructor
        public HealthContractDetailAlterModel() : base() { }
        public HealthContractDetailAlterModel(HealthContractDetailModel oldContract, HealthContractDetailModel newContract) : base(oldContract, newContract)
        {
            try
            {
                PropertyInfo[] lstProp = typeof(HealthContractDetailAlterModel).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                for (int i = 0; i < lstProp.Length; i++)
                {
                    if (lstProp[i].SetMethod == null) { continue; }
                    var _oldValue = oldContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(oldContract);
                    var _newValue = newContract?.GetType().GetProperty(lstProp[i].Name)?.GetValue(newContract);

                    var isChange = _oldValue?.ToString() != _newValue?.ToString();
                    lstProp[i].SetValue(this, isChange);
                }
            }
            catch { }
        }
        #endregion
    }
}
