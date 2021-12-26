using System;
using System.Collections.Generic;
using BPL.Models.WarehouseHT;
using HDLIB.Common;

namespace BPL.Amis.HT
{
    public class WarehoseGetBPL
    {
        public WarehoseGetBPL() { }

        public List<WarehouseBPLModel> GetListWarehoses(out string err_code, out string err_msg)
        {
            try
            {
                List <WarehouseBPLModel> lst = new List<WarehouseBPLModel>();

                for (int i = 1; i <= 100; i++)
                {
                    lst.Add(new WarehouseBPLModel()
                    {
                        ID = i.ToString(),
                        Name = "Kho test số " + i.ToString()
                    });
                }

                err_code = "0";
                err_msg = "Lấy danh sách kho từ Amis thành công!";
                return lst;
            }
            catch (Exception ex)
            {
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
                throw;
            }
        }
    }
}
