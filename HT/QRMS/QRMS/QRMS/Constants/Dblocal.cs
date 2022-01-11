using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMS.Models;
using QRMS.Models.KKDC;
using QRMS.Models.XKDC;
using SQLite;

namespace QRMS
{
    public class Dblocal
    {
        private readonly SQLiteConnection _database;

        public Dblocal(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<TransactionHistoryModel>();
            _database.CreateTable<NhapKhoDungCuModel>();
            _database.CreateTable<SaleOrderItemScanBPL>(); //Xuất kho DC_new
            _database.CreateTable<XuatKhoDungCuModel>();
            //_database.CreateTableAsync<XuatKhoDungCuModel>().Wait();
        }

        public List<TransactionHistoryModel> GetHistoryAsync()
        {
            return _database.Table<TransactionHistoryModel>().ToList();
        }

        public List<TransactionHistoryModel> GetHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Select * From TransactionHistoryModel Where OrderNo = '{purchaseno}'";

            return _database.Query<TransactionHistoryModel>(Sql);
        }

        public int SaveHistoryAsync(TransactionHistoryModel history)
        {
            //if (history.ID != 0)
            //{
            //    return _database.UpdateAsync(history);
            //}
            //else
            //{
            //    return _database.InsertAsync(history);
            //}
            return _database.Insert(history);
        }

        public int DeleteHistoryAsync(TransactionHistoryModel history)
        {
            return _database.Delete(history);
        }

        public List<TransactionHistoryModel> SelectOneHistoryAsync(int id)
        {
            string Sql = $"Select * From TransactionHistoryModel Where ID = {id}";

            var data = _database.Query<TransactionHistoryModel>(Sql);

            return data;
        }

        public void DeleteHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Delete From TransactionHistoryModel Where OrderNo = '{purchaseno}'";

