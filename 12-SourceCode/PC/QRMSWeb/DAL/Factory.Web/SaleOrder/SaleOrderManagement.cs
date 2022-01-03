using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Factory.Web.SaleOrder
{
    public class SaleOrderManagement : BaseManagement
    {
        public SaleOrderManagement()
        {
            db = new QRMSEntities();
        }

        public SaleOrderManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        


        public DAL.SaleOrder Select(int ID)
        {
            try
            {
                return db.SaleOrders.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        
        

        public DAL.SaleOrder GetSaleOrderBySaleOrderNo(string saleOrderNo)
        {
            try
            {
                saleOrderNo = (saleOrderNo?.Trim()) ?? "";
                return db.SaleOrders.Where(item => saleOrderNo.Equals(item.SaleOrderNo)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.SaleOrder[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.SaleOrders.AddRange(_VALUEs);
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