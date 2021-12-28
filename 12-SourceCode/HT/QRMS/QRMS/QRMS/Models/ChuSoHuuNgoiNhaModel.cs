using System;
using System.Collections.Generic;

namespace QRMS.Models
{
    public class ChuSoHuuNgoiNhaModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static IEnumerable<ChuSoHuuNgoiNhaModel> genderModels
        {
            get
            {
                yield return new ChuSoHuuNgoiNhaModel() { Key = "1", Value = "Chủ sở hữu" };
                yield return new ChuSoHuuNgoiNhaModel() { Key = "2", Value = "Sở hữu nhà cho thuê" };
                yield return new ChuSoHuuNgoiNhaModel() { Key = "3", Value = "Người cho thuê" };
            }
        }
    }
}
