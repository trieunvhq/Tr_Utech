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
    public class SaleOrderService : APIService
    {
        public SaleOrderService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<SaleOrderModel>>> SearchSaleOrder(int page, int rowPerPage,
            string exportStatus, string saleOrderNo, string wareHouseCode, string startDate, string endDate)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(exportStatus?.Trim()))
            {
                queryString += $"&exportStatus={Uri.EscapeDataString(exportStatus.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(saleOrderNo?.Trim()))
            {
                queryString += $"&saleOrderNo={Uri.EscapeDataString(saleOrderNo.Trim())}";
            }
            if (!String.IsNullOrEmpty(wareHouseCode?.Trim()))
            {
                queryString += $"&wareHouseCode={Uri.EscapeDataString(wareHouseCode.Trim())}";
            }
            if (!String.IsNullOrEmpty(startDate?.Trim()))
            {
                queryString += $"&startDate={startDate?.Trim()}";
            }

            if (!String.IsNullOrEmpty(endDate?.Trim()))
            {
                queryString += $"&endDate={endDate?.Trim()}";
            }
            var response = await Client.GetAsync("api-wa/sale-order/find-all-sale-order?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<SaleOrderModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<PaginateData<List<SaleOrderItemModel>>> SearchSaleOrderItem(int page, int rowPerPage,
            string saleOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}";

            if (!String.IsNullOrEmpty(saleOrderNo?.Trim()))
            {
                queryString += $"&saleOrderNo={Uri.EscapeDataString(saleOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/sale-order/find-all-sale-order-item?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<SaleOrderItemModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<ResponseData<Object>> ImportSaleOrderItem()
        {
            string url = $"api-wa/sale-order/import-from-amis";
            var response = await Client.GetAsync(url);
            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }

            return await HttpHelper.GetDataResponse<Object>(response);
            
        }

        public async Task<PaginateData<List<TransactionHistoryModel>>> SaleOrderActualScanDetail(int page, int rowPerPage,
            string saleOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}";


            if (!String.IsNullOrEmpty(saleOrderNo?.Trim()))
            {
                queryString += $"&saleOrderNo={Uri.EscapeDataString(saleOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/sale-order/sale-order-actual-scan?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransactionHistoryModel>>>(responseBody);

            return responseData.data;
        }
        public async Task<SaleOrderItemModel> GetsaleOrderItemByID(int saleOrderItemID)
        {
            var response = await Client.GetAsync($"api-wa/sale-order/{saleOrderItemID}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<SaleOrderItemModel>>(responseBody);
            return responseData.data;
        }

        public async Task<SaleOrderModel> GetSaleOrderByPurchaseOrderNo(string saleOrderNo)
        {
            var response = await Client.GetAsync($"api-wa/get-sale-order-by-no?SaleOrderNo={saleOrderNo}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<SaleOrderModel>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> GenerateReportFile(string saleOrderNo)
        {
            string strQuery = $"saleOrderNo={saleOrderNo}";
            var response = await Client.GetAsync($"api_wa/sale-order/export-excel?{strQuery}");
            this.checkResponse(response);

            return response;
           
        }
    }
}