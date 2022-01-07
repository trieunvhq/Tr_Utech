using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.PurchaseOrder
{
    public class PurchaseOrderItemManagement : BaseManagement
    {
        QRMSEntities db;

        public PurchaseOrderItemManagement()
        {
            db = new QRMSEntities();
        }

        public PurchaseOrderItemManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }

        public DAL.PurchaseOrderItem FindByKey(int ID)
        {
            try
            {
                return db.PurchaseOrderItems.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public DAL.PurchaseOrderItem FindByNo(string purchaseOrderNo)
        {
            try
            {
                var query = db.PurchaseOrderItems.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                query = query.Where(n => n.PurchaseOrderNo.ToLower().Contains(purchaseOrderNo.ToLower()));
                var data = query.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        
        public List<DAL.PurchaseOrderItem> GetPurchaseOrderItemBy(string purchaseOrderNo)
        {
            try
            {
                purchaseOrderNo = (purchaseOrderNo?.Trim()) ?? "";
                var query = db.PurchaseOrderItems.AsQueryable();

                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);

                if (!string.IsNullOrWhiteSpace(purchaseOrderNo))
                {
                    query = query.Where(n => n.PurchaseOrderNo.ToLower().Contains(purchaseOrderNo.ToLower()));
                }
                var data = query.OrderByDescending(n => n.CreateDate).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.PurchaseOrder> FindAllPurchaseOrder(int page, int limit,
            string wareHouseCode, string purchaseOrderNo, string inputStatus,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                wareHouseCode = wareHouseCode?.Trim();
                purchaseOrderNo = purchaseOrderNo?.Trim();
                inputStatus = inputStatus?.Trim();

                HDLIB.WebPaging.TPaging<DAL.PurchaseOrder> paging = new HDLIB.WebPaging.TPaging<DAL.PurchaseOrder>();
                var query = db.PurchaseOrders.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(wareHouseCode))
                {
                    query = query.Where(n => n.WarehouseCode.ToLower().Contains(wareHouseCode.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(purchaseOrderNo))
                {
                    query = query.Where(n => n.PurchaseOrderNo.ToLower().Contains(purchaseOrderNo.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(inputStatus))
                {
                    query = query.Where(n => n.InputStatus.ToLower().Contains(inputStatus.ToLower()));
                }
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(n => n.PurchaseOrderDate >= startDate);

                }else
                if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.PurchaseOrderDate <= endDate);

                }else
                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.PurchaseOrderDate >= startDate);
                    query = query.Where(n => n.PurchaseOrderDate <= endDate);
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

        public HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> FindAllPurchaseOrderItem(int page, int limit,
            string purchaseOrderNo)
        {
            try
            {
                purchaseOrderNo = purchaseOrderNo?.Trim();

                HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> paging = new HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem>();
                var query = db.PurchaseOrderItems.AsQueryable();
                
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(purchaseOrderNo))
                {
                    query = query.Where(n => n.PurchaseOrderNo.ToLower().Contains(purchaseOrderNo.ToLower()));
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
        
        public int GetMaxpurchaseOrderId()
        {
            try
            {
                var purchaseOrder = db.PurchaseOrderItems.OrderByDescending(item => item.PurchaseOrderID).FirstOrDefault();
                return (purchaseOrder?.PurchaseOrderID) ?? 0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.PurchaseOrderItem[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.PurchaseOrderItems.AddRange(_VALUEs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Update(DAL.PurchaseOrderItem _VALUE)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.DetachAll<DAL.PurchaseOrderItem>();
                db.Entry(_VALUE).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        #region print
        
        public HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> FindAllPurchaseOrderPrint(int page, int limit,
            string itemType, string wareHouseCode, string purchaseOrderNo, string printStatus,
            DateTime? purchaseOrderDate, bool isSearch)
        {
            try
            {
                itemType = itemType?.Trim();
                wareHouseCode = wareHouseCode?.Trim();
                purchaseOrderNo = purchaseOrderNo?.Trim();
                printStatus = printStatus?.Trim();

                HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> paging = new HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from PurchaseOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                if (isSearch)
                {
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) LIKE '%{itemType.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(wareHouseCode)) ? "" : $" and LOWER(a.LocationCode) LIKE '%{wareHouseCode.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(purchaseOrderNo)) ? "" : $" and LOWER(a.purchaseOrderNo) LIKE '%{purchaseOrderNo.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(printStatus)) ? "" : $" and LOWER(a.PrintStatus) LIKE '%{printStatus.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                }
                else
                {
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(purchaseOrderNo)) ? "" : $" and LOWER(a.purchaseOrderNo) = '{purchaseOrderNo.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(wareHouseCode)) ? "" : $" and LOWER(a.LocationCode) = '{wareHouseCode.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(printStatus)) ? "" : $" and LOWER(a.PrintStatus) = '{printStatus.ToLower()}'";
                }
                if (purchaseOrderDate != null)
                {
                    SQL += $" and (a.purchaseOrderDate == convert(datetime, '{ purchaseOrderDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                SQL += " order by a.id desc";
                var exec_sql = db.PurchaseOrderItems.SqlQuery(SQL);
                var data = exec_sql.AsNoTracking().Skip(offset).Take(limit).ToList();
                int total = exec_sql.Count();
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
        #endregion

    }
}