using HDLIB.Common;
using DAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Factory.Web.TransferInstruction
{
    public class TransferInstructionItemManagement : BaseManagement
    {
        public TransferInstructionItemManagement()
        {
            db = new QRMSEntities();
        }

        public TransferInstructionItemManagement(QRMSEntities db)
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

        
        public DAL.TransferInstruction FindByNo(string transferOrderNo, string transferType)
            {
            try
            {
                var query = db.TransferInstructions.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                query = query.Where(n => n.TransferOrderNo.ToLower().Contains(transferOrderNo.ToLower()) && n.TransferType.Contains(transferType));
                var data = query.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public List<DAL.TransferInstructionItem> GetTransferInstructionItemBy(string TransferOrderNo, string TransferOrderType
          )
        {
            try
            {
                TransferOrderNo = (TransferOrderNo?.Trim()) ?? "";
                TransferOrderType = (TransferOrderType?.Trim()) ?? "";

                var query = db.TransferInstructionItems.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                query = query.Where(n => n.TransferOrderNo.ToLower().Contains(TransferOrderNo.ToLower()) && n.TransferType.Contains(TransferOrderType));
                var data = query.ToList();
                return data;
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

        public HDLIB.WebPaging.TPaging<DAL.TransferInstruction> FindAllTransferInstruction(int page, int limit,string transferType,
            string transferNo, string wareHouseCode_from, string wareHouseCode_to, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                transferNo = transferNo?.Trim();
                wareHouseCode_from = wareHouseCode_from?.Trim();
                wareHouseCode_to = wareHouseCode_to?.Trim();

                HDLIB.WebPaging.TPaging<DAL.TransferInstruction> paging = new HDLIB.WebPaging.TPaging<DAL.TransferInstruction>();
                var query = db.TransferInstructions.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(transferType))
                {
                    query = query.Where(n => n.TransferType.ToLower().Contains(transferType.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(wareHouseCode_to))
                {
                    query = query.Where(n => n.WarehouseCode_To.ToLower().Contains(wareHouseCode_to.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(wareHouseCode_from))
                {
                    query = query.Where(n => n.WarehouseCode_From.ToLower().Contains(wareHouseCode_from.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(transferNo))
                {
                    query = query.Where(n => n.TransferOrderNo.ToLower().Contains(transferNo.ToLower()));
                }
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(n => n.InstructionDate >= startDate);

                }
                else
                if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.InstructionDate <= endDate);

                }
                else
                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(n => n.InstructionDate >= startDate);
                    query = query.Where(n => n.InstructionDate <= endDate);
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

        public HDLIB.WebPaging.TPaging<DAL.TransferInstructionItem> FindAllTransferInstructionItem(int page, int limit,string transferType,
            string transferNo)
        {
            try
            {
                transferNo = transferNo?.Trim();

                HDLIB.WebPaging.TPaging<DAL.TransferInstructionItem> paging = new HDLIB.WebPaging.TPaging<DAL.TransferInstructionItem>();
                var query = db.TransferInstructionItems.AsQueryable();
                query = query.Where(n => n.RecordStatus != null && n.RecordStatus != ConstRecordStatus.Deleted);
                if (!string.IsNullOrWhiteSpace(transferType))
                {
                    query = query.Where(n => n.TransferType.ToLower().Contains(transferType.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(transferNo))
                {
                    query = query.Where(n => n.TransferOrderNo.ToLower().Contains(transferNo.ToLower()));
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

        public int GetMaxTransferInstructionId()
        {
            try
            {
                var transferIns = db.TransferInstructionItems.OrderByDescending(item => item.TransferOrderID).FirstOrDefault();
                return (transferIns?.TransferOrderID)??0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.TransferInstructionItem[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.TransferInstructionItems.AddRange(_VALUEs);
                if (db.SaveChanges() > 0)
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