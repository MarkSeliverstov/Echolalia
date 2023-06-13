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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshV.IsRefreshing = true;
        }
    }
}

