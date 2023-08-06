using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Echolalia.ViewModels
{
    /*
     * A base class and some methods to be used on each ViewModel 
     * Property facilitating its use and repeating less code
     */

    /// <summary>
    /// Base ViewModel class implementing INotifyPropertyChanged interface for MVVM.
    /// </summary>
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

        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for a specific property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. 
        /// If not provided, the caller member name will be used.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        /// <summary>
        /// Sets the property value and raises PropertyChanged event if the value changes.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="storage">Reference to the backing field of the property.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property. 
        /// If not provided, the caller member name will be used.</param>
        /// <returns>True if the property value changed, otherwise false.</returns>
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

