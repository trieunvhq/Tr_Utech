using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDLIB.Paging;
using HDLIB.Common;
using System.Collections.Generic;
using DAL.Model.HT;

namespace DAL.Factory.HT.PurchaseOrders
{
    public class PurchaseOrderDAL : IDisposable
    {
        QRMSEntities db;
        public PurchaseOrderDAL() { db = new QRMSEntities(); }
        public PurchaseOrderDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }


        public List<PurchaseOrder> GetPurchaseOrder(DateTime from_day, DateTime to_day)
        {
            try
            {
                //var s = from c in db.PurchaseOrders where c.CreateDate
                string SQL = $"select * from PurchaseOrder a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }') ";
                SQL += $"and (a.InputStatus is not null and a.InputStatus != '{ InputStatus.Enough }') ";
                SQL += $"and CONVERT(date, '{from_day}') <= CONVERT(date, a.CreateDate) ";
                SQL += $"and CONVERT(date, '{to_day}') >= CONVERT(date, a.CreateDate) ";
                SQL += $"order by a.CreateDate desc ";
                var data = db.PurchaseOrders.SqlQuery(SQL).AsNoTracking().ToList();

                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdatePurchaseOrder(int id, string inputstatus)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var xx = db.PurchaseOrders.Where(f => f.ID == id).FirstOrDefault();
                        if (xx == null) throw new Exception("");
                        else
                            xx.InputStatus = inputstatus;

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
