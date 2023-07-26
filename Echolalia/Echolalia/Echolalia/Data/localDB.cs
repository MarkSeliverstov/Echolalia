using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Echolalia.Models;
using System.IO;

namespace Echolalia.Data
{
    // https://codetraveler.io/2019/11/26/efficiently-initializing-sqlite-database/ - guide

    public class localDB
    {
        readonly SQLiteAsyncConnection _database;

        public localDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            CreateTables();
        }

        private async void CreateTables()
        {
            await _database.CreateTableAsync<Item>();
        }

        public Task<List<Item>> GetItemsAsync() => _database.Table<Item>().ToListAsync();

        public Task<int> EditItem(Item currentItem) => _database.UpdateAsync(currentItem);

        public Task<int> SaveItem<T>(T item) => _database.InsertAsync(item);

        public Task<int> RemoveItem(Item item) => _database.DeleteAsync(item);

        public Task<int> ClearAllData()
        {
            return _database.DeleteAllAsync<Item>();
        }

    }
}

