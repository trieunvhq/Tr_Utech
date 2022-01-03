using DAL.Model.HT;
using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.HT.TransferInstructionItems
{
    public class TransferInstructionItemDAL : IDisposable
    {
        QRMSEntities db;
        public TransferInstructionItemDAL() { db = new QRMSEntities(); }
        public TransferInstructionItemDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }


        public List<XuatKhoDungCuModelDAL> GetTransferInstructionItem(int PurchaseOrderID_Input)
        {
            try
            {
                string SQL = $"select DISTINCT [ID], [TransferOrderID], [TransferOrderNo], [TransferType], [InstructionDate], [ItemCode] ";
                SQL += $", [ItemName], [ItemType], [Quantity], [Unit], [TransferStatus], [RecordStatus], ";
                SQL += $"(case when(select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[TransferOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) is null then 0 ";
                SQL += $"else (select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[TransferOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) end) SoLuongDaNhap, ";
                SQL += $"(case when(select COUNT(*) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[TransferOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) is null then 0 ";
                SQL += $"else (select COUNT(*) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[TransferOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) end) SoLuongBox ";
                SQL += $"from TransferInstructionItem a ";
                SQL += $"where(a.RecordStatus is not null and a.RecordStatus != 'D') ";
                SQL += $"and(a.TransferStatus is not null and a.TransferStatus != 'Y') ";
                SQL += $"and(a.TransferType is not null and a.TransferType = 'O') ";
                SQL += $"and a.[TransferOrderID] = {PurchaseOrderID_Input} ";
                SQL += $"order by a.[InstructionDate] desc ";

                var data = db.Database.SqlQuery<XuatKhoDungCuModelDAL>(SQL).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdateTransferInstructionItem(List<XuatKhoDungCuModelDAL> obj)
        {
            try
            {
                foreach (var item in obj)
                {
                    var dept = db.TransferInstructionItems.Where(f => f.ID == item.ID).FirstOrDefault();
                    if (dept == null) throw new Exception("");
                    else
                        dept.TransferStatus = item.TransferStatus;
                }

                db.SaveChanges();

                return 1;
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
                return -99;
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
