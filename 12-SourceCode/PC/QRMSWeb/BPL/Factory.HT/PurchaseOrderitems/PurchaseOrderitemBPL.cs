using System;
using System.Collections.Generic;
using BPL.Models;
using DAL;
using DAL.Factory.HT.PurchaseOrderitems;
using DAL.Factory.HT.PurchaseOrders;
using DAL.Model.HT;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.PurchaseOrderitems
{
    public class PurchaseOrderitemBPL : BaseBPL
    {
        public List<NhapKhoDungCuBPLModel> GetPurchaseOrderitems(int id, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new PurchaseOrderitemDAL(db);
                var result = pr.GetPurchaseOrderitem(id);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<NhapKhoDungCuBPLModel>();

                    foreach (NhapKhoDungCuModel item in result)
                    {
                        var xx = new NhapKhoDungCuBPLModel();
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
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdatePurchaseOrderitem(List<NhapKhoDungCuBPLModel> obj, out string err_code, out string err_msg)
        {
            try
            {
                bool isDoneD = false;
                bool isDoneY = true;
                string InputStatus_ = InputStatus.NotYetEntered;

                int id = obj[0].PurchaseOrderID;

                var ListOut = new List<NhapKhoDungCuModel>();

                foreach (NhapKhoDungCuBPLModel item in obj)
                {
                    if (item.SoLuongDaNhap >= item.Quantity)
                    {
                        item.InputStatus = InputStatus.Enough;
                    }
                    else if (item.SoLuongDaNhap > 0)
                    {
                        item.InputStatus = InputStatus.NotEnough;
                    }
                    else
                    {
                        item.InputStatus = InputStatus.NotYetEntered;
                    }

                    //
                    if (item.SoLuongDaNhap < item.Quantity)
                    {
                        isDoneY = false;
                    }
                    else if (item.SoLuongDaNhap > 0)
                    {
                        isDoneD = true;
                    }

                    var xx = new NhapKhoDungCuModel();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                //
                if (isDoneY)
                {
                    InputStatus_ = InputStatus.Enough;
                }
                else if (isDoneD)
                {
                    InputStatus_ = InputStatus.NotEnough;
                }

                var pr = new PurchaseOrderitemDAL(db);
                var tr = new PurchaseOrderDAL(db);
                var result = pr.UpdatePurchaseOrderitem(ListOut);
                var result2 = tr.UpdatePurchaseOrder(id, InputStatus_);

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
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return -1;
            }
        }

    }
}
