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
        public List<DAL.PurchaseOrderItem> GetPurchaseOrderItemBy(int? purchaseOrderID, 
            string purchaseOrderNo, string supplierCode,
          string locationCode, string itemCode)
        {
            try
            {
                purchaseOrderNo = (purchaseOrderNo?.Trim()) ?? "";
                supplierCode = (supplierCode?.Trim()) ?? "";
                locationCode = (locationCode?.Trim()) ?? "";
                itemCode = (itemCode?.Trim()) ?? "";
                string SQL = $"select * from PurchaseOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";

                SQL += (purchaseOrderID == null || purchaseOrderID <= 0) ? "" : $" and a.purchaseOrderID = {purchaseOrderID}";
                SQL += (string.IsNullOrEmpty(purchaseOrderNo?.Trim())) ? "" : $" and LOWER(a.purchaseOrderNo) = '{purchaseOrderNo.ToLower()}'";
                SQL += (string.IsNullOrEmpty(supplierCode?.Trim())) ? "" : $" and LOWER(a.SupplierCode) = '{supplierCode.ToLower()}'";
                SQL += (string.IsNullOrEmpty(locationCode?.Trim())) ? "" : $" and LOWER(a.LocationCode) = '{locationCode.ToLower()}'";
                SQL += (string.IsNullOrEmpty(itemCode?.Trim())) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                SQL += " order by a.id desc";
                return db.PurchaseOrderItems.SqlQuery(SQL).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> FindAll(int page, int limit,
            string itemCode, string itemName, string purchaseOrderNo, string locationCode, 
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                itemCode = itemCode?.Trim();
                itemName = itemName?.Trim();
                purchaseOrderNo = purchaseOrderNo?.Trim();
                locationCode = locationCode?.Trim();

                HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem> paging = new HDLIB.WebPaging.TPaging<DAL.PurchaseOrderItem>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from PurchaseOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";
                if (isSearch) { 
                SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(itemName)) ? "" : $" and LOWER(a.ItemName) LIKE '%{itemName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(purchaseOrderNo)) ? "" : $" and LOWER(a.purchaseOrderNo) LIKE '%{purchaseOrderNo.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(locationCode)) ? "" : $" and LOWER(a.LocationCode) LIKE '%{locationCode.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                } else
                {
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(purchaseOrderNo)) ? "" : $" and LOWER(a.purchaseOrderNo) = '{purchaseOrderNo.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(locationCode)) ? "" : $" and LOWER(a.LocationCode) = '{locationCode.ToLower()}'";
                }
                if (startDate != null && endDate != null)
                {
                    SQL += $" and (a.purchaseOrderDate between convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (startDate != null)
                {
                    SQL += $" and (a.purchaseOrderDate >= convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (endDate != null)
                {
                    SQL += $" and (a.purchaseOrderDate <= convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
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
                string SQL = $"select * from PurchaseOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";
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