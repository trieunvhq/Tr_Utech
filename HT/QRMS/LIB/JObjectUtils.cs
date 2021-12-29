using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LIB
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
        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}
