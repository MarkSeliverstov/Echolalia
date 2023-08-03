using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Echolalia.ViewModels;

namespace Echolalia.Views
{	
	public partial class HomePage : ContentPage
	{	
		public HomePage ()
		{
            InitializeComponent();
			this.BindingContext = new HomeViewModel();
		}

        // Invoked when the page is about to be displayed.
        // To download up-to-date data
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as HomeViewModel).GetCountOfWordsPracticedTodayAsync();
        }
    }
}

