using System;
using System.Threading.Tasks;
using Echolalia.Models;
using Xamarin.Forms;

namespace Echolalia.ViewModels
{
	public class EditSelectedViewModel: BaseViewModel
    {
		public string Title { get; }

        string _originalWord = null;
        public string EntryOriginalWord
        {
            get => _originalWord;
            set => SetProperty(ref _originalWord, value);
        }

        string _translationWord = null;
        public string EntryTranslationWord
        {
            get => _translationWord;
            set => SetProperty(ref _translationWord, value);
        }

        public Command EditWordCMD { get;  }

        public EditSelectedViewModel(Item item)
		{
			Title = "Edit";
			EntryOriginalWord = item.Original;
			EntryTranslationWord = item.Translation;

			EditWordCMD = new Command(() => EditWord(item));
		}

		private async void EditWord(Item item)
		{
			if (EntryOriginalWord == null || EntryOriginalWord == "" ||
                EntryTranslationWord == null || EntryTranslationWord == "")
            {
                await ShowAlertAsync("Word and translation can't be empty");
                return;
            }
            else
            {
                item.Original = EntryOriginalWord;
                item.Translation = EntryTranslationWord;
                await App.localDB.EditItem(item);
                await App.Current.MainPage.Navigation.PopAsync();
            }
		}

        public async Task ShowAlertAsync(string msg)
        {
            await Shell.Current.DisplayAlert("Echolaia", msg, "Ok");
        }
    }
}

