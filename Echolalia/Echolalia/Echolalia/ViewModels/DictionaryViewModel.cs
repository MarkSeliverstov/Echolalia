using Echolalia.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Echolalia.Views;

namespace Echolalia.ViewModels
{
    public class DictionaryViewModel : BaseViewModel
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
            set => SetProperty(ref _wordCount, value);
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
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