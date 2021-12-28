using System;
using System.Collections.Generic;
using BPL.Models;
using DAL;
using DAL.Factory.HT.PurchaseOrderitems;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.PurchaseOrderitems
{
    public class PurchaseOrderitemBPL : BaseBPL
    {
        public string GetPurchaseOrderitems(int id, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new PurchaseOrderitemDAL(db);
                var result = pr.GetPurchaseOrderitem(id);
                if (!string.IsNullOrEmpty(result))
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    //var ListOut = new List<PurchaseOrderItemBPLModel>();

                    //foreach (PurchaseOrderItem item in result)
                    //{
                    //    var xx = new PurchaseOrderItemBPLModel();
                    //    xx.CopyPropertiesFrom(item);
                    //    ListOut.Add(xx);
                    //}

                    return result;
                }
                err_code = "5";
                err_msg = "Không có dữ liệu";
                return null;
            }
            catch (Exception ex)
            {
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdatePurchaseOrderitem(List<PurchaseOrderItemBPLModel> obj, out string err_code, out string err_msg)
        {
            try
            {
                var ListOut = new List<PurchaseOrderItem>();

                foreach (PurchaseOrderItemBPLModel item in obj)
                {
                    var xx = new PurchaseOrderItem();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                var pr = new PurchaseOrderitemDAL(db);
                var result = pr.UpdatePurchaseOrderitem(ListOut);
                if (result == 1)
                {
                    err_code = "0";
                    err_msg = "Update dữ liệu thành công";
                }
                else
                {
                    err_code = "5";
                    err_msg = "Không update được dữ liệu";
                }    

                return result;
            }
            catch (Exception ex)
            {
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return 0;
            }
        }

    }
}
