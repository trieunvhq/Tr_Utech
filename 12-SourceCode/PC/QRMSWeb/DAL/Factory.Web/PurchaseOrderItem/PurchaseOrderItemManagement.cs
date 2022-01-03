using HDLIB.Common;
using DAL;
using System;
using System.Linq;

namespace DAL.Factory.Web.PurchaseOrderItems
{
    public class PurchaseOrderItemManagement : IDisposable
    {
        QRMSEntities db;

        public PurchaseOrderItemManagement()
        {
            db = new QRMSEntities();
        }

        public PurchaseOrderItemManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }

        public HDLIB.WebPaging.TPaging<PurchaseOrderItem> GetAllPurchaseOrderItem(int page, int limit,
            string itemName, string itemCode, string locationName, string PurchaseOrderNo)
        {
            try
            {
                itemName = itemName?.Trim();
                itemCode = itemCode?.Trim();
                locationName = locationName?.Trim();
                PurchaseOrderNo = PurchaseOrderNo?.Trim();

                HDLIB.WebPaging.TPaging<PurchaseOrderItem> paging = new HDLIB.WebPaging.TPaging<PurchaseOrderItem>();
                int offset = (page - 1) * limit;

                string SQL = $"select * from [PurchaseOrderItem] a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";
                SQL += (string.IsNullOrEmpty(itemName)) ? "" : $" and LOWER(a.ItemName) LIKE '%{itemName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(itemCode)) ? "" : $" and LOWER(a.ItemCode) LIKE '%{itemCode.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(locationName)) ? "" : $" and LOWER(a.LocationName) LIKE '%{locationName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(PurchaseOrderNo)) ? "" : $" and LOWER(a.PurchaseOrderNo) LIKE '%{PurchaseOrderNo.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";


                //SQL += isAssetPercentFee ? " and a.ASSET_PERCENT_FEE is not null" : " and a.ASSET_PERCENT_FEE is null";
                // SQL += " order by a.common_code, a.common_name asc, a.ACTIVE_DATE_F desc, a.ACTIVE_DATE_T desc";
                SQL += " order by a.id desc";
                var exec_sql = db.PurchaseOrderItems.SqlQuery(SQL);
                var data = exec_sql.AsNoTracking().Skip(offset).Take(limit).ToList();
                int total = exec_sql.Count();
                paging.CalculatePaging(data, page, limit, total);

                return paging;

            }
            catch (Exception e)
            {
                Logging.LogError(e);
                throw;
            }
        }

        public DAL.PurchaseOrderItem Select(int ID)
        {
            try
            {
                return db.PurchaseOrderItems.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
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