using BLL.Factory.Web.ImportEquipment;
using HDLIB;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using PISAS_API.Models.Web.Users;
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
    public class ImportEquipmentWebController : BaseAPIController
    {
        

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/import-equipment/find-all-import-equipment")]
        public BaseModel FindListSaleOrders(int page = 1, int limit = Constant.NumPage,
            string itemCode = null, string orderNo = null, string wareHouseCode = null, 
            DateTime? startDate = null,  DateTime? endDate = null)
        {
            var _return = new BaseModel();

            try
            {
                itemCode = itemCode?.Trim();
                orderNo = orderNo?.Trim();
                wareHouseCode = wareHouseCode?.Trim();

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new ImportEquipmentBLL().FindAllImportEquipment(page, limit, itemCode, orderNo, wareHouseCode, startDate, endDate);
                    
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