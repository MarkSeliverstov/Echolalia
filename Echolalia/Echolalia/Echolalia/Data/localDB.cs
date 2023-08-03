using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Echolalia.Models;
using System;

namespace Echolalia.Data
{
    public class LocalDB
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            CreateTables();
        }

        public void Init()
        {
            return;
        }

        private async void CreateTables()
        {
            await _database.CreateTableAsync<Word>();
        }

        public Task<List<Word>> GetItemsAsync() => _database.Table<Word>().ToListAsync();

        public Task<int> EditItemAsync(Word currentItem) => _database.UpdateAsync(currentItem);

        public Task<int> SaveItemAsync<T>(T item) => _database.InsertAsync(item);

        public Task<int> RemoveItemAsync(Word item) => _database.DeleteAsync(item);

        public Task<int> ClearAllDataAsync() => _database.DeleteAllAsync<Word>();

    }
}
