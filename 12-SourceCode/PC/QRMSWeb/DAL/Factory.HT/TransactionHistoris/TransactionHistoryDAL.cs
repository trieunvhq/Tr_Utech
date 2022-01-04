using System;
using System.Collections.Generic;
using HDLIB.Common;

namespace DAL.Factory.HT.TransactionHistoris
{
    public class TransactionHistoryDAL : IDisposable
    {
        QRMSEntities db;
        public TransactionHistoryDAL() { db = new QRMSEntities(); }
        public TransactionHistoryDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        public long InsertTransactionHistory(List<TransactionHistory> obj)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        long result = 0;
                        db = db ?? GlobalVariable.db;

                        foreach (var item in obj)
                        {
                            var ss = db.TransactionHistories.Add(item);
                            result = ss.ID;
                        }

                        db.DetachAll<TransactionHistory>();
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
