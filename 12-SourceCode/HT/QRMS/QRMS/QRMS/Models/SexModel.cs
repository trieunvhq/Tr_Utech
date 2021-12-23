using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class SexModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static IEnumerable<SexModel> genderModels
        {
            get
            {
                yield return new SexModel() { Key = "1", Value = "Nam" };
                yield return new SexModel() { Key = "2", Value = "Nữ" };
                yield return new SexModel() { Key = "3", Value = "Khác" };
            }
        }
    }
}
