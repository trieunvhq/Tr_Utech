using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPL.Utils
{
    public class CommonUtils
    {
        public static List<SelectItem> GetListItemType()
        {
            List<SelectItem> selectItems = new List<SelectItem>();
            selectItems.Add(new SelectItem()
            {
                Code = "DC",
                Name = "Dụng cụ, Thiết bị"
            });
            selectItems.Add(new SelectItem()
            {
                Code = "NL",
                Name = "Nguyên liệu"
            });
            selectItems.Add(new SelectItem()
            {
                Code = "TP",
                Name = "Thành phẩm"
            });
            return selectItems;
        }
        public static string GetItemTypeName(string itemType)
        {
            var result = GetListItemType().Where(item => item.Code == itemType).FirstOrDefault();
            return result?.Name;
        }

        public static List<SelectItem> GetListPrintStatus()
        {
            List<SelectItem> selectItems = new List<SelectItem>();
            selectItems.Add(new SelectItem()
            {
                Code = "N",
                Name = "Chưa in"
            });
            selectItems.Add(new SelectItem()
            {
                Code = "D",
                Name = "Đang in"
            });
            selectItems.Add(new SelectItem()
            {
                Code = "Y",
                Name = "Đã in"
            });
            return selectItems;

        }
        public static string GetPrintStatusName(string printStatus)
        {
            var result = GetListPrintStatus().Where(item => item.Code == printStatus).FirstOrDefault();
            return result?.Name;
        }

        public static string BuildStringNA(string data)
        {
            return string.IsNullOrEmpty(data) ? "N/A" : data;
        }


    }



    public class SelectItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

}
