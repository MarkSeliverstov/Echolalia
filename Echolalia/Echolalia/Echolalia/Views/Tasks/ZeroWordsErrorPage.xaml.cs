using System;
using System.Collections.Generic;
using Echolalia.ViewModels.Tasks;
using Xamarin.Forms;

namespace Echolalia.Views.Tasks
{	
	public partial class ZeroWordsErrorPage : ContentPage
	{
		public ZeroWordsErrorPage ()
		{
			InitializeComponent ();
			this.BindingContext = new ZeroWordsErrorViewModel();
		}
	}
}

