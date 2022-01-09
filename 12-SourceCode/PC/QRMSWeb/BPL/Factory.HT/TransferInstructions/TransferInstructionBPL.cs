using System;
using System.Collections.Generic;
using BPL.Models;
using DAL;
using DAL.Factory.HT.TransferInstructions;
using HDLIB;
using HDLIB.Common;

namespace BPL.Factory.HT.TransferInstructions
{
    public class TransferInstructionBPL : BaseBPL
    {
        public List<TransferInstructionBPLModel> GetTransferInstruction(DateTime from_day, DateTime to_day, string WarehouseCode, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new TransferInstructionDAL(db);
                var result = pr.GetTransferInstruction(from_day, to_day, WarehouseCode);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<TransferInstructionBPLModel>();

                    foreach (TransferInstruction item in result)
                    {
                        var xx = new TransferInstructionBPLModel();
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


        public List<TransferInstructionBPLModel> GetTransferWarehousesBPL(DateTime from_day, DateTime to_day, string WarehouseCode_From, string WarehouseCode_To, out string err_code, out string err_msg)
        {
            try
            {
                var pr = new TransferInstructionDAL(db);
                var result = pr.GetTransferWarehousesDAL(from_day, to_day, WarehouseCode_From, WarehouseCode_To);
                if (result != null)
                {
                    err_code = "0";
                    err_msg = "Lấy dữ liệu thành công";

                    var ListOut = new List<TransferInstructionBPLModel>();

                    foreach (TransferInstruction item in result)
                    {
                        var xx = new TransferInstructionBPLModel();
                        xx.CopyPropertiesFrom(item);
                        ListOut.Add(xx);
                    }

                    return ListOut;
                }
                err_code = "-1";
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

    }
}
