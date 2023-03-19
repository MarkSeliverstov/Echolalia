using SQLite;
using Echolalia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echolalia.Data
{

	class LibraryDB
	{
		readonly SQLiteAsyncConnection db;

		public LibraryDB(string connection)
		{
			db = new SQLiteAsyncConnection(connection);
			db.CreateTableAsync<Translation>().Wait();
		}

		public Task<List<Translation>> GetTranslationsAsync()
		{
			return db.Table<Translation>().ToListAsync();
		}

		public Task<Translation> GetTranslationByIDAsync(int id)
		{
			return db.Table<Translation>()
				.Where(i => i.ID == id)
				.FirstOrDefaultAsync();

		}

		public Task<int> SaveTranslationAsync(Translation translation)
		{
			if (translation.ID != 0)
			{
				return db.UpdateAsync(translation);
			}
			else
			{
				return db.InsertAsync(translation);
			}
		}

		public Task<int> DeleteTranslationAsync(Translation translation)
		{
			return db.DeleteAsync(translation);
		}

		public Task<int> DeleteAllDataAsync(Translation translation)
		{
			return db.DeleteAllAsync<Translation>();
		}

	}

}

