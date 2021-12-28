using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QRMSWeb.Helper;
using QRMSWeb.Models;

namespace QRMSWeb.Services
{
    public class UserService : APIService
    {
        public UserService(HttpClient client) : base(client)
        {
        }

        public async Task<string> Login(string USERNAME, string PASSWORD)
        {
            var response = await Client.PostAsync("api-wa/auth/login",
                new StringContent(JsonConvert.SerializeObject(new { USERNAME, PASSWORD }), Encoding.UTF8, "application/json"));

            this.checkResponse(response);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<string>>(responseContent); 

            return responseData.data;
        }

        public async Task<PaginateData<List<UserModel>>> SearchAccount(int page, int rowPerPage, string username, string fullname)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(username?.Trim()))
            {
                queryString += $"&username={Uri.EscapeDataString(username.Trim())}";
            }
            
            if(!String.IsNullOrEmpty(fullname?.Trim()))
            {
                queryString += $"&fullname={Uri.EscapeDataString(fullname.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/account/all-account?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<UserModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<List<UserModel>> GetAllAccounts(string aggentCode)
        {
            string url = $"api-wa/account/all";
            if (!string.IsNullOrEmpty(aggentCode?.Trim()))
            {
                url = $"{url}?{Uri.EscapeDataString(aggentCode.Trim())}";
            }
            var response = await Client.GetAsync(url);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<List<UserModel>>>(responseBody);
            return responseData.data;
        }

        public async Task<List<UserModel>> GetAllDivisionAccounts(string divCode)
        {
            if (string.IsNullOrEmpty(divCode?.Trim()))
            {
                return new List<UserModel>();
            }
            var response = await Client.GetAsync("api-wa/account/account-div?divCode=" + Uri.EscapeDataString(divCode.Trim()));
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<List<UserModel>>>(responseBody);
            return responseData.data;
        }

        public async Task<PaginateData<List<UserModel>>> GetAgentAccount(int? page, int? limit,
            string? username, string? fullname)
        {
            string queryString = $"page={page}&limit={limit}&withAgent=true";

            if (!String.IsNullOrEmpty(username?.Trim()))
            {
                queryString += $"&username={Uri.EscapeDataString(username.Trim())}";
            }
            
            if(!String.IsNullOrEmpty(fullname?.Trim()))
            {
                queryString += $"&fullname={Uri.EscapeDataString(fullname.Trim())}";
            }
            
            var response = await Client.GetAsync("api-wa/account/account-agent?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<UserModel>>>(responseBody);
            return responseData.data;
        }

        public async Task<UserModel> GetAccountByID(int accountID)
        {
            var response = await Client.GetAsync($"api-wa/account/{accountID}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<UserModel>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> CreateAccount(UserModel account)
        {
            var response = await Client.PostAsync("api_wa/account/create",
                new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                /*
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
                throw new Exception(responseData.Message);*/
                this.checkResponse(response);
            }

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAccount(UserModel account)
        {
            var response = await Client.PostAsync("api_wa/account/update",
                new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                /*
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
                throw new Exception(responseData.Message);*/
                this.checkResponse(response);
            }

            return response;
        }
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var response = await Client.PostAsync("api_wa/account/change-password",
                new StringContent(JsonConvert.SerializeObject(changePasswordModel), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                /*
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
                throw new Exception(responseData.Message);*/
                this.checkResponse(response);
            }

            return response;
        }
        public async Task<HttpResponseMessage> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var response = await Client.PostAsync("api_wa/account/reset-password",
                new StringContent(JsonConvert.SerializeObject(forgotPasswordModel), Encoding.UTF8, "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                /*
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData<Object>>(responseBody);
                throw new Exception(responseData.Message);*/
                this.checkResponse(response);
            }

            return response;
        }
        public async Task<ResponseData<Object>> DeleteAccount(UserModel account)
        {
            var response = await Client.PostAsync("api_wa/account/delete",
                new StringContent(JsonConvert.SerializeObject(new {ID = account.ID, USER_ID = 0}), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }
        public async Task<ResponseData<Object>> LockAccount(UserModel account)
        {
            var response = await Client.PostAsync("api_wa/account/lock",
                new StringContent(JsonConvert.SerializeObject(new { ID = account.ID, USER_ID = 0 }), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }
        public async Task<ResponseData<Object>> UnlockAccount(UserModel account)
        {
            var response = await Client.PostAsync("api_wa/account/unlock",
                new StringContent(JsonConvert.SerializeObject(new { ID = account.ID, USER_ID = 0 }), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }
    }
}