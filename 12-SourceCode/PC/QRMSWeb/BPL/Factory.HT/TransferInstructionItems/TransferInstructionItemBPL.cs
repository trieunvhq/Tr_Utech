using System;
using System.Collections.Generic;
using BPL.Models;
using DAL.Factory.HT.TransferInstructionItems;
using DAL.Factory.HT.TransferInstructions;
using DAL.Model.HT;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.TransferInstructionItems
{
    public class TransferInstructionItemBPL : BaseBPL
    {
        public List<XuatKhoDungCuBPLModel> GetTransferInstructionItem(int id, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new TransferInstructionItemDAL(db);
                var result = pr.GetTransferInstructionItem(id);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<XuatKhoDungCuBPLModel>();

                    foreach (XuatKhoDungCuModelDAL item in result)
                    {
                        var xx = new XuatKhoDungCuBPLModel();
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

        public int UpdateTransferInstructionItem(List<XuatKhoDungCuBPLModel> obj, out string err_code, out string err_msg)
        {
            try
            {
                bool isDoneY = true;
                string TransferStatus_ = TransferStatus.NotDelivered;

                int id = obj[0].TransferOrderID;

                var ListOut = new List<XuatKhoDungCuModelDAL>();

                foreach (XuatKhoDungCuBPLModel item in obj)
                {
                    if (item.SoLuongDaNhap >= item.Quantity)
                    {
                        item.TransferStatus = TransferStatus.Delivered;
                    }
                    else
                    {
                        item.TransferStatus = TransferStatus.NotDelivered;
                    }

                    //
                    if (item.SoLuongDaNhap < item.Quantity)
                    {
                        isDoneY = false;
                    }

                    var xx = new XuatKhoDungCuModelDAL();
                    xx.CopyPropertiesFrom(item);
                    ListOut.Add(xx);
                }

                //
                if (isDoneY)
                {
                    TransferStatus_ = TransferStatus.Delivered;
                }

                var pr = new TransferInstructionItemDAL(db);
                var tr = new TransferInstructionDAL(db);
                var result = pr.UpdateTransferInstructionItem(ListOut);
                var result2 = tr.UpdateTransferInstruction(id, TransferStatus_);

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
