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
            _database.CreateTableAsync<TransactionHistoryBPLModel>().Wait();
            _database.CreateTableAsync<NhapKhoDungCuModel>().Wait();
            _database.CreateTableAsync<XuatKhoDungCuBPLModel>().Wait();
        }

        public Task<List<TransactionHistoryBPLModel>> GetHistoryAsync()
        {
            return _database.Table<TransactionHistoryBPLModel>().ToListAsync();
        }

        public Task<List<TransactionHistoryBPLModel>> GetHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Select * From TransactionHistoryBPLModel Where OrderNo = '{purchaseno}'";

            return _database.QueryAsync<TransactionHistoryBPLModel>(Sql);
        }

        public Task<int> SaveHistoryAsync(TransactionHistoryBPLModel history)
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

        public Task<int> DeleteHistoryAsync(TransactionHistoryBPLModel history)
        {
            return _database.DeleteAsync(history);
        }

        public Task<List<TransactionHistoryBPLModel>> SelectOneHistoryAsync(int id)
        {
            string Sql = $"Select * From TransactionHistoryBPLModel Where ID = {id}";

            var data = _database.QueryAsync<TransactionHistoryBPLModel>(Sql);

            return data;
        }

        public void DeleteHistoryAsyncWithKey(string purchaseno)
        {
            string Sql = $"Delete From TransactionHistoryBPLModel Where OrderNo = '{purchaseno}'";

            _ = _database.ExecuteAsync(Sql);
        }

        public void DeleteHistoryAsyncWithID(int id)
        {
            string Sql = $"Delete From TransactionHistoryBPLModel Where ID = {id}";

            _ = _database.ExecuteAsync(Sql);
        }

        public void DeleteHistoryAll()
        {
            _database.DeleteAllAsync<TransactionHistoryBPLModel>();
        }


        //For table NhapKhoDungCuModel:
        public Task<List<NhapKhoDungCuModel>> GetPurchaseOrderAsyncWithKey(string purchaseno)
        {
            string Sql = $"Select * From NhapKhoDungCuModel Where PurchaseOrderNo = '{purchaseno}'";

            return _database.QueryAsync<NhapKhoDungCuModel>(Sql);
        }

        public Task<int> SavePurchaseOrderAsync(NhapKhoDungCuModel purchase)
        {
            return _database.InsertAsync(purchase);
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


        //For table XuatKhoDungCuBPLModel:
        public Task<List<XuatKhoDungCuBPLModel>> GetTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Select * From XuatKhoDungCuBPLModel Where TransferOrderNo = '{no}'";

            return _database.QueryAsync<XuatKhoDungCuBPLModel>(Sql);
        }

        public Task<int> SaveTransferInstructionAsync(XuatKhoDungCuBPLModel no)
        {
            return _database.InsertAsync(no);
        }

        public Task<int> UpdateTransferInstructionAsync(XuatKhoDungCuBPLModel no)
        {
            string Sql = $"Update XuatKhoDungCuBPLModel set ";
            Sql += $"SoLuongDaNhap = {no.SoLuongDaNhap}, ";
            Sql += $"SoLuongBox = {no.SoLuongBox} ";
            Sql += $"Where ID = {no.ID}";

            return _database.ExecuteAsync(Sql);
        }


        public void DeleteTransferInstructionAsyncWithKey(string no)
        {
            string Sql = $"Delete From XuatKhoDungCuBPLModel Where TransferOrderNo = '{no}'";

            _ = _database.ExecuteAsync(Sql);
        }
    }
}
