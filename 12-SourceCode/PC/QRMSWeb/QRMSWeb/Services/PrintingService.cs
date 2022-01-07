using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QRMSWeb.Helper;
using QRMSWeb.Models;

namespace QRMSWeb.Services
{
    public class PrintingService : APIService
    {
        public PrintingService(HttpClient client) : base(client)
        {
        }

       
        #region Print
        public async Task<ResponseData<Object>> DeleteLabelPrint(LabelPrintModel labelPrintModel)
        {
            var response = await Client.PostAsync($"api_wa/printing-by-instruction-order/delete-print-instruction/{labelPrintModel.ID}",
                new StringContent(JsonConvert.SerializeObject(labelPrintModel), Encoding.UTF8,
                    "application/json"));

            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
            {
                this.checkResponse(response);
            }
            return await HttpHelper.GetDataResponse<Object>(response);
        }

         public async Task<PaginateData<List<LabelPrintModel>>> SearchLabelPrint(int page, int rowPerPage,
            string itemType, string printOrderNo, string wareHouseCode, string printStatus,
            string startDate, string endDate, bool isSearch)
         {
            string queryString = $"page={page}&limit={rowPerPage}";

            if (!String.IsNullOrEmpty(itemType?.Trim()))
            {
                queryString += $"&itemType={Uri.EscapeDataString(itemType.Trim())}";
            }

            if (!String.IsNullOrEmpty(printOrderNo?.Trim()))
            {
                queryString += $"&printOrderNo={Uri.EscapeDataString(printOrderNo.Trim())}";
            }
            if (!String.IsNullOrEmpty(wareHouseCode?.Trim()))
            {
                queryString += $"&wareHouseCode={Uri.EscapeDataString(wareHouseCode.Trim())}";
            }
            
            if (!String.IsNullOrEmpty(printStatus?.Trim()))
            {
                queryString += $"&printStatus={printStatus?.Trim()}";
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
            var response = await Client.GetAsync("api-wa/printing-by-instruction/all-label-prints?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponsePaginateData<List<LabelPrintModel>>>(responseBody);

            return responseData.data;
        }

        public async Task<ResponseData<Object>> ImportExcelFile(byte[] fileData, string fileName)
        {
            try
            {
                if (fileData != null && fileData.Length > 0)
                {
                    //using (Client)
                    {
                        try
                        {
                            /*byte[] data;
                            
                            using (var br = new BinaryReader(file.OpenReadStream()))
                                data = br.ReadBytes((int)file.OpenReadStream().Length);
                            */
                            ByteArrayContent bytes = new ByteArrayContent(fileData);


                            MultipartFormDataContent multiContent = new MultipartFormDataContent();

                            multiContent.Add(bytes, "file", fileName);
                            /*
                            using (var formData = new MultipartFormDataContent())
                            {
                                formData.Add(stringContent, "param1", "param1");
                                formData.Add(fileStreamContent, "file1", "file1");
                                formData.Add(bytesContent, "file2", "file2");
                                var response = await client.PostAsync(actionUrl, formData);
                                if (!response.IsSuccessStatusCode)
                                {
                                    return null;
                                }
                                return await response.Content.ReadAsStreamAsync();
                            }
                            */


                            var response = await Client.PostAsync("api-wa/printing-by-instruction/import-excel-file", multiContent);

                            if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != HttpStatusCode.BadRequest)
                            {
                                this.checkResponse(response);
                            }
                            return await HttpHelper.GetDataResponse<Object>(response);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

                throw new Exception("File bị rỗng"); // 400 is bad request

            }
            catch (Exception e)
            {
                throw e; // 500 is generic server error
            }
        }

        public async Task<LabelPrintModel> GetLabelPrintDetail(int page, int rowPerPage,
            int printOrderId)
        {
            string queryString = $"page={page}&limit={rowPerPage}&printOrderId={printOrderId}";

            var response = await Client.GetAsync("api-wa/printing-by-instruction/label-print-detail?" + queryString);
            this.checkResponse(response);
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<LabelPrintModel>>(responseBody);

            return responseData.data;
        }

        #endregion
    }
}