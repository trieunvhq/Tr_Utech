using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.SaleOrder
{
    public class SaleOrderItemManagement : BaseManagement
    {
        public SaleOrderItemManagement()
        {
            db = new QRMSEntities();
        }

        public SaleOrderItemManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }


        public DAL.SaleOrderItem FindByKey(int ID)
        {
            try
            {
                return db.SaleOrderItems.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        
        public DAL.SaleOrder FindByNo(string saleOrderNo)
            {
            try
            {
                var query = db.SaleOrders.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                query = query.Where(n => n.SaleOrderNo.ToLower().Contains(saleOrderNo.ToLower()));
                var data = query.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public List<DAL.SaleOrderItem> GetSaleOrderItemBy(int? saleOrderID, string saleOrderNo, string customerCode,
          string locationCode, string itemCode)
        {
            try
            {
                saleOrderNo = (saleOrderNo?.Trim()) ?? "";
                customerCode = (customerCode?.Trim()) ?? "";
                locationCode = (locationCode?.Trim()) ?? "";
                itemCode = (itemCode?.Trim()) ?? "";
                string SQL = $"select * from SaleOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";

                SQL += (saleOrderID == null || saleOrderID <= 0) ? "" : $" and a.SaleOrderID = {saleOrderID}";
                SQL += (string.IsNullOrEmpty(saleOrderNo?.Trim())) ? "" : $" and LOWER(a.SaleOrderNo) = '{saleOrderNo.ToLower()}'";
                SQL += (string.IsNullOrEmpty(customerCode?.Trim())) ? "" : $" and LOWER(a.CustomerCode) = '{customerCode.ToLower()}'";
                SQL += (string.IsNullOrEmpty(locationCode?.Trim())) ? "" : $" and LOWER(a.LocationCode) = '{locationCode.ToLower()}'";
                SQL += (string.IsNullOrEmpty(itemCode?.Trim())) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                SQL += " order by a.id desc";
                return db.SaleOrderItems.SqlQuery(SQL).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public List<DAL.SaleOrderItem> GetSaleOrderItemBySaleOrderNo(string saleOrderNo)
        {
            try
            {
                saleOrderNo = (saleOrderNo?.Trim()) ?? "";
                var query = db.SaleOrderItems.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted && n.SaleOrderNo.Contains(saleOrderNo));
                var data = query.OrderByDescending(n => n.CreateDate).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.SaleOrder> FindAllSaleOrder(int page, int limit,
            string exportStatus, string saleOrderNo, string wareHouseCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                exportStatus = exportStatus?.Trim();
                saleOrderNo = saleOrderNo?.Trim();
                wareHouseCode = wareHouseCode?.Trim();

                HDLIB.WebPaging.TPaging<DAL.SaleOrder> paging = new HDLIB.WebPaging.TPaging<DAL.SaleOrder>();
                var query = db.SaleOrders.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(wareHouseCode))
                {
                    query = query.Where(n => n.WarehouseCode.ToLower().Contains(wareHouseCode.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(saleOrderNo))
                {
                    query = query.Where(n => n.SaleOrderNo.ToLower().Contains(saleOrderNo.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(exportStatus))
                {
                    query = query.Where(n => n.ExportStatus.ToLower().Contains(exportStatus.ToLower()));
                }
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(n => n.SaleOrderDate >= startDate);

                }
                else
                if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.SaleOrderDate <= endDate);

                }
                else
                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.SaleOrderDate >= startDate);
                    query = query.Where(n => n.SaleOrderDate <= endDate);
                }
                var total = query.Count();
                var data = query.OrderByDescending(n => n.CreateDate).Skip((page - 1) * limit).Take(limit).ToList();
                paging.CalculatePaging(data, page, limit, total);
                return paging;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.SaleOrderItem> FindAllSaleOrderItem(int page, int limit,
            string saleOrderItem)
        {
            try
            {
                saleOrderItem = saleOrderItem?.Trim();

                HDLIB.WebPaging.TPaging<DAL.SaleOrderItem> paging = new HDLIB.WebPaging.TPaging<DAL.SaleOrderItem>();
                var query = db.SaleOrderItems.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(saleOrderItem))
                {
                    query = query.Where(n => n.SaleOrderNo.ToLower().Contains(saleOrderItem.ToLower()));
                }
                var total = query.Count();
                var data = query.OrderByDescending(n => n.CreateDate).Skip((page - 1) * limit).Take(limit).ToList();
                paging.CalculatePaging(data, page, limit, total);
                return paging;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }

        public int GetMaxSaleOrderId()
        {
            try
            {
                var saleOrder = db.SaleOrderItems.OrderByDescending(item => item.SaleOrderID).FirstOrDefault();
                return (saleOrder?.SaleOrderID)??0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.SaleOrderItem[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.SaleOrderItems.AddRange(_VALUEs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Update(DAL.SaleOrderItem _VALUE)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.DetachAll<DAL.SaleOrderItem>();
                db.Entry(_VALUE).State = System.Data.Entity.EntityState.Modified;
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