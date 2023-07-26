using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using Echolalia.Helpers;

namespace Echolalia.ViewModels
{
	public class SettingsViewModel: BaseViewModel
    {
        /*
            Bindings
         */
        public string Title { get; }
		public string UserName { get => Preferences.Get(SettingKeys.UserName_Key, "User"); }
		public List<int> CountWordsPerTrainList { get; }
		public List<string> LanguagesList { get; }

        public Command DeleteAllDataCmd { get;  }
        public Command ConfirmCmd { get;  }

        string _entryCellUserName;
        public string EntryCellUserName
		{
            get => _entryCellUserName;
            set => SetProperty(ref _entryCellUserName, value);
		}

        int _selectedCountWordsPerTrainIndex;
        public int SelectedCountWordsPerTrainIndex
        {
            get => _selectedCountWordsPerTrainIndex;
            set => SetProperty(ref _selectedCountWordsPerTrainIndex, value);
        }

        int _selectedLanguageIndex;
        public int SelectedLanguageIndex
        {
            get => _selectedLanguageIndex;
            set => SetProperty(ref _selectedLanguageIndex, value);
        }

        public SettingsViewModel()
		{
            CountWordsPerTrainList = new List<int> { 10, 15, 20 };
            LanguagesList = new List<string> { "English", "Czech" };

            Title = "Settings";
            DeleteAllDataCmd = new Command(DeleteAllData);
            ConfirmCmd = new Command(Confirm);

            SelectedCountWordsPerTrainIndex = CountWordsPerTrainList.IndexOf(
                Preferences.Get(SettingKeys.wordsCountPerTrain_Key, CountWordsPerTrainList[0])
            );
            SelectedLanguageIndex = LanguagesList.IndexOf(
                Preferences.Get(SettingKeys.InterfaceLanguage_key, LanguagesList[0])
            );
        }

        public async void DeleteAllData()
        {
            await App.localDB.ClearAllData();
            await Shell.Current.DisplayAlert("Data cleared", null, "ok");
            Preferences.Clear();
        }

        public void Confirm()
        {
            Preferences.Set(
                SettingKeys.wordsCountPerTrain_Key,
                CountWordsPerTrainList[SelectedCountWordsPerTrainIndex]
            );
            Preferences.Set(
                SettingKeys.InterfaceLanguage_key,
                LanguagesList[SelectedLanguageIndex]
            );

            if (EntryCellUserName != null && EntryCellUserName != "")
                Preferences.Set(SettingKeys.UserName_Key, EntryCellUserName);

            App.Current.MainPage.Navigation.PopAsync();
        }
	}
}

