using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using Echolalia.Data;
using Xamarin.Forms.Internals;

namespace Echolalia.ViewModels
{
	public class SettingsViewModel: BindableObject
    {
        /*
            Keys for Preferences
         */
        const string wordsCountPerTrain_Key = "wordsCountPerTrain";
        const string UserName_Key = "UserName";
        const string InterfaceLanguage_key = "InterfaceLanguage";

        /*
            Bindings
         */
        public string Title { get; }
		public string UserName { get => Preferences.Get(UserName_Key, "User"); }
		public List<int> CountWordsPerTrainList { get; }
		public List<string> LanguagesList { get; }

        public Command DeleteAllDataCmd { get;  }
        public Command ConfirmCmd { get;  }
        
		public string EntryCellUserName
		{
			get => Preferences.Get(UserName_Key, "User");
            set
			{
                Preferences.Set(UserName_Key, value);
				OnPropertyChanged();
			}
		}

        int _selectedCountWordsPerTrain { get; set; }
        public int SelectedCountWordsPerTrain
        {
            get => _selectedCountWordsPerTrain;
            set
            {
                Preferences.Set(wordsCountPerTrain_Key, value);
                Console.WriteLine(value);
                OnPropertyChanged();
            }
        }


        public int SelectedLanguage
        {
            get => LanguagesList.IndexOf(Preferences.Get(InterfaceLanguage_key, "English"));
            set
            {
                Preferences.Set(InterfaceLanguage_key, value);
                Console.WriteLine(value);
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
		{
            CountWordsPerTrainList = new List<int> { 10, 15, 20 };
            LanguagesList = new List<string> { "English", "Czech" };

            Title = "Settings";
            DeleteAllDataCmd = new Command(DeleteAllData);
            ConfirmCmd = new Command(Confirm);

            SelectedCountWordsPerTrain = Preferences.Get(wordsCountPerTrain_Key, CountWordsPerTrainList[0]);
            SelectedLanguage = Preferences.Get(InterfaceLanguage_key, LanguagesList[0]);
        }

        public async void DeleteAllData()
        {
            await App.localDB.ClearAllData();
            await Shell.Current.DisplayAlert("Data cleared", null, "ok");
            Preferences.Clear();
        }

        public void Confirm()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
	}
}

