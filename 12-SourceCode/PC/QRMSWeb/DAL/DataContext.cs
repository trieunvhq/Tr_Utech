using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataContext
    {
        private static volatile DAL.QRMSEntities instance;
        public static DAL.QRMSEntities getEntities()
        {
            if (instance==null)
            {
                instance = new QRMSEntities();
            }
            return instance;
        }
        public static void DetachAll<T>() where T : class
        {
            var entityEntries =  getEntities().ChangeTracker.Entries<T>().ToArray();
            foreach (DbEntityEntry entityEntry in entityEntries)
            {
                entityEntry.State = EntityState.Detached;
            }
        }

        private DataContext()
        {
        }

        public static DbContextTransaction GetTransaction(DbContext db)
        {
            var currTrans = db.Database.CurrentTransaction;
            return currTrans ?? db.Database.BeginTransaction();
        }

        private class Nested
        {
            internal static readonly DAL.QRMSEntities instance = new DAL.QRMSEntities();
            static Nested()
            {
            }
        }
    }

    public static class DBContextExtensions
    {
        public static void DetachAll<T>(this DbContext db) where T : class
        {
            var entityEntries = db.ChangeTracker.Entries<T>().ToArray();
            foreach (DbEntityEntry entityEntry in entityEntries)
            {
                entityEntry.State = EntityState.Detached;
            }
            GC.Collect();
        }
        public static void Detach<T>(this DbContext db,int ID) where T : class
        {
            var entityEntries = db.ChangeTracker.Entries<T>().ToArray();
            foreach (DbEntityEntry entityEntry in entityEntries)
            {
                if (entityEntry.Property("ID").CurrentValue.ToString() == ID.ToString())
                {
                    entityEntry.State = EntityState.Detached;
                    break;
                }
            }
            GC.Collect();
        }
        public static void DetachAll(this DbContext db)
        {
            var entityEntries = db.ChangeTracker.Entries().ToArray();
            foreach (DbEntityEntry entityEntry in entityEntries)
            {
                entityEntry.State = EntityState.Detached;
            }
            GC.Collect();
        }
        public static void DisposeDB(this DbContext db)
        {
            if (db !=null) db.Dispose();            
        }
        public static void CommitData(this DbContext db)
        {
            var transac = DataContext.GetTransaction(db);
            transac.Commit();
        }
        public static void RollbackData(this DbContext db)
        {
            var transac = DataContext.GetTransaction(db);
            transac.Rollback();
        }
        public static void DisposeTransac(this DbContext db)
        {
            var transac = db.Database.CurrentTransaction;
            transac?.Dispose();
        }
    }
}
