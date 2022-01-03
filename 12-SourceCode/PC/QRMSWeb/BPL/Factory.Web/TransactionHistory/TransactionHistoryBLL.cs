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

namespace BLL.Factory.Web.PurchaseOrder
{
    public class TransactionHistoryBLL : BaseBLL
    {

        public HDLIB.WebPaging.TPaging<TransactionHistoryModel> FindAll(int page, int limit,
            string itemType, string orderNo, DateTime? orderDate, bool isSearch)
        {
            try
            {
                HDLIB.WebPaging.TPaging<TransactionHistoryModel> pagging = new HDLIB.WebPaging.TPaging<TransactionHistoryModel>();

                var result = new TransactionHistoryManagement(db).FindAll(page, limit, itemType, orderNo, orderDate, isSearch);
                List<TransactionHistoryModel> transactionHistoryModels = new List<TransactionHistoryModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var transactionHistoryModel = new TransactionHistoryModel();
                    transactionHistoryModel.CopyPropertiesFrom(row);
                    transactionHistoryModels.Add(transactionHistoryModel);
                }
                pagging.rows = transactionHistoryModels;
                return pagging;
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

                data.RecordStatus = RecordStatus.New;
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
