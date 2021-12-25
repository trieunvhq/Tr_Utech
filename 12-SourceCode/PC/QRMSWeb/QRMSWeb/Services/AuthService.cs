using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using QRMSWeb.Models;

namespace QRMSWeb.Services
{
    public class AuthService : APIService
    {
        public AuthService(HttpClient client) : base(client)
        {
            
        }
        public async Task<AuthModel> GetAuth()
        {
            var response = await Client.GetAsync($"api-wa/auth/get-auth-of-login");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<AuthModel>>(responseBody);
            return responseData.data;
        }
    }
}