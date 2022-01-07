using Microsoft.AspNetCore.Mvc;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class TransferDirectiveController : Controller
    {
        static readonly HttpClient _httpClient = new HttpClient();

        private TransferDirectiveService _ImportDirectiveService = new TransferDirectiveService(_httpClient);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult ActualScanDetail(int? ID=null, string purchaseOrderNo = null,
            string locationCode = null, string dateFrom = null, string dateTo = null)
        {
            ViewBag.ID = ID;
            ViewBag.PurchaseOrderNo = purchaseOrderNo;
            ViewBag.LocationCode = locationCode;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            return View();
        }
        public bool Delete()
        {
            return true;
        }

        
        public async Task<IActionResult> ViewExcelReport(int? ID)
        {
            var response = await _ImportDirectiveService.GenerateReportFile(ID ?? 0);
            //this.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xlsx");

            //this.HttpContext.Response.RegisterForDispose(response);
            //    return new HttpResponseMessageResult(response);
            var bData = await response.Content.ReadAsByteArrayAsync();
            return File(bData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"TransferDirectiveDetail_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }

       
    }
}