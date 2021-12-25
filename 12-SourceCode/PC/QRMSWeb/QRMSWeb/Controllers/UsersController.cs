using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QRMSWeb.Controllers
{
    public class AccountsController : Controller
    {
        // GET
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
        
        public bool Delete()
        {
            return true;
        }
    }
}