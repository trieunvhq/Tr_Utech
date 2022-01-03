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
        public List<DAL.SaleOrderItem> GetSaleOrderItemBy(int? saleOrderID, string saleOrderNo, string customerCode,
          string locationCode, string itemCode)
        {
            try
            {
                saleOrderNo = (saleOrderNo?.Trim()) ?? "";
                customerCode = (customerCode?.Trim()) ?? "";
                locationCode = (locationCode?.Trim()) ?? "";
                itemCode = (itemCode?.Trim()) ?? "";
                string SQL = $"select * from SaleOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";

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

        public HDLIB.WebPaging.TPaging<DAL.SaleOrderItem> FindAll(int page, int limit,
            string itemCode, string itemName, string saleOrderNo, string locationName, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                itemCode = itemCode?.Trim();
                itemName = itemName?.Trim();
                saleOrderNo = saleOrderNo?.Trim();
                locationName = locationName?.Trim();

                HDLIB.WebPaging.TPaging<DAL.SaleOrderItem> paging = new HDLIB.WebPaging.TPaging<DAL.SaleOrderItem>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from SaleOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";

                SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(itemName?.Trim())) ? "" : $" and LOWER(a.ItemName) LIKE '%{itemName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(saleOrderNo?.Trim())) ? "" : $" and LOWER(a.SaleOrderNo) LIKE '%{saleOrderNo.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(locationName?.Trim())) ? "" : $" and LOWER(a.LocationName) LIKE '%{locationName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";

                if (startDate != null && endDate != null)
                {
                    SQL += $" and (a.SaleOrderDate between convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (startDate != null)
                {
                    SQL += $" and (a.SaleOrderDate >= convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (endDate != null)
                {
                    SQL += $" and (a.SaleOrderDate <= convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                SQL += " order by a.id desc";
                var exec_sql = db.SaleOrderItems.SqlQuery(SQL);
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