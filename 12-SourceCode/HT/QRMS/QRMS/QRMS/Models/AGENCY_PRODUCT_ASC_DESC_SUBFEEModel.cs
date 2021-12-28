using Newtonsoft.Json;
using QRMS.AppLIB.Common;
using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public partial class AGENCY_PRODUCT_ASC_DESC_SUBFEEModel : Notifiable
    {
        public int ID { get; set; }
        public int AGENCY_PRODUCT_ID { get; set; }
        public int INSURANCE_AGENT_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public int PRODUCT_COST_BASIC_ID { get; set; }
        public string COST_BASIC_CODE { get; set; }
        public string COST_BASIC_NAME { get; set; }
        public decimal? FEE_INCREASE_RATIO_F { get; set; }
        public decimal? FEE_INCREASE_RATIO_T { get; set; }
        public decimal? FEE_DECREASE_RATIO_F { get; set; }
        public decimal? FEE_DECREASE_RATIO_T { get; set; }
        public DateTime ACTIVE_DATE_START { get; set; }
        public DateTime? ACTIVE_DATE_END { get; set; }
        public string REMARK { get; set; }
        public string STATUS_RECORD { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public int? UPDATE_USER_ID { get; set; }
    }

    public partial class AGENCY_PRODUCT_ASC_DESC_SUBFEEModel
    {
        [JsonIgnore]
        public bool IsAllowAlter => ((FEE_DECREASE_RATIO_F ?? 0) != 0 && (FEE_INCREASE_RATIO_F ?? 0) != 0) || ((FEE_DECREASE_RATIO_T ?? 0) != 0 && (FEE_INCREASE_RATIO_T ?? 0) != 0);
    }

    public static class extendAGENCY_PRODUCT_ASC_DESC_SUBFEEModel
    {
        public static bool isIncr(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj)
        {
            return ((obj?.FEE_INCREASE_RATIO_F ?? 0) > 0)
                || ((obj?.FEE_INCREASE_RATIO_T ?? 0) > 0);
        }
        public static bool isDecr(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj)
        {
            return ((obj?.FEE_DECREASE_RATIO_F ?? 0) > 0)
                || ((obj?.FEE_DECREASE_RATIO_T ?? 0) > 0);
        }
        public static bool isAdjust(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj)
        {
            if (obj == null) return false;
            return (obj.isIncr() || obj.isDecr());
        }

        public static decimal? GetInrcRatio(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj,decimal? value)
        {
            if (value < obj?.FEE_INCREASE_RATIO_F) return obj?.FEE_INCREASE_RATIO_F;
            if (value > obj?.FEE_INCREASE_RATIO_T) return obj?.FEE_INCREASE_RATIO_T;
            else return value.Abs();
        }
        public static decimal? GetDercRatio(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj,decimal? value)
        {
            if (value < obj?.FEE_DECREASE_RATIO_F) return obj?.FEE_DECREASE_RATIO_F;
            if (value > obj?.FEE_DECREASE_RATIO_T) return obj?.FEE_DECREASE_RATIO_T;
            else return value.Abs();
        }

        public static string GetIncrErrorMess(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj)
        {
            string _result = string.Empty;
            if (((obj?.FEE_INCREASE_RATIO_F.Abs() ?? 0) > 0) && ((obj?.FEE_INCREASE_RATIO_T.Abs() ?? 0) > 0))
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError1, obj?.FEE_INCREASE_RATIO_F.Abs(), obj?.FEE_INCREASE_RATIO_T.Abs());
            }
            else if ((obj?.FEE_INCREASE_RATIO_F.Abs() ?? 0) > 0)
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError2, obj?.FEE_INCREASE_RATIO_F.Abs());
            }
            else if ((obj?.FEE_INCREASE_RATIO_T.Abs() ?? 0) > 0)
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError3, obj?.FEE_INCREASE_RATIO_T.Abs());
            }
            return _result;
        }

        public static string GetDecrErrorMess(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj)
        {
            string _result = string.Empty;
            if (((obj?.FEE_DECREASE_RATIO_F.Abs() ?? 0) > 0) && ((obj?.FEE_DECREASE_RATIO_T.Abs() ?? 0) > 0))
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError1, obj?.FEE_DECREASE_RATIO_F.Abs(), obj?.FEE_DECREASE_RATIO_T.Abs());
            }
            else if ((obj?.FEE_DECREASE_RATIO_F.Abs() ?? 0) > 0)
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError2, obj?.FEE_DECREASE_RATIO_F.Abs());
            }
            else if ((obj?.FEE_DECREASE_RATIO_T.Abs() ?? 0) > 0)
            {
                _result = string.Format(QRMS.Resources.AppResources.TravelRateInputError3, obj?.FEE_DECREASE_RATIO_T.Abs());
            }
            return _result;
        }

        public static string GetRatioError(this AGENCY_PRODUCT_ASC_DESC_SUBFEEModel obj,bool IsIncr)
        {
            if (IsIncr)
            {
                return obj?.GetIncrErrorMess();
            }
            else
            {
                return obj?.GetDecrErrorMess();
            }
        }
    }
}
