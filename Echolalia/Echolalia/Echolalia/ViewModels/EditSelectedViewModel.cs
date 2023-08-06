using Echolalia.Models;
using Xamarin.Forms;

namespace Echolalia.ViewModels
{
    /// <summary>
    /// ViewModel for editing a selected word.
    /// </summary>
    public class EditSelectedViewModel : BaseViewModel
    {
        /*
         *  Propetries for binding
         */
        private Color flipIsFavoriteColor;
        public Color FlipIsFavoriteColor {
            get => flipIsFavoriteColor;
            set => SetProperty(ref flipIsFavoriteColor, value);
        }

        private string markAsLearnedLabel;
        public string MarkAsLearnedLabel {
            get => markAsLearnedLabel;
            set => SetProperty(ref markAsLearnedLabel, value);
        }

        public string EntryOriginalWord { get; set; }
        public string EntryTranslationWord { get; set; }
        public Command SaveWordCMD { get; }
        public Command MarkAsLearnedCMD { get; }
        public Command FlipIsFavoriteCMD { get; }

        readonly Color yellow = Color.FromHex("#FFB74A");
        readonly Color blue = Color.FromHex("2196F3");
        Word currentWord;

        /// <summary>
        /// Initializes a new instance of the EditSelectedViewModel class.
        /// </summary>
        /// <param name="word">The word to be edited.</param>
        public EditSelectedViewModel(Word word)
        {
            Title = "Edit word";
            currentWord = word;
            EntryOriginalWord = word.Original;
            EntryTranslationWord = word.Translation;

            // Set the initial color and label based on the word properties
            if (word.IsFavorite)
                FlipIsFavoriteColor = yellow;
            else
                FlipIsFavoriteColor = blue;

            if (word.Progress == LearningProgress.learned)
                MarkAsLearnedLabel = "Already learned";
            else
                MarkAsLearnedLabel = "Mark as learned";

            // Initialize commands for actions
            SaveWordCMD = new Command(() => SaveWord());
            MarkAsLearnedCMD = new Command(() => MarkAsLearned());
            FlipIsFavoriteCMD = new Command(() => FlipIsFavorite());
        }

        private async void SaveWord()
        {
            if (string.IsNullOrWhiteSpace(EntryOriginalWord) ||
                string.IsNullOrWhiteSpace(EntryTranslationWord))
            {
                await Shell.Current.DisplayAlert(
                    "Echolalia",
                    "Word and translation can't be empty",
                    "Ok"
                );
                return;
            }

            // Update the word properties with edited values
            currentWord.Original = EntryOriginalWord.Trim();
            currentWord.Translation = EntryTranslationWord.Trim();
            await App.localDB.EditItemAsync(currentWord);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void MarkAsLearned()
        {
            if (currentWord.Progress == LearningProgress.learned)
            {
                await Shell.Current.DisplayAlert("Echolaia",
                    "Word already learned",
                    "Ok"
                );
                return;
            }
            currentWord.Progress = LearningProgress.learned;
            MarkAsLearnedLabel = "Already learned";
            await Shell.Current.DisplayAlert("Echolaia",
                    "Word marked as Learned",
                    "Ok"
            );
        }

        private async void FlipIsFavorite()
        {
            // Flip the favorite status of the word
            currentWord.IsFavorite = !currentWord.IsFavorite;
            string notMark = currentWord.IsFavorite ? "" : "not";
            FlipIsFavoriteColor = currentWord.IsFavorite ? yellow : blue;
            await Shell.Current.DisplayAlert("Echolaia",
                    $"Word marked as {notMark} favorite",
                    "Ok"
            );
        }
    }
}

