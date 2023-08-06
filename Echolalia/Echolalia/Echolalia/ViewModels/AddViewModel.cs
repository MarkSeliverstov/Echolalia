using Echolalia.Models;
using Xamarin.Forms;

namespace Echolalia.ViewModels
{ 
    /// <summary>
    /// ViewModel for managing the process of adding words to the dictionary in the application.
    /// </summary>
    public class AddViewModel : BaseViewModel
    {
        string _originalWord;
        public string EntryOriginalWord {
            get => _originalWord;
            set => SetProperty(ref _originalWord, value);
        }

        string _translationWord;
        public string EntryTranslationWord {
            get => _translationWord;
            set => SetProperty(ref _translationWord, value);
        }
        public Command AddWord { get;  }

        public AddViewModel()
        {
            Title = "Add to Dictionary";
            AddWord = new Command(ExecuteAddWordCmd);
        }

        public async void ExecuteAddWordCmd()
        {

            if (string.IsNullOrWhiteSpace(EntryOriginalWord) ||
                string.IsNullOrWhiteSpace(EntryTranslationWord))
            {
                await Shell.Current.DisplayAlert("Echolaia",
                    "Word and translation can't be empty",
                    "Ok"
                );
                return;
            }
            await App.localDB.SaveItemAsync(new Word
            {
                Progress = LearningProgress.unknown,
                Original = EntryOriginalWord.Trim(),
                Translation = EntryTranslationWord.Trim(),
                IsAddedByUser = true,
                IsFavorite = false
            });

            // Clear the input fields after adding the word
            EntryOriginalWord = null;
            EntryTranslationWord = null;
        }
    }
}
