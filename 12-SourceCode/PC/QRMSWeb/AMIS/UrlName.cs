using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS
{
    public class UrlName
    {
        //Lấy danh sách thông tin nguyên liệu, thành phẩm, dụng cụ (GET)
        public readonly static string GetListItemMaster = System.Configuration.ConfigurationSettings.AppSettings["UrlGetListItemMaster"];
        //Lấy danh sách thông tin kho (GET)
        public readonly static string GetListWarehouse = System.Configuration.ConfigurationSettings.AppSettings["UrlGetListWarehouse"];
        //Lấy dánh sách đơn mua vật liệu (GET)
        public readonly static string GetListPurchaseOrder = System.Configuration.ConfigurationSettings.AppSettings["UrlGetListPurchaseOrder"];
        //Lấy danh sách đơn khách hàng đặt mua (GET)
        public readonly static string GetListSaleOrder = System.Configuration.ConfigurationSettings.AppSettings["UrlGetListSaleOrder"];
        //Lấy danh sách chỉ thị Xuất kho / chuyển kho (GET)
        public readonly static string GetListTransferOrder = System.Configuration.ConfigurationSettings.AppSettings["UrlGetListTransferOrder"];
    }
}
