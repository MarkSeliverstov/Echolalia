using Echolalia.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Echolalia.Views;
using System.Collections.Generic;

namespace Echolalia.ViewModels
{
    public class DictionaryViewModel : BaseViewModel
    {
        /* 
         * ObservableCollection<T> is a generic class in C# 
         * that is part of the System.Collections.ObjectModel namespace. 
         * It is a collection class that provides notifications whenever items 
         * are added, removed, or when the collection is refreshed.
         */
        /// <summary>
        /// ObservableCollection of Word to store the translations in the dictionary.
        /// </summary>
        public ObservableCollection<Word> Translations {get; set;}

        public Command RefreshCmdAsync { get; }
        public Command DeleteItemCmdAsync { get;  }
        public Command EditItemCmdAsync { get;  }

        int _wordCount;
        public int WordCount
        {
            get => _wordCount;
            set => SetProperty(ref _wordCount, value);
        }

        // Indicates if the dictionary is currently being refreshed.
        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public DictionaryViewModel()
        {
            Title = "My Dictionary";
            Translations = new ObservableCollection<Word>();

            RefreshCmdAsync = new Command(RefrechAsync);
            DeleteItemCmdAsync = new Command(DeleteItemAsync);
            EditItemCmdAsync = new Command(EditItem);
        }


        private void RefrechAsync()
        {
            IsBusy = true;
            LoadTranslations();
            IsBusy = false;
        }

        private async void DeleteItemAsync(object o)
        {
            if (o is not Word item) return;

            Translations.Remove(item);
            WordCount--;

            await App.localDB.RemoveItemAsync(item);
        }

        private async void EditItem(object o)
        {
            if (o is not Word item) return;

            await Shell.Current.Navigation.PushAsync(new EditSelected(item));
        }

        private async void LoadTranslations()
        {
            Translations.Clear();

            List<Word> result = await App.localDB.GetItemsAsync();
            result.ForEach(Translations.Add);

            WordCount = Translations.Count;
        }
    }
}