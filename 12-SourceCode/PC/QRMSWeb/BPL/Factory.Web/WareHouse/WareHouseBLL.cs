using AMIS.APIManagement;
using BPL.Factory.Web;
using BPL.Models.Web;
using BPL.Models.Web.Report;
using DAL;
using DAL.Factory.Web.PurchaseOrder;
using HDLIB;
using HDLIB.Common;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DAL.Factory.Web.TransactionHistory;

namespace BLL.Factory.Web.WareHouse
{
    public class WareHouseBLL : BaseBLL
    {

        public List<WareHouseModel> FindAllWithoutPagging()
        {
            try
            {
                
                var result = new WareHouseAPIManagement().GetWareHouseInfo<List<WareHouseModel>>(0, 1, 10000);
                
                if (result.ResultCode != 0)
                {

                    return new List<WareHouseModel>();
                }
                return result.Data.PageData;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
        
        public int Insert(DAL.TransactionHistory data, string userName)
        {
            try
            {
                //data.ID = 0;

                data.RecordStatus = ConstRecordStatus.New;
                if (data.CreateDate == null)
                {
                    data.CreateDate = DateTime.Now;
                }
                data.UserCreate = userName;

                return new TransactionHistoryManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

    }


}
