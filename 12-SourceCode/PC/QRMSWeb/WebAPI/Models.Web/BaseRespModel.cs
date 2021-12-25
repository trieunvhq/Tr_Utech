using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Controllers.Web
{
    public class BaseRespModel
    {
        public string Message { get; set; }
        public string RespondCode { get; set; }
        public string ErrorCode { get; set; }
        public virtual object data { get; set; }

        public BaseRespModel() { }

        public BaseRespModel(string Message, string RespondCode, string ErrorCode, object data = null)
        {
            this.Message = Message;
            this.RespondCode = RespondCode;
            this.ErrorCode = ErrorCode;
            this.data = data;
        }
    }
}