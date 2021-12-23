using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB
{

    public class Constants
    {
        public enum CRUD_Error
        {
            Fail = -1,//thất bại
            Order_Quan_Exceed = -2,//vượt quá số lượng trong chỉ thị
            Not_Exist = -3,//không tôn tại
            Order_Not_Exist = -5,//không tôn tại chỉ thị
            Label_Not_Exist = -6,//không tôn tại nhãn
            Already_Exist = -4,//đã được thêm vào
        }

        public class Item_CType
        {
            public static string Material { get { return "M"; } }
            public static string Product { get { return "P"; } }
            public static string Semi { get { return "S"; } }
            public static string Packing { get { return "K"; } }
        }

        public static IDictionary<string, string> ItemCType
        {
            get
            {
                return new Dictionary<string, string>()
                    {
                        { "Material" ,"M" },
                        { "Product" ,"P" },
                        { "Semi" ,"S" },
                        { "Packing" ,"K" },
                    };
            }
        }

        public static IDictionary<string, string> ItemPackingType
        {
            get
            {
                return new Dictionary<string, string>()
                    {
                        { "Cũ", ItemMaster_PackingType.Old },
                        { "Mới", ItemMaster_PackingType.New },
                    };
            }
        }

        public class Warehouse_CType
        {
            public static string Material { get { return "M"; } }
            public static string MaterialNG { get { return "MN"; } }
            public static string Product { get { return "P"; } }
            public static string Process { get { return "I"; } }
            public static string ProductNG { get { return "IN"; } }
        }
        public class DIItem_Type
        {
            public static string InInstruction { get { return "I"; } }
            public static string OutInstruction { get { return "O"; } }
        }

        public class Transaction_CType
        {
            public static string Input { get { return "I"; } }
            public static string Output { get { return "O"; } }
            public static string Change { get { return "C"; } }
            public static string OQC { get { return "Q"; } }
            public static string CheckInventory { get { return "Y"; } }
			public static string IPQC { get { return "A"; } }
        }

        public class Status_Type
        {
            public static string Done { get { return "Y"; } }
            public static string Processing { get { return "D"; } }
            public static string Wait { get { return "N"; } }
        }

        public static IDictionary<string, string> Status = new Dictionary<string, string>()
        {
            { "Done" ,"Y" },
            { "Processing" ,"D" },
            { "Wait" ,"N" },
        };

        public class LangCodeConstant
        {
            public const string Vietnamese = "vi";
            public const string English = "en";
            public static List<string> allLangCode
            {
                get
                {
                    return new List<string>()
                {
                    English,
                    Vietnamese
                };
                }
            }
        }
        public static IDictionary<string, string> TransferCType
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "Output Material" ,"OM" },    //NL -> SX
                    { "Input Product" ,"IP" },      //SX -> TP
                    { "QA Return" ,"QA" },          //SX -> NG
                    { "Customer Return" ,"CR" },    //KH trả về
                    { "Supplier Return" ,"SR" },    //trả về NCC
                    { "Material Return" ,"MR" },    //SX -> NL
                    { "Product Return" ,"PR" },     //TP -> SX
                    { "NG Return" ,"NR" },          //NG -> SX

                };
            }
        }
        public class Transfer_Ctype
        {
            public static string OutputMaterial { get { return TransferCType["Output Material"].ToString(); } }
            public static string InputProduct { get { return TransferCType["Input Product"].ToString(); } }
            public static string QAReturn { get { return TransferCType["QA Return"].ToString(); } }
            public static string CustomerReturn { get { return TransferCType["Customer Return"].ToString(); } }
            public static string SupplierReturn { get { return TransferCType["Supplier Return"].ToString(); } }
            public static string MaterialReturn { get { return TransferCType["Material Return"].ToString(); } }
            public static string ProductReturn { get { return TransferCType["Product Return"].ToString(); } }
            public static string NGReturn { get { return TransferCType["NG Return"].ToString(); } }
        }
        public static IDictionary<Tuple<string, string>, string> WHTypeTransfer
        {
            get
            {
                return new Dictionary<Tuple<string, string>, string>()
                {
                    { new Tuple<string, string>(Warehouse_CType.Material,Warehouse_CType.Process),Transfer_Ctype.OutputMaterial },
                    { new Tuple<string, string>(Warehouse_CType.Process,Warehouse_CType.Product),Transfer_Ctype.InputProduct },
                    { new Tuple<string, string>(Warehouse_CType.Process,Warehouse_CType.ProductNG),Transfer_Ctype.QAReturn },
                    { new Tuple<string, string>("",Warehouse_CType.Product),Transfer_Ctype.CustomerReturn },
                    { new Tuple<string, string>(Warehouse_CType.Material,""),Transfer_Ctype.SupplierReturn },
                    { new Tuple<string, string>(Warehouse_CType.Process,Warehouse_CType.Material),Transfer_Ctype.MaterialReturn },
                    { new Tuple<string, string>(Warehouse_CType.Product,Warehouse_CType.Process),Transfer_Ctype.ProductReturn },
                    { new Tuple<string, string>(Warehouse_CType.ProductNG,Warehouse_CType.Process),Transfer_Ctype.NGReturn }
                };
            }
        }
        public static IDictionary<string, string> ItemTypeTransfer
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { Transfer_Ctype.OutputMaterial, Item_CType.Material },
                    { Transfer_Ctype.InputProduct, Item_CType.Product },
                    { Transfer_Ctype.QAReturn, Item_CType.Product },
                    { Transfer_Ctype.CustomerReturn, Item_CType.Product },
                    { Transfer_Ctype.SupplierReturn, Item_CType.Material },
                    { Transfer_Ctype.MaterialReturn, Item_CType.Material },
                    { Transfer_Ctype.ProductReturn, Item_CType.Product },
                    { Transfer_Ctype.NGReturn, Item_CType.Product },
                };
            }
        }

        public class MergeSplit_Ctype
        {
            public const int Merge_Parent = 0;
            public const int Merge_Child = 1;
            public const int Split_Parent = 2;
            public const int Split_Child = 3;
        }

		public class ConfigPaging
		{
			public const int page = 1;
			public const int rowPerPage = 30;
			public const int numberPageView = 7;
		}

		public class PackingSize
		{
			public const string Size4x3 = "4x3";
		}

        public class ItemMaster_PackingType
        {
            public const string Old = "O";
            public const string New = "N";
        }

    }
}
