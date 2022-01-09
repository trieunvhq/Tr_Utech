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

namespace BLL.Factory.Web.Item
{
    public class ItemBLL : BaseBLL
    {

        public List<ItemModel> FindAllWithoutPagging()
        {
            try
            {
                
                var result = new ItemAPIManagement().GetItemInfo<List<ItemModel>>(0, 1, 10000);
                
                if (result.ResultCode != 0)
                {

                    return new List<ItemModel>();
                }
                return result.Data.PageData;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
    }


}
