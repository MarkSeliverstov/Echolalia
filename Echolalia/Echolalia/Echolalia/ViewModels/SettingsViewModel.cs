using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using Echolalia.Data;
using System.Linq;

namespace Echolalia.ViewModels
{
	public class SettingsViewModel: BaseViewModel
    {
		public string UserName { get; }
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
            Title = "Settings";
            
            UserName = App.preferencesDB.UserName;

            CountWordsPerTrainList = App.preferencesDB.WordsCountPerTrainList;
            LanguagesList = Enum.GetValues(typeof(Languages))
                                .Cast<Languages>()
                                .Select(v => v.ToString())
                                .ToList();

            SelectedCountWordsPerTrainIndex = CountWordsPerTrainList.IndexOf(App.preferencesDB.WordsCountPerTrain);
            SelectedLanguageIndex = LanguagesList.IndexOf(App.preferencesDB.InterfaceLanguage);

            Console.WriteLine(App.preferencesDB.WordsCountPerTrain);
            Console.WriteLine(App.preferencesDB.InterfaceLanguage);

            DeleteAllDataCmd = new Command(DeleteAllData);
            ConfirmCmd = new Command(Confirm);
        }

        public async void DeleteAllData()
        {
            await App.localDB.ClearAllDataAsync();
            await Shell.Current.DisplayAlert("Data cleared", null, "ok");
            App.preferencesDB.Clear();
            Confirm();
        }

        public void Confirm()
        {
            App.preferencesDB.WordsCountPerTrain = CountWordsPerTrainList[SelectedCountWordsPerTrainIndex];
            App.preferencesDB.InterfaceLanguage = LanguagesList[SelectedLanguageIndex];

            if (EntryCellUserName != null && EntryCellUserName != "")
                App.preferencesDB.UserName = EntryCellUserName;

            App.Current.MainPage.Navigation.PopAsync();
        }
	}
}

