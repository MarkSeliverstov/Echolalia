using System;
using Echolalia.ViewModels;

using Xamarin.Forms;

namespace Echolalia.Views
{
    public partial class MyDictionaryPage : ContentPage
    {
        public MyDictionaryPage()
        {
            InitializeComponent();
            this.BindingContext = new DictionaryViewModel();
        }

        // Invoked when the page is about to be displayed.
        // To download up-to-date data
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshV.IsRefreshing = true;
        }
    }
}

