using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB
{
    public class JObjectUtils
    {
        public static string GetValueByKeyJObject(JObject jobj, string key)
        {
            foreach (KeyValuePair<string, JToken> keyValuePair in jobj)
            {
                if (keyValuePair.Key == key)
                    return keyValuePair.Value.ToString();
            }
            return string.Empty;
        }
    }
}
