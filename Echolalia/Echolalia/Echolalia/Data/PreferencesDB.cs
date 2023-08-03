/*
 * Global app settings.
 */

using System.Collections.Generic;
using Xamarin.Essentials;

namespace Echolalia.Data
{
	public enum Languages{
		English,
		Czech,
		Russian,
		Spanish,
		Italian,
		German,
	}

	public class PreferencesDB
	{
        const int defaultWordsPerTrainCount = 10;
        const string defaultUserName = "Guest";

		const string wordsCountPerTrain_Key = "wordsCountPerTrain";
        const string UserName_Key = "UserName";
        const string InterfaceLanguage_key = "InterfaceLanguage";

        public readonly List<int> WordsCountPerTrainList = new List<int> { 10, 15, 20 };

        public string UserName
		{
			get => Preferences.Get(UserName_Key, defaultUserName);
            set => Preferences.Set(UserName_Key, value);
		}
		public string InterfaceLanguage
        {
			get => Preferences.Get(InterfaceLanguage_key, Languages.English.ToString());
            set => Preferences.Set(InterfaceLanguage_key, value);
		}
		public int WordsCountPerTrain
        {
			get => Preferences.Get(wordsCountPerTrain_Key, defaultWordsPerTrainCount);
            set => Preferences.Set(wordsCountPerTrain_Key, value);
		}
		public void Clear()
		{
			Preferences.Clear();
		}
	}
}