            _ = _database.Execute(Sql);
        }

        public void DeleteHistoryAsyncWithID(int id)
        {
            string Sql = $"Delete From TransactionHistoryModel Where ID = {id}";

            _ = _database.Execute(Sql);
        }

        public void DeleteHistoryAll()
        {
            _database.DeleteAll<TransactionHistoryModel>();
        }


        //For table NhapKhoDungCuModel:
        public List<NhapKhoDungCuModel> GetPurchaseOrderAsyncWithKey(string purchaseno)
        {
            try
            {
                string Sql = $"Select * From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchaseno}'";
                var result = _database.Query<NhapKhoDungCuModel>(Sql);
                return result;
            }
            catch (Exception ea)
            {
                return null;
            }
        }

        public int SavePurchaseOrderAsync(NhapKhoDungCuModel purchase)
        {
            string Sql = $"Select * From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchase.PurchaseOrderNo}' ";
                   Sql += $"and ID = {purchase.ID} and PurchaseOrderID = {purchase.PurchaseOrderID} ";
                   Sql += $"and ItemCode = '{purchase.ItemCode}' and ItemName = '{purchase.ItemName}' ";
                   Sql += $"and ItemType = '{purchase.ItemType}' and Unit = '{purchase.Unit}' ";
                   Sql += $"and InputStatus = '{purchase.InputStatus}' and RecordStatus = '{purchase.RecordStatus}' ";

            var res = _database.Query<NhapKhoDungCuModel>(Sql);

            if (res.Count == 0)
                return _database.Insert(purchase);

            else return -1;
        }

        public int UpdatePurchaseOrderAsync(NhapKhoDungCuModel purchase)
        {
            string Sql = $"Update NhapKhoDungCuModel set ";
                   Sql += $"SoLuongDaNhap = {purchase.SoLuongDaNhap}, ";
                   Sql += $"SoLuongBox = {purchase.SoLuongBox} ";
                   Sql += $"Where ID = {purchase.ID}";

            return _database.Execute(Sql);
        }


        public void DeletePurchaseOrderAsyncWithKey(string purchaseno)
        {
            string Sql = $"Delete From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchaseno}'";

            _ = _database.Execute(Sql);
        }


        //For table Xuất kho dụng cụ :
        public List<SaleOrderItemScanBPL> GetSaleOrderItemScanAsyncWithKey(string no)
        {
            string Sql = $"Select * From SaleOrderItemScanBPL Where SaleOrderNo = '{no}'";

            return _database.Query<SaleOrderItemScanBPL>(Sql);
        }

        public int SaveSaleOrderItemScanAsync(SaleOrderItemScanBPL no)
        {
            string Sql = $"Select * From SaleOrderItemScanBPL Where SaleOrderNo = '{no.SaleOrderNo}' ";
            Sql += $"and ID = { no.ID} and SaleOrderID = '{no.SaleOrderID}' ";
            Sql += $"and ItemCode = '{no.ItemCode}' and ItemName = '{no.ItemName}' ";
            Sql += $"and ItemType = '{no.ItemType}' and Unit = '{no.Unit}' ";
            Sql += $"and RecordStatus = '{no.RecordStatus}' ";

            var res = _database.Query<SaleOrderItemScanBPL>(Sql);

            if (res.Count == 0)
                return _database.Insert(no);
            else return -1;
        }

        public int UpdateSaleOrderItemScanAsync(SaleOrderItemScanBPL no)
        {
            string Sql = $"Update SaleOrderItemScanBPL set ";
            Sql += $"SoLuongDaNhap = {no.SoLuongDaNhap}, ";
            Sql += $"SoLuongBox = {no.SoLuongBox} ";
            Sql += $"Where ID = {no.ID}";

            return _database.Execute(Sql);
        }


        public void DeleteSaleOrderItemScanBPLAsyncWithKey(string no)
        {
            string Sql = $"Delete From SaleOrderItemScanBPL Where SaleOrderNo = '{no}'";

            _ = _database.Execute(Sql);
        }



        //For table XuatKhoDungCuModel:
        public List<XuatKhoDungCuModel> GetTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Select * From XuatKhoDungCuModel Where TransferOrderNo = '{no}'";

            return _database.Query<XuatKhoDungCuModel>(Sql);
        }

        public int SaveTransferInstructionAsync(XuatKhoDungCuModel no)
        {
            string Sql = $"Select * From XuatKhoDungCuModel Where TransferOrderNo = '{no.TransferOrderNo}' ";
            Sql += $"and ID = { no.ID} and TransferOrderID = '{no.TransferOrderID}' ";
            Sql += $"and ItemCode = '{no.ItemCode}' and ItemName = '{no.ItemName}' ";
            Sql += $"and ItemType = '{no.ItemType}' and Unit = '{no.Unit}' ";
            Sql += $"and TransferType = '{no.TransferType}' and RecordStatus = '{no.RecordStatus}' ";

            var res = _database.Query<NhapKhoDungCuModel>(Sql);

            if (res.Count == 0)
                return _database.Insert(no);
            else return -1;
        }

        public int UpdateTransferInstructionAsync(XuatKhoDungCuModel no)
        {
            string Sql = $"Update XuatKhoDungCuModel set ";
            Sql += $"SoLuongDaNhap = {no.SoLuongDaNhap}, ";
            Sql += $"SoLuongBox = {no.SoLuongBox} ";
            Sql += $"Where ID = {no.ID}";

            return _database.Execute(Sql);
        }


        public void DeleteTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Delete From XuatKhoDungCuModel Where TransferOrderNo = '{no}'";

            _ = _database.Execute(Sql);
        }


        //Kiểm kê dụng cụ:
        public List<KKDCModel> GetTransactionHistory_KKDC(string OrderNo, string WarehouseCode_From)
        {
            string Sql = $"Select DISTRINCT TransactionType, OrderNo, WarehouseCode_From, WarehouseName_From, WarehouseType_From, ";
                   Sql += $"WarehouseCode_To, WarehouseName_To, WarehouseType_To, ItemCode, ItemName, ItemType, Unit ";
                   Sql += $"From TransactionHistoryModel a ";
                   Sql += $"(case when(select sum(b.[Quantity]) from TransactionHistoryModel b ";
                   Sql += $"where b.[OrderNo] = a.[OrderNo] and b.[ItemCode] = a.[ItemCode] ";
                   Sql += $"and b.[ItemName] = a.[ItemName] ";
                   Sql += $"and b.[ItemType] = a.[ItemType] and b.WarehouseCode_From = a.WarehouseCode_From and b.TransactionType = a.TransactionType) is null then 0 ";
                   Sql += $"else (select sum(b.[Quantity]) from TransactionHistoryModel b where b.[OrderNo] = a.[OrderNo] and b.[ItemCode] = a.[ItemCode] ";
                   Sql += $"and b.[ItemName] = a.[ItemName] ";
                   Sql += $"and b.[ItemType] = a.[ItemType] and b.WarehouseCode_From = a.WarehouseCode_From and b.TransactionType = a.TransactionType) end) SoLuongKiemKe, ";

                   Sql += $"(case when(select COUNT(*) from TransactionHistoryModel b ";
                   Sql += $"where b.[OrderNo] = a.[OrderNo] and b.[ItemCode] = a.[ItemCode] ";
                   Sql += $"and b.[ItemName] = a.[ItemName] ";
                   Sql += $"and b.[ItemType] = a.[ItemType] and b.WarehouseCode_From = a.WarehouseCode_From and b.TransactionType = a.TransactionType) is null then 0 ";
                   Sql += $"else (select COUNT(*) from TransactionHistoryModel b where b.[OrderNo] = a.[OrderNo] and b.[ItemCode] = a.[ItemCode] ";
                   Sql += $"and b.[ItemName] = a.[ItemName] ";
                   Sql += $"and b.[ItemType] = a.[ItemType] and b.WarehouseCode_From = a.WarehouseCode_From and b.TransactionType = a.TransactionType) end) SoNhan ";

                   Sql += $"Where a.OrderNo = '{OrderNo}' and a.TransactionType = 'K' ";
                   Sql += $"and a.WarehouseCode_From = '{WarehouseCode_From}' and a.TransactionType = 'K' ";


            var data = _database.Query<KKDCModel>(Sql);

            return data;
        }
    }
}
