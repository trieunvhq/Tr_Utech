using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Factory.Web.TransferInstruction
{
    public class TransferInstructionManagement : BaseManagement
    {
        public TransferInstructionManagement()
        {
            db = new QRMSEntities();
        }

        public TransferInstructionManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }
        


        public DAL.SaleOrder Select(int ID)
        {
            try
            {
                return db.SaleOrders.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        
        

        public DAL.TransferInstruction GetTransferInstructionByTranferNo(string transferOrderNo ,string transferType)
        {
            try
            {
                transferOrderNo = (transferOrderNo?.Trim()) ?? "";
                return db.TransferInstructions.Where(item => transferOrderNo.Equals(item.TransferOrderNo) && transferType.Equals(item.TransferType)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int Insert(params DAL.TransferInstruction[] _VALUEs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.TransferInstructions.AddRange(_VALUEs);
                if (db.SaveChanges() > 0)
                {
                    return _VALUEs[0].ID;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
    }
}