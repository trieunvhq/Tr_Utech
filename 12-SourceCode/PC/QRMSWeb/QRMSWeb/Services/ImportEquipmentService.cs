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
    public class ImportEquipmentService : APIService
    {
        public ImportEquipmentService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<TransactionHistoryModel>>> SearchImportEquipment(int page, int rowPerPage,
            string itemCode, string OrderNo, string wareHouseCode, string startDate, string endDate)
        {
            string queryString = $"page={page}&limit={rowPerPage}";
            
            if (!String.IsNullOrEmpty(itemCode?.Trim()))
            {
                queryString += $"&itemCode={Uri.EscapeDataString(itemCode.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(OrderNo?.Trim()))
            {
                queryString += $"&orderNo={Uri.EscapeDataString(OrderNo.Trim())}";
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
            var response = await Client.GetAsync("api-wa/import-equipment/find-all-import-equipment?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransactionHistoryModel>>>(responseBody);

            return responseData.data;
        }
    }
}