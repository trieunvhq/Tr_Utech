using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Model.HT;
using HDLIB.Common;
using Newtonsoft.Json;

namespace DAL.Factory.HT.PurchaseOrderitems
{
    public class PurchaseOrderitemDAL : IDisposable
    {
        QRMSEntities db;
        public PurchaseOrderitemDAL() { db = new QRMSEntities(); }
        public PurchaseOrderitemDAL(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        public string GetPurchaseOrderitem(int PurchaseOrderID_Input)
        {
            try
            {
                var settings = new JsonSerializerSettings().AddSqlConverters();
                DataTable dt;

                using (PrPurchaseOrderItem cl = new PrPurchaseOrderItem())
                {
                    dt = cl.GetPurchaseOrderItem_MHDC(PurchaseOrderID_Input);
                    if (dt == null)
                    {
                        return null;
                    }
                    else
                    {
                        string Result = JsonConvert.SerializeObject(dt, settings);
                        return Result;
                    }
                }
                //string SQL = $"select DISTINCT [ID], [PurchaseOrderID], [PurchaseOrderNo], [PurchaseOrderDate], [ItemCode] ";
                //SQL += $", [ItemName], [ItemType], [Quantity], [Unit], [InputStatus], [RecordStatus], ";
                //SQL += $"(case when(select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[ItemCode] = a.[ItemCode] ";
                //SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) is null then 0 ";
                //SQL += $"else (select sum(b.[Quantity]) from[dbo].[TransactionHistory] b where b.[ItemCode] = a.[ItemCode] ";
                //SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) end) SoLuongDaNhap, ";
                //SQL += $"(case when(select COUNT(*) from[dbo].[TransactionHistory] b where b.[ItemCode] = a.[ItemCode] ";
                //SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) is null then 0 ";
                //SQL += $"else (select COUNT(*) from[dbo].[TransactionHistory] b where b.[ItemCode] = a.[ItemCode] ";
                //SQL += $"and b.[ItemName] = a.[ItemName] and b.[ItemName] = a.[ItemName] and b.[ItemType] = a.[ItemType]) end) SoLuongBox ";
                //SQL += $"from PurchaseOrderItem a ";
                //SQL += $"where(a.RecordStatus is not null and a.RecordStatus != 'D') ";
                //SQL += $"and(a.InputStatus is not null and a.InputStatus != 'Y') ";
                //SQL += $"and a.[PurchaseOrderID] = {PurchaseOrderID_Input} ";
                //SQL += $"order by a.[PurchaseOrderDate] desc ";
                //////var s = from c in db.PurchaseOrderItems where c.PurchaseOrderID select ;
                ////string SQL = $"select * from PurchaseOrderItem a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }') ";
                ////SQL += $"and (a.InputStatus is not null and a.InputStatus != '{ InputStatus.Enough }') ";
                ////SQL += $"and a.PurchaseOrderID = '{PurchaseOrderID_Input}' ";
                ////SQL += $"order by a.CreateDate desc ";
                //var data = db.PurchaseOrderItems.SqlQuery(SQL).AsNoTracking().ToList<NhapKhoDungCuModel>();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }


        public int UpdatePurchaseOrderitem(List<PurchaseOrderItem> obj)
        {
            try
            {
                foreach (var item in obj)
                {
                    var dept = db.PurchaseOrderItems.Where(f => f.ID == item.ID).FirstOrDefault();
                    if (dept == null) throw new Exception("");
                    else
                        dept.InputStatus = item.InputStatus;
                }

                db.SaveChanges();

                return 1;
            }
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
