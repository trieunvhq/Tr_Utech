using DAL;

namespace BPL.Factory.HT
{
    public class BaseBPL
    {
        public QRMSEntities db;
        public BaseBPL() { db = new QRMSEntities(); }
        public BaseBPL(QRMSEntities db) { db = db ?? new QRMSEntities(); }
    }
}
