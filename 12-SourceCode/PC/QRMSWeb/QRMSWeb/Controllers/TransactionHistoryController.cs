using Microsoft.AspNetCore.Mvc;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class TransactionHistoryController : Controller
    {
        static readonly HttpClient _httpClient = new HttpClient();

        private TransactionHistoryService _transactionHistoryService = new TransactionHistoryService(_httpClient);

        public async Task<IActionResult> ViewExcelReport(string transactionType, string orderNo)
        {
            var response = await _transactionHistoryService.GenerateReportFile(transactionType, orderNo);
            var bData = await response.Content.ReadAsByteArrayAsync();
            return File(bData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"PurchaseOrderDetail_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }
    }
}