using DAL.Model.HT;
using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.HT.TransferInstructions
{
    public class TransferInstructionDAL : IDisposable
    {
        QRMSEntities db;
        public TransferInstructionDAL() { db = new QRMSEntities(); }
        public TransferInstructionDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }


        public List<TransferInstruction> GetTransferInstruction(DateTime from_day, DateTime to_day, string WarehouseCode)
        {
            try
            {
                //var s = from c in db.TransferInstructions where c.WarehouseCode_From
                string SQL = $"select * from TransferInstruction a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }') ";
                SQL += $"and (a.TransferStatus is not null and a.TransferStatus != '{ ConstTransferStatus.Delivered }') ";
                SQL += $"and (a.TransferType is not null and a.TransferType = '{ ConstTransferType.Export }') ";
                SQL += $"and (a.WarehouseCode_From is not null and a.WarehouseCode_From = '{ WarehouseCode }') ";
                SQL += $"and CONVERT(date, '{from_day}') <= CONVERT(date, a.InstructionDate) ";
                SQL += $"and CONVERT(date, '{to_day}') >= CONVERT(date, a.InstructionDate) ";
                SQL += $"order by a.InstructionDate desc ";
                var data = db.TransferInstructions.SqlQuery(SQL).AsNoTracking().ToList();

                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdateTransferInstruction(int id, string tranferstatus)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (tranferstatus == "Y")
                        {
                            var xx = db.TransferInstructions.Where(f => f.ID == id).FirstOrDefault();
                            if (xx == null) throw new Exception("");
                            else
                                xx.TransferStatus = "Y";
                        }

                        db.DetachAll<PurchaseOrder>();
                        db.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        Logging.LogMessage(ex.ToString());
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //    //Logging.LogError(ex);
            //    //return 0;
            //}
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return -1;
            }
        }

        //Chuyển kho
        public List<TransferInstruction> GetTransferWarehousesDAL(DateTime from_day, DateTime to_day, string WarehouseCode_From, string WarehouseCode_To)
        {
            try
            {
                //var s = from c in db.TransferInstructions where c.InstructionDate
                string SQL = $"select * from TransferInstruction a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }') ";
                SQL += $"and (a.TransferStatus is not null and a.TransferStatus != '{ ConstTransferStatus.Delivered }') ";
                SQL += $"and (a.TransferType is not null and a.TransferType = '{ ConstTransferType.WarehouseTransfer}') ";
                SQL += $"and (a.WarehouseCode_From is not null and a.WarehouseCode_From = '{ WarehouseCode_From }') ";
                SQL += $"and (a.WarehouseCode_To is not null and a.WarehouseCode_To = '{ WarehouseCode_To }') ";
                SQL += $"and CONVERT(date, '{from_day}') <= CONVERT(date, a.InstructionDate) ";
                SQL += $"and CONVERT(date, '{to_day}') >= CONVERT(date, a.InstructionDate) ";
                SQL += $"order by a.InstructionDate desc ";
                var data = db.TransferInstructions.SqlQuery(SQL).AsNoTracking().ToList();

                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AccountManagement() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
