using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class AdjustmentModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static IEnumerable<AdjustmentModel> GetAdjustments()
        {
            yield return new AdjustmentModel() { Key = "1", Value = QRMS.Resources.AppResources.ReceiptInformationIncrease };
            yield return new AdjustmentModel() { Key = "2", Value = QRMS.Resources.AppResources.ReceiptInformationDecrease };
        }
    }
}
