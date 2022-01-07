using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.TransactionHistory
{
    public class TransactionHistoryManagement : BaseManagement
    {
        public TransactionHistoryManagement()
        {
            db = new QRMSEntities();
        }

        public TransactionHistoryManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        public DAL.TransactionHistory Select(int ID)
        {
            try
            {
                return db.TransactionHistories.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.TransactionHistory> FindAll(int page, int limit,
                    string transactionType, string orderNo, DateTime? orderDate,
            string itemType, string itemCode, DateTime? orderDateFrom, DateTime? orderDateTo, bool isSearch)
        {
            try
            {
                transactionType = transactionType?.Trim();
                orderNo = orderNo?.Trim();
                itemType = itemType?.Trim();
                itemCode = itemCode?.Trim();

                HDLIB.WebPaging.TPaging<DAL.TransactionHistory> paging = new HDLIB.WebPaging.TPaging<DAL.TransactionHistory>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from TransactionHistory a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                if (isSearch)
                {
                    SQL += (string.IsNullOrEmpty(transactionType)) ? "" : $" and LOWER(a.TransactionType) LIKE '%{transactionType.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(orderNo)) ? "" : $" and LOWER(a.OrderNo) LIKE '%{orderNo.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) LIKE '%{itemType.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                }
                else
                {
                    SQL += (string.IsNullOrEmpty(transactionType)) ? "" : $" and LOWER(a.TransactionType) = '{transactionType.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(orderNo)) ? "" : $" and LOWER(a.OrderNo) = '{orderNo.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                }
                if (orderDate != null)
                {
                    SQL += $" and (a.OrderDate == convert(datetime, '{ orderDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                if (orderDateFrom != null && orderDateTo != null)
                {
                    SQL += $" and (a.OrderDate between convert(datetime, '{ orderDateFrom.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ orderDateTo.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (orderDateFrom != null)
                {
                    SQL += $" and (a.OrderDate >= convert(datetime, '{ orderDateFrom.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (orderDateTo != null)
                {
                    SQL += $" and (a.OrderDate <= convert(datetime, '{ orderDateTo.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }


                SQL += " order by a.id desc";
                var exec_sql = db.TransactionHistories.SqlQuery(SQL);
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

        public List<DAL.TransactionHistory> FindAllWithOutPagging(string transactionType, string orderNo, DateTime? orderDate,
            string itemType, string itemCode, DateTime? orderDateFrom, DateTime? orderDateTo, bool isSearch)
        {
            try
            {
                transactionType = transactionType?.Trim();
                orderNo = orderNo?.Trim();
                itemType = itemType?.Trim();
                itemCode = itemCode?.Trim();

                string SQL = $"select * from TransactionHistory a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                if (isSearch)
                {
                    SQL += (string.IsNullOrEmpty(transactionType)) ? "" : $" and LOWER(a.TransactionType) LIKE '%{transactionType.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(orderNo)) ? "" : $" and LOWER(a.OrderNo) LIKE '%{orderNo.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) LIKE '%{itemType.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                }
                else
                {
                    SQL += (string.IsNullOrEmpty(transactionType)) ? "" : $" and LOWER(a.TransactionType) = '{transactionType.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(orderNo)) ? "" : $" and LOWER(a.OrderNo) = '{orderNo.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                }
                if (orderDate != null)
                {
                    SQL += $" and (a.OrderDate == convert(datetime, '{ orderDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                if (orderDateFrom != null && orderDateTo != null)
                {
                    SQL += $" and (a.OrderDate between convert(datetime, '{ orderDateFrom.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ orderDateTo.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (orderDateFrom != null)
                {
                    SQL += $" and (a.OrderDate >= convert(datetime, '{ orderDateFrom.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (orderDateTo != null)
                {
                    SQL += $" and (a.OrderDate <= convert(datetime, '{ orderDateTo.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }


                SQL += " order by a.id desc";
                return db.TransactionHistories.SqlQuery(SQL).AsNoTracking().ToList();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }

        public List<DAL.TransactionHistory> GetAllBy(string orderNo,string transactionType)
        {
            try
            {
                orderNo = (orderNo?.Trim()) ?? "";

                return db.TransactionHistories.Where(item => orderNo.Equals(item.OrderNo) && item.TransactionType.Equals(transactionType) && item.RecordStatus != Constant.DeletedRecordStatus).ToList();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.TransactionHistory[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.TransactionHistories.AddRange(_VALUEs);
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