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
    public class PurchaseOrderService : APIService
    {
        public PurchaseOrderService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<PurchaseOrderModel>>> SearchPurchaseOrder(int page, int rowPerPage,
            string wareHouseCode, string purchaseOrderNo, string inputStatus, 
            string startDate, string endDate, bool isSearch)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(wareHouseCode?.Trim()))
            {
                queryString += $"&wareHouseCode={Uri.EscapeDataString(wareHouseCode.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(purchaseOrderNo?.Trim()))
            {
                queryString += $"&purchaseOrderNo={Uri.EscapeDataString(purchaseOrderNo.Trim())}";
            }
            if (!String.IsNullOrEmpty(inputStatus?.Trim()))
            {
                queryString += $"&inputStatus={Uri.EscapeDataString(inputStatus.Trim())}";
            }
            if (!String.IsNullOrEmpty(startDate?.Trim()))
            {
                queryString += $"&startDate={startDate?.Trim()}";
            }

            if (!String.IsNullOrEmpty(endDate?.Trim()))
            {
                queryString += $"&endDate={endDate?.Trim()}";
            }
            queryString += $"&isSearch={isSearch}";
            var response = await Client.GetAsync("api-wa/purchase-order/all-purchase-order?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<PurchaseOrderModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<PaginateData<List<PurchaseOrderItemModel>>> SearchPurchaseOrderItem(int page, int rowPerPage,
            string purchaseOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}";

            if (!String.IsNullOrEmpty(purchaseOrderNo?.Trim()))
            {
                queryString += $"&purchaseOrderNo={Uri.EscapeDataString(purchaseOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/purchase-order/all-purchase-order-item?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<PurchaseOrderItemModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<PaginateData<List<TransactionHistoryModel>>> PurchaseOrderActualScanDetail(int page, int rowPerPage,
            string purchaseOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}";

            
            if (!String.IsNullOrEmpty(purchaseOrderNo?.Trim()))
            {
                queryString += $"&purchaseOrderNo={Uri.EscapeDataString(purchaseOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/purchase-order/purchase-order-actual-scan?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransactionHistoryModel>>>(responseBody);

            return responseData.data;
        }
        public async Task<ResponseData<Object>> ImportPurchaseOrder()
        {
            string url = $"api-wa/purchase-order/import-from-amis";
            var response = await Client.GetAsync(url);
            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }

            return await HttpHelper.GetDataResponse<Object>(response);
        }

        public async Task<PurchaseOrderModel> GetPurchaseOrderByID(int purchaseOrderID)
        {
            var response = await Client.GetAsync($"api-wa/purchase-order/{purchaseOrderID}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<PurchaseOrderModel>>(responseBody);
            return responseData.data;
        }

        public async Task<PurchaseOrderModel> GetPurchaseOrderByPurchaseOrderNo(string purchaseOrderNo)
        {
            var response = await Client.GetAsync($"api-wa/get-purchase-order-by-no?PurchaseOrderNo={purchaseOrderNo}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<PurchaseOrderModel>>(responseBody);
            return responseData.data;
        }
        public async Task<HttpResponseMessage> GenerateReportFile(string purchaseOrderNo)
        {
            string strQuery = $"purchaseOrderNo={purchaseOrderNo}";
            var response = await Client.GetAsync($"api_wa/purchase-order/export-excel?{strQuery}");
            this.checkResponse(response);
            return response;
        }

        #region Print
        public async Task<ResponseData<Object>> DeletePurchaseOrder(int ID)
        {
            var response = await Client.PostAsync("api_wa/purchase-order/delete",
                new StringContent(JsonConvert.SerializeObject(new { ID = ID }), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }

         public async Task<PaginateData<List<PurchaseOrderItemModel>>> SearchPurchaseOrderItemPrint(int page, int rowPerPage,
            string itemType, string wareHouseCode, string purchaseOrderNo, string printStatus,
            string purchaseOrderDate, bool isSearch)
         {
            string queryString = $"page={page}&limit={rowPerPage}";

            if (!String.IsNullOrEmpty(itemType?.Trim()))
            {
                queryString += $"&itemType={Uri.EscapeDataString(itemType.Trim())}";
            }

            if (!String.IsNullOrEmpty(wareHouseCode?.Trim()))
            {
                queryString += $"&wareHouseCode={Uri.EscapeDataString(wareHouseCode.Trim())}";
            }
            if (!String.IsNullOrEmpty(purchaseOrderNo?.Trim()))
            {
                queryString += $"&purchaseOrderNo={Uri.EscapeDataString(purchaseOrderNo.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(printStatus?.Trim()))
            {
                queryString += $"&printStatus={printStatus?.Trim()}";
            }

            if (!String.IsNullOrEmpty(purchaseOrderDate?.Trim()))
            {
                queryString += $"&purchaseOrderDate={purchaseOrderDate?.Trim()}";
            }
            queryString += $"&isSearch={isSearch}";
            var response = await Client.GetAsync("api-wa/purchase-order/all-purchase-order-print?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<PurchaseOrderItemModel>>>(responseBody);

            return responseData.data;
        }
        #endregion
    }
}