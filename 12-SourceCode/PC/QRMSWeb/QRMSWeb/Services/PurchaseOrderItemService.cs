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
    public class PurchaseOrderItemService : APIService
    {
        public PurchaseOrderItemService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<PurchaseOrderItemModel>>> SearchPurchaseOrderItem(int page, int rowPerPage, string itemCode, string itemName, string localtionName, string purchaseOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(itemCode?.Trim()))
            {
                queryString += $"&itemcode={Uri.EscapeDataString(itemCode.Trim())}";
            }
            
            if(!String.IsNullOrEmpty(itemName?.Trim()))
            {
                queryString += $"&itemname={Uri.EscapeDataString(itemName.Trim())}";
            }
            if (!String.IsNullOrEmpty(localtionName?.Trim()))
            {
                queryString += $"&locationname={Uri.EscapeDataString(localtionName.Trim())}";
            }
            if (!String.IsNullOrEmpty(purchaseOrderNo?.Trim()))
            {
                queryString += $"&purchaseorderno={Uri.EscapeDataString(purchaseOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/purchase-order-item/all-purchaseOrderItem?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<PurchaseOrderItemModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<List<PurchaseOrderItemModel>> GetAllPurchaseOrderItems(string aggentCode)
        {
            string url = $"api-wa/purchase-order-item/all";
            if (!string.IsNullOrEmpty(aggentCode?.Trim()))
            {
                url = $"{url}?{Uri.EscapeDataString(aggentCode.Trim())}";
            }
            var response = await Client.GetAsync(url);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<List<PurchaseOrderItemModel>>>(responseBody);
            return responseData.data;
        }
        public async Task<List<PurchaseOrderItemModel>> ImportPurchaseOrderItem()
        {
            string url = $"api-wa/purchase-order-item/import";
            var response = await Client.GetAsync(url);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<List<PurchaseOrderItemModel>>>(responseBody);
            return responseData.data;
        }

        
        public async Task<PaginateData<List<PurchaseOrderItemModel>>> GetPurchaseOrderItems(int? page, int? limit,
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
            
            var response = await Client.GetAsync("api-wa/sale-order-item/sale-order-item-agent?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<PurchaseOrderItemModel>>>(responseBody);
            return responseData.data;
        }

        public async Task<PurchaseOrderItemModel> GetPurchaseOrderItemByID(int saleOrderItemID)
        {
            var response = await Client.GetAsync($"api-wa/sale-order-item/{saleOrderItemID}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<PurchaseOrderItemModel>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> CreatesaleOrderItem(PurchaseOrderItemModel saleOrderItem)
        {
            var response = await Client.PostAsync("api_wa/sale-order-item/create",
                new StringContent(JsonConvert.SerializeObject(saleOrderItem), Encoding.UTF8, "application/json"));

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

        public async Task<HttpResponseMessage> UpdatesaleOrderItem(PurchaseOrderItemModel saleOrderItem)
        {
            var response = await Client.PostAsync("api_wa/sale-order-item/update",
                new StringContent(JsonConvert.SerializeObject(saleOrderItem), Encoding.UTF8, "application/json"));

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
        public async Task<ResponseData<Object>> DeletePurchaseOrderItem(PurchaseOrderItemModel saleOrderItem)
        {
            var response = await Client.PostAsync("api_wa/sale-order-item/delete",
                new StringContent(JsonConvert.SerializeObject(new {ID = saleOrderItem.ID, USER_ID = 0}), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode) 200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }
    }
}