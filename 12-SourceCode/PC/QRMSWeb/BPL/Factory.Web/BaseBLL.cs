using DAL;

namespace BPL.Factory.Web
{
    public class BaseBLL
    {
        public QRMSEntities db;
        public BaseBLL() { db = new QRMSEntities(); }
        public BaseBLL(QRMSEntities db) { db = db ?? new QRMSEntities(); }

    }
}
