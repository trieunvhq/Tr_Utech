using Microsoft.AspNetCore.Mvc;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class SaleOrderController : Controller
    {
        static readonly HttpClient _httpClient = new HttpClient();

        private SaleOrderService _SaleOrderService = new SaleOrderService(_httpClient);

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
        public IActionResult ActualScanDetail()
        {
            
            return View();
        }
        public bool Delete()
        {
            return true;
        }
        public async Task<IActionResult> ViewExcelReport(string saleOrderNo)
        {
            var response = await _SaleOrderService.GenerateReportFile(saleOrderNo);
            //this.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xlsx");

            //this.HttpContext.Response.RegisterForDispose(response);
            //    return new HttpResponseMessageResult(response);
            var bData = await response.Content.ReadAsByteArrayAsync();
            return File(bData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"SaleOrderDetail_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }
    }
}