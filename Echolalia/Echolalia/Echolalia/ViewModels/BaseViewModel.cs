using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Echolalia.ViewModels
{
    /*
     * DRY: Implemented base INotifyPropertyChanged class. 
     * 
     * A base class and some methods to be used on each ViewModel 
     * Property facilitating its use and repeating less code
     */
    public class BaseViewModel: INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /*
         * From: https://onewindowsdev.com/2017/07/21/basic-mvvm-base-class-inotifypropertychanged-implementation/
         */
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}

