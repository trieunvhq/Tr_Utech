using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QRMSWeb.Models;

namespace QRMSWeb.Services
{
    public abstract class BaseService<T> : APIService
    {
        public BaseService(HttpClient client) : base(client)
        {
        }

        public async Task<PaginateData<List<T>>> Get(string endpoint, string queryString)
        {
            var response = await Client.GetAsync($"{endpoint}?{queryString}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData =
                JsonConvert.DeserializeObject<ResponsePaginateData<List<T>>>(responseBody);
            return responseData.data;
        }

        public async Task<T> GetByID(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(responseBody);
            return responseData.data;
        }

        public async Task<List<T>> GetAll(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<List<T>>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> Create(string endpoint, T agent)
        {
            var response = await Client.PostAsync(endpoint,
                new StringContent(JsonConvert.SerializeObject(agent), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
            if (!string.IsNullOrEmpty(responseData.ErrorCode?.Trim()))
            {
                throw new Exception(responseData.Message);
            }
            return response;
        }

        public async Task<HttpResponseMessage> Update(string endpoint, T agent)
        {
            var response = await Client.PostAsync(endpoint,
                new StringContent(JsonConvert.SerializeObject(agent), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
            if (!string.IsNullOrEmpty(responseData.ErrorCode?.Trim()))
            {
                throw new Exception(responseData.Message);
            }
            return response;
        }

        
        public async Task<HttpResponseMessage> Delete(string endpoint, T agent)
        {
            var response = await Client.PostAsync(endpoint,
                new StringContent(JsonConvert.SerializeObject(agent), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
            if (!string.IsNullOrEmpty(responseData.ErrorCode?.Trim()))
            {
                throw new Exception(responseData.Message);
            }
            return response;
        }
    }
}