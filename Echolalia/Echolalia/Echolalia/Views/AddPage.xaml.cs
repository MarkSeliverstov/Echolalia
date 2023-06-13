using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Echolalia.ViewModels;

namespace Echolalia.Views
{	
	public partial class AddPage : ContentPage
	{	
		public AddPage ()
		{
			InitializeComponent ();
			this.BindingContext = new AddViewModel();
		}
    }
}

