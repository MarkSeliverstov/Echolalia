using System;
using SQLite;

namespace Echolalia.Models
{
	public public class Translation
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string TypeOfItem { get; set; }
		public string Item { get; set; }
		public string TranslateItem { get; set; }

	}
}

