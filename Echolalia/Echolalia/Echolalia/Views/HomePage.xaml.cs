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
    }
}

