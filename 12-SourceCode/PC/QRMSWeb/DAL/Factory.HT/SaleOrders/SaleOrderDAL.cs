using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.HT.SaleOrders
{
    public class SaleOrderDAL : IDisposable
    {
        QRMSEntities db;
        public SaleOrderDAL() { db = new QRMSEntities(); }
        public SaleOrderDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }


        public List<SaleOrder> GetSaleOrderDAL(DateTime from_day, DateTime to_day, string WarehouseCode)
        {
            try
            {
                //var s = from c in db.SaleOrders where c.ExportStatus
                string SQL = $"select * from SaleOrder a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }') ";
                SQL += $"and (a.ExportStatus is not null and a.ExportStatus != '{ ConstInputStatus.Enough }') ";
                SQL += $"and (a.WarehouseCode is not null and a.WarehouseCode = '{ WarehouseCode }') ";
                SQL += $"and CONVERT(date, '{from_day}') <= CONVERT(date, a.SaleOrderDate) ";
                SQL += $"and CONVERT(date, '{to_day}') >= CONVERT(date, a.SaleOrderDate) ";
                SQL += $"order by a.SaleOrderDate desc ";
                var data = db.SaleOrders.SqlQuery(SQL).AsNoTracking().ToList();

                return data;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdateSaleOrderDAL(int id, string inputstatus)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var xx = db.SaleOrders.Where(f => f.ID == id).FirstOrDefault();
                        if (xx == null) throw new Exception("");
                        else
                            xx.ExportStatus = inputstatus;

                        db.DetachAll<SaleOrder>();
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
