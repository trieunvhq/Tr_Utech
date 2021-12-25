using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QRMSWeb.Services;
using QRMSWeb.Utils;

namespace QRMSWeb.Helper
{
    public static class HttpHelper
    {
        public static async Task<ResponseData<T>> GetDataResponse<T>(HttpResponseMessage responseMessage)
        {
            try
            {
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(responseBody);
                return responseData;
            }
            catch (Exception) { }
            return null;
        }
    }
}
