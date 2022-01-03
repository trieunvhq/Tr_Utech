using Microsoft.AspNetCore.Mvc;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class PrintingByInstructionController : Controller
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

        public IActionResult PrintByInstruction()
        {
            return View();
        }
    }
}