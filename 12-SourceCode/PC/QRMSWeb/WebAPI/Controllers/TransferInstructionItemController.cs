using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPL.Factory.HT.TransferInstructionItems;
using BPL.Models;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TransferInstructionItemController : ApiController
    {
        [HttpPost]
        [Route("api-ht/transferinstructionitem/getitem")]
        public BaseModel GetTransferInstructionItem([FromBody] JObject input)
        {
            var _return = new BaseModel();
            try
            {
                string err_code = "";
                string err_msg = "";
                int TransferInstructionItemID = input["ID"].ToObject<int>();

                var result = new TransferInstructionItemBPL().GetTransferInstructionItem(TransferInstructionItemID, out err_code, out err_msg);

                _return.ErrorCode = err_code;
                _return.Message = err_msg;
                _return.data = result;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = ResponseErrorCode.Error.ToString();
                _return.Message = ex.Message;
            }
            return _return;
        }

        [HttpPost]
        [Route("api-ht/transferinstructionitem/updateitem")]
        public BaseModel Updatetransferinstructionitem([FromBody] List<XuatKhoDungCuBPLModel> input)
        {
            var _return = new BaseModel();

            try
            {
                string err_code = "";
                string err_msg = "";

                var result = new TransferInstructionItemBPL().UpdateTransferInstructionItem(input, out err_code, out err_msg);

                _return.ErrorCode = err_code;
                _return.Message = err_msg;
                _return.data = result;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = ResponseErrorCode.Error.ToString();
                _return.Message = ex.Message;
            }
            return _return;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}