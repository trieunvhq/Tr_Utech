using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.Web
{
    public class BaseManagement //:  IDisposable
    {
        public QRMSEntities db;

        public BaseManagement()
        {
            db = new QRMSEntities();
        }

        public BaseManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
    }

}
