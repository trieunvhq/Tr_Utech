using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LIB.Common;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.IO;
using System.Net;

namespace LIB
{
    public class APICaller
    {
        public static string AccessToken { get; set; }

        //public static JToken GetJSONFromAPI(string url, string method, object paramaters)
        //{
        //    try
        //    {
        //        Task<string> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            //.AppendPathSegment("api")
        //            .AppendPathSegment(method)
        //            .SetQueryParams(paramaters)
        //            .GetAsync().ReceiveString();

        //        return JObject.Parse(_APIClient.Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static T PostObjectToAPI<T>(string url, string method, object data)
        //{
        //    try
        //    {
        //        var jobInJson = JsonConvert.SerializeObject(data);
        //        //Logging.LogMessage("Json:" + jobInJson);
        //        var json = new StringContent(jobInJson, Encoding.UTF8);
        //        json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");

        //        var _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(method)
        //            .PostAsync(json)
        //            .ReceiveJson<T>();

        //        //var _result = JsonConvert.DeserializeObject<T>(_APIClient.Result);

        //        return _APIClient.Result;
        //        //return _result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static Task<T> PostObjectToAPIAsync<T>(string url, string method, object data)
        //{
        //    var jobInJson = JsonConvert.SerializeObject(data);
        //    //Logging.LogMessage("Json:" + jobInJson);
        //    var json = new StringContent(jobInJson, Encoding.UTF8);
        //    json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");

        //    var _APIClient = url
        //        .AllowAnyHttpStatus()
        //        .AppendPathSegment(method)
        //        .PostAsync(json)
        //        .ReceiveJson<T>();
        //    return _APIClient;
        //}

        //public static BaseAPIModel<T> PostObjectToAPImodel<T>(string url, string method, object data)
        //{
        //    var _return = new BaseAPIModel<T>();
        //    try
        //    {
        //        var jobInJson = JsonConvert.SerializeObject(data);
        //        var json = new StringContent(jobInJson, Encoding.UTF8);
        //        json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");

        //        var _APIClient = url
        //            //.AllowAnyHttpStatus()
        //            .AppendPathSegment(method)
        //            .PostAsync(json)
        //            .ReceiveJson<T>();

        //        _return.data = _APIClient.Result;
        //        _return.RespondCode = (int)HttpStatusCode.OK;
        //    }
        //    catch (FlurlHttpTimeoutException ex)
        //    {
        //        _return.RespondCode = 600;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        _return.RespondCode = ((int?)ex.Call?.Response?.StatusCode) ?? 600;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    catch (Exception ex)
        //    {
        //        _return.RespondCode = 700;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    return _return;
        //}
        public static async Task<BaseAPIModel<T>> PostObjectToAPImodelAsync2222222<T>(string url, string method, object data)
        {
            var _return = new BaseAPIModel<T>();
            try
            {
                var json = new StringContent("", Encoding.UTF8);
                if (data != null && data.ToString() != "")
                {
                    var jobInJson = JsonConvert.SerializeObject(data);
                   
                    json = new StringContent(jobInJson, Encoding.UTF8);
                    json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");
                }

                var _APIClient = await url.WithTimeout(35)
                    .AllowAnyHttpStatus()
                    .AppendPathSegment(method)
                    .WithOAuthBearerToken(AccessToken)
                    .PostAsync(json)
                    .ReceiveJson<T>().ConfigureAwait(false);

                _return.data = _APIClient;
                _return.RespondCode = (int)HttpStatusCode.OK;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                _return.RespondCode = 600;
                _return.Message = $"{ex.Message} - body: {ex.Call.RequestBody}";
                _return.data = default(T);
            }
            catch (FlurlHttpException ex)
            {
                _return.RespondCode = ((int?)ex.Call?.Response?.StatusCode) ?? 600;
                _return.Message = $"{ex.Message} - body: {ex.Call.RequestBody}";
                _return.data = default(T);
            }
            catch (Exception ex)
            {
                _return.RespondCode = 700;
                _return.Message = ex.Message;
                _return.data = default(T);
            }
            return _return;
        }
       
        public static string myjson = "";
        public static async Task<BaseAPIModel<T>> PostObjectToAPImodelAsync<T>(string url, string method, object data)
        {
            var _return = new BaseAPIModel<T>();
            try
            {
                var json = new StringContent("", Encoding.UTF8);
                if (data != null && data.ToString() != "")
                {
                    var jobInJson = JsonConvert.SerializeObject(data);
                    myjson = jobInJson;
                  
                    json = new StringContent(jobInJson, Encoding.UTF8);
                    json.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; odata=verbose");

                }

                var _APIClient = await url.WithTimeout(35)
                    .AllowAnyHttpStatus()
                    .AppendPathSegment(method)
                    .WithOAuthBearerToken(AccessToken)
                    .PostAsync(json)
                    .ReceiveJson<T>().ConfigureAwait(false);

                _return.data = _APIClient;
                _return.RespondCode = (int)HttpStatusCode.OK;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                _return.RespondCode = 600;
                _return.Message = $"{ex.Message} - body: {ex.Call.RequestBody}";
                _return.data = default(T);
            }
            catch (FlurlHttpException ex)
            {
                _return.RespondCode = ((int?)ex.Call?.Response?.StatusCode)??600;
                _return.Message = $"{ex.Message} - body: {ex.Call.RequestBody}"   ;
                _return.data = default(T);
            }
            catch (Exception ex)
            {
                _return.RespondCode = 700;
                _return.Message = ex.Message;
                _return.data = default(T);
            }
            return _return;
        } 

