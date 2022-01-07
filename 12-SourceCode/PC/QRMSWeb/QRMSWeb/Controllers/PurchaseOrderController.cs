using Microsoft.AspNetCore.Mvc;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class PurchaseOrderController : Controller
    {
        static readonly HttpClient _httpClient = new HttpClient();

        private PurchaseOrderService _PurchaseOrderService = new PurchaseOrderService(_httpClient);

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
            string WareHouseName = null, string PurchaseOrderDate = null)
        {
            ViewBag.ID = ID;
            ViewBag.PurchaseOrderNo = purchaseOrderNo;
            ViewBag.WareHouseName = WareHouseName;
            ViewBag.PurchaseOrderDate = PurchaseOrderDate;
            return View();
        }
        public bool Delete()
        {
            return true;
        }
        public async Task<IActionResult> ViewExcelReport(string purchaseOrderNo)
        {
            var response = await _PurchaseOrderService.GenerateReportFile(purchaseOrderNo);
            //this.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xlsx");
            //this.HttpContext.Response.RegisterForDispose(response);
            //    return new HttpResponseMessageResult(response);
            var bData = await response.Content.ReadAsByteArrayAsync();
            return File(bData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ChiTietScanThucTeDonMuaHang_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx");
        }
    }
}