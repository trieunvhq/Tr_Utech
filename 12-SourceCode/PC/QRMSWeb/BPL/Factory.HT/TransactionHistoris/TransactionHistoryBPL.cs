using System;
using DAL;

namespace BPL.Factory.HT.TransactionHistoris
{
    public class TransactionHistoryBPL
    {
        QRMSEntities db;
        public TransactionHistoryBPL() { db = new QRMSEntities(); }
        public TransactionHistoryBPL(QRMSEntities db) { this.db = db ?? new QRMSEntities(); }

        
    }
}
