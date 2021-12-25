using System;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected DAL.QRMSEntities db = new DAL.QRMSEntities();
        #region IDispose
        // WebApi 2 will call this automatically after each 
        // request. You need this to ensure your context is disposed
        // and the memory it is using is freed when your app does garbage 
        // collection.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            GC.Collect();
            base.Dispose(disposing);
        }
        #endregion
    }
}