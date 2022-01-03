using HDLIB.Common;
using DAL;
using System;
using System.Linq;

namespace DAL.Factory.Web.PurchaseOrder
{
    public class PurchaseOrderManagement : BaseManagement
    {
        public PurchaseOrderManagement()
        {
            db = new QRMSEntities();
        }

        public PurchaseOrderManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        public DAL.PurchaseOrder Select(int ID)
        {
            try
            {
                return db.PurchaseOrders.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }



        public DAL.PurchaseOrder GetpurchaseOrderBypurchaseOrderNo(string purchaseOrderNo)
        {
            try
            {
                purchaseOrderNo = (purchaseOrderNo?.Trim()) ?? "";
                return db.PurchaseOrders.Where(item => purchaseOrderNo.Equals(item.PurchaseOrderNo)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.PurchaseOrder[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.PurchaseOrders.AddRange(_VALUEs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }


    }
}