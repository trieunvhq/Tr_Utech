using System;
using System.Collections.Generic;
using DAL;
using DAL.Factory.HT.PurchaseOrders;
using HDLIB.Common;

namespace BPL.Factory.HT.PurchaseOrders
{
    public class PurchaseOrderBPL
    {
        QRMSEntities db;
        public PurchaseOrderBPL() { db = new QRMSEntities(); }
        public PurchaseOrderBPL(QRMSEntities db) { this.db = db ?? new QRMSEntities(); }

        public List<PurchaseOrder> GetPurchaseOrder(DateTime from_day, DateTime to_day, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new PurchaseOrderDAL(db);
                var result = pr.GetPurchaseOrder(from_day, to_day);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

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

    }
}
