using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Echolalia.Data;
using System.Linq;

namespace Echolalia.ViewModels
{
    /// <summary>
    /// ViewModel for managing application settings.
    /// </summary>
	public class SettingsViewModel: BaseViewModel
    {
        /// <summary>
        /// Gets the user's name from the preferences database.
        /// </summary>
		public string UserName { get; }

        /// <summary>
        /// Gets the list of available word count options for training sessions.
        /// </summary>
        public List<int> CountWordsPerTrainList { get; }
		public List<string> LanguagesList { get; }

        public Command DeleteAllDataCmd { get;  }
        public Command ConfirmCmd { get;  }

        /*
         * Properties for binding
         */
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
            
            // Initialize properties with values from the preferences database
            UserName = App.preferencesDB.UserName;
            CountWordsPerTrainList = App.preferencesDB.WordsCountPerTrainList;
            LanguagesList = Enum.GetValues(typeof(Languages))
                                .Cast<Languages>()
                                .Select(v => v.ToString())
                                .ToList();

            SelectedCountWordsPerTrainIndex = CountWordsPerTrainList.IndexOf(App.preferencesDB.WordsCountPerTrain);
            SelectedLanguageIndex = LanguagesList.IndexOf(App.preferencesDB.InterfaceLanguage);

            DeleteAllDataCmd = new Command(DeleteAllData);
            ConfirmCmd = new Command(Confirm);
        }

        /// <summary>
        /// Clears all data from the local database and preferences database.
        /// </summary>
        public async void DeleteAllData()
        {
            await App.localDB.ClearAllDataAsync();
            await Shell.Current.DisplayAlert("Data cleared", null, "ok");
            App.preferencesDB.Clear();
            Confirm();
        }

        /// <summary>
        /// Saves and applies the selected settings to the preferences database.
        /// </summary>
        public void Confirm()
        {
            // Save the selected settings to the preferences database
            App.preferencesDB.WordsCountPerTrain = CountWordsPerTrainList[SelectedCountWordsPerTrainIndex];
            App.preferencesDB.InterfaceLanguage = LanguagesList[SelectedLanguageIndex];

            if (EntryCellUserName != null && EntryCellUserName != "")
                App.preferencesDB.UserName = EntryCellUserName;

            // Navigate back to the previous page
            App.Current.MainPage.Navigation.PopAsync();
        }
	}
}

