using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Common
{
    public class CommonType
    {
        public enum TypeCode
        {
            HealthInsPackages = 15,
            InsuredRelation = 16,
            BenefitRelation = 17
        }
        public static IDictionary<TypeCode, string> TypeName = new Dictionary<TypeCode, string>()
        {
            {TypeCode.HealthInsPackages,"Gói bảo hiểm sức khỏe" },
            {TypeCode.InsuredRelation,"Mối quan hệ của người được bảo hiểm" },
            {TypeCode.BenefitRelation,"Mối quan hệ của người thụ hưởng" }
        };
    }

    public class CommonValue
    {
        
    }
}
