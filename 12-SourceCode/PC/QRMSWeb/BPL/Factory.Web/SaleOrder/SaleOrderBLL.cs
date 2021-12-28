using BPL.Factory.Web;
using BPL.Models.Web;
using DAL;
using DAL.Factory.Web.Users;
using DAL.Factory.Web.SaleOrder;
using HDLIB;
using HDLIB.Common;
using System;
using System.Collections.Generic;

namespace BLL.Factory.Web.SaleOrder
{
    public class SaleOrderBLL: BaseBLL
    {
        public SaleOrderModel GetSaleOrderById(int id)
        {
            try
            {
                var _SaleOrder = new SaleOrderManagement(db).Select(id);
                if (_SaleOrder != null)
                {
                    SaleOrderModel _SaleOrderModel = new SaleOrderModel();
                    _SaleOrderModel.CopyPropertiesFrom(_SaleOrder);
                    return _SaleOrderModel;
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
