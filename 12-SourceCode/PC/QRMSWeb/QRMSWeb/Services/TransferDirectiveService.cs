using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QRMSWeb.Helper;
using QRMSWeb.Constants;
using QRMSWeb.Models;

namespace QRMSWeb.Services
{
    public class TransferDirectiveService : APIService
    {
        public TransferDirectiveService(HttpClient client) : base(client)
        {
        }
        public async Task<PaginateData<List<TransferInstructionModel>>> SearchTransferDirective(int page, int rowPerPage,
           string tranferOrderNo, string wareHouseCode_from, string wareHouseCode_to, string startDate, string endDate)
        {
            string queryString = $"page={page}&limit={rowPerPage}&transferType={ConstTransferType.ChuyenKho}";
            if (!String.IsNullOrEmpty(tranferOrderNo?.Trim()))
            {
                queryString += $"&transferNo={Uri.EscapeDataString(tranferOrderNo.Trim())}";
            }
            if (!String.IsNullOrEmpty(wareHouseCode_from?.Trim()))
            {
                queryString += $"&wareHouseCode_from={Uri.EscapeDataString(wareHouseCode_from.Trim())}";
            }
            if (!String.IsNullOrEmpty(wareHouseCode_to?.Trim()))
            {
                queryString += $"&wareHouseCode_to={Uri.EscapeDataString(wareHouseCode_to.Trim())}";
            }
            if (!String.IsNullOrEmpty(startDate?.Trim()))
            {
                queryString += $"&startDate={startDate?.Trim()}";
            }

            if (!String.IsNullOrEmpty(endDate?.Trim()))
            {
                queryString += $"&endDate={endDate?.Trim()}";
            }
            var response = await Client.GetAsync("api-wa/transfer-ins/find-all-transfer-ins?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransferInstructionModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<PaginateData<List<TransferInstructionItemModel>>> SearchTransferDirectiveItem(int page, int rowPerPage,
            string tranferOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}&transferType={ConstTransferType.ChuyenKho}";

            if (!String.IsNullOrEmpty(tranferOrderNo?.Trim()))
            {
                queryString += $"&tranferOrderNo={Uri.EscapeDataString(tranferOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/transfer-ins/find-all-transfer-ins-item?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransferInstructionItemModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<ResponseData<Object>> ImportTransferDirectiveItem()
        {
            string url = $"api-wa/transfer-ins/import-from-amis";
            var response = await Client.GetAsync(url);
            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }

            return await HttpHelper.GetDataResponse<Object>(response);

        }

        public async Task<PaginateData<List<TransactionHistoryModel>>> TransferDirectiveActualScanDetail(int page, int rowPerPage,
            string tranferOrderNo)
        {
            string queryString = $"page={page}&limit={rowPerPage}&transferType={ConstTransferType.ChuyenKho}";


            if (!String.IsNullOrEmpty(tranferOrderNo?.Trim()))
            {
                queryString += $"&tranferOrderNo={Uri.EscapeDataString(tranferOrderNo.Trim())}";
            }
            var response = await Client.GetAsync("api-wa/transfer-ins/transfer-ins-actual-scan?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<TransactionHistoryModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<TransferInstructionModel> GetTransferDirectiveByTranferOrderNo(string tranferOrderNo)
        {
            var response = await Client.GetAsync($"api-wa/get-transfer-ins-by-no?transferOrderNo={tranferOrderNo}&transferType={ConstTransferType.ChuyenKho}");
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<TransferInstructionModel>>(responseBody);
            return responseData.data;
        }

        public async Task<HttpResponseMessage> GenerateReportFile(string tranferOrderNo)
        {
            string strQuery = $"tranferOrderNo={tranferOrderNo}&transferType={ConstTransferType.ChuyenKho}";
            var response = await Client.GetAsync($"api_wa/transfer-ins/export-excel?{strQuery}");
            this.checkResponse(response);

            return response;

        }
    }
}