using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace AMIS_Test.Controllers.Web
{
    public class TestAPIController : ApiController
    {
        

        [HttpGet]
        [Route("api/GetListItemMaster")]
        public JObject GetListItemMaster(string itemcode=null, string itemtype=null, int page=1, int pagesize=500)
        {

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/");
                var path = Path.Combine(root, "DataTest");
                string filePath = Path.Combine(path, System.Configuration.ConfigurationSettings.AppSettings["FileItemMasterData"]);
                if (File.Exists(filePath)) {
                    return JObject.Parse(File.ReadAllText(filePath));
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        [HttpGet]
        [Route("api/GetListWarehouse")]
        public JObject GetListWarehouse(string itemcode=null, string itemtype=null, int page=1, int pagesize=500)
        {

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/");
                var path = Path.Combine(root, "DataTest");
                string filePath = Path.Combine(path, System.Configuration.ConfigurationSettings.AppSettings["FileWarehouseData"]);
                if (File.Exists(filePath))
                {
                    var result= JObject.Parse(File.ReadAllText(filePath));
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        [HttpGet]
        [Route("api/GetListPurchaseOrder")]
        public JObject GetListPurchaseOrder(int? po_id=null, int page=1, int pagesize=500)
        {

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/");
                var path = Path.Combine(root, "DataTest");
                string filePath = Path.Combine(path, System.Configuration.ConfigurationSettings.AppSettings["FilePurchaseOrderData"]);
                if (File.Exists(filePath))
                {
                    return JObject.Parse(File.ReadAllText(filePath));
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        [HttpGet]
        [Route("api/GetListSaleOrder")]
        public JObject GetListSaleOrder(int? so_id=null, int page=1, int pagesize=500)
        {

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/");
                var path = Path.Combine(root, "DataTest");
                string filePath = Path.Combine(path, System.Configuration.ConfigurationSettings.AppSettings["FileSaleOrderData"]);
                if (File.Exists(filePath))
                {
                    return JObject.Parse(File.ReadAllText(filePath));
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        [HttpGet]
        [Route("api/GetListTransferOrder")]
        public JObject GetListTransferOrder(string itemcode=null, string itemtype=null, int page=1, int pagesize=500)
        {

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/");
                var path = Path.Combine(root, "DataTest");
                string filePath = Path.Combine(path, System.Configuration.ConfigurationSettings.AppSettings["FileTransferOrderData"]);
                if (File.Exists(filePath))
                {
                    return JObject.Parse(File.ReadAllText(filePath));
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


    }
}