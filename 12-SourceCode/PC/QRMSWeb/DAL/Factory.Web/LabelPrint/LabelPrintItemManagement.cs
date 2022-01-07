using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.LabelPrint
{
    public class LabelPrintItemManagement : BaseManagement
    {
        public LabelPrintItemManagement()
        {
            db = new QRMSEntities();
        }

        public LabelPrintItemManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        public DAL.LabelPrintItem Find(int ID)
        {
            try
            {
                return db.LabelPrintItems.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }



        public DAL.LabelPrintItem GetLabelPrintItemByOrderNo(string printOrderNo)
        {
            try
            {
                printOrderNo = (printOrderNo?.Trim()) ?? "";
                string SQL = $"select * from LabelPrintItem a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                SQL += $" and LOWER(a.PrintOrderNo) = '{printOrderNo.ToLower()}'";

                return db.LabelPrintItems.SqlQuery(SQL).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.LabelPrintItem> FindAll(int page, int limit,
            string itemType, string printOrderNo, string printStatus,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                itemType = itemType?.Trim();
                printOrderNo = printOrderNo?.Trim();
                printStatus = printStatus?.Trim();

                HDLIB.WebPaging.TPaging<DAL.LabelPrintItem> paging = new HDLIB.WebPaging.TPaging<DAL.LabelPrintItem>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from LabelPrintItem a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                
                SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                if (isSearch)
                {
                    SQL += (string.IsNullOrEmpty(printOrderNo)) ? "" : $" and LOWER(a.PrintOrderNo) LIKE '%{printOrderNo.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    
                }
                else
                {
                    SQL += (string.IsNullOrEmpty(printOrderNo)) ? "" : $" and LOWER(a.PrintOrderNo) = '{printOrderNo.ToLower()}'";
                }
                SQL += (string.IsNullOrEmpty(printStatus)) ? "" : $" and LOWER(a.PrintStatus) = '{printStatus.ToLower()}'";


                if (startDate != null && endDate != null)
                {
                    SQL += $" and (a.PrintOrderDate between convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (startDate != null)
                {
                    SQL += $" and (a.PrintOrderDate >= convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (endDate != null)
                {
                    SQL += $" and (a.PrintOrderDate <= convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                SQL += " order by a.id desc";
                var exec_sql = db.LabelPrintItems.SqlQuery(SQL);
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
        public List<DAL.LabelPrintItem> FindAllWithOutPagging(
            string itemType, string printOrderNo, string printStatus, string itemCode, string itemName,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                itemType = itemType?.Trim();
                printOrderNo = printOrderNo?.Trim();
                printStatus = printStatus?.Trim();
                itemCode = itemCode?.Trim();
                itemName = itemName?.Trim();
                string SQL = $"select * from LabelPrintItem a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";

                SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                if (isSearch)
                {
                    SQL += (string.IsNullOrEmpty(printOrderNo)) ? "" : $" and LOWER(a.PrintOrderNo) LIKE '%{printOrderNo.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                }
                else
                {
                    SQL += (string.IsNullOrEmpty(printOrderNo)) ? "" : $" and LOWER(a.PrintOrderNo) = '{printOrderNo.ToLower()}'";
                    SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) = '{itemCode.ToLower()}'";
                }
                SQL += (string.IsNullOrEmpty(printStatus)) ? "" : $" and LOWER(a.PrintStatus) = '{printStatus.ToLower()}'";
                SQL += (string.IsNullOrEmpty(itemName)) ? "" : $" and LOWER(a.ItemName) LIKE '%{itemName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";

                if (startDate != null && endDate != null)
                {
                    SQL += $" and (a.PrintOrderDate between convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103) and convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 23:59:59") }', 103)) ";
                }
                else if (startDate != null)
                {
                    SQL += $" and (a.PrintOrderDate >= convert(datetime, '{ startDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }
                else if (endDate != null)
                {
                    SQL += $" and (a.PrintOrderDate <= convert(datetime, '{ endDate.Value.ToString("dd-MM-yyyy 00:00:00") }', 103))";
                }

                SQL += " order by a.id desc";
                return db.LabelPrintItems.SqlQuery(SQL).AsNoTracking().ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.LabelPrintItem[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.LabelPrintItems.AddRange(_VALUEs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Update(DAL.LabelPrintItem _VALUE)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.DetachAll<DAL.LabelPrintItem>();
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