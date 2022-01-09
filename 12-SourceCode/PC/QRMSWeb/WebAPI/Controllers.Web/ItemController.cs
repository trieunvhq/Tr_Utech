using BLL.Factory.Web.Item;
using HDLIB.Common;
using System;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class ItemWebController : BaseAPIController
    {
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/item/all-items")]
        public BaseModel GetAllItems()
        {
            var _return = new BaseModel();
            try
            {
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new ItemBLL().FindAllWithoutPagging();

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