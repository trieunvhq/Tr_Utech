using System;
using System.Collections.Generic;
using DAL;
using DAL.Factory.HT.PurchaseOrderitems;
using HDLIB.Common;

namespace BPL.Factory.HT.PurchaseOrderitems
{
    public class PurchaseOrderitemBPL
    {
        QRMSEntities db;
        public PurchaseOrderitemBPL() { db = new QRMSEntities(); }
        public PurchaseOrderitemBPL(QRMSEntities db) { this.db = db ?? new QRMSEntities(); }

        public List<PurchaseOrderItem> GetPurchaseOrderitems(int id, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new PurchaseOrderitemDAL(db);
                var result = pr.GetPurchaseOrderitem(id);
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
