using BPL.Factory.Web;
using BPL.Models.Web;
using DAL.Factory.Web.ImportEquipment;
using DAL.Factory.Web.TransactionHistory;
using HDLIB;
using HDLIB.Common;
using HDLIB.Helper;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL.Factory.Web.ImportEquipment
{
    public class ImportEquipmentBLL : BaseBLL
    {
        public HDLIB.WebPaging.TPaging<TransactionHistoryModel> FindAllImportEquipment(int page, int limit,
            string itemCode, string orderNo, string wareHouseCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HDLIB.WebPaging.TPaging<TransactionHistoryModel> pagging = new HDLIB.WebPaging.TPaging<TransactionHistoryModel>();

                var result = new ImportEquipmentManagement(db).FindAllImportEquipment(page, limit, itemCode, orderNo, wareHouseCode, startDate, endDate);
                List<TransactionHistoryModel> _importEquipmentModels = new List<TransactionHistoryModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var saleOrderModel = new TransactionHistoryModel();
                    saleOrderModel.CopyPropertiesFrom(row);
                    _importEquipmentModels.Add(saleOrderModel);
                }
                pagging.rows = _importEquipmentModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
    }
}
