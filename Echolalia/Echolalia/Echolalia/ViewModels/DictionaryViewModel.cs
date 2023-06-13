using System;
using Echolalia.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using Echolalia.Views;

namespace Echolalia.ViewModels
{
    public class DictionaryViewModel : BindableObject
    {

        public string Title { get; }
        public ObservableCollection<Item> Translations {get; set;}

        public Command RefreshCmdAsync { get; }
        public Command DeleteItemCmdAsync { get;  }
        public Command EditItemCmdAsync { get;  }

        public DictionaryViewModel()
        {
            Title = "My Dictionary";
            Translations = new ObservableCollection<Item>();

            RefreshCmdAsync = new Command(RefrechAsync);
            DeleteItemCmdAsync = new Command(async (sender) => await DeleteItemAsync(sender));
            EditItemCmdAsync = new Command(async (sender) => await EditItem(sender));
        }

        int _wordCount;
        public int WordCount
        {
            get => _wordCount;
            set
            {
                if (_wordCount == value) return;
                _wordCount = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private void RefrechAsync()
        {
            IsBusy = true;
            LoadTranslations();
            IsBusy = false;
        }

        private async Task DeleteItemAsync(object o)
        {
            var item = o as Item;
            if (item == null) return;
            Translations.Remove(item);
            WordCount--;
            await App.localDB.RemoveItem(item);
        }

        private async Task EditItem(object o)
        {
            var item = o as Item;
            if (item == null) return;
            await Shell.Current.Navigation.PushAsync(new EditSelected(item));
        }

        private async void LoadTranslations()
        {
            Translations.Clear();
            var result = await App.localDB.GetItemsAsync();
            foreach (Item i in result)
            {
                Translations.Add(i);
            }
            WordCount = Translations.Count;
        }
    }
}