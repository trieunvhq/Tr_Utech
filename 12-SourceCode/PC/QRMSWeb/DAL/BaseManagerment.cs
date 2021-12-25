using System;

namespace DAL
{
    public class BaseManagement : IDisposable
    {
        protected QRMSEntities db;
        private bool isNewContext;
        private bool disposedValue;

        public BaseManagement() { db = new QRMSEntities(); isNewContext = true; }
        public BaseManagement(QRMSEntities db) { this.db = db ?? GlobalVariable.db; isNewContext = false; }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                if (isNewContext)
                {
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseManagerment()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
