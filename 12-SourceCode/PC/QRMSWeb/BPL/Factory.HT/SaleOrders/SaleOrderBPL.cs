using System;
using System.Collections.Generic;
using BPL.Models;
using DAL;
using DAL.Factory.HT.SaleOrders;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.SaleOrders
{
    public class SaleOrderBPL : BaseBPL
    {
        public List<SaleOrderBPLModel> GetSaleOrderBPL(DateTime from_day, DateTime to_day, string WarehouseCode, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new SaleOrderDAL(db);
                var result = pr.GetSaleOrderDAL(from_day, to_day, WarehouseCode);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<SaleOrderBPLModel>();

                    foreach (SaleOrder item in result)
                    {
                        var xx = new SaleOrderBPLModel();
                        xx.CopyPropertiesFrom(item);
                        ListOut.Add(xx);
                    }

                    return ListOut;
                }
                err_code = "-1";
                err_msg = "Không có dữ liệu";
                return null;
            }
            catch (Exception ex)
            {
                err_code = ConstResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
            }
        }
    }
}
