using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using QRMSWeb.Components.Common.AuthWrapper;

namespace QRMSWeb.Services
{
    public abstract class APIService
    {
        public HttpClient Client { get; set; }

        public APIService(HttpClient client)
        {
            if (client.BaseAddress == null)
            {
                var ApiBaseUrl =
                    new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppConfig")[
                        "API_BASE_URL"];

                client.BaseAddress = new Uri(ApiBaseUrl ?? string.Empty);
                               
            }
            var token = AuthTokenInstance.Instance.GetToken();
            token = token?.Trim();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}");
            Client = client;
        }

        public void checkResponse(HttpResponseMessage response)
        {
            if (response.StatusCode != (HttpStatusCode)200)
            {
                /*
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
                throw new Exception(responseData.Message);*/
                throw new Exception("" + response.StatusCode);
            }
            
        }
    }

    public class ResponseData<T>
    {
        public string Message { get; set; }
        public string RespondCode { get; set; }
        public string ErrorCode { get; set; }
        public T data { get; set; }
    }

    public class PaginateData<T>
    {
        public int TotalItem { get; set; }
        public int PageCount { get; set; }
        public T CurrentPageItems { get; set; }

        public int total { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public T rows { get; set; }
    }

    public class ResponsePaginateData<T>
    {
        public PaginateData<T> data { get; set; }
    }
}