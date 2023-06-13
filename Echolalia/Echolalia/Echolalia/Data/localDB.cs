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
            await _database.CreateTableAsync<DayLearnedStats>();
        }

        /*
         For Items
         */
        public Task<List<Item>> GetItemsAsync() => _database.Table<Item>().ToListAsync();

        public Task<int> SaveItem<T>(T item) => _database.InsertAsync(item);

        public Task<int> RemoveItem(Item item) => _database.DeleteAsync(item);

        public Task<int> ClearAllData()
        {
            _database.DeleteAllAsync<Item>();
            return _database.DeleteAllAsync<DayLearnedStats>();
        }

        public Task<int> EditItem(Item item) => _database.UpdateAsync(item);

        public Task<int> Count() => _database.Table<Item>().CountAsync();

        /*
         For DayLearnedStats
         */

        public Task<List<DayLearnedStats>> GetMouthStats()
        {
            return _database.Table<DayLearnedStats>().ToListAsync();
        }
    }
}

