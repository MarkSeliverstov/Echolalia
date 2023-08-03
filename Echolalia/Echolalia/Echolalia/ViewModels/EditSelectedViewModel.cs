using Echolalia.Models;
using Xamarin.Forms;

namespace Echolalia.ViewModels
{
    public class EditSelectedViewModel : BaseViewModel
    {
        public string EntryOriginalWord { get; set; }
        public string EntryTranslationWord { get; set; }
        public Command EditWordCMD { get; }

        public EditSelectedViewModel(Word item)
        {
            Title = "Edit";
            EntryOriginalWord = item.Original;
            EntryTranslationWord = item.Translation;
            EditWordCMD = new Command(() => EditWord(item));
        }

        private async void EditWord(Word item)
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

            item.Original = EntryOriginalWord.Trim();
            item.Translation = EntryTranslationWord.Trim();
            await App.localDB.EditItemAsync(item);
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}

