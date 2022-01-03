using BPL.Factory.Web;
using BPL.Models.Web;
using DAL;
using DAL.Factory.Web.Users;
using DAL.Factory.Web.PurchaseOrderItems;
using HDLIB;
using HDLIB.Common;
using System;
using System.Collections.Generic;

namespace BLL.Factory.Web.PurchaseOrderItem
{
    public class SaleOrderItemBLL : BaseBLL
    {

        public HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> GetAllPurchaseOrderItem(int page, int limit, string itemName, string itemCode, 
            string locationName, string purchaseOrderNo)
        {
            HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> paging = new HDLIB.WebPaging.TPaging<PurchaseOrderItemModel>();
            var myPagging = new PurchaseOrderItemManagement(db).GetAllPurchaseOrderItem(
                        page,
                        limit,
                        itemName,
                        itemCode, locationName, purchaseOrderNo
                    );
            paging.page = myPagging.page;
            paging.total = myPagging.total;
            paging.pages = myPagging.pages;
            paging.limit = myPagging.limit;
            List<PurchaseOrderItemModel> lstSaleOrderItem = new List<PurchaseOrderItemModel>();
            foreach (var row in myPagging.rows)
            {
                PurchaseOrderItemModel PurchaseOrderItemModel = new PurchaseOrderItemModel();
                PurchaseOrderItemModel.CopyPropertiesFrom(row);
                lstSaleOrderItem.Add(PurchaseOrderItemModel);
            }
            paging.rows = lstSaleOrderItem;
            return paging;
        }

        public PurchaseOrderItemModel GetSaleOrderItemById(int id)
        {
            try
            {
                var _SaleOrderItem = new PurchaseOrderItemManagement(db).Select(id);
                if (_SaleOrderItem != null)
                {
                    PurchaseOrderItemModel _PurchaseOrderItemModel = new PurchaseOrderItemModel();
                    _PurchaseOrderItemModel.CopyPropertiesFrom(_SaleOrderItem);
                    return _PurchaseOrderItemModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

    }
}
