using System;
using System.Collections.Generic;
using BPL.Models;
using DAL;
using DAL.Factory.HT.TransactionHistoris;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.TransactionHistoris
{
    public class TransactionHistoryBPL : BaseBPL
    {

        public int InsertTransactionHistory(List<TransactionHistoryBPLModel> obj, out string err_code, out string err_msg)
        {
            try
            {
                var ListOut = new List<TransactionHistory>();

                foreach (TransactionHistoryBPLModel item in obj)
                {
                    var xx = new TransactionHistory();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                var pr = new TransactionHistoryDAL(db);
                var result = pr.InsertTransactionHistory(ListOut);
                if (result == 1)
                {
                    err_code = "0";
                    err_msg = "Update dữ liệu thành công";
                }
                else
                {
                    err_code = "5";
                    err_msg = "Không update được dữ liệu";
                }

                return result;
            }
            catch (Exception ex)
            {
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return 0;
            }
        }

    }
}
