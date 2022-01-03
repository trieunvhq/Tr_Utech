using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Flurl;
using Flurl.Http;
using System.Configuration;
using log4net;

namespace AMIS.APIManagement
{
    public class ConnectManagement
    {
        private static string user = ConfigurationManager.AppSettings["userAPI"].ToString().Trim();
        private static string pass = ConfigurationManager.AppSettings["passAPI"].ToString().Trim();
        private static string url = ConfigurationManager.AppSettings["urlAPI"].ToString().Trim();
        static readonly ILog m_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static class SingletonHelper
        {
            public static readonly IFlurlRequest INSTANCE = url
               .AllowAnyHttpStatus()
               .WithBasicAuth(user, pass);
            public static readonly FlurlClient _clientINSTANCE = new FlurlClient(url).WithBasicAuth(user, pass).AllowAnyHttpStatus();
        }
        public static IFlurlRequest GetRequestInstance()
        {
            SingletonHelper.INSTANCE.Url = url;
            return SingletonHelper.INSTANCE;
        }

        public static FlurlClient getClientInstance()
        {
            return SingletonHelper._clientINSTANCE;
        }

        public static JToken GetJSONFromAPI(string method, object paramaters)
        {
            try
            {
                Task<HttpResponseMessage> _APIClient = GetRequestInstance()
                    //.AppendPathSegment("api")
                    .AppendPathSegment(method)
                    .SetQueryParams(paramaters)
                    .GetAsync();

                Task<string> result = _APIClient.ReceiveString();
                return JObject.Parse(result.Result);
            }
            catch(Exception ex)
            {
                m_log.Debug("Error:" + ex.Message);
                throw ex;
            }
        }

        public static T PostObjectToAPI<T>(string method, object data, string rootNameObject) where T : class
        {
            try
            {
                var jobInJson = JsonConvert.SerializeObject(data);
                //ignore root                                
                var json_no_root = JObject.Parse(jobInJson).SelectToken(rootNameObject).ToString();
                m_log.Debug("Json:" + json_no_root);
                var json = new StringContent(json_no_root, Encoding.UTF8);
                json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");

                Task<T> _APIClient = GetRequestInstance()
                    .AppendPathSegment(method)
                    .PostAsync(json)
                    .ReceiveJson<T>();

                return _APIClient.Result;
            }
            catch (Exception ex)
            {
                m_log.Debug("Error:" + ex.Message);
                throw ex;
            }
        }

        public static T GetObjectFromAPI<T>(string path, object paramaters) where T:class
        {
            try
            {
                Task<T> _APIClient = GetRequestInstance()
                    .AppendPathSegment(path)
                    .SetQueryParams(paramaters)
                    .GetJsonAsync<T>();
                //HttpStatusCode status = (HttpStatusCode)_APIClient.Result;
                //Task<string> result = _APIClient.ReceiveString();
                return _APIClient.Result;
            }
            catch(Exception ex)
            {
                m_log.Debug("Error:" + ex.Message);
                throw ex;
            }
        }

        public static T GetObjectFromAPI<T>(string path) where T : class
        {
            try
            {
                Task<T> _APIClient = GetRequestInstance()
                    .AppendPathSegment(path)                
                    .GetJsonAsync<T>();
                
                return _APIClient.Result;
            }
            catch (Exception ex)
            {
                m_log.Debug("Error:" + ex.Message);
                throw ex;
            }
        }
    }
}
