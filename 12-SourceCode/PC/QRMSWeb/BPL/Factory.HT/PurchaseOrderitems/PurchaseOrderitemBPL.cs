﻿using System;
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
                var ListOut = new List<NhapKhoDungCuModel>();

                foreach (NhapKhoDungCuBPLModel item in obj)
                {
                    var xx = new NhapKhoDungCuModel();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                var pr = new PurchaseOrderitemDAL(db);
                var tr = new PurchaseOrderDAL(db);
                var result = pr.UpdatePurchaseOrderitem(ListOut);
                var result2 = tr.UpdatePurchaseOrder(ListOut);

                if (result == 1 && result2 == 1)
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
