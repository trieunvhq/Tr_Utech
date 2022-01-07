using BLL.Factory.Web.PurchaseOrder;
using BLL.Factory.Web.WareHouse;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class WareHouseWebController : BaseAPIController
    {
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/ware-house/all-warehouses")]
        public BaseModel GetAllWareHouses()
        {
            var _return = new BaseModel();
            try
            {
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new WareHouseBLL().FindAllWithoutPagging();

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }
        
    }
}