        //public static T GetObjectFromAPI<T>(string url, string path, object paramaters)
        //{
        //        Task<T> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .SetQueryParams(paramaters)
        //            .GetJsonAsync<T>();
        //        return _APIClient.Result;
        //}


        //public static Task<T> GetObjectFromAPIAsync<T>(string url, string path, object paramaters)
        //{
        //    try
        //    {
        //        Task<T> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .SetQueryParams(paramaters)
        //            .GetJsonAsync<T>();
        //        return _APIClient;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static BaseAPIModel<T> GetObjectFromAPImodel<T>(string url, string path, object paramaters)
        //{
        //    var _return = new BaseAPIModel<T>();
        //    try
        //    {
        //        Task<T> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .SetQueryParams(paramaters)
        //            .GetJsonAsync<T>();
        //        _return.data = _APIClient.Result;
        //        _return.RespondCode = (int)HttpStatusCode.OK;
        //    }
        //    catch (FlurlHttpTimeoutException ex)
        //    {
        //        _return.RespondCode = 600;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        _return.RespondCode = ((int?)ex.Call?.Response?.StatusCode) ?? 600;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    catch (Exception ex)
        //    {
        //        _return.RespondCode = 700;
        //        _return.Message = ex.Message;
        //        _return.data = default(T);
        //    }
        //    return _return;
        //}

        public static async Task<BaseAPIModel<T>> GetObjectFromAPImodelAsync<T>(string url, string path, object paramaters)
        {
            var _return = new BaseAPIModel<T>();
            try
            {
                T _APIClient = await url/*.WithTimeout(5)*/
                    .AllowAnyHttpStatus()                    
                    .AppendPathSegment(path)
                    .WithOAuthBearerToken(AccessToken)
                    .SetQueryParams(paramaters)
                    .GetJsonAsync<T>().ConfigureAwait(false);

                _return.data = _APIClient;
                _return.RespondCode = (int)HttpStatusCode.OK;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                _return.RespondCode = 600;
                _return.Message = ex.Message;
                _return.data = default(T);
            }
            catch (FlurlHttpException ex)
            {
                _return.RespondCode = ((int?)ex.Call?.Response?.StatusCode) ?? 600;
                _return.Message = ex.Message;
                _return.data = default(T);
            }
            catch (Exception ex)
            {
                _return.RespondCode = 700;
                _return.Message = ex.Message;
                _return.data = default(T);
            }
            return _return;
        }
         
        //public static Task<Stream> GetStreamFromAPIAsync(string url, string path, object paramaters)
        //{
        //    try
        //    {
        //        Task<Stream> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .SetQueryParams(paramaters)
        //            .GetStreamAsync();
        //        return _APIClient;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static  Stream GetStreamFromAPI(string url, string path, object paramaters)
        //{
        //    try
        //    {
        //        Stream _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .SetQueryParams(paramaters)
        //            .GetStreamAsync().Result;
        //        return _APIClient;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static T GetObjectFromAPI<T>(string url, string path) where T : class
        //{
        //    try
        //    {
        //        Task<T> _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path)
        //            .GetJsonAsync<T>();

        //        return _APIClient.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}   
        
        //public static HttpClient GetHttpclient(string url, string path)
        //{
        //    try
        //    {
        //        var _APIClient = url
        //            .AllowAnyHttpStatus()
        //            .AppendPathSegment(path).Client.HttpClient;

        //        return _APIClient;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static string GenerateXML()
        {
            XNamespace ns = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace myns = "http://tempuri.org/";

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XAttribute(XNamespace.Xmlns + "soap", ns),
                    new XElement(ns + "Body",
                        new XElement(myns + "CompulsoryAutoMobileInsur",
                            new XElement(myns + "partnerId", "dsfsadfasdf sdf sdfsadf sdf sdf"),
                            new XElement(myns + "data", "sdf jsdfkl jsdhfl ksjhf lksadjdf "),
                            new XElement(myns + "signature", "sdf jsdfkl jsdhfl ksjhf lksadjdf ")
                        )
                    )
                ));
            return soapRequest.ToString();
        }

        public static string GenerateXML(object obj)
        {
            XNamespace ns = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace myns = "http://tempuri.org/";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XAttribute(XNamespace.Xmlns + "soap", ns),
                    new XElement(ns + "Body",
                        new XElement(myns + "CompulsoryAutoMobileInsur",
                            new XElement(myns + "partnerId", "dsfsadfasdf sdf sdfsadf sdf sdf"),
                            new XElement(myns + "data", "sdf jsdfkl jsdhfl ksjhf lksadjdf "),
                            new XElement(myns + "signature", "sdf jsdfkl jsdhfl ksjhf lksadjdf ")
                        )
                    )
                ));
            return soapRequest.ToString();
        }

    }

    public class BaseAPIModel<T>
    {
        public string Message { get; set; }
        public int RespondCode { get; set; }
        public string ErrorCode { get; set; }
        public T data { get; set; }
    }
}
