using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.LabelPrint
{
    public class LabelPrintManagement : BaseManagement
    {
        public LabelPrintManagement()
        {
            db = new QRMSEntities();
        }

        public LabelPrintManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        public DAL.LabelPrint Find(int ID)
        {
            try
            {
                return db.LabelPrints.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }



        public DAL.LabelPrint GetLabelPrintByOrderNo(string printOrderNo)
        {
            try
            {
                printOrderNo = (printOrderNo?.Trim()) ?? "";
                string SQL = $"select * from LabelPrint a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                SQL += $" and LOWER(a.PrintOrderNo) = '{printOrderNo.ToLower()}'";

                return db.LabelPrints.SqlQuery(SQL).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public HDLIB.WebPaging.TPaging<DAL.LabelPrint> FindAll(int page, int limit,
            string itemType, string printOrderNo, string wareHouseCode, string printStatus,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                itemType = itemType?.Trim();
                printOrderNo = printOrderNo?.Trim();
                wareHouseCode = wareHouseCode?.Trim();
                printStatus = printStatus?.Trim();

                HDLIB.WebPaging.TPaging<DAL.LabelPrint> paging = new HDLIB.WebPaging.TPaging<DAL.LabelPrint>();
                int offset = (page - 1) * limit;
                string SQL = $"select * from LabelPrint a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";
                
                SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                SQL += (string.IsNullOrEmpty(wareHouseCode)) ? "" : $" and LOWER(a.WarehouseCode) = '{wareHouseCode.ToLower()}'";
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
                var exec_sql = db.LabelPrints.SqlQuery(SQL);
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
        public List<DAL.LabelPrint> FindAllWithOutPagging(
            string itemType, string printOrderNo, string wareHouseCode, string printStatus,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                itemType = itemType?.Trim();
                printOrderNo = printOrderNo?.Trim();
                wareHouseCode = wareHouseCode?.Trim();
                printStatus = printStatus?.Trim();

                string SQL = $"select * from LabelPrint a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }')";

                SQL += (string.IsNullOrEmpty(itemType)) ? "" : $" and LOWER(a.ItemType) = '{itemType.ToLower()}'";
                SQL += (string.IsNullOrEmpty(wareHouseCode)) ? "" : $" and LOWER(a.WarehouseCode) = '{wareHouseCode.ToLower()}'";
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
                return db.LabelPrints.SqlQuery(SQL).AsNoTracking().ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.LabelPrint[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.LabelPrints.AddRange(_VALUEs);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return _VALUEs[0].ID;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Update(DAL.LabelPrint _VALUE)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.DetachAll<DAL.LabelPrint>();
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