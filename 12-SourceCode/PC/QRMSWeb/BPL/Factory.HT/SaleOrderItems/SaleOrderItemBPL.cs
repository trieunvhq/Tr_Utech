using System;
using System.Collections.Generic;
using BPL.Models;
using DAL.Factory.HT.SaleOrderItems;
using DAL.Factory.HT.SaleOrders;
using DAL.Model.HT;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.SaleOrderItems
{
    public class SaleOrderItemBPL : BaseBPL
    {
        public List<SaleOrderItemScanBPL> GetSaleOrderItemBPL(int id, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new SaleOrderItemDAL(db);
                var result = pr.GetSaleOrderItemDAL(id);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<SaleOrderItemScanBPL>();

                    foreach (SaleOrderItemScanDAL item in result)
                    {
                        var xx = new SaleOrderItemScanBPL();
                        xx.CopyPropertiesFrom(item);
                        ListOut.Add(xx);
                    }

                    return ListOut;
                }
                err_code = "5";
                err_msg = "Không có dữ liệu";
                return null;
            }
            catch (Exception ex)
            {
                err_code = ConstResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdateSaleOrderItemBPL(List<SaleOrderItemScanBPL> obj, out string err_code, out string err_msg)
        {
            try
            {
                bool isDoneY = true;
                string OutputStatus_ = ConstTransferStatus.NotDelivered;

                int id = obj[0].SaleOrderID;

                var ListOut = new List<SaleOrderItemScanDAL>();

                foreach (SaleOrderItemScanBPL item in obj)
                {
                    if (item.SoLuongDaNhap >= item.Quantity)
                    {
                        item.OutputStatus = ConstTransferStatus.Delivered;
                    }
                    else
                    {
                        item.OutputStatus = ConstTransferStatus.NotDelivered;
                    }

                    //
                    if (item.SoLuongDaNhap < item.Quantity)
                    {
                        isDoneY = false;
                    }

                    var xx = new SaleOrderItemScanDAL();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                //
                if (isDoneY)
                {
                    OutputStatus_ = ConstTransferStatus.Delivered;
                }

                var pr = new SaleOrderItemDAL(db);
                var tr = new SaleOrderDAL(db);
                var result = pr.UpdateSaleOrderItemDAL(ListOut);
                var result2 = tr.UpdateSaleOrderDAL(id, OutputStatus_);

                if (result == 1 && result2 == 1)
                {
                    err_code = "0";
                    err_msg = "Update dữ liệu thành công";
                }
                else
                {
                    err_code = "-1";
                    err_msg = "Không update được dữ liệu";
                }

                return result;
            }
            catch (Exception ex)
            {
                err_code = ConstResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return -1;
            }
        }

    }
}
