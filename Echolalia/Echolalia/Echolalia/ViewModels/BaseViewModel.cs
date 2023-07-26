using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/*
 * DRY: Implemented base INotifyPropertyChanged class. 
 * 
 * A base class and some methods to be used on each ViewModel 
 * Property facilitating its use and repeating less code
 */

namespace Echolalia.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
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

