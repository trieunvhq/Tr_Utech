using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.ImportEquipment
{
    public class ImportEquipmentManagement : BaseManagement
    {
        public ImportEquipmentManagement()
        {
            db = new QRMSEntities();
        }

        public ImportEquipmentManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }

        public HDLIB.WebPaging.TPaging<DAL.TransactionHistory> FindAllImportEquipment(int page, int limit,
            string itemCode, string orderNo, string wareHouseCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                itemCode = itemCode?.Trim();
                orderNo = orderNo?.Trim();
                wareHouseCode = wareHouseCode?.Trim();

                HDLIB.WebPaging.TPaging<DAL.TransactionHistory> paging = new HDLIB.WebPaging.TPaging<DAL.TransactionHistory>();
                var query = db.TransactionHistories.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted && n.TransactionType == ConstTransactionType.Import);
                if (!string.IsNullOrWhiteSpace(wareHouseCode))
                {
                    query = query.Where(n => n.WarehouseCode_To.ToLower().Contains(wareHouseCode.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(n => n.OrderNo.ToLower().Contains(orderNo.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(itemCode))
                {
                    query = query.Where(n => n.ItemCode.ToLower().Contains(itemCode.ToLower()));
                }
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(n => n.OrderDate >= startDate);

                }
                else
                if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.OrderDate <= endDate);

                }
                else
                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.OrderDate >= startDate);
                    query = query.Where(n => n.OrderDate <= endDate);
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
    }
}