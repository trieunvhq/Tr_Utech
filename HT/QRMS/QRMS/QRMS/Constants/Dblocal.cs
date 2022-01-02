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
        }

        public Task<List<TransactionHistoryBPLModel>> GetHistoryAsync()
        {
            return _database.Table<TransactionHistoryBPLModel>().ToListAsync();
        }

        public Task<int> SaveHistoryAsync(TransactionHistoryBPLModel history)
        {
            if (history.ID != 0)
            {
                return _database.UpdateAsync(history);
            }
            else
            {
                return _database.InsertAsync(history);
            }
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

        public void DeleteHistoryAsyncWithKey(int id)
        {
            string Sql = $"Delete From TransactionHistoryBPLModel Where ID = {id}";

            _ = _database.ExecuteAsync(Sql);
        }

        public void DeleteHistoryAll()
        {
            _database.DeleteAllAsync<TransactionHistoryBPLModel>();
        }
    }
}
