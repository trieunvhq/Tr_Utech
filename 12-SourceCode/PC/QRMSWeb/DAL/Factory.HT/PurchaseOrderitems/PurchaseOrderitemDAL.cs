using System;
using System.Collections.Generic;
using System.Linq;
using HDLIB.Common;

namespace DAL.Factory.HT.PurchaseOrderitems
{
    public class PurchaseOrderitemDAL : IDisposable
    {
        QRMSEntities db;
        public PurchaseOrderitemDAL() { db = new QRMSEntities(); }
        public PurchaseOrderitemDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        public List<PurchaseOrderItem> GetPurchaseOrderitem(int PurchaseOrderID_Input)
        {
            try
            {
                //var s = from c in db.PurchaseOrderItems where c.UpdateDate select ;
                string SQL = $"select * from PurchaseOrderItems a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }') ";
                SQL += $"and (a.InputStatus is not null and a.InputStatus != '{ InputStatus.Enough }') ";
                SQL += $"and a.PurchaseOrderID = '{PurchaseOrderID_Input}' ";
                SQL += $"order by a.CreateDate desc ";
                var data = db.PurchaseOrderItems.SqlQuery(SQL).AsNoTracking().ToList();

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
