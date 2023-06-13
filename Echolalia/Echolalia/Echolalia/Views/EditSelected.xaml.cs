using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Echolalia.Models;
using Echolalia.ViewModels;

namespace Echolalia.Views
{	
	public partial class EditSelected : ContentPage
	{	
		public EditSelected (Item item)
		{
			InitializeComponent ();
			this.BindingContext = new EditSelectedViewModel(item);
		}
	}
}

