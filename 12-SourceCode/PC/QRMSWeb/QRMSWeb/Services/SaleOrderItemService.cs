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
    public class SaleOrderItemService : APIService
    {
        public SaleOrderItemService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<SaleOrderItemModel>>> SearchSaleOrderItem(int page, int rowPerPage,
            string itemCode, string itemName, string saleOrderNo, string locationName, string startDate, string endDate)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(itemCode?.Trim()))
            {
                queryString += $"&itemCode={Uri.EscapeDataString(itemCode.Trim())}";
            }
            
            if(!String.IsNullOrEmpty(itemName?.Trim()))
            {
                queryString += $"&itemName={Uri.EscapeDataString(itemName.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(saleOrderNo?.Trim()))
            {
                queryString += $"&saleOrderNo={Uri.EscapeDataString(saleOrderNo.Trim())}";
            }
            if (!String.IsNullOrEmpty(locationName?.Trim()))
            {
                queryString += $"&locationName={Uri.EscapeDataString(locationName.Trim())}";
            }
            if (!String.IsNullOrEmpty(startDate?.Trim()))
            {
                queryString += $"&startDate={startDate?.Trim()}";
            }

            if (!String.IsNullOrEmpty(endDate?.Trim()))
            {
                queryString += $"&endDate={endDate?.Trim()}";
            }
            var response = await Client.GetAsync("api-wa/sale-order-item/find-all?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<SaleOrderItemModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<ResponseData<Object>> ImportSaleOrderItem()
        {
            string url = $"api-wa/sale-order-item/import-from-amis";
            var response = await Client.GetAsync(url);
            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }

            return await HttpHelper.GetDataResponse<Object>(response);
            
        }

        
        public async Task<PaginateData<List<SaleOrderItemModel>>> GetSaleOrderItems(int? page, int? limit,
            string username, string fullname)
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
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<SaleOrderItemModel>>>(responseBody);
            return responseData.data;
        }

        public async Task<SaleOrderItemModel> GetsaleOrderItemByID(int saleOrderItemID)
        {
            var response = await Client.GetAsync($"api-wa/sale-order-item/{saleOrderItemID}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<SaleOrderItemModel>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> CreatesaleOrderItem(SaleOrderItemModel saleOrderItem)
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

        public async Task<HttpResponseMessage> UpdatesaleOrderItem(SaleOrderItemModel saleOrderItem)
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
        public async Task<ResponseData<Object>> DeletesaleOrderItem(SaleOrderItemModel saleOrderItem)
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
        public async Task<HttpResponseMessage> GenerateReportFile(int saleOrderID)
        {
            string strQuery = $"saleOrderID={saleOrderID}";
            var response = await Client.GetAsync($"api_wa/sale-order-item/export-excel?{strQuery}");
            this.checkResponse(response);

            return response;
           
        }
    }
}