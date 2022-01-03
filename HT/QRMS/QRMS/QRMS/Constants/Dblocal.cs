using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRMS.Models;
using SQLite;

namespace QRMS
{
    public class Dblocal
    {
        private readonly SQLiteAsyncConnection _database;

        public Dblocal(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TransactionHistoryModel>().Wait();
            _database.CreateTableAsync<NhapKhoDungCuModel>().Wait();
            _database.CreateTableAsync<XuatKhoDungCuModel>().Wait();
        }

        public Task<List<TransactionHistoryModel>> GetHistoryAsync()
        {
            return _database.Table<TransactionHistoryModel>().ToListAsync();
        }

        public Task<List<TransactionHistoryModel>> GetHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Select * From TransactionHistoryModel Where OrderNo = '{purchaseno}'";

            return _database.QueryAsync<TransactionHistoryModel>(Sql);
        }

        public Task<int> SaveHistoryAsync(TransactionHistoryModel history)
        {
            //if (history.ID != 0)
            //{
            //    return _database.UpdateAsync(history);
            //}
            //else
            //{
            //    return _database.InsertAsync(history);
            //}
            return _database.InsertAsync(history);
        }

        public Task<int> DeleteHistoryAsync(TransactionHistoryModel history)
        {
            return _database.DeleteAsync(history);
        }

        public Task<List<TransactionHistoryModel>> SelectOneHistoryAsync(int id)
        {
            string Sql = $"Select * From TransactionHistoryModel Where ID = {id}";

            var data = _database.QueryAsync<TransactionHistoryModel>(Sql);

            return data;
        }

        public void DeleteHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Delete From TransactionHistoryModel Where OrderNo = '{purchaseno}'";

            _ = _database.ExecuteAsync(Sql);
        }

        public void DeleteHistoryAsyncWithID(int id)
        {
            string Sql = $"Delete From TransactionHistoryModel Where ID = {id}";

            _ = _database.ExecuteAsync(Sql);
        }

        public void DeleteHistoryAll()
        {
            _database.DeleteAllAsync<TransactionHistoryModel>();
        }


        //For table NhapKhoDungCuModel:
        public Task<List<NhapKhoDungCuModel>> GetPurchaseOrderAsyncWithKey(string purchaseno)
        {
            string Sql = $"Select * From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchaseno}'";

            return _database.QueryAsync<NhapKhoDungCuModel>(Sql);
        }

        public Task<int> SavePurchaseOrderAsync(NhapKhoDungCuModel purchase)
        {
            string Sql = $"Select * From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchase.PurchaseOrderNo}' ";
                   Sql += $"and ID = {purchase.ID} and PurchaseOrderID = '{purchase.PurchaseOrderID}' ";
                   Sql += $"and ItemCode = '{purchase.ItemCode}' and ItemName = '{purchase.ItemName}' ";
                   Sql += $"and ItemType = '{purchase.ItemType}' and Unit = '{purchase.Unit}' ";
                   Sql += $"and InputStatus = '{purchase.InputStatus}' and RecordStatus = '{purchase.RecordStatus}' ";

            var res = _database.QueryAsync<NhapKhoDungCuModel>(Sql);

            if (res.Result.Count == 0)
                return _database.InsertAsync(purchase);

            else return null;
        }

        public Task<int> UpdatePurchaseOrderAsync(NhapKhoDungCuModel purchase)
        {
            string Sql = $"Update NhapKhoDungCuModel set ";
                   Sql += $"SoLuongDaNhap = {purchase.SoLuongDaNhap}, ";
                   Sql += $"SoLuongBox = {purchase.SoLuongBox} ";
                   Sql += $"Where ID = {purchase.ID}";

            return _database.ExecuteAsync(Sql);
        }


        public void DeletePurchaseOrderAsyncWithKey(string purchaseno)
        {
            string Sql = $"Delete From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchaseno}'";

            _ = _database.ExecuteAsync(Sql);
        }


        //For table XuatKhoDungCuModel:
        public Task<List<XuatKhoDungCuModel>> GetTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Select * From XuatKhoDungCuModel Where TransferOrderNo = '{no}'";

            return _database.QueryAsync<XuatKhoDungCuModel>(Sql);
        }

        public Task<int> SaveTransferInstructionAsync(XuatKhoDungCuModel no)
        {
            string Sql = $"Select * From XuatKhoDungCuModel Where TransferOrderNo = '{no.TransferOrderNo}' ";
            Sql += $"and ID = { no.ID} and TransferOrderID = '{no.TransferOrderID}' ";
            Sql += $"and ItemCode = '{no.ItemCode}' and ItemName = '{no.ItemName}' ";
            Sql += $"and ItemType = '{no.ItemType}' and Unit = '{no.Unit}' ";
            Sql += $"and TransferType = '{no.TransferType}' and RecordStatus = '{no.RecordStatus}' ";

            var res = _database.QueryAsync<NhapKhoDungCuModel>(Sql);

            if (res.Result.Count == 0)
                return _database.InsertAsync(no);
            else return null;
        }

        public Task<int> UpdateTransferInstructionAsync(XuatKhoDungCuModel no)
        {
            string Sql = $"Update XuatKhoDungCuModel set ";
            Sql += $"SoLuongDaNhap = {no.SoLuongDaNhap}, ";
            Sql += $"SoLuongBox = {no.SoLuongBox} ";
            Sql += $"Where ID = {no.ID}";

            return _database.ExecuteAsync(Sql);
        }


        public void DeleteTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Delete From XuatKhoDungCuModel Where TransferOrderNo = '{no}'";

            _ = _database.ExecuteAsync(Sql);
        }
    }
}
