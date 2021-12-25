using HDLIB.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Controllers.Web;

namespace Web_API.Attributes.Web
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var ErroModel = actionContext.ModelState.SelectMany(s => s.Value.Errors)
                    .FirstOrDefault(_ => _.Exception == null);
                string error = "Có lỗi xảy ra (unkonow error)";
                if (ErroModel != null)
                {
                    error = ErroModel.ErrorMessage;
                }

                var baseRespModel = new BaseRespModel(error, APIResponseCode.VALIDATION, APIErrorCode.VALIDATION);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, baseRespModel);
            }
        }
    }
}
