using System;
using Echolalia.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Echolalia.ViewModels
{
    public class AddViewModel : BindableObject
    {
        public string Title { get; }
        public string AddWordBtnTitle { get; }

        string _originalWord = null;
        public string EntryOriginalWord {
            get => _originalWord;
            set {
                _originalWord = value;
                OnPropertyChanged();
            }
        }

        string _translationWord = null;
        public string EntryTranslationWord {
            get => _translationWord;
            set {
                _translationWord = value;
                OnPropertyChanged();
            }
        }
        public Command AddWord { get;  }

        public AddViewModel()
        {
            Title = "Add to Dictionary";
            AddWordBtnTitle = "Add";
            AddWord = new Command(() => ExecuteAddWordCmd());
        }

        public async void ExecuteAddWordCmd()
        {

            if (EntryOriginalWord == null || EntryOriginalWord == "" ||
                EntryTranslationWord == null || EntryTranslationWord == "")
            {
                await ShowAlertAsync("Word and translation can't be empty");
                return;
            }
            await App.localDB.SaveItem(new Item
            {
                IsCustom = true,
                Original = EntryOriginalWord,
                Translation = EntryTranslationWord
            });
            EntryOriginalWord = null;
            EntryTranslationWord = null;
        }

        public async Task ShowAlertAsync(string msg)
        {
            await Shell.Current.DisplayAlert("Echolaia", msg, "Ok");
        }
    }
}
