using DAL.Model.HT;
using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.HT.SaleOrderItems
{
    public class SaleOrderItemDAL : IDisposable
    {
        QRMSEntities db;
        public SaleOrderItemDAL() { db = new QRMSEntities(); }
        public SaleOrderItemDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        
        //public decimal SoLuongDaNhap { get; set; }
        //public int SoLuongBox { get; set; }

        public List<SaleOrderItemScanDAL> GetSaleOrderItemDAL(int SaleOrderID_Input)
        {
            try
            {
                //var s = from c in db.SaleOrderItems where c.sta
                //var s = from c in db.TransactionHistories where c.TransactionType

                string SQL = $"select DISTINCT [ID], [SaleOrderID], [SaleOrderNo], [SaleOrderDate], [CustomerCode], [CustomerName], [WarehouseCode] ";
                SQL += $", [WarehouseName], [LocationCode], [LocationName], [ItemType], [ItemCode], [ItemName], ";
                SQL += $"[Quantity], [Unit], [RecordStatus], [InputStatus], ";
                SQL += $"(case when(select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[SaleOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType] and b.TransactionType = 'O') is null then 0 ";
                SQL += $"else (select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[SaleOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType] and b.TransactionType = 'O') end) SoLuongDaNhap, ";
                SQL += $"(case when(select COUNT(*) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[SaleOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType] and b.TransactionType = 'O') is null then 0 ";
                SQL += $"else (select COUNT(*) from[dbo].[TransactionHistory] b where b.[OrderNo] = a.[SaleOrderNo] and b.[ItemCode] = a.[ItemCode] ";
                SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType] and b.TransactionType = 'O') end) SoLuongBox ";
                SQL += $"from SaleOrderItem a ";
                SQL += $"where(a.RecordStatus is not null and a.RecordStatus != 'D') ";
                //SQL += $"and(a.TransferStatus is not null and a.TransferStatus != 'Y') ";
                SQL += $"and a.[SaleOrderID] = {SaleOrderID_Input} ";
                SQL += $"order by a.[SaleOrderDate] desc ";

                var data = db.Database.SqlQuery<SaleOrderItemScanDAL>(SQL).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdateSaleOrderItemDAL(List<SaleOrderItemScanDAL> obj)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in obj)
                        {
                            var dept = db.SaleOrderItems.Where(f => f.ID == item.ID).FirstOrDefault();
                            if (dept == null) throw new Exception("");
                            else
                                dept.OutputStatus = item.OutputStatus;
                        }

                        db.DetachAll<SaleOrderItem>();
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
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return -1;
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
